// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

#include "Stdafx.h"
#include "JpegOptimizer.h"

#ifdef MAGICK_NET_LINUX
#include <jpeglib.h>
#include <jerror.h>
#define fopen_utf8 fopen
#else
#include <jpeg/jpeglib.h>
#include <jpeg/jerror.h>
#endif
#include <setjmp.h>

#define MaxBufferExtent 16384

typedef struct _Marker
{
  JOCTET
    *buffer;

  int
    code;

  size_t
    length;
} Marker;

typedef struct _ClientData
{
  boolean
    lossless,
    progressive;

  const char
    *inputFileName,
    *outputFileName;

  CustomStreamHandler
    reader,
    writer;

  jmp_buf
    error_recovery;

  JSAMPARRAY
    buffer;

  jvirt_barray_ptr
    *coefficients;

  Marker
    *icc_marker;

  size_t
    height,
    quality;
} ClientData;

typedef struct _SourceManager
{
  struct jpeg_source_mgr
    manager;

  JOCTET
    *buffer;

  FILE
    *inputFile;

  CustomStreamHandler
    reader;

  boolean
    startOfBlob;
} SourceManager;

typedef struct _DestinationManager
{
  struct jpeg_destination_mgr
    manager;

  JOCTET
    *buffer;

  FILE
    *outputFile;

  CustomStreamHandler
    writer;
} DestinationManager;

static void JpegErrorHandler(j_common_ptr jpeg_info)
{
  ClientData
    *client_data;

  client_data = (ClientData *)jpeg_info->client_data;

  //TODO: Report errors
  longjmp(client_data->error_recovery, 1);
}

static void JpegWarningHandler(j_common_ptr jpeg_info, int level)
{
  (void)jpeg_info;
  (void)level;

  //TODO: Report warnings
}

static void InitializeSource(j_decompress_ptr decompress_info)
{
  ClientData
    *client_data;

  SourceManager
    *source;

  client_data = (ClientData *)decompress_info->client_data;

  source = (SourceManager *)decompress_info->src;
  source->startOfBlob = TRUE;

  if (client_data->inputFileName != (const char *)NULL)
  {
    source->inputFile = fopen_utf8(client_data->inputFileName, "rb");
    if (source->inputFile == (FILE*)NULL)
      ERREXIT(decompress_info, JERR_FILE_READ);
  }
  else
    source->reader = client_data->reader;
}

static inline boolean FillInputBuffer(j_decompress_ptr decompress_info)
{
  SourceManager
    *source;

  source = (SourceManager *)decompress_info->src;

  if (source->inputFile != (FILE *)NULL)
    source->manager.bytes_in_buffer = (size_t)fread(source->buffer, 1, MaxBufferExtent, source->inputFile);
  else
    source->manager.bytes_in_buffer = (size_t)source->reader(source->buffer, MaxBufferExtent, (void*)NULL);
  if (source->manager.bytes_in_buffer == 0)
  {
    if (source->startOfBlob != FALSE)
      ERREXIT(decompress_info, JERR_INPUT_EMPTY);
    WARNMS(decompress_info, JWRN_JPEG_EOF);
    source->buffer[0] = (JOCTET)0xff;
    source->buffer[1] = (JOCTET)JPEG_EOI;
    source->manager.bytes_in_buffer = 2;
  }
  source->manager.next_input_byte = source->buffer;
  source->startOfBlob = FALSE;
  return TRUE;
}

static void SkipInputData(j_decompress_ptr decompress_info, long number_bytes)
{
  SourceManager
    *source;

  if (number_bytes <= 0)
    return;

  source = (SourceManager *)decompress_info->src;
  while (number_bytes > (long)source->manager.bytes_in_buffer)
  {
    number_bytes -= (long)source->manager.bytes_in_buffer;
    FillInputBuffer(decompress_info);
  }
  source->manager.next_input_byte += number_bytes;
  source->manager.bytes_in_buffer -= number_bytes;
}

static void TerminateSource(j_decompress_ptr decompress_info)
{
  SourceManager
    *source;

  source = (SourceManager *)decompress_info->src;
  if (source->inputFile != (FILE *)NULL)
  {
    fclose(source->inputFile);
    source->inputFile = (FILE *)NULL;
  }
}

