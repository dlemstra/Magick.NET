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
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTagValue.JPEGTables"/> tag.
        /// </summary>
        public static ExifByteArray JPEGTables => new ExifByteArray(ExifTagValue.JPEGTables, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTagValue.OECF"/> tag.
        /// </summary>
        public static ExifByteArray JPEGTOECFables => new ExifByteArray(ExifTagValue.OECF, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTagValue.ExifVersion"/> tag.
        /// </summary>
        public static ExifByteArray ExifVersion => new ExifByteArray(ExifTagValue.ExifVersion, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTagValue.ComponentsConfiguration"/> tag.
        /// </summary>
        public static ExifByteArray ComponentsConfiguration => new ExifByteArray(ExifTagValue.ComponentsConfiguration, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTagValue.MakerNote"/> tag.
        /// </summary>
        public static ExifByteArray MakerNote => new ExifByteArray(ExifTagValue.MakerNote, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTagValue.UserComment"/> tag.
        /// </summary>
        public static ExifByteArray UserComment => new ExifByteArray(ExifTagValue.UserComment, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTagValue.FlashpixVersion"/> tag.
        /// </summary>
        public static ExifByteArray FlashpixVersion => new ExifByteArray(ExifTagValue.FlashpixVersion, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTagValue.SpatialFrequencyResponse"/> tag.
        /// </summary>
        public static ExifByteArray SpatialFrequencyResponse => new ExifByteArray(ExifTagValue.SpatialFrequencyResponse, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTagValue.SpatialFrequencyResponse2"/> tag.
        /// </summary>
        public static ExifByteArray SpatialFrequencyResponse2 => new ExifByteArray(ExifTagValue.SpatialFrequencyResponse2, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTagValue.Noise"/> tag.
        /// </summary>
        public static ExifByteArray Noise => new ExifByteArray(ExifTagValue.Noise, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTagValue.CFAPattern"/> tag.
        /// </summary>
        public static ExifByteArray CFAPattern => new ExifByteArray(ExifTagValue.CFAPattern, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTagValue.DeviceSettingDescription"/> tag.
        /// </summary>
        public static ExifByteArray DeviceSettingDescription => new ExifByteArray(ExifTagValue.DeviceSettingDescription, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTagValue.ImageSourceData"/> tag.
        /// </summary>
        public static ExifByteArray ImageSourceData => new ExifByteArray(ExifTagValue.ImageSourceData, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTagValue.GPSProcessingMethod"/> tag.
        /// </summary>
        public static ExifByteArray GPSProcessingMethod => new ExifByteArray(ExifTagValue.GPSProcessingMethod, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByteArray"/> instance for the <see cref="ExifTagValue.GPSAreaInformation"/> tag.
        /// </summary>
        public static ExifByteArray GPSAreaInformation => new ExifByteArray(ExifTagValue.GPSAreaInformation, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByte"/> instance for the <see cref="ExifTagValue.FileSource"/> tag.
        /// </summary>
        public static ExifByte FileSource => new ExifByte(ExifTagValue.FileSource, ExifDataType.Undefined);

        /// <summary>
        /// Gets a new <see cref="ExifByte"/> instance for the <see cref="ExifTagValue.SceneType"/> tag.
        /// </summary>
        public static ExifByte SceneType => new ExifByte(ExifTagValue.SceneType, ExifDataType.Undefined);
    }
}