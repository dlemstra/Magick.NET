//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
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
#pragma once

MAGICK_NET_EXPORT Image *MagickImage_Create(const ImageInfo *, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_Dispose(Image *);

MAGICK_NET_EXPORT size_t MagickImage_AnimationDelay_Get(const Image *);
MAGICK_NET_EXPORT void MagickImage_AnimationDelay_Set(Image *, const size_t);

MAGICK_NET_EXPORT size_t MagickImage_AnimationIterations_Get(const Image *);
MAGICK_NET_EXPORT void MagickImage_AnimationIterations_Set(Image *, const size_t);

MAGICK_NET_EXPORT PixelInfo *MagickImage_BackgroundColor_Get(const Image *);
MAGICK_NET_EXPORT void MagickImage_BackgroundColor_Set(Image *, const PixelInfo *);

MAGICK_NET_EXPORT size_t MagickImage_BaseHeight_Get(const Image *);

MAGICK_NET_EXPORT size_t MagickImage_BaseWidth_Get(const Image *);

MAGICK_NET_EXPORT MagickBooleanType MagickImage_BlackPointCompensation_Get(const Image *);
MAGICK_NET_EXPORT void MagickImage_BlackPointCompensation_Set(Image *, MagickBooleanType);

MAGICK_NET_EXPORT PixelInfo *MagickImage_BorderColor_Get(const Image *);
MAGICK_NET_EXPORT void MagickImage_BorderColor_Set(Image *, const PixelInfo *);

MAGICK_NET_EXPORT RectangleInfo *MagickImage_BoundingBox_Get(const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT size_t MagickImage_ChannelCount_Get(const Image *);

MAGICK_NET_EXPORT PrimaryInfo *MagickImage_ChromaBluePrimary_Get(const Image *);
MAGICK_NET_EXPORT void MagickImage_ChromaBluePrimary_Set(Image *, const PrimaryInfo *);

MAGICK_NET_EXPORT PrimaryInfo *MagickImage_ChromaGreenPrimary_Get(const Image *);
MAGICK_NET_EXPORT void MagickImage_ChromaGreenPrimary_Set(Image *, const PrimaryInfo *);

MAGICK_NET_EXPORT PrimaryInfo *MagickImage_ChromaRedPrimary_Get(const Image *);
MAGICK_NET_EXPORT void MagickImage_ChromaRedPrimary_Set(Image *, const PrimaryInfo *);

MAGICK_NET_EXPORT PrimaryInfo *MagickImage_ChromaWhitePoint_Get(const Image *);
MAGICK_NET_EXPORT void MagickImage_ChromaWhitePoint_Set(Image *, const PrimaryInfo *);

MAGICK_NET_EXPORT size_t MagickImage_ClassType_Get(const Image *, ExceptionInfo **);
MAGICK_NET_EXPORT void MagickImage_ClassType_Set(Image *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT double MagickImage_ColorFuzz_Get(const Image *);
MAGICK_NET_EXPORT void MagickImage_ColorFuzz_Set(Image *, const double);

MAGICK_NET_EXPORT ssize_t MagickImage_ColormapSize_Get(const Image *, ExceptionInfo **);
MAGICK_NET_EXPORT void MagickImage_ColormapSize_Set(Image *, const ssize_t, ExceptionInfo **);

MAGICK_NET_EXPORT size_t MagickImage_ColorSpace_Get(const Image *, ExceptionInfo **);
MAGICK_NET_EXPORT void MagickImage_ColorSpace_Set(Image *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT size_t MagickImage_ColorType_Get(const Image *, ExceptionInfo **);
MAGICK_NET_EXPORT void MagickImage_ColorType_Set(Image *, const size_t value, ExceptionInfo **);

MAGICK_NET_EXPORT size_t MagickImage_Compose_Get(const Image *);
MAGICK_NET_EXPORT void MagickImage_Compose_Set(Image *, const size_t);

MAGICK_NET_EXPORT size_t MagickImage_CompressionMethod_Get(const Image *);
MAGICK_NET_EXPORT void MagickImage_CompressionMethod_Set(Image *, const size_t);

MAGICK_NET_EXPORT size_t MagickImage_Depth_Get(const Image *);
MAGICK_NET_EXPORT void MagickImage_Depth_Set(Image *, const size_t);

MAGICK_NET_EXPORT const char *MagickImage_EncodingGeometry_Get(const Image *);

MAGICK_NET_EXPORT size_t MagickImage_Endian_Get(const Image *);
MAGICK_NET_EXPORT void MagickImage_Endian_Set(Image *, const size_t);

MAGICK_NET_EXPORT const char *MagickImage_FileName_Get(const Image *);
MAGICK_NET_EXPORT void MagickImage_FileName_Set(Image *, const char *);

MAGICK_NET_EXPORT long MagickImage_FileSize_Get(const Image *);

MAGICK_NET_EXPORT size_t MagickImage_FilterType_Get(const Image *);
MAGICK_NET_EXPORT void MagickImage_FilterType_Set(Image *, const size_t);

MAGICK_NET_EXPORT const char *MagickImage_Format_Get(const Image *);
MAGICK_NET_EXPORT void MagickImage_Format_Set(Image *, const char *);

MAGICK_NET_EXPORT double MagickImage_Gamma_Get(const Image *);

MAGICK_NET_EXPORT size_t MagickImage_GifDisposeMethod_Get(const Image *);
MAGICK_NET_EXPORT void MagickImage_GifDisposeMethod_Set(Image *, const size_t);

MAGICK_NET_EXPORT size_t MagickImage_Interpolate_Get(const Image *);
MAGICK_NET_EXPORT void MagickImage_Interpolate_Set(Image *, const size_t);

MAGICK_NET_EXPORT MagickBooleanType MagickImage_HasAlpha_Get(const Image *, ExceptionInfo **);
MAGICK_NET_EXPORT void MagickImage_HasAlpha_Set(Image *, const MagickBooleanType, ExceptionInfo **);

MAGICK_NET_EXPORT size_t MagickImage_Height_Get(const Image *);

MAGICK_NET_EXPORT size_t MagickImage_Interlace_Get(const Image *);
MAGICK_NET_EXPORT void MagickImage_Interlace_Set(Image *, const size_t);

MAGICK_NET_EXPORT MagickBooleanType MagickImage_IsOpaque_Get(const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT PixelInfo *MagickImage_MatteColor_Get(const Image *);
MAGICK_NET_EXPORT void MagickImage_MatteColor_Set(Image *, const PixelInfo *);

MAGICK_NET_EXPORT double MagickImage_MeanErrorPerPixel_Get(const Image *);

MAGICK_NET_EXPORT double MagickImage_NormalizedMaximumError_Get(const Image *);

MAGICK_NET_EXPORT double MagickImage_NormalizedMeanError_Get(const Image *);

MAGICK_NET_EXPORT size_t MagickImage_Orientation_Get(const Image *);
MAGICK_NET_EXPORT void MagickImage_Orientation_Set(Image *, const size_t);

MAGICK_NET_EXPORT size_t MagickImage_RenderingIntent_Get(const Image *);
MAGICK_NET_EXPORT void MagickImage_RenderingIntent_Set(Image *, const size_t);

MAGICK_NET_EXPORT RectangleInfo *MagickImage_Page_Get(const Image *);
MAGICK_NET_EXPORT void MagickImage_Page_Set(Image *, const RectangleInfo *);

MAGICK_NET_EXPORT size_t MagickImage_Quality_Get(const Image *);
MAGICK_NET_EXPORT void MagickImage_Quality_Set(Image *, const size_t);

MAGICK_NET_EXPORT Image *MagickImage_ReadMask_Get(const Image *, ExceptionInfo **);
MAGICK_NET_EXPORT void MagickImage_ReadMask_Set(Image *, const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT size_t MagickImage_ResolutionUnits_Get(const Image *);
MAGICK_NET_EXPORT void MagickImage_ResolutionUnits_Set(Image *, const  size_t);

MAGICK_NET_EXPORT double MagickImage_ResolutionX_Get(const Image *);
MAGICK_NET_EXPORT void MagickImage_ResolutionX_Set(Image *, const  double);

MAGICK_NET_EXPORT double MagickImage_ResolutionY_Get(const Image *);
MAGICK_NET_EXPORT void MagickImage_ResolutionY_Set(Image *, const double);

MAGICK_NET_EXPORT const char *MagickImage_Signature_Get(Image *, ExceptionInfo **);

MAGICK_NET_EXPORT size_t MagickImage_TotalColors_Get(const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT size_t MagickImage_VirtualPixelMethod_Get(const Image *, ExceptionInfo **);
MAGICK_NET_EXPORT void MagickImage_VirtualPixelMethod_Set(Image *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT size_t MagickImage_Width_Get(const Image *);

MAGICK_NET_EXPORT Image *MagickImage_WriteMask_Get(const Image *, ExceptionInfo **);
MAGICK_NET_EXPORT void MagickImage_WriteMask_Set(Image *, const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_AdaptiveBlur(const Image *, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_AdaptiveResize(const Image *, const size_t, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_AdaptiveSharpen(Image *, const double, const double, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_AdaptiveThreshold(const Image *, const size_t, const size_t, const double, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_AddNoise(Image *, const size_t, const double, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_AddProfile(Image *, const char *, const unsigned char *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_AffineTransform(Image *, const double, const double, const double, const double, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_Annotate(Image *, const DrawInfo *, char *, char *, const size_t, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_AnnotateGravity(Image *, const DrawInfo *, char *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_AutoGamma(Image *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_AutoLevel(Image *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_AutoOrient(const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_BlackThreshold(Image *, const char *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_BlueShift(const Image *, const double, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Blur(Image *, const double, const double, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Border(const Image *, const RectangleInfo *, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_BrightnessContrast(Image *, const double, const double, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_CannyEdge(const Image *, const double, const double, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT size_t MagickImage_ChannelOffset(const Image *, const size_t);

MAGICK_NET_EXPORT Image *MagickImage_Charcoal(const Image *, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Chop(const Image *, const RectangleInfo *, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_Clamp(Image *, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_ClampChannel(Image *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_Clip(Image *, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_ClipPath(Image *, const char *, const MagickBooleanType, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Clone(const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_CloneArea(const Image *, const size_t, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_Clut(Image *, Image *, const size_t, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_ColorDecisionList(Image *, const char *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Colorize(const Image *, const PixelInfo *, const char *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_ColorMatrix(const Image *, const KernelInfo *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Compare(Image *, Image *, const size_t, const size_t, double *, ExceptionInfo **);

MAGICK_NET_EXPORT double MagickImage_CompareDistortion(Image *, Image *, const size_t, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_Composite(Image *, const Image *, const ssize_t, const ssize_t, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_CompositeGraviy(Image *, const Image *, const size_t, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_ConnectedComponents(const Image *, const size_t, CCObjectInfo **, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_Contrast(Image *, const MagickBooleanType, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_ContrastStretch(Image *, const double, const double, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Convolve(const Image *, const KernelInfo *, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_CopyPixels(Image *, const Image *, const RectangleInfo *, const OffsetInfo *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Crop(const Image *, const RectangleInfo *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_CropToTiles(const Image *, const char *, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_CycleColormap(Image *, const ssize_t, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_Decipher(Image *, const char *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Deskew(const Image *, const double, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Despeckle(const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT const size_t MagickImage_DetermineColorType(const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Distort(const Image *, const size_t, const MagickBooleanType, const double *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Edge(const Image *, const double, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Emboss(const Image *, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_Encipher(Image *, const char *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Enhance(const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_Equalize(Image *, ExceptionInfo **);

MAGICK_NET_EXPORT MagickBooleanType MagickImage_Equals(const Image *, const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_EvaluateFunction(Image *, const size_t, const size_t, const double *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_EvaluateGeometry(Image *, const size_t, const RectangleInfo *, const size_t, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_EvaluateOperator(Image *, const size_t, const size_t, const double, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Extent(const Image *, const char *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_ExtentGravity(const Image *, const char *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT MagickBooleanType MagickImage_HasChannel(const Image *, const size_t);

MAGICK_NET_EXPORT Image *MagickImage_Flip(const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_FloodFillAlpha(Image *, const DrawInfo *, const ssize_t, const ssize_t, const PixelInfo *, const MagickBooleanType, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_FloodFillColor(Image *, const DrawInfo *, const PixelInfo *, const ssize_t, const ssize_t, const PixelInfo *, const MagickBooleanType, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_FloodFillImage(Image *, const DrawInfo *, const Image *, const ssize_t, const ssize_t, const PixelInfo *, const MagickBooleanType, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Flop(const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT TypeMetric *MagickImage_FontTypeMetrics(Image *, const DrawInfo *, const MagickBooleanType, ExceptionInfo **);

MAGICK_NET_EXPORT char *MagickImage_FormatExpression(Image *, ImageInfo *, const char *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Frame(const Image *, const RectangleInfo *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Fx(Image *, const char *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_GammaCorrect(Image *, const double, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_GaussianBlur(Image *, const double, const double, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT const char *MagickImage_GetArtifact(const Image *, const char *);

MAGICK_NET_EXPORT const char *MagickImage_GetAttribute(const Image *, const char *, ExceptionInfo **);

MAGICK_NET_EXPORT size_t MagickImage_GetBitDepth(Image *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT PixelInfo *MagickImage_GetColormap(const Image *, const size_t);

MAGICK_NET_EXPORT Image *MagickImage_GetNext(const Image *);

MAGICK_NET_EXPORT const char *MagickImage_GetNextArtifactName(const Image *);

MAGICK_NET_EXPORT const char *MagickImage_GetNextAttributeName(const Image *);

MAGICK_NET_EXPORT const char *MagickImage_GetNextProfileName(const Image *);

MAGICK_NET_EXPORT const StringInfo *MagickImage_GetProfile(const Image *, const char *);

MAGICK_NET_EXPORT void MagickImage_Grayscale(Image *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_HaldClut(Image *, const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT MagickBooleanType MagickImage_HasProfile(const Image *, const char *);

MAGICK_NET_EXPORT PixelInfo *MagickImage_Histogram(const Image *, size_t *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_HoughLine(const Image *, const size_t, const size_t, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Implode(const Image *, const double, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Kuwahara(const Image *, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_Level(Image *, const double, const double, const double, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_LevelColors(Image *, const PixelInfo *, const PixelInfo *, const size_t, const MagickBooleanType, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_Levelize(Image *, const double, const double, const double, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_LinearStretch(Image *, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_LiquidRescale(const Image *, const char *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_LocalContrast(const Image *, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Magnify(const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT const MagickBooleanType MagickImage_Map(Image *, const Image *, const QuantizeInfo *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Minify(const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_Modulate(Image *, const char *, ExceptionInfo **);

MAGICK_NET_EXPORT ChannelMoments *MagickImage_Moments(const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Morphology(Image *, const size_t, const char *, const size_t, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_MotionBlur(const Image *, const double, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_Negate(Image *, const MagickBooleanType, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_Normalize(Image *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_OilPaint(const Image *, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_Opaque(Image *, const PixelInfo *, const PixelInfo *, const MagickBooleanType, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_OrderedDither(Image *, const char *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_Perceptible(Image *, const double, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT ChannelPerceptualHash *MagickImage_PerceptualHash(const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_Quantize(Image *, const QuantizeInfo *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Polaroid(Image *, const DrawInfo *, const char *, const double, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_Posterize(Image *, const size_t, const size_t, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_RaiseOrLower(Image *, const size_t, const MagickBooleanType, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_RandomThreshold(Image *, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_ReadBlob(const ImageInfo *, const unsigned char *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_ReadFile(const ImageInfo *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_ReadPixels(const size_t, const size_t, const char *, const size_t, const unsigned char *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_ReadStream(ImageInfo *, const CustomStreamHandler, const CustomStreamSeeker, const CustomStreamTeller, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_RegionMask(Image *, const RectangleInfo *, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_RemoveArtifact(Image *, const char *);

MAGICK_NET_EXPORT void MagickImage_RemoveAttribute(Image *, const char *);

MAGICK_NET_EXPORT void MagickImage_RemoveProfile(Image *, const char *);

MAGICK_NET_EXPORT void MagickImage_ResetArtifactIterator(const Image *);

MAGICK_NET_EXPORT void MagickImage_ResetAttributeIterator(const Image *);

MAGICK_NET_EXPORT void MagickImage_ResetProfileIterator(const Image *);

MAGICK_NET_EXPORT Image *MagickImage_Resample(const Image *, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Resize(const Image *, const char *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Roll(const Image *, const ssize_t, const ssize_t, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Rotate(const Image *, const double, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_RotationalBlur(Image *, const double, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Sample(const Image *, const char *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Scale(const Image *, const char *, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_Segment(Image *, const size_t, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_SelectiveBlur(Image *, const double, const double, const double, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Separate(Image *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_SepiaTone(Image *, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_SetAlpha(Image *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_SetArtifact(Image *, const char *, const char *);

MAGICK_NET_EXPORT void MagickImage_SetAttribute(Image *, const char *, const char *, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_SetBitDepth(Image *, const size_t, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_SetColormap(Image *, const size_t, const PixelInfo *, ExceptionInfo **);

MAGICK_NET_EXPORT MagickBooleanType MagickImage_SetColorMetric(Image *, const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_SetNext(Image *, Image *);

MAGICK_NET_EXPORT void MagickImage_SetProgressDelegate(Image *, const MagickProgressMonitor);

MAGICK_NET_EXPORT Image *MagickImage_Shade(Image *, const double, const double, const MagickBooleanType, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Shadow(Image *, const ssize_t, const ssize_t, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Sharpen(Image *, const double, const double, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Shave(const Image *, const size_t, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Shear(const Image *, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_SigmoidalContrast(Image *, const MagickBooleanType, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_SparseColor(Image *, const size_t, const size_t, const double *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Spread(const Image *, const size_t, const double, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Sketch(Image *, const double, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_Solarize(Image *, const double, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Splice(const Image *, const RectangleInfo *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Statistic(const Image *, const size_t, const size_t, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT ChannelStatistics *MagickImage_Statistics(Image *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Stegano(const Image *, const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Stereo(const Image *, const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_Strip(Image *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_SubImageSearch(Image *, const Image *, const size_t, const double, RectangleInfo *, double *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Swirl(const Image *, const size_t, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_Texture(Image *, const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_Threshold(Image *, const double, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Thumbnail(const Image *, const char *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Tint(const Image *, const char *, const PixelInfo *, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_Transparent(Image *, const PixelInfo *, const MagickBooleanType, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_TransparentChrome(Image *, const PixelInfo *, const PixelInfo *, const MagickBooleanType, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Transpose(Image *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Transverse(Image *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Trim(Image *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_UniqueColors(const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_UnsharpMask(Image *, const double, const double, const double, const double, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Vignette(const Image *, const double, const double, const ssize_t, const ssize_t, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_Wave(const Image *, const size_t, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImage_WaveletDenoise(const Image *, const double, const double, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_WhiteThreshold(Image *, const char *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_WriteFile(Image *, const ImageInfo *, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImage_WriteStream(Image *, ImageInfo *, const CustomStreamHandler, const CustomStreamHandler, const CustomStreamSeeker, const CustomStreamTeller, ExceptionInfo **);