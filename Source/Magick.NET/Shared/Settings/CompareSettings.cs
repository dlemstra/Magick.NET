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
    /// Class that contains setting for the compare operations.
    /// </summary>
    public sealed class CompareSettings
    {
        /// <summary>
        /// Gets or sets the error metric to use.
        /// </summary>
        public ErrorMetric Metric { get; set; }

        /// <summary>
        /// Gets or sets the color that emphasize pixel differences.
        /// </summary>
        public MagickColor HighlightColor { get; set; }

        /// <summary>
        /// Gets or sets the color that de-emphasize pixel differences.
        /// </summary>
        public MagickColor LowlightColor { get; set; }

        /// <summary>
        /// Gets or sets the color of pixels that are inside the read mask.
        /// </summary>
        public MagickColor MasklightColor { get; set; }
    }
}
