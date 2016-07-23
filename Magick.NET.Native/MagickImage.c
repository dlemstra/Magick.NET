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
#include "MagickImage.h"
#include "Colors/MagickColor.h"
#include "Settings/DrawingSettings.h"
#include "Settings/QuantizeSettings.h"
#include "Types/PrimaryInfo.h"
#include "Types/MagickRectangle.h"
#include "Types/TypeMetric.h"

#define MagickPI 3.14159265358979323846264338327950288419716939937510
#define DegreesToRadians(x) (MagickPI*(x)/180.0)

#define SetChannelMask(image, channels) \
  { \
    ChannelType \
      channel_mask; \
    channel_mask=SetPixelChannelMask(image, (ChannelType)channels)

#define RestoreChannelMask(image) \
    SetPixelChannelMask(image, channel_mask); \
  }

static inline void RemoveFrames(Image *image)
{
  if (image != (Image *)NULL && image->next != (Image *)NULL)
  {
    Image
      *next;

    next = image->next;
    image->next = (Image *)NULL;
    next->previous = (Image *)NULL;
    DestroyImageList(next);
  }
}

static inline void SetTransformRotation(DrawInfo *instance, const double angle)
{
  AffineMatrix
    affine,
    current;

  affine.sx = 1.0;
  affine.rx = 0.0;
  affine.ry = 0.0;
  affine.sy = 1.0;
  affine.tx = 0.0;
  affine.ty = 0.0;

  current = instance->affine;
  affine.sx = cos(DegreesToRadians(fmod(angle, 360.0)));
  affine.rx = (-sin(DegreesToRadians(fmod(angle, 360.0))));
  affine.ry = sin(DegreesToRadians(fmod(angle, 360.0)));
  affine.sy = cos(DegreesToRadians(fmod(angle, 360.0)));

  instance->affine.sx = current.sx*affine.sx + current.ry*affine.rx;
  instance->affine.rx = current.rx*affine.sx + current.sy*affine.rx;
  instance->affine.ry = current.sx*affine.ry + current.ry*affine.sy;
  instance->affine.sy = current.rx*affine.ry + current.sy*affine.sy;
  instance->affine.tx = current.sx*affine.tx + current.ry*affine.ty + current.tx;
  instance->affine.ty = current.rx*affine.tx + current.sy*affine.ty + current.ty;
}

