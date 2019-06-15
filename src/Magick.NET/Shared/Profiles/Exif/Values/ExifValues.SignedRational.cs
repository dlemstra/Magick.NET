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
        /// Gets a new <see cref="ExifSignedRational"/> instance for the <see cref="ExifTag.ShutterSpeedValue"/> tag.
        /// </summary>
        public static ExifSignedRational ShutterSpeedValue => new ExifSignedRational(ExifTag.ShutterSpeedValue);

        /// <summary>
        /// Gets a new <see cref="ExifSignedRational"/> instance for the <see cref="ExifTag.BrightnessValue"/> tag.
        /// </summary>
        public static ExifSignedRational BrightnessValue => new ExifSignedRational(ExifTag.BrightnessValue);

        /// <summary>
        /// Gets a new <see cref="ExifSignedRational"/> instance for the <see cref="ExifTag.ExposureBiasValue"/> tag.
        /// </summary>
        public static ExifSignedRational ExposureBiasValue => new ExifSignedRational(ExifTag.ExposureBiasValue);

        /// <summary>
        /// Gets a new <see cref="ExifSignedRational"/> instance for the <see cref="ExifTag.AmbientTemperature"/> tag.
        /// </summary>
        public static ExifSignedRational AmbientTemperature => new ExifSignedRational(ExifTag.AmbientTemperature);

        /// <summary>
        /// Gets a new <see cref="ExifSignedRational"/> instance for the <see cref="ExifTag.WaterDepth"/> tag.
        /// </summary>
        public static ExifSignedRational WaterDepth => new ExifSignedRational(ExifTag.WaterDepth);

        /// <summary>
        /// Gets a new <see cref="ExifSignedRational"/> instance for the <see cref="ExifTag.CameraElevationAngle"/> tag.
        /// </summary>
        public static ExifSignedRational CameraElevationAngle => new ExifSignedRational(ExifTag.CameraElevationAngle);
    }
}