static SourceManager* CreateSourceManager(j_decompress_ptr decompress_info)
{
  SourceManager
    *source;

  source = (SourceManager *)(*decompress_info->mem->alloc_small)
    ((j_common_ptr)decompress_info, JPOOL_IMAGE, sizeof(SourceManager));
  if (source != (SourceManager*)NULL)
  {
    source->buffer = (JOCTET *)(*decompress_info->mem->alloc_small)
      ((j_common_ptr)decompress_info, JPOOL_IMAGE, MaxBufferExtent * sizeof(JOCTET));
    source->manager.init_source = InitializeSource;
    source->manager.fill_input_buffer = FillInputBuffer;
    source->manager.skip_input_data = SkipInputData;
    source->manager.resync_to_restart = jpeg_resync_to_restart;
    source->manager.term_source = TerminateSource;
    source->manager.bytes_in_buffer = 0;
    source->manager.next_input_byte = (const JOCTET *)NULL;
    source->inputFile = (FILE *)NULL;
    source->reader = (CustomStreamHandler)NULL;
    decompress_info->src = (struct jpeg_source_mgr *) source;
  }

  return source;
}

static size_t DetermineQuality(j_decompress_ptr decompress_info)
{
  ssize_t
    i,
    j,
    qvalue,
    quality,
    sum;

  /*
    Determine the JPEG compression quality from the quantization tables.
  */
  sum = 0;
  quality = 85;
  for (i = 0; i < NUM_QUANT_TBLS; i++)
  {
    if (decompress_info->quant_tbl_ptrs[i] != NULL)
      for (j = 0; j < DCTSIZE2; j++)
        sum += decompress_info->quant_tbl_ptrs[i]->quantval[j];
  }
  if ((decompress_info->quant_tbl_ptrs[0] != NULL) &&
    (decompress_info->quant_tbl_ptrs[1] != NULL))
  {
    ssize_t
      hash[101] =
    {
      1020, 1015,  932,  848,  780,  735,  702,  679,  660,  645,
       632,  623,  613,  607,  600,  594,  589,  585,  581,  571,
       555,  542,  529,  514,  494,  474,  457,  439,  424,  410,
       397,  386,  373,  364,  351,  341,  334,  324,  317,  309,
       299,  294,  287,  279,  274,  267,  262,  257,  251,  247,
       243,  237,  232,  227,  222,  217,  213,  207,  202,  198,
       192,  188,  183,  177,  173,  168,  163,  157,  153,  148,
       143,  139,  132,  128,  125,  119,  115,  108,  104,   99,
        94,   90,   84,   79,   74,   70,   64,   59,   55,   49,
        45,   40,   34,   30,   25,   20,   15,   11,    6,    4,
         0
    },
      sums[101] =
    {
      32640, 32635, 32266, 31495, 30665, 29804, 29146, 28599, 28104,
      27670, 27225, 26725, 26210, 25716, 25240, 24789, 24373, 23946,
      23572, 22846, 21801, 20842, 19949, 19121, 18386, 17651, 16998,
      16349, 15800, 15247, 14783, 14321, 13859, 13535, 13081, 12702,
      12423, 12056, 11779, 11513, 11135, 10955, 10676, 10392, 10208,
       9928,  9747,  9564,  9369,  9193,  9017,  8822,  8639,  8458,
       8270,  8084,  7896,  7710,  7527,  7347,  7156,  6977,  6788,
       6607,  6422,  6236,  6054,  5867,  5684,  5495,  5305,  5128,
       4945,  4751,  4638,  4442,  4248,  4065,  3888,  3698,  3509,
       3326,  3139,  2957,  2775,  2586,  2405,  2216,  2037,  1846,
       1666,  1483,  1297,  1109,   927,   735,   554,   375,   201,
        128,     0
    };

    qvalue = (ssize_t)(decompress_info->quant_tbl_ptrs[0]->quantval[2] +
      decompress_info->quant_tbl_ptrs[0]->quantval[53] +
      decompress_info->quant_tbl_ptrs[1]->quantval[0] +
      decompress_info->quant_tbl_ptrs[1]->quantval[DCTSIZE2 - 1]);
    for (i = 0; i < 100; i++)
    {
      if ((qvalue < hash[i]) && (sum < sums[i]))
        continue;
      if (((qvalue <= hash[i]) && (sum <= sums[i])) || (i >= 50))
        quality = (size_t)i + 1;
      break;
    }
  }
  else if (decompress_info->quant_tbl_ptrs[0] != NULL)
  {
    ssize_t
      hash[101] =
    {
      510,  505,  422,  380,  355,  338,  326,  318,  311,  305,
      300,  297,  293,  291,  288,  286,  284,  283,  281,  280,
      279,  278,  277,  273,  262,  251,  243,  233,  225,  218,
      211,  205,  198,  193,  186,  181,  177,  172,  168,  164,
      158,  156,  152,  148,  145,  142,  139,  136,  133,  131,
      129,  126,  123,  120,  118,  115,  113,  110,  107,  105,
      102,  100,   97,   94,   92,   89,   87,   83,   81,   79,
       76,   74,   70,   68,   66,   63,   61,   57,   55,   52,
       50,   48,   44,   42,   39,   37,   34,   31,   29,   26,
       24,   21,   18,   16,   13,   11,    8,    6,    3,    2,
        0
    },
      sums[101] =
    {
      16320, 16315, 15946, 15277, 14655, 14073, 13623, 13230, 12859,
      12560, 12240, 11861, 11456, 11081, 10714, 10360, 10027,  9679,
       9368,  9056,  8680,  8331,  7995,  7668,  7376,  7084,  6823,
       6562,  6345,  6125,  5939,  5756,  5571,  5421,  5240,  5086,
       4976,  4829,  4719,  4616,  4463,  4393,  4280,  4166,  4092,
       3980,  3909,  3835,  3755,  3688,  3621,  3541,  3467,  3396,
       3323,  3247,  3170,  3096,  3021,  2952,  2874,  2804,  2727,
       2657,  2583,  2509,  2437,  2362,  2290,  2211,  2136,  2068,
       1996,  1915,  1858,  1773,  1692,  1620,  1552,  1477,  1398,
       1326,  1251,  1179,  1109,  1031,   961,   884,   814,   736,
        667,   592,   518,   441,   369,   292,   221,   151,    86,
         64,     0
    };

    qvalue = (ssize_t)(decompress_info->quant_tbl_ptrs[0]->quantval[2] +
      decompress_info->quant_tbl_ptrs[0]->quantval[53]);
    for (i = 0; i < 100; i++)
    {
      if ((qvalue < hash[i]) && (sum < sums[i]))
        continue;
      if (((qvalue <= hash[i]) && (sum <= sums[i])) || (i >= 50))
        quality = (size_t)i + 1;
      break;
    }
  }

  return quality < 85 ? quality : 85;
}

