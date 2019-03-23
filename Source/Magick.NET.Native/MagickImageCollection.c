// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
#include "MagickImageCollection.h"

#define SetChannelMask(channels) \
  { \
    ChannelType \
      *channel_masks; \
    Image \
      *channel_image; \
    size_t \
      channel_index, \
      channel_length; \
    channel_length = GetImageListLength(images); \
    channel_masks = (ChannelType *)AcquireMagickMemory(channel_length*sizeof(*channel_masks)); \
    channel_index = 0; \
    channel_image = images; \
    while (channel_image != (Image *) NULL) \
    { \
      channel_masks[channel_index++] = SetImageChannelMask(channel_image, (ChannelType)channels); \
      channel_image = channel_image->next; \
    }

#define RestoreChannelMask \
    channel_index = 0; \
    channel_image = images; \
    while (channel_image != (Image *)NULL) \
    { \
      SetPixelChannelMask(channel_image, channel_masks[channel_index++]); \
      channel_image = channel_image->next; \
    } \
    RelinquishMagickMemory(channel_masks); \
  }

MAGICK_NET_EXPORT Image *MagickImageCollection_Append(const Image *images, const MagickBooleanType stack, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = AppendImages(images, stack, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImageCollection_Coalesce(const Image *images, ExceptionInfo **exception)
{
  Image
    *result;

  MAGICK_NET_GET_EXCEPTION;
  result = CoalesceImages(images, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return result;
}

MAGICK_NET_EXPORT Image *MagickImageCollection_Combine(Image *images, const ColorspaceType colorSpace, ExceptionInfo **exception)
{
  Image
    *result;

  MAGICK_NET_GET_EXCEPTION;
  images->colorspace = colorSpace;
  result = CombineImages(images, colorSpace, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return result;
}

MAGICK_NET_EXPORT Image *MagickImageCollection_Deconstruct(const Image *images, ExceptionInfo **exception)
{
  Image
    *result;

  MAGICK_NET_GET_EXCEPTION;
  result = CompareImagesLayers(images, CompareAnyLayer, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return result;
}

MAGICK_NET_EXPORT void MagickImageCollection_Dispose(Image *images)
{
  DestroyImageList(images);
}

MAGICK_NET_EXPORT Image *MagickImageCollection_Evaluate(const Image *images, const size_t evaluateOperator, ExceptionInfo **exception)
{
  Image
    *result;

  MAGICK_NET_GET_EXCEPTION;
  result = EvaluateImages(images, (MagickEvaluateOperator) evaluateOperator, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return result;
}

MAGICK_NET_EXPORT void MagickImageCollection_Map(Image *images, const QuantizeInfo *settings, const Image *remapImage, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  RemapImages(settings, images, remapImage, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT Image *MagickImageCollection_Merge(Image *images, const size_t method, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = MergeImageLayers(images, (LayerMethod) method, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImageCollection_Montage(const Image *images, const MontageInfo *settings, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = MontageImages(images, settings, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImageCollection_Morph(const Image *images, const size_t frames, ExceptionInfo **exception)
{
  Image
    *result;

  MAGICK_NET_GET_EXCEPTION;
  result = MorphImages(images, frames, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return result;
}

MAGICK_NET_EXPORT Image *MagickImageCollection_Optimize(const Image *images, ExceptionInfo **exception)
{
  Image
    *result;

  MAGICK_NET_GET_EXCEPTION;
  result = OptimizeImageLayers(images, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return result;
}

MAGICK_NET_EXPORT Image *MagickImageCollection_OptimizePlus(const Image *images, ExceptionInfo **exception)
{
  Image
    *result;

  MAGICK_NET_GET_EXCEPTION;
  result = OptimizePlusImageLayers(images, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return result;
}

MAGICK_NET_EXPORT void MagickImageCollection_OptimizeTransparency(Image *images, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  OptimizeImageTransparency(images, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT Image *MagickImageCollection_Polynomial(Image *instance, const double *terms, const size_t length, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = PolynomialImage(instance, length, terms, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}


MAGICK_NET_EXPORT void MagickImageCollection_Quantize(Image *images, const QuantizeInfo *settings, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  QuantizeImages(settings, images, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT Image *MagickImageCollection_ReadBlob(const ImageInfo *settings, const unsigned char *data, const size_t length, ExceptionInfo **exception)
{
  Image
    *images;

  MAGICK_NET_GET_EXCEPTION;
  images = BlobToImage(settings, (const void *) data, length, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return images;
}

MAGICK_NET_EXPORT Image *MagickImageCollection_ReadFile(ImageInfo *settings, ExceptionInfo **exception)
{
  Image
    *images;

  MAGICK_NET_GET_EXCEPTION;
  images = ReadImages(settings, settings->filename, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return images;
}

MAGICK_NET_EXPORT Image *MagickImageCollection_ReadStream(ImageInfo *settings, const CustomStreamHandler reader, const CustomStreamSeeker seeker, const CustomStreamTeller teller, ExceptionInfo **exception)
{
  Image
    *images;

  CustomStreamInfo
    *info;

  MAGICK_NET_GET_EXCEPTION;
  info = AcquireCustomStreamInfo(exceptionInfo);
  SetCustomStreamReader(info, reader);
  SetCustomStreamSeeker(info, seeker);
  SetCustomStreamTeller(info, teller);
  SetImageInfoCustomStream(settings, info);
  images = CustomStreamToImage(settings, exceptionInfo);
  SetImageInfoCustomStream(settings, (CustomStreamInfo *) NULL);
  info = DestroyCustomStreamInfo(info);
  MAGICK_NET_SET_EXCEPTION;
  return images;
}

MAGICK_NET_EXPORT Image *MagickImageCollection_Smush(const Image *image, const ssize_t offset, const MagickBooleanType stack, ExceptionInfo **exception)
{
  Image
    *result;

  MAGICK_NET_GET_EXCEPTION;
  result = SmushImages(image, stack, offset, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return result;
}

MAGICK_NET_EXPORT void MagickImageCollection_WriteFile(Image *image, const ImageInfo *settings, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  WriteImages(settings, image, settings->filename, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT void MagickImageCollection_WriteStream(Image *image, ImageInfo *settings, const CustomStreamHandler writer, const CustomStreamSeeker seeker, const CustomStreamTeller teller, const CustomStreamHandler reader, ExceptionInfo **exception)
{
  CustomStreamInfo
    *info;

  MAGICK_NET_GET_EXCEPTION;
  info = AcquireCustomStreamInfo(exceptionInfo);
  SetCustomStreamWriter(info, writer);
  SetCustomStreamSeeker(info, seeker);
  SetCustomStreamTeller(info, teller);
  SetCustomStreamReader(info, reader);
  SetImageInfoCustomStream(settings, info);
  ImagesToCustomStream(settings, image, exceptionInfo);
  SetImageInfoCustomStream(settings, (CustomStreamInfo *) NULL);
  info = DestroyCustomStreamInfo(info);
  MAGICK_NET_SET_EXCEPTION;
}