MAGICK_NET_EXPORT Image *MagickImage_Create(const ImageInfo *settings, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = AcquireImage(settings, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT void MagickImage_Dispose(Image *instance)
{
  DestroyImage(instance);
}

MAGICK_NET_EXPORT PixelInfo *MagickImage_AlphaColor_Get(const Image *instance)
{
  return MagickColor_Clone(&instance->alpha_color);
}

MAGICK_NET_EXPORT void MagickImage_AlphaColor_Set(Image *instance, const PixelInfo *value)
{
  if (value != (PixelInfo *)NULL)
    instance->alpha_color = *value;
}

MAGICK_NET_EXPORT size_t MagickImage_AnimationDelay_Get(const Image *instance)
{
  return instance->delay;
}

MAGICK_NET_EXPORT void MagickImage_AnimationDelay_Set(Image *instance, const size_t value)
{
  instance->delay = value;
}

MAGICK_NET_EXPORT size_t MagickImage_AnimationIterations_Get(const Image *instance)
{
  return instance->iterations;
}

MAGICK_NET_EXPORT void MagickImage_AnimationIterations_Set(Image *instance, const size_t value)
{
  instance->iterations = value;
}

MAGICK_NET_EXPORT PixelInfo *MagickImage_BackgroundColor_Get(const Image *instance)
{
  return MagickColor_Clone(&instance->background_color);
}

MAGICK_NET_EXPORT void MagickImage_BackgroundColor_Set(Image *instance, const PixelInfo *value)
{
  if (value != (PixelInfo *)NULL)
    instance->background_color = *value;
}

MAGICK_NET_EXPORT size_t MagickImage_BaseHeight_Get(const Image *instance)
{
  return instance->magick_rows;
}

MAGICK_NET_EXPORT size_t MagickImage_BaseWidth_Get(const Image *instance)
{
  return instance->magick_columns;
}

MAGICK_NET_EXPORT MagickBooleanType MagickImage_BlackPointCompensation_Get(const Image *instance)
{
  return instance->black_point_compensation;
}

MAGICK_NET_EXPORT void MagickImage_BlackPointCompensation_Set(Image *instance, MagickBooleanType value)
{
  instance->black_point_compensation = value;
}

MAGICK_NET_EXPORT PixelInfo *MagickImage_BorderColor_Get(const Image *instance)
{
  return MagickColor_Clone(&instance->border_color);
}

MAGICK_NET_EXPORT void MagickImage_BorderColor_Set(Image *instance, const PixelInfo *value)
{
  if (value != (PixelInfo *)NULL)
    instance->border_color = *value;
}

MAGICK_NET_EXPORT RectangleInfo *MagickImage_BoundingBox_Get(const Image *instance, ExceptionInfo **exception)
{
  RectangleInfo
    *result;

  MAGICK_NET_GET_EXCEPTION;
  result = MagickRectangle_Create();
  *result = GetImageBoundingBox(instance, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return result;
}

MAGICK_NET_EXPORT size_t MagickImage_ChannelCount_Get(const Image *instance)
{
  return instance->number_channels;
}

MAGICK_NET_EXPORT PrimaryInfo *MagickImage_ChromaBluePrimary_Get(const Image *instance)
{
  PrimaryInfo
    *result;

  result = PrimaryInfo_Create();
  *result = instance->chromaticity.blue_primary;
  return result;
}

MAGICK_NET_EXPORT void MagickImage_ChromaBluePrimary_Set(Image *instance, const PrimaryInfo *value)
{
  if (value != (PrimaryInfo *)NULL)
    instance->chromaticity.blue_primary = *value;
}

MAGICK_NET_EXPORT PrimaryInfo *MagickImage_ChromaGreenPrimary_Get(const Image *instance)
{
  PrimaryInfo
    *result;

  result = PrimaryInfo_Create();
  *result = instance->chromaticity.green_primary;
  return result;
}

MAGICK_NET_EXPORT void MagickImage_ChromaGreenPrimary_Set(Image *instance, const PrimaryInfo *value)
{
  if (value != (PrimaryInfo *)NULL)
    instance->chromaticity.green_primary = *value;
}

MAGICK_NET_EXPORT PrimaryInfo *MagickImage_ChromaRedPrimary_Get(const Image *instance)
{
  PrimaryInfo
    *result;

  result = PrimaryInfo_Create();
  *result = instance->chromaticity.red_primary;
  return result;
}

MAGICK_NET_EXPORT void MagickImage_ChromaRedPrimary_Set(Image *instance, const PrimaryInfo *value)
{
  if (value != (PrimaryInfo *)NULL)
    instance->chromaticity.red_primary = *value;
}

MAGICK_NET_EXPORT PrimaryInfo *MagickImage_ChromaWhitePoint_Get(const Image *instance)
{
  PrimaryInfo
    *result;

  result = PrimaryInfo_Create();
  *result = instance->chromaticity.white_point;
  return result;
}

MAGICK_NET_EXPORT void MagickImage_ChromaWhitePoint_Set(Image *instance, const PrimaryInfo *value)
{
  if (value != (PrimaryInfo *)NULL)
    instance->chromaticity.white_point = *value;
}

MAGICK_NET_EXPORT size_t MagickImage_ClassType_Get(const Image *instance, ExceptionInfo **exception)
{
  (void)exception;

  return instance->storage_class;
}

MAGICK_NET_EXPORT void MagickImage_ClassType_Set(Image *instance, const size_t value, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  if ((ClassType)value == DirectClass && instance->storage_class == PseudoClass)
  {
    SyncImage(instance, exceptionInfo);
    instance->colormap = (PixelInfo *)RelinquishMagickMemory(instance->colormap);
    instance->storage_class = DirectClass;
  }
  else if ((ClassType)value == PseudoClass && instance->storage_class == DirectClass)
  {
    QuantizeInfo
      *settings;

    settings = QuantizeSettings_Create();
    settings->number_colors = MaxColormapSize;
    QuantizeImage(settings, instance, exceptionInfo);
    QuantizeSettings_Dispose(settings);
    instance->storage_class = PseudoClass;
  }
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT double MagickImage_ColorFuzz_Get(const Image *instance)
{
  return instance->fuzz;
}

MAGICK_NET_EXPORT void MagickImage_ColorFuzz_Set(Image *instance, const double value)
{
  instance->fuzz = value;
}

MAGICK_NET_EXPORT ssize_t MagickImage_ColormapSize_Get(const Image *instance, ExceptionInfo **exception)
{
  (void)exception;

  if (instance->colormap == (PixelInfo *)NULL)
    return -1;

  return instance->colors;
}

MAGICK_NET_EXPORT void MagickImage_ColormapSize_Set(Image *instance, const ssize_t value, ExceptionInfo **exception)
{
  if (value < 0 || value > MaxColormapSize)
    return;

  MAGICK_NET_GET_EXCEPTION;
  AcquireImageColormap(instance, (size_t)value, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT size_t MagickImage_ColorSpace_Get(const Image *instance, ExceptionInfo **exception)
{
  (void)exception;

  return (size_t)instance->colorspace;
}

MAGICK_NET_EXPORT void MagickImage_ColorSpace_Set(Image *instance, const size_t value, ExceptionInfo **exception)
{
  if (instance->colorspace == (ColorspaceType)value)
    return;

  MAGICK_NET_GET_EXCEPTION;
  TransformImageColorspace(instance, (const ColorspaceType)value, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT size_t MagickImage_ColorType_Get(const Image *instance, ExceptionInfo **exception)
{
  (void)exception;

  return (size_t)GetImageType(instance);
}

MAGICK_NET_EXPORT void MagickImage_ColorType_Set(Image *instance, const size_t value, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  SetImageType(instance, (const ImageType)value, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT size_t MagickImage_Compose_Get(const Image *instance)
{
  return (size_t)instance->compose;
}

MAGICK_NET_EXPORT void MagickImage_Compose_Set(Image *instance, const size_t value)
{
  instance->compose = (CompositeOperator)value;
}

MAGICK_NET_EXPORT size_t MagickImage_CompressionMethod_Get(const Image *instance)
{
  return (size_t)instance->compression;
}

MAGICK_NET_EXPORT void MagickImage_CompressionMethod_Set(Image *instance, const size_t value)
{
  instance->compression = (CompressionType)value;
}

MAGICK_NET_EXPORT size_t MagickImage_Depth_Get(const Image *instance)
{
  return instance->depth;
}

MAGICK_NET_EXPORT void MagickImage_Depth_Set(Image *instance, const size_t value)
{
  if (value > MAGICKCORE_QUANTUM_DEPTH)
    instance->depth = MAGICKCORE_QUANTUM_DEPTH;
  else
    instance->depth = value;
}

MAGICK_NET_EXPORT const char *MagickImage_EncodingGeometry_Get(const Image *instance)
{
  return instance->geometry;
}

MAGICK_NET_EXPORT size_t MagickImage_Endian_Get(const Image *instance)
{
  return (size_t)instance->endian;
}

MAGICK_NET_EXPORT void MagickImage_Endian_Set(Image *instance, const size_t value)
{
  instance->endian = (EndianType)value;
}

MAGICK_NET_EXPORT const char *MagickImage_FileName_Get(const Image *instance)
{
  return (const char *)&(instance->filename);
}

MAGICK_NET_EXPORT void MagickImage_FileName_Set(Image *instance, const char *value)
{
  if (value == (const char *)NULL)
    *instance->filename = '\0';
  else
    CopyMagickString(instance->filename, value, MaxTextExtent);
}

MAGICK_NET_EXPORT long MagickImage_FileSize_Get(const Image *instance)
{
  return (long)GetBlobSize(instance);
}

MAGICK_NET_EXPORT size_t MagickImage_FilterType_Get(const Image *instance)
{
  return (size_t)instance->filter;
}

MAGICK_NET_EXPORT void MagickImage_FilterType_Set(Image *instance, const size_t value)
{
  instance->filter = (FilterType)value;
}

MAGICK_NET_EXPORT const char *MagickImage_Format_Get(const Image *instance)
{
  return (const char *)&(instance->magick);
}

MAGICK_NET_EXPORT void MagickImage_Format_Set(Image *instance, const char *value)
{
  if (value == (const char *)NULL)
    *instance->magick = '\0';
  else
    CopyMagickString(instance->magick, value, MaxTextExtent);
}

MAGICK_NET_EXPORT double MagickImage_Gamma_Get(const Image *instance)
{
  return instance->gamma;
}

MAGICK_NET_EXPORT size_t MagickImage_GifDisposeMethod_Get(const Image *instance)
{
  return (size_t)instance->dispose;
}

MAGICK_NET_EXPORT void MagickImage_GifDisposeMethod_Set(Image *instance, const size_t value)
{
  instance->dispose = (DisposeType)value;
}

MAGICK_NET_EXPORT size_t MagickImage_Interpolate_Get(const Image *instance)
{
  return (size_t)instance->interpolate;
}

MAGICK_NET_EXPORT void MagickImage_Interpolate_Set(Image *instance, const size_t value)
{
  instance->interpolate = (PixelInterpolateMethod)value;
}

MAGICK_NET_EXPORT MagickBooleanType MagickImage_HasAlpha_Get(const Image *instance, ExceptionInfo **exception)
{
  (exception);
  return instance->alpha_trait == BlendPixelTrait ? MagickTrue : MagickFalse;
}

MAGICK_NET_EXPORT void MagickImage_HasAlpha_Set(Image *instance, const MagickBooleanType value, ExceptionInfo **exception)
{
  CacheView
    *cache_view;

  instance->alpha_trait = value ? BlendPixelTrait : UndefinedPixelTrait;
  MAGICK_NET_GET_EXCEPTION;
  cache_view = AcquireAuthenticCacheView(instance, exceptionInfo);
  (void)GetCacheViewAuthenticPixels(cache_view, 0, 0, 1, 1, exceptionInfo);
  cache_view = DestroyCacheView(cache_view);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT size_t MagickImage_Height_Get(const Image *instance)
{
  return instance->rows;
}

MAGICK_NET_EXPORT size_t MagickImage_Interlace_Get(const Image *instance)
{
  return (size_t)instance->interlace;
}

MAGICK_NET_EXPORT void MagickImage_Interlace_Set(Image *instance, const size_t value)
{
  instance->interlace = (InterlaceType)value;
}

MAGICK_NET_EXPORT MagickBooleanType MagickImage_IsOpaque_Get(const Image *instance, ExceptionInfo **exception)
{
  MagickBooleanType
    result;

  MAGICK_NET_GET_EXCEPTION;
  result = IsImageOpaque(instance, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return result;
}

MAGICK_NET_EXPORT double MagickImage_MeanErrorPerPixel_Get(const Image *instance)
{
  return instance->error.mean_error_per_pixel;
}

MAGICK_NET_EXPORT double MagickImage_NormalizedMaximumError_Get(const Image *instance)
{
  return instance->error.normalized_maximum_error;
}

MAGICK_NET_EXPORT double MagickImage_NormalizedMeanError_Get(const Image *instance)
{
  return instance->error.normalized_mean_error;
}

MAGICK_NET_EXPORT size_t MagickImage_Orientation_Get(const Image *instance)
{
  return (size_t)instance->orientation;
}

MAGICK_NET_EXPORT void MagickImage_Orientation_Set(Image *instance, const size_t value)
{
  instance->orientation = (OrientationType)value;
}

MAGICK_NET_EXPORT size_t MagickImage_RenderingIntent_Get(const Image *instance)
{
  return (size_t)instance->rendering_intent;
}

MAGICK_NET_EXPORT void MagickImage_RenderingIntent_Set(Image *instance, const size_t value)
{
  instance->rendering_intent = (RenderingIntent)value;
}

MAGICK_NET_EXPORT RectangleInfo *MagickImage_Page_Get(const Image *instance)
{
  RectangleInfo
    *rectangle_info;

  rectangle_info = (RectangleInfo *)AcquireMagickMemory(sizeof(*rectangle_info));
  if (rectangle_info == (RectangleInfo *)NULL)
    return (RectangleInfo *)NULL;
  *rectangle_info = instance->page;
  return rectangle_info;
}

MAGICK_NET_EXPORT void MagickImage_Page_Set(Image *instance, const RectangleInfo *value)
{
  instance->page = *value;
}

MAGICK_NET_EXPORT size_t MagickImage_Quality_Get(const Image *instance)
{
  return instance->quality;
}

MAGICK_NET_EXPORT void MagickImage_Quality_Set(Image *instance, const size_t value)
{
  instance->quality = value;
}

MAGICK_NET_EXPORT Image *MagickImage_ReadMask_Get(const Image *instance, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = GetImageMask(instance, ReadPixelMask, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT void MagickImage_ReadMask_Set(Image *instance, const Image *mask, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  SetImageMask(instance, ReadPixelMask, mask, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT size_t MagickImage_ResolutionUnits_Get(const Image *instance)
{
  return (size_t)instance->units;
}

MAGICK_NET_EXPORT void MagickImage_ResolutionUnits_Set(Image *instance, const size_t value)
{
  instance->units = (ResolutionType)value;
}

MAGICK_NET_EXPORT double MagickImage_ResolutionX_Get(const Image *instance)
{
  return instance->resolution.x;
}

MAGICK_NET_EXPORT void MagickImage_ResolutionX_Set(Image *instance, const double value)
{
  instance->resolution.x = value;
}

MAGICK_NET_EXPORT double MagickImage_ResolutionY_Get(const Image *instance)
{
  return instance->resolution.y;
}

MAGICK_NET_EXPORT void MagickImage_ResolutionY_Set(Image *instance, const double value)
{
  instance->resolution.y = value;
}

MAGICK_NET_EXPORT const char *MagickImage_Signature_Get(Image *instance, ExceptionInfo **exception)
{
  const char
    *property;

  MAGICK_NET_GET_EXCEPTION;
  property = (const char *)NULL;
  if (instance->taint == MagickFalse)
    property = GetImageProperty(instance, "Signature", exceptionInfo);
  if (property == (const char *)NULL)
  {
    SignatureImage(instance, exceptionInfo);
    property = GetImageProperty(instance, "Signature", exceptionInfo);
  }
  MAGICK_NET_SET_EXCEPTION;
  return property;
}

MAGICK_NET_EXPORT size_t MagickImage_TotalColors_Get(const Image *instance, ExceptionInfo **exception)
{
  size_t
    colors;

  MAGICK_NET_GET_EXCEPTION;
  colors = GetNumberColors(instance, (FILE *)NULL, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return colors;
}

MAGICK_NET_EXPORT size_t MagickImage_VirtualPixelMethod_Get(const Image *instance, ExceptionInfo **exception)
{
  (void)exception;

  return GetImageVirtualPixelMethod(instance);
}

MAGICK_NET_EXPORT void MagickImage_VirtualPixelMethod_Set(Image *instance, const size_t value, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  SetImageVirtualPixelMethod(instance, (const VirtualPixelMethod)value, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION
}

MAGICK_NET_EXPORT size_t MagickImage_Width_Get(const Image *instance)
{
  return instance->columns;
}

MAGICK_NET_EXPORT Image *MagickImage_WriteMask_Get(const Image *instance, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = GetImageMask(instance, WritePixelMask, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT void MagickImage_WriteMask_Set(Image *instance, const Image *mask, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  SetImageMask(instance, WritePixelMask, mask, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT Image *MagickImage_AdaptiveBlur(const Image *instance, const double radius, const double sigma, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = AdaptiveBlurImage(instance, radius, sigma, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_AdaptiveResize(const Image *instance, const size_t width, const size_t height, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = AdaptiveResizeImage(instance, width, height, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_AdaptiveSharpen(Image *instance, const double radius, const double sigma, const size_t channels, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  image = AdaptiveSharpenImage(instance, radius, sigma, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_AdaptiveThreshold(const Image *instance, const size_t width, const size_t height, const double bias, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = AdaptiveThresholdImage(instance, width, height, bias, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_AddNoise(Image *instance, const size_t noiseType, const double attenuate, const size_t channels, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  image = AddNoiseImage(instance, (const NoiseType)noiseType, attenuate, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT void MagickImage_AddProfile(Image *instance, const char *name, const unsigned char * datum, const size_t length, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  ProfileImage(instance, name, datum, length, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT Image *MagickImage_AffineTransform(Image *instance, const double scaleX, const double scaleY, const double shearX, const double shearY, const double translateX, const double translateY, ExceptionInfo **exception)
{
  AffineMatrix
    matrix;

  Image
    *image;

  matrix.sx = scaleX;
  matrix.sy = scaleY;
  matrix.rx = shearX;
  matrix.ry = shearY;
  matrix.tx = translateX;
  matrix.ty = translateY;

  MAGICK_NET_GET_EXCEPTION;
  image = AffineTransformImage(instance, &matrix, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT void MagickImage_Annotate(Image *instance, const DrawInfo *settings, char *text, char *boundingArea, const size_t gravity, const double angle, ExceptionInfo **exception)
{
  DrawInfo
    *drawInfo;

  drawInfo = CloneDrawInfo((const ImageInfo *)NULL, settings);
  drawInfo->text = DestroyString(drawInfo->text);
  drawInfo->text = text;
  drawInfo->geometry = DestroyString(drawInfo->geometry);
  drawInfo->geometry = boundingArea;
  drawInfo->gravity = (GradientType)gravity;

  if (angle != 0.0)
    SetTransformRotation(drawInfo, angle);

  MAGICK_NET_GET_EXCEPTION;
  AnnotateImage(instance, drawInfo, exceptionInfo);
  drawInfo->text = (char *)NULL;
  drawInfo->geometry = (char *)NULL;
  DestroyDrawInfo(drawInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT void MagickImage_AnnotateGravity(Image *instance, const DrawInfo *settings, char *text, const size_t gravity, ExceptionInfo **exception)
{
  DrawInfo
    *drawInfo;

  drawInfo = CloneDrawInfo((const ImageInfo *)NULL, settings);
  drawInfo->text = DestroyString(drawInfo->text);
  drawInfo->text = text;
  drawInfo->gravity = (GravityType)gravity;

  MAGICK_NET_GET_EXCEPTION;
  AnnotateImage(instance, drawInfo, exceptionInfo);
  drawInfo->text = (char *)NULL;
  DestroyDrawInfo(drawInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT void MagickImage_AutoGamma(Image *instance, const size_t channels, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  AutoGammaImage(instance, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT void MagickImage_AutoLevel(Image *instance, const size_t channels, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  AutoLevelImage(instance, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT Image *MagickImage_AutoOrient(const Image *instance, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = AutoOrientImage(instance, instance->orientation, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT void MagickImage_BlackThreshold(Image *instance, const char *threshold, const size_t channels, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  BlackThresholdImage(instance, threshold, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT Image *MagickImage_BlueShift(const Image *instance, const double factor, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = BlueShiftImage(instance, factor, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_Blur(Image *instance, const double radius, const double sigma, const size_t channels, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  image = BlurImage(instance, radius, sigma, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_Border(const Image *instance, const RectangleInfo *value, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = BorderImage(instance, value, instance->compose, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT void MagickImage_BrightnessContrast(Image *instance, const double brightness, const double contrast, const size_t channels, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  BrightnessContrastImage(instance, brightness, contrast, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT Image *MagickImage_CannyEdge(const Image *instance, const double radius, const double sigma, const double lower, const double upper, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = CannyEdgeImage(instance, radius, sigma, lower, upper, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT size_t MagickImage_ChannelOffset(const Image *instance, const size_t channel)
{
  return instance->channel_map[channel].offset;
}

MAGICK_NET_EXPORT Image *MagickImage_Charcoal(const Image *instance, const double radius, const double sigma, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = CharcoalImage(instance, radius, sigma, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_Chop(const Image *instance, const RectangleInfo *geometry, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = ChopImage(instance, geometry, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT void MagickImage_Clamp(Image *instance, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  ClampImage(instance, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT void MagickImage_ClampChannel(Image *instance, const size_t channels, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  ClampImage(instance, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT void MagickImage_Clip(Image *instance, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  ClipImage(instance, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT void MagickImage_ClipPath(Image *instance, const char *pathName, const MagickBooleanType inside, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  ClipImagePath(instance, pathName, inside, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT Image *MagickImage_Clone(const Image *instance, ExceptionInfo **exception)
{
  Image
    *image;

  if (instance == (const Image *)NULL)
    return (Image *)NULL;

  MAGICK_NET_GET_EXCEPTION;
  image = CloneImage(instance, 0, 0, MagickTrue, exceptionInfo);
  SyncImage(image, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT void MagickImage_Clut(Image *instance, Image *clutImage, const size_t method, const size_t channels, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(clutImage, channels);
  ClutImage(instance, clutImage, (const PixelInterpolateMethod)method, exceptionInfo);
  RestoreChannelMask(clutImage);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT void MagickImage_ColorDecisionList(Image *instance, const char *fileName, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  ColorDecisionListImage(instance, fileName, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT Image *MagickImage_Colorize(const Image *instance, const PixelInfo *color, const char *blend, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = ColorizeImage(instance, blend, color, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_ColorMatrix(const Image *instance, const KernelInfo *kernel, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = ColorMatrixImage(instance, kernel, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_Compare(Image *instance, Image *reference, const size_t metric, const size_t channels, double *distortion, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(reference, channels);
  image = CompareImages(instance, reference, (const MetricType)metric, distortion, exceptionInfo);
  RestoreChannelMask(reference);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT double MagickImage_CompareDistortion(Image *instance, Image *reference, const size_t metric, const size_t channels, ExceptionInfo **exception)
{
  double
    result;

  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(reference, channels);
  GetImageDistortion(instance, reference, (const MetricType)metric, &result, exceptionInfo);
  RestoreChannelMask(reference);
  MAGICK_NET_SET_EXCEPTION;
  return result;
}

MAGICK_NET_EXPORT void MagickImage_Composite(Image *instance, const Image *reference, const ssize_t x, const ssize_t y, const size_t compose, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  CompositeImage(instance, reference, (const CompositeOperator)compose, MagickTrue, x, y, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT void MagickImage_CompositeGravity(Image *instance, const Image *reference, const size_t gravity, const size_t compose, ExceptionInfo **exception)
{
  RectangleInfo
    geometry;

  SetGeometry(reference, &geometry);
  GravityAdjustGeometry(instance->columns, instance->rows, gravity, &geometry);
  MagickImage_Composite(instance, reference, geometry.x, geometry.y, compose, exception);
}

MAGICK_NET_EXPORT Image *MagickImage_ConnectedComponents(const Image *instance, const size_t connectivity, CCObjectInfo **objects, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = ConnectedComponentsImage(instance, connectivity, objects, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT void MagickImage_Contrast(Image *instance, const MagickBooleanType enhance, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  ContrastImage(instance, enhance, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT void MagickImage_ContrastStretch(Image *instance, const double blackPoint, const double whitePoint, const size_t channels, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  ContrastStretchImage(instance, blackPoint, whitePoint, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT Image *MagickImage_Convolve(const Image *instance, const KernelInfo *kernel, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = ConvolveImage(instance, kernel, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT void MagickImage_CopyPixels(Image *instance, const Image *image, const RectangleInfo *geometry, const OffsetInfo *offset, const size_t channels, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  CopyImagePixels(instance, image, geometry, offset, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT Image *MagickImage_Crop(const Image *instance, const RectangleInfo *geometry, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = CropImage(instance, geometry, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_CropToTiles(const Image *instance, const char *geometry, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = CropImageToTiles(instance, geometry, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT void MagickImage_CycleColormap(Image *instance, const ssize_t amount, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  CycleColormapImage(instance, amount, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT void MagickImage_Decipher(Image *instance, const char *passphrase, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  DecipherImage(instance, passphrase, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT Image *MagickImage_Deskew(const Image *instance, const double threshold, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = DeskewImage(instance, threshold, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_Despeckle(const Image *instance, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = DespeckleImage(instance, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT const size_t MagickImage_DetermineColorType(const Image *instance, ExceptionInfo **exception)
{
  ImageType
    imageType;

  MAGICK_NET_GET_EXCEPTION;
  imageType = IdentifyImageType(instance, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return imageType;
}

MAGICK_NET_EXPORT Image *MagickImage_Distort(const Image *instance, const size_t method, const MagickBooleanType bestfit, const double *arguments, const size_t length, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = DistortImage(instance, (const DistortMethod)method, length, arguments, bestfit, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_Edge(const Image *instance, const double radius, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = EdgeImage(instance, radius, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_Emboss(const Image *instance, const double radius, const double sigma, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = EmbossImage(instance, radius, sigma, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT void MagickImage_Encipher(Image *instance, const char *passphrase, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  EncipherImage(instance, passphrase, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT Image *MagickImage_Enhance(const Image *instance, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = EnhanceImage(instance, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT void MagickImage_Equalize(Image *instance, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  EqualizeImage(instance, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT MagickBooleanType MagickImage_Equals(const Image *instance, const Image *reference, ExceptionInfo **exception)
{
  MagickBooleanType
    result;

  MAGICK_NET_GET_EXCEPTION;
  result = IsImagesEqual(instance, reference, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return result;
}

MAGICK_NET_EXPORT void MagickImage_EvaluateFunction(Image *instance, const size_t channels, const size_t evaluateFunction, const double *values, const size_t length, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  FunctionImage(instance, (const MagickFunction)evaluateFunction, length, values, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT void MagickImage_EvaluateGeometry(Image *instance, const size_t channels, const RectangleInfo *geometry, const size_t evaluateOperator, const double value, ExceptionInfo **exception)
{
  Image
    *cropImage;

  MAGICK_NET_GET_EXCEPTION;
  cropImage = CropImage(instance, geometry, exceptionInfo);
  SetChannelMask(cropImage, channels);
  EvaluateImage(cropImage, evaluateOperator, value, exceptionInfo);
  RestoreChannelMask(cropImage);
  CompositeImage(instance, cropImage, instance->alpha_trait == BlendPixelTrait ? OverCompositeOp : CopyCompositeOp, MagickFalse, geometry->x, geometry->y, exceptionInfo);
  DestroyImage(cropImage);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT void MagickImage_EvaluateOperator(Image *instance, const size_t channels, const size_t evaluateOperator, const double value, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  EvaluateImage(instance, (const MagickEvaluateOperator)evaluateOperator, value, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT Image *MagickImage_Extent(const Image *instance, const char *geometry, ExceptionInfo **exception)
{
  Image
    *image;

  RectangleInfo
    rectangle;

  SetGeometry(instance, &rectangle);
  ParseMetaGeometry(geometry, &rectangle.x, &rectangle.y, &rectangle.width, &rectangle.height);

  MAGICK_NET_GET_EXCEPTION;
  image = ExtentImage(instance, &rectangle, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_ExtentGravity(const Image *instance, const char *geometry, const size_t gravity, ExceptionInfo **exception)
{
  Image
    *image;

  RectangleInfo
    rectangle;

  SetGeometry(instance, &rectangle);
  ParseMetaGeometry(geometry, &rectangle.x, &rectangle.y, &rectangle.width, &rectangle.height);
  GravityAdjustGeometry(instance->columns, instance->rows, (const GravityType)gravity, &rectangle);

  MAGICK_NET_GET_EXCEPTION;
  image = ExtentImage(instance, &rectangle, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT MagickBooleanType MagickImage_HasChannel(const Image *instance, const size_t channel)
{
  PixelChannel
    pixelChannel;

  pixelChannel = (PixelChannel)channel;
  if (GetPixelChannelTraits(instance, pixelChannel) == UndefinedPixelTrait)
    return MagickFalse;

  if (pixelChannel == GreenPixelChannel || pixelChannel == BluePixelChannel)
  {
    if (GetPixelChannelOffset(instance, pixelChannel) != (ssize_t)channel)
      return MagickFalse;
  }

  return MagickTrue;
}

MAGICK_NET_EXPORT Image *MagickImage_Flip(const Image *instance, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = FlipImage(instance, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT void MagickImage_FloodFill(Image *instance, const DrawInfo *settings, const ssize_t x, const ssize_t y, const PixelInfo *target, const MagickBooleanType invert, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  FloodfillPaintImage(instance, settings, target, x, y, invert, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT Image *MagickImage_Flop(const Image *instance, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = FlopImage(instance, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT TypeMetric *MagickImage_FontTypeMetrics(Image *instance, const DrawInfo *settings, const MagickBooleanType ignoreNewLines, ExceptionInfo **exception)
{
  TypeMetric
    *result;

  result = TypeMetric_Create();
  MAGICK_NET_GET_EXCEPTION;
  if (ignoreNewLines != MagickFalse)
    GetTypeMetrics(instance, settings, result, exceptionInfo);
  else
    GetMultilineTypeMetrics(instance, settings, result, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return result;
}

MAGICK_NET_EXPORT char *MagickImage_FormatExpression(Image *instance, ImageInfo *settings, const char *expression, ExceptionInfo **exception)
{
  char
    *result;

  MAGICK_NET_GET_EXCEPTION;
  result = InterpretImageProperties(settings, instance, expression, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return result;
}

MAGICK_NET_EXPORT Image *MagickImage_Frame(const Image *instance, const RectangleInfo *geometry, ExceptionInfo **exception)
{
  FrameInfo
    info;

  Image
    *result;

  info.x = geometry->width;
  info.y = geometry->height;
  info.width = instance->columns + (((size_t)info.x) << 1);
  info.height = instance->rows + (((size_t)info.y) << 1);
  info.outer_bevel = geometry->x;
  info.inner_bevel = geometry->y;

  MAGICK_NET_GET_EXCEPTION;
  result = FrameImage(instance, &info, instance->compose, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return result;
}

MAGICK_NET_EXPORT Image *MagickImage_Fx(Image *instance, const char *expression, const size_t channels, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  image = FxImage(instance, expression, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT void MagickImage_GammaCorrect(Image *instance, const double gamma, const size_t channels, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  GammaImage(instance, gamma, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT Image *MagickImage_GaussianBlur(Image *instance, const double radius, const double sigma, const size_t channels, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  image = GaussianBlurImage(instance, radius, sigma, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT const char *MagickImage_GetArtifact(const Image *instance, const char *name)
{
  return GetImageArtifact(instance, name);
}

MAGICK_NET_EXPORT const char *MagickImage_GetAttribute(const Image *instance, const char *name, ExceptionInfo **exception)
{
  const char
    *result;

  MAGICK_NET_GET_EXCEPTION;
  result = GetImageProperty(instance, name, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return result;
}

MAGICK_NET_EXPORT size_t MagickImage_GetBitDepth(Image *instance, const size_t channels, ExceptionInfo **exception)
{
  size_t
    result;

  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  result = GetImageDepth(instance, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
  return result;
}

MAGICK_NET_EXPORT PixelInfo *MagickImage_GetColormap(const Image *instance, const size_t index)
{
  if (instance->colormap == (PixelInfo *)NULL)
    return (PixelInfo *)NULL;

  if (index >= instance->colors)
    return (PixelInfo *)NULL;

  return MagickColor_Clone(&instance->colormap[index]);
}

MAGICK_NET_EXPORT Image *MagickImage_GetNext(const Image *instance)
{
  return instance->next;
}

MAGICK_NET_EXPORT const char *MagickImage_GetNextArtifactName(const Image *instance)
{
  return GetNextImageArtifact(instance);
}

MAGICK_NET_EXPORT const char *MagickImage_GetNextAttributeName(const Image *instance)
{
  return GetNextImageProperty(instance);
}

MAGICK_NET_EXPORT const char *MagickImage_GetNextProfileName(const Image *instance)
{
  return GetNextImageProfile(instance);
}

MAGICK_NET_EXPORT const StringInfo *MagickImage_GetProfile(const Image *instance, const char *name)
{
  return GetImageProfile(instance, name);
}

MAGICK_NET_EXPORT void MagickImage_Grayscale(Image *instance, const size_t method, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  GrayscaleImage(instance, (const PixelIntensityMethod)method, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT void MagickImage_HaldClut(Image *instance, const Image *image, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  HaldClutImage(instance, image, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT MagickBooleanType MagickImage_HasProfile(const Image *instance, const char *name)
{
  return (GetImageProfile(instance, name) == (const StringInfo *)NULL) ? MagickFalse : MagickTrue;
}

MAGICK_NET_EXPORT PixelInfo *MagickImage_Histogram(const Image *instance, size_t *length, ExceptionInfo **exception)
{
  PixelInfo
    *result;

  MAGICK_NET_GET_EXCEPTION;
  result = GetImageHistogram(instance, length, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return result;
}

MAGICK_NET_EXPORT Image *MagickImage_HoughLine(const Image *instance, const size_t width, const size_t height, const size_t threshold, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = HoughLineImage(instance, width, height, threshold, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_Implode(const Image *instance, const double amount, const size_t method, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = ImplodeImage(instance, amount, (const PixelInterpolateMethod)method, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_Kuwahara(const Image *instance, const double radius, const double sigma, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = KuwaharaImage(instance, radius, sigma, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT void MagickImage_Level(Image *instance, const double blackPoint, const double whitePoint, const double gamma, const size_t channels, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  LevelImage(instance, blackPoint, whitePoint, gamma, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT void MagickImage_LevelColors(Image *instance, const PixelInfo *blackColor, const PixelInfo *whiteColor, const size_t channels, const MagickBooleanType invert, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  LevelImageColors(instance, blackColor, whiteColor, invert, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT void MagickImage_Levelize(Image *instance, const double blackPoint, const double whitePoint, const double gamma, const size_t channels, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  LevelizeImage(instance, blackPoint, whitePoint, gamma, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT void MagickImage_LinearStretch(Image *instance, const double blackPoint, const double whitePoint, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  LinearStretchImage(instance, blackPoint, whitePoint, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT Image *MagickImage_LiquidRescale(const Image *instance, const char *geometry, ExceptionInfo **exception)
{
  Image
    *result;

  size_t
    height,
    width;

  ssize_t
    x = 0,
    y = 0;

  width = instance->columns;
  height = instance->rows;
  ParseMetaGeometry(geometry, &x, &y, &width, &height);

  MAGICK_NET_GET_EXCEPTION;
  result = LiquidRescaleImage(instance, width, height, (double)x, (double)y, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return result;
}

MAGICK_NET_EXPORT Image *MagickImage_LocalContrast(const Image *instance, const double radius, const double strength, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = LocalContrastImage(instance, radius, strength, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_Magnify(const Image *instance, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = MagnifyImage(instance, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT const MagickBooleanType MagickImage_Map(Image *instance, const Image *image, const QuantizeInfo *settings, ExceptionInfo **exception)
{
  MagickBooleanType
    result;

  MAGICK_NET_GET_EXCEPTION;
  result = RemapImage(settings, instance, image, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return result;
}

MAGICK_NET_EXPORT Image *MagickImage_Minify(const Image *instance, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = MinifyImage(instance, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT void MagickImage_Modulate(Image *instance, const char *modulate, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  ModulateImage(instance, modulate, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT ChannelMoments *MagickImage_Moments(const Image *instance, ExceptionInfo **exception)
{
  ChannelMoments
    *result;

  MAGICK_NET_GET_EXCEPTION;
  result = GetImageMoments(instance, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return result;
}

MAGICK_NET_EXPORT Image *MagickImage_Morphology(Image *instance, const size_t method, const char *kernel, const size_t channels, const size_t iterations, ExceptionInfo **exception)
{
  Image
    *image;

  KernelInfo
    *kernelInfo;

  MAGICK_NET_GET_EXCEPTION;
  kernelInfo = AcquireKernelInfo(kernel, exceptionInfo);
  if (kernelInfo == (KernelInfo *)NULL)
  {
    MAGICK_NET_RAISE_EXCEPTION(OptionError, "Unable to parse kernel.");
    return (Image *)NULL;
  }
  SetChannelMask(instance, channels);
  image = MorphologyImage(instance, (const MorphologyMethod)method, iterations, kernelInfo, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_MotionBlur(const Image *instance, const double radius, const double sigma, const double angle, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = MotionBlurImage(instance, radius, sigma, angle, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT void MagickImage_Negate(Image *instance, const MagickBooleanType onlyGrayscale, const size_t channels, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  NegateImage(instance, onlyGrayscale, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT void MagickImage_Normalize(Image *instance, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  NormalizeImage(instance, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT Image *MagickImage_OilPaint(const Image *instance, const double radius, const double sigma, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = OilPaintImage(instance, radius, sigma, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT void MagickImage_Opaque(Image *instance, const PixelInfo *target, const PixelInfo *fill, const MagickBooleanType invert, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  OpaquePaintImage(instance, target, fill, invert, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT void MagickImage_OrderedDither(Image *instance, const char *thresholdMap, const size_t channels, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  OrderedDitherImage(instance, thresholdMap, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT void MagickImage_Perceptible(Image *instance, const double epsilon, const size_t channels, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  PerceptibleImage(instance, epsilon, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT ChannelPerceptualHash *MagickImage_PerceptualHash(const Image *instance, ExceptionInfo **exception)
{
  ChannelPerceptualHash
    *result;

  MAGICK_NET_GET_EXCEPTION;
  result = GetImagePerceptualHash(instance, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return result;
}

MAGICK_NET_EXPORT void MagickImage_Quantize(Image *instance, const QuantizeInfo *settings, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  QuantizeImage(settings, instance, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT Image *MagickImage_Polaroid(Image *instance, const DrawInfo *settings, const char *caption, const double angle, const size_t method, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = PolaroidImage(instance, settings, caption, angle, (const PixelInterpolateMethod)method, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT void MagickImage_Posterize(Image *instance, const size_t levels, const size_t method, const size_t channels, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  PosterizeImage(instance, levels, (const DitherMethod)method, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT void MagickImage_RaiseOrLower(Image *image, const size_t size, const MagickBooleanType raise, ExceptionInfo **exception)
{
  RectangleInfo
    geometry;

  geometry.width = size;
  geometry.height = size;
  MAGICK_NET_GET_EXCEPTION;
  RaiseImage(image, &geometry, raise, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT void MagickImage_RandomThreshold(Image *instance, const double low, const double high, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  RandomThresholdImage(instance, low, high, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT Image *MagickImage_ReadBlob(const ImageInfo *settings, const unsigned char *data, const size_t length, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = BlobToImage(settings, (const void *)data, length, exceptionInfo);
  RemoveFrames(image);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_ReadFile(const ImageInfo *settings, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = ReadImage(settings, exceptionInfo);
  RemoveFrames(image);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_ReadPixels(const size_t width, const size_t height, const char *map, const size_t storageType, const unsigned char *data, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = ConstituteImage(width, height, map, (const StorageType)storageType, (const void *)data, exceptionInfo);
  RemoveFrames(image);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT void MagickImage_RemoveArtifact(Image *instance, const char *name)
{
  DestroyString(RemoveImageArtifact(instance, name));
}

MAGICK_NET_EXPORT void MagickImage_RemoveAttribute(Image *instance, const char *name)
{
  DestroyString(RemoveImageProperty(instance, name));
}

MAGICK_NET_EXPORT void MagickImage_RemoveProfile(Image *instance, const char *name)
{
  DestroyStringInfo(RemoveImageProfile(instance, name));
}

MAGICK_NET_EXPORT void MagickImage_ResetArtifactIterator(const Image *instance)
{
  ResetImageArtifactIterator(instance);
}

MAGICK_NET_EXPORT void MagickImage_ResetAttributeIterator(const Image *instance)
{
  ResetImagePropertyIterator(instance);
}

MAGICK_NET_EXPORT void MagickImage_ResetProfileIterator(const Image *instance)
{
  ResetImageProfileIterator(instance);
}

MAGICK_NET_EXPORT Image *MagickImage_Resample(const Image *instance, const double resolutionX, const double resolutionY, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = ResampleImage(instance, resolutionX, resolutionY, instance->filter, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_Resize(const Image *instance, const char *geometry, ExceptionInfo **exception)
{
  Image
    *image;

  RectangleInfo
    rectangle;

  SetGeometry(instance, &rectangle);
  ParseMetaGeometry(geometry, &rectangle.x, &rectangle.y, &rectangle.width, &rectangle.height);

  MAGICK_NET_GET_EXCEPTION;
  image = ResizeImage(instance, rectangle.width, rectangle.height, instance->filter, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_Roll(const Image *instance, const ssize_t x, const ssize_t y, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = RollImage(instance, x, y, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_Rotate(const Image *instance, const double degrees, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = RotateImage(instance, degrees, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_RotationalBlur(Image *instance, const double angle, const size_t channels, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  image = RotationalBlurImage(instance, angle, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_Sample(const Image *instance, const RectangleInfo *geometry, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = SampleImage(instance, geometry->width, geometry->height, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;

}

MAGICK_NET_EXPORT Image *MagickImage_Scale(const Image *instance, const char *geometry, ExceptionInfo **exception)
{
  Image
    *image;

  RectangleInfo
    rectangle;

  SetGeometry(instance, &rectangle);
  ParseMetaGeometry(geometry, &rectangle.x, &rectangle.y, &rectangle.width, &rectangle.height);

  MAGICK_NET_GET_EXCEPTION;
  image = ScaleImage(instance, rectangle.width, rectangle.height, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT void MagickImage_Segment(Image *instance, const size_t colorSpace, const double clusterThreshold, const double smoothingThreshold, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  SegmentImage(instance, (const ColorspaceType)colorSpace, MagickFalse, clusterThreshold, smoothingThreshold, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT Image *MagickImage_SelectiveBlur(Image *instance, const double radius, const double sigma, const double threshold, const size_t channels, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  image = SelectiveBlurImage(instance, radius, sigma, threshold, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_Separate(Image *instance, const size_t channels, ExceptionInfo **exception)
{
  Image
    *images;

  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  images = SeparateImages(instance, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
  return images;
}

MAGICK_NET_EXPORT Image *MagickImage_SepiaTone(Image *instance, const double threshold, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = SepiaToneImage(instance, threshold, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT void MagickImage_SetAlpha(Image *instance, const size_t value, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  SetImageAlphaChannel(instance, (const AlphaChannelOption)value, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT void MagickImage_SetArtifact(Image *instance, const char *name, const char *value)
{
  (void)SetImageArtifact(instance, name, value);
}

MAGICK_NET_EXPORT void MagickImage_SetAttribute(Image *instance, const char *name, const char *value, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  SetImageProperty(instance, name, value, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT void MagickImage_SetBitDepth(Image *instance, const size_t channels, const size_t value, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  SetImageDepth(instance, value, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT void MagickImage_SetColormap(Image *instance, const size_t index, const PixelInfo *color, ExceptionInfo **exception)
{
  if (instance->colormap == (PixelInfo *)NULL || color == (const PixelInfo *)NULL)
    return;

  if (index >= MaxColormapSize)
    return;

  if (instance->colors < (index + 1))
    MagickImage_ColormapSize_Set(instance, index + 1, exception);

  instance->colormap[index] = *color;
}

MAGICK_NET_EXPORT MagickBooleanType MagickImage_SetColorMetric(Image *instance, const Image *reference, ExceptionInfo **exception)
{
  MagickBooleanType
    result;

  MAGICK_NET_GET_EXCEPTION;
  result = SetImageColorMetric(instance, reference, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return result;
}

MAGICK_NET_EXPORT void MagickImage_SetNext(Image *image, Image *next)
{
  if (next == (Image *)NULL)
  {
    if (image->next != (Image *)NULL)
      image->next->previous = (Image *)NULL;
    image->next = (Image *)NULL;
  }
  else
  {
    image->next = next;
    next->previous = image;
  }
}

MAGICK_NET_EXPORT void MagickImage_SetProgressDelegate(Image *instance, const MagickProgressMonitor method)
{
  instance->progress_monitor = method;
}

MAGICK_NET_EXPORT Image *MagickImage_Shade(Image *instance, const double azimuth, const double elevation, const MagickBooleanType colorShading, const size_t channels, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  image = ShadeImage(instance, colorShading, azimuth, elevation, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_Shadow(Image *instance, const ssize_t x, const ssize_t y, const double sigma, const double alphaPercentage, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = ShadowImage(instance, alphaPercentage, sigma, x, y, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_Sharpen(Image *instance, const double radius, const double sigma, const size_t channels, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  image = SharpenImage(instance, radius, sigma, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_Shave(const Image *instance, const size_t leftRight, const size_t topBottom, ExceptionInfo **exception)
{
  RectangleInfo
    geometry;

  Image
    *image;

  geometry.width = leftRight;
  geometry.height = topBottom;
  MAGICK_NET_GET_EXCEPTION;
  image = ShaveImage(instance, &geometry, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_Shear(const Image *instance, const double xAngle, const double yAngle, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = ShearImage(instance, xAngle, yAngle, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT void MagickImage_SigmoidalContrast(Image *instance, const MagickBooleanType sharpen, const double contrast, const double midpoint, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  SigmoidalContrastImage(instance, sharpen, contrast, midpoint, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT Image *MagickImage_SparseColor(Image *instance, const size_t channels, const size_t method, const double *arguments, const size_t length, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  image = SparseColorImage(instance, (const SparseColorMethod)method, length, arguments, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_Spread(const Image *instance, const size_t method, const double radius, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = SpreadImage(instance, (const PixelInterpolateMethod)method, radius, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_Sketch(Image *instance, const double radius, const double sigma, const double angle, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = SketchImage(instance, radius, sigma, angle, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT void MagickImage_Solarize(Image *instance, const double factor, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  SolarizeImage(instance, factor, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT Image *MagickImage_Splice(const Image *instance, const RectangleInfo *geometry, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = SpliceImage(instance, geometry, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_Statistic(const Image *instance, const size_t type, const size_t width, const size_t height, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = StatisticImage(instance, (const StatisticType)type, width, height, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT ChannelStatistics *MagickImage_Statistics(Image *instance, ExceptionInfo **exception)
{
  ChannelStatistics
    *result;

  MAGICK_NET_GET_EXCEPTION;
  result = GetImageStatistics(instance, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return result;
}

MAGICK_NET_EXPORT Image *MagickImage_Stegano(const Image *instance, const Image *watermark, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = SteganoImage(instance, watermark, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_Stereo(const Image *instance, const Image *rightImage, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = StereoImage(instance, rightImage, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT void MagickImage_Strip(Image *instance, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  StripImage(instance, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT Image *MagickImage_SubImageSearch(Image *instance, const Image *reference, const size_t metric, const double similarityThreshold, RectangleInfo *offset, double *similarityMetric, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = SimilarityImage(instance, reference, (const MetricType)metric, similarityThreshold, offset, similarityMetric, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_Swirl(const Image *instance, const size_t method, const double degrees, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = SwirlImage(instance, degrees, (const PixelInterpolateMethod)method, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT void MagickImage_Texture(Image *instance, const Image *image, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  TextureImage(instance, image, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT void MagickImage_Threshold(Image *instance, const double threshold, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  BilevelImage(instance, threshold, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT Image *MagickImage_Thumbnail(const Image *instance, const char *geometry, ExceptionInfo **exception)
{
  Image
    *image;

  RectangleInfo
    rectangle;

  SetGeometry(instance, &rectangle);
  ParseMetaGeometry(geometry, &rectangle.x, &rectangle.y, &rectangle.width, &rectangle.height);

  MAGICK_NET_GET_EXCEPTION;
  image = ThumbnailImage(instance, rectangle.width, rectangle.height, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_Tint(const Image *instance, const char *opacity, const PixelInfo *tint, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = TintImage(instance, opacity, tint, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT void MagickImage_Transparent(Image *instance, const PixelInfo *color, const MagickBooleanType invert, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  TransparentPaintImage(instance, color, TransparentAlpha, invert, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT void MagickImage_TransparentChroma(Image *instance, const PixelInfo *colorLow, const PixelInfo *colorHigh, const MagickBooleanType invert, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  TransparentPaintImageChroma(instance, colorLow, colorHigh, TransparentAlpha, invert, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT Image *MagickImage_Transpose(Image *instance, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = TransposeImage(instance, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_Transverse(Image *instance, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = TransverseImage(instance, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_Trim(Image *instance, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = TrimImage(instance, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_UniqueColors(const Image *instance, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = UniqueImageColors(instance, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_UnsharpMask(Image *instance, const double radius, const double sigma, const double amount, const double threshold, const size_t channels, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  image = UnsharpMaskImage(instance, radius, sigma, amount, threshold, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_Vignette(const Image *instance, const double radius, const double sigma, const ssize_t x, const ssize_t y, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = VignetteImage(instance, radius, sigma, x, y, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_Wave(const Image *instance, const size_t method, const double amplitude, const double length, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = WaveImage(instance, amplitude, length, (const PixelInterpolateMethod)method, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT Image *MagickImage_WaveletDenoise(const Image *instance, const double threshold, const double softness, ExceptionInfo **exception)
{
  Image
    *image;

  MAGICK_NET_GET_EXCEPTION;
  image = WaveletDenoiseImage(instance, threshold, softness, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return image;
}

MAGICK_NET_EXPORT void MagickImage_WhiteThreshold(Image *instance, const char *threshold, const size_t channels, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  SetChannelMask(instance, channels);
  WhiteThresholdImage(instance, threshold, exceptionInfo);
  RestoreChannelMask(instance);
  MAGICK_NET_SET_EXCEPTION;
}

MAGICK_NET_EXPORT unsigned char *MagickImage_WriteBlob(Image *instance, const ImageInfo *settings, size_t *length, ExceptionInfo **exception)
{
  unsigned char
    *data;

  MAGICK_NET_GET_EXCEPTION;
  data = (unsigned char *)ImageToBlob(settings, instance, length, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
  return data;
}

MAGICK_NET_EXPORT void MagickImage_WriteFile(Image *instance, const ImageInfo *settings, ExceptionInfo **exception)
{
  MAGICK_NET_GET_EXCEPTION;
  WriteImage(settings, instance, exceptionInfo);
  MAGICK_NET_SET_EXCEPTION;
}