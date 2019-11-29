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
        /// Gets a new <see cref="ExifRationalArray"/> instance for the <see cref="ExifTagValue.WhitePoint"/> tag.
        /// </summary>
        public static ExifRationalArray WhitePoint => new ExifRationalArray(ExifTagValue.WhitePoint);

        /// <summary>
        /// Gets a new <see cref="ExifRationalArray"/> instance for the <see cref="ExifTagValue.PrimaryChromaticities"/> tag.
        /// </summary>
        public static ExifRationalArray PrimaryChromaticities => new ExifRationalArray(ExifTagValue.PrimaryChromaticities);

        /// <summary>
        /// Gets a new <see cref="ExifRationalArray"/> instance for the <see cref="ExifTagValue.YCbCrCoefficients"/> tag.
        /// </summary>
        public static ExifRationalArray YCbCrCoefficients => new ExifRationalArray(ExifTagValue.YCbCrCoefficients);

        /// <summary>
        /// Gets a new <see cref="ExifRationalArray"/> instance for the <see cref="ExifTagValue.ReferenceBlackWhite"/> tag.
        /// </summary>
        public static ExifRationalArray ReferenceBlackWhite => new ExifRationalArray(ExifTagValue.ReferenceBlackWhite);

        /// <summary>
        /// Gets a new <see cref="ExifRationalArray"/> instance for the <see cref="ExifTagValue.PixelScale"/> tag.
        /// </summary>
        public static ExifRationalArray PixelScale => new ExifRationalArray(ExifTagValue.PixelScale);

        /// <summary>
        /// Gets a new <see cref="ExifRationalArray"/> instance for the <see cref="ExifTagValue.IntergraphMatrix"/> tag.
        /// </summary>
        public static ExifRationalArray IntergraphMatrix => new ExifRationalArray(ExifTagValue.IntergraphMatrix);

        /// <summary>
        /// Gets a new <see cref="ExifRationalArray"/> instance for the <see cref="ExifTagValue.ModelTiePoint"/> tag.
        /// </summary>
        public static ExifRationalArray ModelTiePoint => new ExifRationalArray(ExifTagValue.ModelTiePoint);

        /// <summary>
        /// Gets a new <see cref="ExifRationalArray"/> instance for the <see cref="ExifTagValue.ModelTransform"/> tag.
        /// </summary>
        public static ExifRationalArray ModelTransform => new ExifRationalArray(ExifTagValue.ModelTransform);

        /// <summary>
        /// Gets a new <see cref="ExifRationalArray"/> instance for the <see cref="ExifTagValue.GPSLatitude"/> tag.
        /// </summary>
        public static ExifRationalArray GPSLatitude => new ExifRationalArray(ExifTagValue.GPSLatitude);

        /// <summary>
        /// Gets a new <see cref="ExifRationalArray"/> instance for the <see cref="ExifTagValue.GPSLongitude"/> tag.
        /// </summary>
        public static ExifRationalArray GPSLongitude => new ExifRationalArray(ExifTagValue.GPSLongitude);

        /// <summary>
        /// Gets a new <see cref="ExifRationalArray"/> instance for the <see cref="ExifTagValue.GPSTimestamp"/> tag.
        /// </summary>
        public static ExifRationalArray GPSTimestamp => new ExifRationalArray(ExifTagValue.GPSTimestamp);

        /// <summary>
        /// Gets a new <see cref="ExifRationalArray"/> instance for the <see cref="ExifTagValue.GPSDestLatitude"/> tag.
        /// </summary>
        public static ExifRationalArray GPSDestLatitude => new ExifRationalArray(ExifTagValue.GPSDestLatitude);

        /// <summary>
        /// Gets a new <see cref="ExifRationalArray"/> instance for the <see cref="ExifTagValue.GPSDestLongitude"/> tag.
        /// </summary>
        public static ExifRationalArray GPSDestLongitude => new ExifRationalArray(ExifTagValue.GPSDestLongitude);
    }
}