static boolean DecompressJpeg(j_decompress_ptr decompress_info, ClientData *client_data)
{
  size_t
    i,
    width;

  jpeg_start_decompress(decompress_info);

  client_data->buffer = malloc(sizeof(JSAMPROW) * decompress_info->output_height);
  if (client_data->buffer == (JSAMPARRAY)NULL)
    return FALSE;
  client_data->height = decompress_info->output_height;

  width = sizeof(JSAMPLE) * decompress_info->output_width * decompress_info->out_color_components;
  for (i = 0; i < decompress_info->output_height; i++)
  {
    client_data->buffer[i] = malloc(width);
    if (client_data->buffer[i] == (JSAMPLE *)NULL)
      return FALSE;
  }

  while (decompress_info->output_scanline < decompress_info->output_height)
  {
    jpeg_read_scanlines(decompress_info, &client_data->buffer[decompress_info->output_scanline],
      decompress_info->output_height - decompress_info->output_scanline);
  }

  return TRUE;
}

static int GetCharacter(j_decompress_ptr jpeg_info)
{
  if (jpeg_info->src->bytes_in_buffer == 0)
    (void) (*jpeg_info->src->fill_input_buffer)(jpeg_info);
  jpeg_info->src->bytes_in_buffer--;
  return (int)GETJOCTET(*jpeg_info->src->next_input_byte++);
}

