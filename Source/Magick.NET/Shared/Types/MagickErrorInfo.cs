﻿// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    /// Encapsulates the error information.
    /// </summary>
    public sealed class MagickErrorInfo
    {
        internal MagickErrorInfo()
          : this(0, 0, 0)
        {
        }

        internal MagickErrorInfo(double meanErrorPerPixel, double normalizedMeanError, double normalizedMaximumError)
        {
            MeanErrorPerPixel = meanErrorPerPixel;
            NormalizedMeanError = normalizedMeanError;
            NormalizedMaximumError = normalizedMaximumError;
        }

        /// <summary>
        /// Gets the mean error per pixel computed when an image is color reduced.
        /// </summary>
        public double MeanErrorPerPixel
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the normalized maximum error per pixel computed when an image is color reduced.
        /// </summary>
        public double NormalizedMaximumError
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the normalized mean error per pixel computed when an image is color reduced.
        /// </summary>
        public double NormalizedMeanError
        {
            get;
            private set;
        }
    }
}
