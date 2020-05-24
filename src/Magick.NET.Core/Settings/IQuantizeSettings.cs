// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    /// Class that contains setting for quantize operations.
    /// </summary>
    public interface IQuantizeSettings
    {
        /// <summary>
        /// Gets or sets the maximum number of colors to quantize to.
        /// </summary>
        int Colors { get; set; }

        /// <summary>
        /// Gets or sets the colorspace to quantize in.
        /// </summary>
        ColorSpace ColorSpace { get; set; }

        /// <summary>
        /// Gets or sets the dither method to use.
        /// </summary>
        DitherMethod? DitherMethod { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether errors should be measured.
        /// </summary>
        bool MeasureErrors { get; set; }

        /// <summary>
        /// Gets or sets the quantization tree-depth.
        /// </summary>
        int TreeDepth { get; set; }
    }
}