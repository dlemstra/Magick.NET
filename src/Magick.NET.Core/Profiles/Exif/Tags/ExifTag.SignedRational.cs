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

namespace ImageMagick
{
    /// <content/>
    public abstract partial class ExifTag
    {
        /// <summary>
        /// Gets the ShutterSpeedValue exif tag.
        /// </summary>
        public static ExifTag<SignedRational> ShutterSpeedValue { get; } = new ExifTag<SignedRational>(ExifTagValue.ShutterSpeedValue);

        /// <summary>
        /// Gets the BrightnessValue exif tag.
        /// </summary>
        public static ExifTag<SignedRational> BrightnessValue { get; } = new ExifTag<SignedRational>(ExifTagValue.BrightnessValue);

        /// <summary>
        /// Gets the ExposureBiasValue exif tag.
        /// </summary>
        public static ExifTag<SignedRational> ExposureBiasValue { get; } = new ExifTag<SignedRational>(ExifTagValue.ExposureBiasValue);

        /// <summary>
        /// Gets the AmbientTemperature exif tag.
        /// </summary>
        public static ExifTag<SignedRational> AmbientTemperature { get; } = new ExifTag<SignedRational>(ExifTagValue.AmbientTemperature);

        /// <summary>
        /// Gets the WaterDepth exif tag.
        /// </summary>
        public static ExifTag<SignedRational> WaterDepth { get; } = new ExifTag<SignedRational>(ExifTagValue.WaterDepth);

        /// <summary>
        /// Gets the CameraElevationAngle exif tag.
        /// </summary>
        public static ExifTag<SignedRational> CameraElevationAngle { get; } = new ExifTag<SignedRational>(ExifTagValue.CameraElevationAngle);
    }
}