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

MAGICK_NET_EXPORT Image *MagickImageCollection_Append(const Image *, const MagickBooleanType, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImageCollection_Coalesce(const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImageCollection_Combine(Image *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImageCollection_Dispose(Image *);

MAGICK_NET_EXPORT Image *MagickImageCollection_Deconstruct(const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImageCollection_Evaluate(const Image *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImageCollection_Flatten(Image *, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImageCollection_Map(Image *, const QuantizeInfo *, const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImageCollection_Merge(Image *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImageCollection_Montage(const Image *, const MontageInfo *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImageCollection_Morph(const Image *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImageCollection_Optimize(const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImageCollection_OptimizePlus(const Image *, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImageCollection_OptimizeTransparency(Image *, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImageCollection_Quantize(Image *, const QuantizeInfo *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImageCollection_ReadBlob(const ImageInfo *, const unsigned char *, const size_t, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImageCollection_ReadFile(ImageInfo *, ExceptionInfo **);

MAGICK_NET_EXPORT Image *MagickImageCollection_Smush(const Image *, const ssize_t, const MagickBooleanType, ExceptionInfo **);

MAGICK_NET_EXPORT unsigned char *MagickImageCollection_WriteBlob(Image *, const ImageInfo *, size_t *, ExceptionInfo **);

MAGICK_NET_EXPORT void MagickImageCollection_WriteFile(Image *, const ImageInfo *, ExceptionInfo **);