//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

namespace ImageMagick
{
    /// <summary>
    /// Specified the type of noise that should be added to the image.
    /// </summary>
    public enum NoiseType
    {
        /// <summary>
        /// Undefined
        /// </summary>
        Undefined,

        /// <summary>
        /// Uniform
        /// </summary>
        Uniform,

        /// <summary>
        /// Gaussian
        /// </summary>
        Gaussian,

        /// <summary>
        /// MultiplicativeGaussian
        /// </summary>
        MultiplicativeGaussian,

        /// <summary>
        /// Impulse
        /// </summary>
        Impulse,

        /// <summary>
        /// Poisson
        /// </summary>
        Laplacian,

        /// <summary>
        /// Poisson
        /// </summary>
        Poisson,

        /// <summary>
        /// Random
        /// </summary>
        Random
    }
}
