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
    /// Specifies the morphology methods.
    /// </summary>
    public enum MorphologyMethod
    {
        /// <summary>
        /// Undefined
        /// </summary>
        Undefined,

        /// <summary>
        /// Convolve
        /// </summary>
        Convolve,

        /// <summary>
        /// Correlate
        /// </summary>
        Correlate,

        /// <summary>
        /// Erode
        /// </summary>
        Erode,

        /// <summary>
        /// Dilate
        /// </summary>
        Dilate,

        /// <summary>
        /// ErodeIntensity
        /// </summary>
        ErodeIntensity,

        /// <summary>
        /// DilateIntensity
        /// </summary>
        DilateIntensity,

        /// <summary>
        /// IterativeDistance
        /// </summary>
        IterativeDistance,

        /// <summary>
        /// Open
        /// </summary>
        Open,

        /// <summary>
        /// Close
        /// </summary>
        Close,

        /// <summary>
        /// OpenIntensity
        /// </summary>
        OpenIntensity,

        /// <summary>
        /// CloseIntensity
        /// </summary>
        CloseIntensity,

        /// <summary>
        /// Smooth
        /// </summary>
        Smooth,

        /// <summary>
        /// EdgeIn
        /// </summary>
        EdgeIn,

        /// <summary>
        /// EdgeOut
        /// </summary>
        EdgeOut,

        /// <summary>
        /// Edge
        /// </summary>
        Edge,

        /// <summary>
        /// TopHat
        /// </summary>
        TopHat,

        /// <summary>
        /// BottomHat
        /// </summary>
        BottomHat,

        /// <summary>
        /// HitAndMiss
        /// </summary>
        HitAndMiss,

        /// <summary>
        /// Thinning
        /// </summary>
        Thinning,

        /// <summary>
        /// Thicken
        /// </summary>
        Thicken,

        /// <summary>
        /// Distance
        /// </summary>
        Distance,

        /// <summary>
        /// Voronoi
        /// </summary>
        Voronoi
    }
}