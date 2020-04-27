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
    /// Specifies distortion methods.
    /// </summary>
    public enum DistortMethod
    {
        /// <summary>
        /// Undefined
        /// </summary>
        Undefined,

        /// <summary>
        /// Affine
        /// </summary>
        Affine,

        /// <summary>
        /// AffineProjection
        /// </summary>
        AffineProjection,

        /// <summary>
        /// ScaleRotateTranslate
        /// </summary>
        ScaleRotateTranslate,

        /// <summary>
        /// Perspective
        /// </summary>
        Perspective,

        /// <summary>
        /// PerspectiveProjection
        /// </summary>
        PerspectiveProjection,

        /// <summary>
        /// BilinearForward
        /// </summary>
        BilinearForward,

        /// <summary>
        /// BilinearReverse
        /// </summary>
        BilinearReverse,

        /// <summary>
        /// Polynomial
        /// </summary>
        Polynomial,

        /// <summary>
        /// Arc
        /// </summary>
        Arc,

        /// <summary>
        /// Polar
        /// </summary>
        Polar,

        /// <summary>
        /// DePolar
        /// </summary>
        DePolar,

        /// <summary>
        /// Cylinder2Plane
        /// </summary>
        Cylinder2Plane,

        /// <summary>
        /// Plane2Cylinder
        /// </summary>
        Plane2Cylinder,

        /// <summary>
        /// Barrel
        /// </summary>
        Barrel,

        /// <summary>
        /// BarrelInverse
        /// </summary>
        BarrelInverse,

        /// <summary>
        /// Shepards
        /// </summary>
        Shepards,

        /// <summary>
        /// Resize
        /// </summary>
        Resize,

        /// <summary>
        /// Sentinel
        /// </summary>
        Sentinel,

        /// <summary>
        /// RigidAffine
        /// </summary>
        RigidAffine,
    }
}