static boolean ReadICCMarker(j_decompress_ptr jpeg_info)
{
  ClientData
    *client_data;

  register ssize_t
    i;

  register JOCTET
    *p;

  size_t
    length;

  length = (size_t)((size_t)GetCharacter(jpeg_info) << 8);
  length += (size_t)GetCharacter(jpeg_info);
  if (length <= 2)
    return TRUE;
  length -= 2;

  client_data = (ClientData *)jpeg_info->client_data;
  client_data->icc_marker = malloc(sizeof(*client_data->icc_marker));
  if (client_data->icc_marker == (Marker *)NULL)
    return FALSE;

  ResetMagickMemory(client_data->icc_marker, 0, sizeof(*client_data->icc_marker));

  client_data->icc_marker->code = jpeg_info->unread_marker;
  client_data->icc_marker->length = length;
  client_data->icc_marker->buffer = malloc(length * sizeof(*client_data->icc_marker->buffer));
  if (client_data->icc_marker->buffer == (JOCTET *)NULL)
    return FALSE;

  p = client_data->icc_marker->buffer;
  for (i = 0; i < (ssize_t)length; i++)
    *p++ = (JOCTET)GetCharacter(jpeg_info);

  return TRUE;
}

static boolean ReadJpeg(j_decompress_ptr decompress_info, ClientData *client_data)
{
  SourceManager
    *source;

  source = (SourceManager *)NULL;

  if (setjmp(client_data->error_recovery) != 0)
  {
    if (source != (SourceManager *)NULL && source->inputFile != (FILE *)NULL)
      fclose(source->inputFile);
    return FALSE;
  }

  jpeg_create_decompress(decompress_info);
  source = CreateSourceManager(decompress_info);
  if (source == (SourceManager *)NULL)
    return FALSE;

  /* For now we only preserve the ICC profile */
  jpeg_set_marker_processor(decompress_info, (int)(JPEG_APP0 + 2), ReadICCMarker);

  jpeg_read_header(decompress_info, TRUE);

  /*
      When the chroma subsampling is 4:4:4 it will be change to 4:2:0 but this will
      require reading the scanlines.
  */
  if ((client_data->lossless == FALSE) &&
    (decompress_info->comp_info[0].h_samp_factor == 1) &&
    (decompress_info->comp_info[0].v_samp_factor == 1) &&
    (decompress_info->comp_info[1].h_samp_factor == 1) &&
    (decompress_info->comp_info[1].v_samp_factor == 1) &&
    (decompress_info->comp_info[2].h_samp_factor == 1) &&
    (decompress_info->comp_info[2].v_samp_factor == 1))
  {
    return DecompressJpeg(decompress_info, client_data);
  }
  else
  {
    client_data->coefficients = jpeg_read_coefficients(decompress_info);

    return client_data->coefficients == (jvirt_barray_ptr *)NULL ? FALSE : TRUE;
  }
}

static void InitializeDestination(j_compress_ptr compress_info)
{
  ClientData
    *client_data;

  DestinationManager
    *destination;

  client_data = (ClientData *)compress_info->client_data;
  destination = (DestinationManager *)compress_info->dest;
  destination->buffer = (JOCTET *)(*compress_info->mem->alloc_small)
    ((j_common_ptr)compress_info, JPOOL_IMAGE, MaxBufferExtent * sizeof(JOCTET));
  destination->manager.next_output_byte = destination->buffer;
  destination->manager.free_in_buffer = MaxBufferExtent;

  if (client_data->outputFileName != (const char *)NULL)
  {
    destination->outputFile = fopen_utf8(client_data->outputFileName, "wb");
    if (destination->outputFile == (FILE*)NULL)
      ERREXIT(compress_info, JERR_FILE_WRITE);
  }
  else
    destination->writer = client_data->writer;
}

