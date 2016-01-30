//=================================================================================================
// Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in 
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

#include "Stdafx.h"
#include "JpegOptimizer.h"

#include <jpeg/jpeglib.h>
#include <jpeg/jerror.h>
#include <setjmp.h>

#define MaxBufferExtent 16384

typedef struct _ClientData
{
  const char
    *inputFileName,
    *outputFileName;

  boolean
    progressive;

  jvirt_barray_ptr
    *coefficients;

  jmp_buf
    error_recovery;
} ClientData;

typedef struct _SourceManager
{
  struct jpeg_source_mgr
    manager;

  JOCTET
    *buffer;

  FILE
    *inputFile;

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

  if (fopen_s(&source->inputFile, client_data->inputFileName, "rb") != 0)
    ERREXIT(decompress_info, JERR_FILE_READ);
}

static inline boolean FillInputBuffer(j_decompress_ptr decompress_info)
{
  SourceManager
    *source;

  source = (SourceManager *)decompress_info->src;
  source->manager.bytes_in_buffer = (size_t)fread(source->buffer, 1, MaxBufferExtent,
    source->inputFile);
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
  fclose(source->inputFile);
  source->inputFile = (FILE *)NULL;
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
      ((j_common_ptr)decompress_info, JPOOL_IMAGE, MaxBufferExtent*sizeof(JOCTET));
    source->manager.init_source = InitializeSource;
    source->manager.fill_input_buffer = FillInputBuffer;
    source->manager.skip_input_data = SkipInputData;
    source->manager.resync_to_restart = jpeg_resync_to_restart;
    source->manager.term_source = TerminateSource;
    source->manager.bytes_in_buffer = 0;
    source->manager.next_input_byte = (const JOCTET *)NULL;
    decompress_info->src = (struct jpeg_source_mgr *) source;
  }

  return source;
}

static boolean ReadCoefficients(j_decompress_ptr decompress_info, ClientData *client_data)
{
  SourceManager
    *source;

  struct jpeg_error_mgr
    jpeg_error;

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

  decompress_info->err = jpeg_std_error(&jpeg_error);
  decompress_info->err->emit_message = (void(*)(j_common_ptr, int)) JpegWarningHandler;
  decompress_info->err->error_exit = (void(*)(j_common_ptr)) JpegErrorHandler;
  decompress_info->client_data = (void *)client_data;

  jpeg_read_header(decompress_info, TRUE);
  client_data->coefficients = jpeg_read_coefficients(decompress_info);

  return client_data->coefficients == (jvirt_barray_ptr *)NULL ? FALSE : TRUE;
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
    ((j_common_ptr)compress_info, JPOOL_IMAGE, MaxBufferExtent*sizeof(JOCTET));
  destination->manager.next_output_byte = destination->buffer;
  destination->manager.free_in_buffer = MaxBufferExtent;

  if (fopen_s(&destination->outputFile, client_data->outputFileName, "wb") != 0)
    ERREXIT(compress_info, JERR_FILE_WRITE);
}

static boolean EmptyOutputBuffer(j_compress_ptr compress_info)
{
  DestinationManager
    *destination;

  destination = (DestinationManager *)compress_info->dest;
  destination->manager.free_in_buffer = fwrite((const char *)destination->buffer, 1,
    MaxBufferExtent, destination->outputFile);
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
    if (fwrite((const char *)destination->buffer, 1, count, destination->outputFile) != count)
      ERREXIT(compress_info, JERR_FILE_WRITE);
  }
  fclose(destination->outputFile);
  destination->outputFile = (FILE *)NULL;
}

static inline DestinationManager* CreateDestinationManager(j_compress_ptr compress_info)
{
  DestinationManager
    *destination;

  compress_info->dest = (struct jpeg_destination_mgr *) (*compress_info->mem->alloc_small)
    ((j_common_ptr)compress_info, JPOOL_IMAGE, sizeof(DestinationManager));
  destination = (DestinationManager *)compress_info->dest;
  destination->manager.init_destination = InitializeDestination;
  destination->manager.empty_output_buffer = EmptyOutputBuffer;
  destination->manager.term_destination = TerminateDestination;
  return destination;
}

static boolean WriteCoefficients(j_decompress_ptr decompress_info, ClientData *client_data)
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

  jpeg_copy_critical_parameters(decompress_info, &compress_info);
  compress_info.optimize_coding = TRUE;

  if (client_data->progressive != FALSE)
    jpeg_simple_progression(&compress_info);

  jpeg_write_coefficients(&compress_info, client_data->coefficients);
  jpeg_finish_compress(&compress_info);
  jpeg_destroy_compress(&compress_info);

  return TRUE;
}

MAGICK_NET_EXPORT size_t JpegOptimizer_Optimize(const char *input, const char *output, const MagickBooleanType progressive)
{
  ClientData
    client_data;

  struct jpeg_decompress_struct
    decompress_info;

  ResetMagickMemory(&client_data, 0, sizeof(client_data));
  ResetMagickMemory(&decompress_info, 0, sizeof(decompress_info));

  client_data.progressive = progressive != MagickFalse ? TRUE : FALSE;
  client_data.inputFileName = input;
  client_data.outputFileName = output;

  if (ReadCoefficients(&decompress_info, &client_data) == FALSE)
  {
    jpeg_destroy_decompress(&decompress_info);
    return 1;
  }

  if (WriteCoefficients(&decompress_info, &client_data) == FALSE)
  {
    jpeg_destroy_decompress(&decompress_info);
    return 2;
  }

  jpeg_finish_decompress(&decompress_info);
  jpeg_destroy_decompress(&decompress_info);

  return 0;
}