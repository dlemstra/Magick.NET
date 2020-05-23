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

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick
{
    /// <summary>
    /// Class that contains setting for the morphology operation.
    /// </summary>
    public sealed class MorphologySettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MorphologySettings"/> class.
        /// </summary>
        public MorphologySettings()
        {
            Channels = Channels.Composite;
            Iterations = 1;
            KernelArguments = string.Empty;
        }

        /// <summary>
        /// Gets or sets the channels to apply the kernel to.
        /// </summary>
        public Channels Channels { get; set; }

        /// <summary>
        /// Gets or sets the bias to use when the method is Convolve.
        /// </summary>
        public Percentage? ConvolveBias { get; set; }

        /// <summary>
        /// Gets or sets the scale to use when the method is Convolve.
        /// </summary>
        public IMagickGeometry ConvolveScale { get; set; }

        /// <summary>
        /// Gets or sets the number of iterations.
        /// </summary>
        public int Iterations { get; set; }

        /// <summary>
        /// Gets or sets built-in kernel.
        /// </summary>
        public Kernel Kernel { get; set; }

        /// <summary>
        /// Gets or sets kernel arguments.
        /// </summary>
        public string KernelArguments { get; set; }

        /// <summary>
        /// Gets or sets the morphology method.
        /// </summary>
        public MorphologyMethod Method { get; set; }

        /// <summary>
        /// Gets or sets user suplied kernel.
        /// </summary>
        public string UserKernel { get; set; }

        internal void SetImageArtifacts(IMagickImage<QuantumType> image)
        {
            if (ConvolveBias != null)
                image.SetArtifact("convolve:bias", ConvolveBias.ToString());

            if (ConvolveScale != null)
                image.SetArtifact("convolve:scale", ConvolveScale.ToString());
        }
    }
}