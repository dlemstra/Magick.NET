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

#if !NETSTANDARD

using System;
using System.Drawing.Imaging;

namespace ImageMagick
{
    /// <summary>
    /// Class that contains information about an image format.
    /// </summary>
    public sealed partial class MagickFormatInfo
    {
        internal static MagickFormat GetFormat(ImageFormat format)
        {
            if (format == ImageFormat.Bmp || format == ImageFormat.MemoryBmp)
                return MagickFormat.Bmp;
            else if (format == ImageFormat.Gif)
                return MagickFormat.Gif;
            else if (format == ImageFormat.Icon)
                return MagickFormat.Icon;
            else if (format == ImageFormat.Jpeg)
                return MagickFormat.Jpeg;
            else if (format == ImageFormat.Png)
                return MagickFormat.Png;
            else if (format == ImageFormat.Tiff)
                return MagickFormat.Tiff;
            else
                throw new NotSupportedException("Unsupported image format: " + format.ToString());
        }
    }
}

#endif