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
    /// <summary>
    /// Specifies the filter types.
    /// </summary>
    public enum FilterType
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined,

        /// <summary>
        /// Point.
        /// </summary>
        Point,

        /// <summary>
        /// Box.
        /// </summary>
        Box,

        /// <summary>
        /// Triangle.
        /// </summary>
        Triangle,

        /// <summary>
        /// Hermite.
        /// </summary>
        Hermite,

        /// <summary>
        /// Hann.
        /// </summary>
        Hann,

        /// <summary>
        /// Hamming.
        /// </summary>
        Hamming,

        /// <summary>
        /// Blackman.
        /// </summary>
        Blackman,

        /// <summary>
        /// Gaussian.
        /// </summary>
        Gaussian,

        /// <summary>
        /// Quadratic.
        /// </summary>
        Quadratic,

        /// <summary>
        /// Cubic.
        /// </summary>
        Cubic,

        /// <summary>
        /// Catrom.
        /// </summary>
        Catrom,

        /// <summary>
        /// Mitchell.
        /// </summary>
        Mitchell,

        /// <summary>
        /// Jinc.
        /// </summary>
        Jinc,

        /// <summary>
        /// Sinc.
        /// </summary>
        Sinc,

        /// <summary>
        /// SincFast.
        /// </summary>
        SincFast,

        /// <summary>
        /// Kaiser.
        /// </summary>
        Kaiser,

        /// <summary>
        /// Welch.
        /// </summary>
        Welch,

        /// <summary>
        /// Parzen.
        /// </summary>
        Parzen,

        /// <summary>
        /// Bohman.
        /// </summary>
        Bohman,

        /// <summary>
        /// Bartlett.
        /// </summary>
        Bartlett,

        /// <summary>
        /// Lagrange.
        /// </summary>
        Lagrange,

        /// <summary>
        /// Lanczos.
        /// </summary>
        Lanczos,

        /// <summary>
        /// LanczosSharp.
        /// </summary>
        LanczosSharp,

        /// <summary>
        /// Lanczos2.
        /// </summary>
        Lanczos2,

        /// <summary>
        /// Lanczos2Sharp.
        /// </summary>
        Lanczos2Sharp,

        /// <summary>
        /// Robidoux.
        /// </summary>
        Robidoux,

        /// <summary>
        /// RobidouxSharp.
        /// </summary>
        RobidouxSharp,

        /// <summary>
        /// Cosine.
        /// </summary>
        Cosine,

        /// <summary>
        /// Spline.
        /// </summary>
        Spline,

        /// <summary>
        /// LanczosRadius.
        /// </summary>
        LanczosRadius,

        /// <summary>
        /// CubicSpline.
        /// </summary>
        CubicSpline,
    }
}