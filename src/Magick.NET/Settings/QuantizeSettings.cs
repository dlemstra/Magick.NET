// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
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
    public sealed partial class QuantizeSettings : IQuantizeSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuantizeSettings"/> class.
        /// </summary>
        public QuantizeSettings()
        {
            Colors = 1;
            DitherMethod = ImageMagick.DitherMethod.Riemersma;
        }

        /// <summary>
        /// Gets or sets the maximum number of colors to quantize to.
        /// </summary>
        public int Colors { get; set; }

        /// <summary>
        /// Gets or sets the colorspace to quantize in.
        /// </summary>
        public ColorSpace ColorSpace { get; set; }

        /// <summary>
        /// Gets or sets the dither method to use.
        /// </summary>
        public DitherMethod? DitherMethod { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether errors should be measured.
        /// </summary>
        public bool MeasureErrors { get; set; }

        /// <summary>
        /// Gets or sets the quantization tree-depth.
        /// </summary>
        public int TreeDepth { get; set; }

        private static INativeInstance CreateNativeInstance(IQuantizeSettings settings)
        {
            var instance = new NativeQuantizeSettings();
            instance.SetColors(settings.Colors);
            instance.SetColorSpace(settings.ColorSpace);
            instance.SetDitherMethod(settings.DitherMethod ?? ImageMagick.DitherMethod.No);
            instance.SetMeasureErrors(settings.MeasureErrors);
            instance.SetTreeDepth(settings.TreeDepth);

            return instance;
        }
    }
}