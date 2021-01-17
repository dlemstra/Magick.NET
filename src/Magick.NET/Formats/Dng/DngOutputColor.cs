// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

namespace ImageMagick.Formats
{
    /// <summary>
    /// Defines the dng output colors.
    /// </summary>
    public enum DngOutputColor
    {
        /// <summary>
        /// Raw color (unique to each camera).
        /// </summary>
        Raw = 0,

        /// <summary>
        /// sRGB D65 (default).
        /// </summary>
        SRGB = 1,

        /// <summary>
        /// Adobe RGB (1998) D65.
        /// </summary>
        AdobeRGB = 2,

        /// <summary>
        /// Wide Gamut RGB D65.
        /// </summary>
        WideGamutRGB = 3,

        /// <summary>
        /// Kodak ProPhoto RGB D65.
        /// </summary>
        KodakProPhotoRGB = 4,

        /// <summary>
        /// XYZ.
        /// </summary>
        XYZ,
    }
}
