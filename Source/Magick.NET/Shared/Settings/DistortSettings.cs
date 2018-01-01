// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    /// Class that contains setting for the distort operation.
    /// </summary>
    public sealed class DistortSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether distort attempt to 'bestfit' the size of the resulting image.
        /// </summary>
        public bool Bestfit { get; set; }

        /// <summary>
        /// Gets or sets a value to scale the size of the output canvas by this amount to provide a method of
        /// Zooming, and for super-sampling the results.
        /// </summary>
        public double? Scale { get; set; }

        /// <summary>
        /// Gets or sets the viewport that directly set the output image canvas area and offest to use for the
        /// resulting image, rather than use the original images canvas, or a calculated 'bestfit' canvas.
        /// </summary>
        public MagickGeometry Viewport { get; set; }
    }
}
