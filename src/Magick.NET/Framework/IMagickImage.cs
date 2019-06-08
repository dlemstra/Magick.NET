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

using System.Drawing;
using System.Drawing.Imaging;

#if !NET20
using System.Windows.Media.Imaging;
#endif

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick
{
    /// <content>
    /// Contains code that is not compatible with .NET Core.
    /// </content>
    public partial interface IMagickImage
    {
        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="bitmap">The bitmap to read the image from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Read(Bitmap bitmap);

        /// <summary>
        /// Converts this instance to a <see cref="Bitmap"/> using <see cref="ImageFormat.Bmp"/>.
        /// </summary>
        /// <returns>A <see cref="Bitmap"/> that has the format <see cref="ImageFormat.Bmp"/>.</returns>
        Bitmap ToBitmap();

        /// <summary>
        /// Converts this instance to a <see cref="Bitmap"/> using <see cref="ImageFormat.Bmp"/>.
        /// </summary>
        /// <param name="bitmapDensity">The bitmap density.</param>
        /// <returns>A <see cref="Bitmap"/> that has the format <see cref="ImageFormat.Bmp"/>.</returns>
        Bitmap ToBitmap(BitmapDensity bitmapDensity);

        /// <summary>
        /// Converts this instance to a <see cref="Bitmap"/> using the specified <see cref="ImageFormat"/>.
        /// Supported formats are: Bmp, Gif, Icon, Jpeg, Png, Tiff.
        /// </summary>
        /// <param name="imageFormat">The image format.</param>
        /// <returns>A <see cref="Bitmap"/> that has the specified <see cref="ImageFormat"/>.</returns>
        Bitmap ToBitmap(ImageFormat imageFormat);

        /// <summary>
        /// Converts this instance to a <see cref="Bitmap"/> using the specified <see cref="ImageFormat"/>.
        /// Supported formats are: Bmp, Gif, Icon, Jpeg, Png, Tiff.
        /// </summary>
        /// <param name="imageFormat">The image format.</param>
        /// <param name="bitmapDensity">The bitmap density.</param>
        /// <returns>A <see cref="Bitmap"/> that has the specified <see cref="ImageFormat"/>.</returns>
        Bitmap ToBitmap(ImageFormat imageFormat, BitmapDensity bitmapDensity);

#if !NET20
        /// <summary>
        /// Converts this instance to a <see cref="BitmapSource"/>.
        /// </summary>
        /// <returns>A <see cref="BitmapSource"/>.</returns>
        BitmapSource ToBitmapSource();

        /// <summary>
        /// Converts this instance to a <see cref="BitmapSource"/>.
        /// </summary>
        /// <param name="bitmapDensity">The bitmap density.</param>
        /// <returns>A <see cref="BitmapSource"/>.</returns>
        BitmapSource ToBitmapSource(BitmapDensity bitmapDensity);
#endif
    }
}

#endif