static boolean EmptyOutputBuffer(j_compress_ptr compress_info)
{
  DestinationManager
    *destination;

  destination = (DestinationManager *)compress_info->dest;
  if (destination->outputFile != (FILE *)NULL)
    destination->manager.free_in_buffer = fwrite((const char *)destination->buffer, 1, MaxBufferExtent, destination->outputFile);
  else
    destination->manager.free_in_buffer = (size_t)destination->writer((unsigned char *)destination->buffer, MaxBufferExtent, (void*)NULL);
  if (destination->manager.free_in_buffer != MaxBufferExtent)
    ERREXIT(compress_info, JERR_FILE_WRITE);
  destination->manager.next_output_byte = destination->buffer;
  return TRUE;
}

static void TerminateDestination(j_compress_ptr compress_info)
{
  DestinationManager
    *destination;

  size_t
    count;

  destination = (DestinationManager *)compress_info->dest;
  count = (MaxBufferExtent - (int)destination->manager.free_in_buffer);
  if (count > 0)
  {
    if (destination->outputFile != (FILE *)NULL)
    {
      if (fwrite((const char *)destination->buffer, 1, count, destination->outputFile) != count)
        ERREXIT(compress_info, JERR_FILE_WRITE);
    }
    else
      destination->writer((unsigned char *)destination->buffer, count, (void*)NULL);
  }
  if (destination->outputFile != (FILE *)NULL)
  {
    fclose(destination->outputFile);
    destination->outputFile = (FILE *)NULL;
  }
}

static inline DestinationManager* CreateDestinationManager(j_compress_ptr compress_info)
{
  DestinationManager
    *destination;

  destination = (DestinationManager *)NULL;
  compress_info->dest = (struct jpeg_destination_mgr *) (*compress_info->mem->alloc_small)
    ((j_common_ptr)compress_info, JPOOL_IMAGE, sizeof(DestinationManager));
  if (compress_info->dest != (struct jpeg_destination_mgr *)NULL)
  {
    destination = (DestinationManager *)compress_info->dest;
    destination->manager.init_destination = InitializeDestination;
    destination->manager.empty_output_buffer = EmptyOutputBuffer;
    destination->manager.term_destination = TerminateDestination;
    destination->outputFile = (FILE *)NULL;
    destination->writer = (CustomStreamHandler)NULL;
  }
  return destination;
}

static void CompressJpeg(j_decompress_ptr decompress_info, j_compress_ptr compress_info,
  ClientData *client_data)
{
  size_t
    quality;

  compress_info->in_color_space = decompress_info->out_color_space;
  compress_info->input_components = decompress_info->output_components;
  compress_info->image_width = decompress_info->image_width;
  compress_info->image_height = decompress_info->image_height;
  jpeg_set_defaults(compress_info);
  if (client_data->quality > 0)
    quality = client_data->quality;
  else
    quality = DetermineQuality(decompress_info);
  jpeg_set_quality(compress_info, quality, TRUE);
  compress_info->optimize_coding = TRUE;
  if (client_data->progressive != FALSE)
    jpeg_simple_progression(compress_info);

  jpeg_start_compress(compress_info, TRUE);

  while (compress_info->next_scanline < compress_info->image_height)
  {
    jpeg_write_scanlines(compress_info, &client_data->buffer[compress_info->next_scanline],
      client_data->height);
  }
}

static void WriteCoefficients(j_decompress_ptr decompress_info, j_compress_ptr compress_info,
  ClientData *client_data)
{
  jpeg_copy_critical_parameters(decompress_info, compress_info);

  compress_info->optimize_coding = TRUE;
  if (client_data->progressive != FALSE)
    jpeg_simple_progression(compress_info);

  jpeg_write_coefficients(compress_info, client_data->coefficients);
}

static void WriteICCMarker(j_compress_ptr compress_info, ClientData *client_data)
{
  if (client_data->icc_marker == (Marker*)NULL)
    return;

  jpeg_write_marker(compress_info, client_data->icc_marker->code, (const JOCTET *)client_data->icc_marker->buffer, (unsigned int)client_data->icc_marker->length);
}

