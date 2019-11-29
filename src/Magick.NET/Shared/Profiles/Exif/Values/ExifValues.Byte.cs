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
        /// Gets a new <see cref="ExifByte"/> instance for the <see cref="ExifTagValue.FaxProfile"/> tag.
        /// </summary>
        public static ExifByte FaxProfile => new ExifByte(ExifTagValue.FaxProfile, ExifDataType.Byte);

        /// <summary>
        /// Gets a new <see cref="ExifByte"/> instance for the <see cref="ExifTagValue.ModeNumber"/> tag.
        /// </summary>
        public static ExifByte ModeNumber => new ExifByte(ExifTagValue.ModeNumber, ExifDataType.Byte);

        /// <summary>
        /// Gets a new <see cref="ExifByte"/> instance for the <see cref="ExifTagValue.GPSAltitudeRef"/> tag.
        /// </summary>
        public static ExifByte GPSAltitudeRef => new ExifByte(ExifTagValue.GPSAltitudeRef, ExifDataType.Byte);
    }
}