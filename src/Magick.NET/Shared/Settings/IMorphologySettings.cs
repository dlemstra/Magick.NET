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
    /// Class that contains setting for the morphology operation.
    /// </summary>
    public interface IMorphologySettings
    {
        /// <summary>
        /// Gets or sets the channels to apply the kernel to.
        /// </summary>
        Channels Channels { get; set; }

        /// <summary>
        /// Gets or sets the bias to use when the method is Convolve.
        /// </summary>
        Percentage? ConvolveBias { get; set; }

        /// <summary>
        /// Gets or sets the scale to use when the method is Convolve.
        /// </summary>
        IMagickGeometry ConvolveScale { get; set; }

        /// <summary>
        /// Gets or sets the number of iterations.
        /// </summary>
        int Iterations { get; set; }

        /// <summary>
        /// Gets or sets built-in kernel.
        /// </summary>
        Kernel Kernel { get; set; }

        /// <summary>
        /// Gets or sets kernel arguments.
        /// </summary>
        string KernelArguments { get; set; }

        /// <summary>
        /// Gets or sets the morphology method.
        /// </summary>
        MorphologyMethod Method { get; set; }

        /// <summary>
        /// Gets or sets user suplied kernel.
        /// </summary>
        string UserKernel { get; set; }
    }
}