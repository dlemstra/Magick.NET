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
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTag.JPEGTables"/> tag.
        /// </summary>
        public static ExifByteArray JPEGTables => new ExifByteArray(ExifTag.JPEGTables, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTag.OECF"/> tag.
        /// </summary>
        public static ExifByteArray JPEGTOECFables => new ExifByteArray(ExifTag.OECF, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTag.ExifVersion"/> tag.
        /// </summary>
        public static ExifByteArray ExifVersion => new ExifByteArray(ExifTag.ExifVersion, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTag.ComponentsConfiguration"/> tag.
        /// </summary>
        public static ExifByteArray ComponentsConfiguration => new ExifByteArray(ExifTag.ComponentsConfiguration, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTag.MakerNote"/> tag.
        /// </summary>
        public static ExifByteArray MakerNote => new ExifByteArray(ExifTag.MakerNote, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTag.UserComment"/> tag.
        /// </summary>
        public static ExifByteArray UserComment => new ExifByteArray(ExifTag.UserComment, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTag.FlashpixVersion"/> tag.
        /// </summary>
        public static ExifByteArray FlashpixVersion => new ExifByteArray(ExifTag.FlashpixVersion, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTag.SpatialFrequencyResponse"/> tag.
        /// </summary>
        public static ExifByteArray SpatialFrequencyResponse => new ExifByteArray(ExifTag.SpatialFrequencyResponse, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTag.SpatialFrequencyResponse2"/> tag.
        /// </summary>
        public static ExifByteArray SpatialFrequencyResponse2 => new ExifByteArray(ExifTag.SpatialFrequencyResponse2, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTag.Noise"/> tag.
        /// </summary>
        public static ExifByteArray Noise => new ExifByteArray(ExifTag.Noise, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTag.CFAPattern"/> tag.
        /// </summary>
        public static ExifByteArray CFAPattern => new ExifByteArray(ExifTag.CFAPattern, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTag.DeviceSettingDescription"/> tag.
        /// </summary>
        public static ExifByteArray DeviceSettingDescription => new ExifByteArray(ExifTag.DeviceSettingDescription, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTag.ImageSourceData"/> tag.
        /// </summary>
        public static ExifByteArray ImageSourceData => new ExifByteArray(ExifTag.ImageSourceData, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTag.GPSProcessingMethod"/> tag.
        /// </summary>
        public static ExifByteArray GPSProcessingMethod => new ExifByteArray(ExifTag.GPSProcessingMethod, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTag.GPSAreaInformation"/> tag.
        /// </summary>
        public static ExifByteArray GPSAreaInformation => new ExifByteArray(ExifTag.GPSAreaInformation, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByte"/> instance for the <see cref="ExifTag.FileSource"/> tag.
        /// </summary>
        public static ExifByte FileSource => new ExifByte(ExifTag.FileSource, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByte"/> instance for the <see cref="ExifTag.SceneType"/> tag.
        /// </summary>
        public static ExifByte SceneType => new ExifByte(ExifTag.SceneType, ExifDataType.Undefined);
    }
}