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

namespace ImageMagick
{
    /// <summary>
    /// Contains the possible exif values.
    /// </summary>
    public static partial class ExifValues
    {
        /// <summary>
        /// Gets a new <see cref="ExifRationalArray"/> instance for the <see cref="ExifTag.WhitePoint"/> tag.
        /// </summary>
        public static ExifRationalArray WhitePoint => new ExifRationalArray(ExifTag.WhitePoint);

        /// <summary>
        /// Gets a new <see cref="ExifRationalArray"/> instance for the <see cref="ExifTag.PrimaryChromaticities"/> tag.
        /// </summary>
        public static ExifRationalArray PrimaryChromaticities => new ExifRationalArray(ExifTag.PrimaryChromaticities);

        /// <summary>
        /// Gets a new <see cref="ExifRationalArray"/> instance for the <see cref="ExifTag.YCbCrCoefficients"/> tag.
        /// </summary>
        public static ExifRationalArray YCbCrCoefficients => new ExifRationalArray(ExifTag.YCbCrCoefficients);

        /// <summary>
        /// Gets a new <see cref="ExifRationalArray"/> instance for the <see cref="ExifTag.ReferenceBlackWhite"/> tag.
        /// </summary>
        public static ExifRationalArray ReferenceBlackWhite => new ExifRationalArray(ExifTag.ReferenceBlackWhite);

        /// <summary>
        /// Gets a new <see cref="ExifRationalArray"/> instance for the <see cref="ExifTag.PixelScale"/> tag.
        /// </summary>
        public static ExifRationalArray PixelScale => new ExifRationalArray(ExifTag.PixelScale);

        /// <summary>
        /// Gets a new <see cref="ExifRationalArray"/> instance for the <see cref="ExifTag.IntergraphMatrix"/> tag.
        /// </summary>
        public static ExifRationalArray IntergraphMatrix => new ExifRationalArray(ExifTag.IntergraphMatrix);

        /// <summary>
        /// Gets a new <see cref="ExifRationalArray"/> instance for the <see cref="ExifTag.ModelTiePoint"/> tag.
        /// </summary>
        public static ExifRationalArray ModelTiePoint => new ExifRationalArray(ExifTag.ModelTiePoint);

        /// <summary>
        /// Gets a new <see cref="ExifRationalArray"/> instance for the <see cref="ExifTag.ModelTransform"/> tag.
        /// </summary>
        public static ExifRationalArray ModelTransform => new ExifRationalArray(ExifTag.ModelTransform);

        /// <summary>
        /// Gets a new <see cref="ExifRationalArray"/> instance for the <see cref="ExifTag.GPSLatitude"/> tag.
        /// </summary>
        public static ExifRationalArray GPSLatitude => new ExifRationalArray(ExifTag.GPSLatitude);

        /// <summary>
        /// Gets a new <see cref="ExifRationalArray"/> instance for the <see cref="ExifTag.GPSLongitude"/> tag.
        /// </summary>
        public static ExifRationalArray GPSLongitude => new ExifRationalArray(ExifTag.GPSLongitude);

        /// <summary>
        /// Gets a new <see cref="ExifRationalArray"/> instance for the <see cref="ExifTag.GPSTimestamp"/> tag.
        /// </summary>
        public static ExifRationalArray GPSTimestamp => new ExifRationalArray(ExifTag.GPSTimestamp);

        /// <summary>
        /// Gets a new <see cref="ExifRationalArray"/> instance for the <see cref="ExifTag.GPSDestLatitude"/> tag.
        /// </summary>
        public static ExifRationalArray GPSDestLatitude => new ExifRationalArray(ExifTag.GPSDestLatitude);

        /// <summary>
        /// Gets a new <see cref="ExifRationalArray"/> instance for the <see cref="ExifTag.GPSDestLongitude"/> tag.
        /// </summary>
        public static ExifRationalArray GPSDestLongitude => new ExifRationalArray(ExifTag.GPSDestLongitude);
    }
}