static boolean WriteJpeg(j_decompress_ptr decompress_info, ClientData *client_data)
{
  DestinationManager
    *destination;

  struct jpeg_compress_struct
    compress_info;

  struct jpeg_error_mgr
    jpeg_error;

  ResetMagickMemory(&compress_info, 0, sizeof(compress_info));

  destination = (DestinationManager *)NULL;
  if (setjmp(client_data->error_recovery) != 0)
  {
    jpeg_destroy_compress(&compress_info);
    if (destination != (DestinationManager *)NULL && destination->outputFile != (FILE *)NULL)
      fclose(destination->outputFile);
    return FALSE;
  }

  jpeg_create_compress(&compress_info);
  destination = CreateDestinationManager(&compress_info);
  if (destination == (DestinationManager *)NULL)
  {
    jpeg_destroy_compress(&compress_info);
    return FALSE;
  }

  compress_info.err = jpeg_std_error(&jpeg_error);
  compress_info.err->emit_message = (void(*)(j_common_ptr, int)) JpegWarningHandler;
  compress_info.err->error_exit = (void(*)(j_common_ptr)) JpegErrorHandler;
  compress_info.client_data = (void *)client_data;

  if (client_data->height != 0)
    CompressJpeg(decompress_info, &compress_info, client_data);
  else
    WriteCoefficients(decompress_info, &compress_info, client_data);

  WriteICCMarker(&compress_info, client_data);

  jpeg_finish_compress(&compress_info);
  jpeg_destroy_compress(&compress_info);

  return TRUE;
}

static void TerminateClientData(ClientData *client_data)
{
  size_t
    i;

  if (client_data->icc_marker != (Marker *)NULL)
  {
    if (client_data->icc_marker->buffer != (JOCTET *) NULL)
      free(client_data->icc_marker->buffer);
    free(client_data->icc_marker);
  }

  if (client_data->height == 0)
    return;

  for (i = 0; i < client_data->height; i++)
    free(client_data->buffer[i]);
  free(client_data->buffer);

  client_data->height = 0;
}

static size_t JpegOptimizer_Compress(ClientData *client_data, const MagickBooleanType progressive, const MagickBooleanType lessless, const MagickBooleanType quality)
{
  struct jpeg_decompress_struct
    decompress_info;

  struct jpeg_error_mgr
    jpeg_error;

  client_data->progressive = progressive != MagickFalse ? TRUE : FALSE;
  client_data->lossless = lessless != MagickFalse ? TRUE : FALSE;
  client_data->quality = quality;

  ResetMagickMemory(&decompress_info, 0, sizeof(decompress_info));
  decompress_info.err = jpeg_std_error(&jpeg_error);
  decompress_info.err->emit_message = (void(*)(j_common_ptr, int)) JpegWarningHandler;
  decompress_info.err->error_exit = (void(*)(j_common_ptr)) JpegErrorHandler;
  decompress_info.client_data = (void *)client_data;

  if (ReadJpeg(&decompress_info, client_data) == FALSE)
  {
    jpeg_destroy_decompress(&decompress_info);
    TerminateClientData(client_data);
    return 1;
  }

  if (WriteJpeg(&decompress_info, client_data) == FALSE)
  {
    jpeg_destroy_decompress(&decompress_info);
    TerminateClientData(client_data);
    return 2;
  }

  jpeg_finish_decompress(&decompress_info);
  jpeg_destroy_decompress(&decompress_info);
  TerminateClientData(client_data);

  return 0;
}

MAGICK_NET_EXPORT size_t JpegOptimizer_CompressFile(const char *input, const char *output, const MagickBooleanType progressive, const MagickBooleanType lessless, const MagickBooleanType quality)
{
  ClientData
    client_data;

  ResetMagickMemory(&client_data, 0, sizeof(client_data));

  client_data.inputFileName = input;
  client_data.outputFileName = output;

  return(JpegOptimizer_Compress(&client_data, progressive, lessless, quality));
}

MAGICK_NET_EXPORT size_t JpegOptimizer_CompressStream(const CustomStreamHandler reader, const CustomStreamHandler writer, const MagickBooleanType progressive, const MagickBooleanType lessless, const MagickBooleanType quality)
{
  ClientData
    client_data;

  ResetMagickMemory(&client_data, 0, sizeof(client_data));

  client_data.reader = reader;
  client_data.writer = writer;

  return(JpegOptimizer_Compress(&client_data, progressive, lessless, quality));
}