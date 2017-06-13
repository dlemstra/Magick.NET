//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

#if !NETSTANDARD1_3

using System.Drawing;
using System.Drawing.Imaging;

namespace ImageMagick
{
    /// <summary>
    /// Represents the collection of images.
    /// </summary>
    public partial interface IMagickImageCollection
    {
        /// <summary>
        /// Converts this instance to a <see cref="Bitmap"/> using <see cref="ImageFormat.Tiff"/>.
        /// </summary>
        /// <returns>A <see cref="Bitmap"/> that has the format <see cref="ImageFormat.Tiff"/>.</returns>
        Bitmap ToBitmap();

        /// <summary>
        /// Converts this instance to a <see cref="Bitmap"/> using the specified <see cref="ImageFormat"/>.
        /// Supported formats are: Gif, Icon, Tiff.
        /// </summary>
        /// <param name="imageFormat">The image format.</param>
        /// <returns>A <see cref="Bitmap"/> that has the specified <see cref="ImageFormat"/></returns>
        Bitmap ToBitmap(ImageFormat imageFormat);
    }
}

#endif