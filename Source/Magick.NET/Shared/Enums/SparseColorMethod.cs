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
  /// The sparse color methods.
  /// </summary>
  public enum SparseColorMethod
  {
    /// <summary>
    /// Undefined
    /// </summary>
    Undefined,

    /// <summary>
    /// Barycentric
    /// </summary>
    Barycentric = DistortMethod.Affine,

    /// <summary>
    /// Bilinear
    /// </summary>
    Bilinear = DistortMethod.BilinearReverse,

    /// <summary>
    /// Polynomial
    /// </summary>
    Polynomial = DistortMethod.Polynomial,

    /// <summary>
    /// Shepards
    /// </summary>
    Shepards = DistortMethod.Shepards,

    /// <summary>
    /// Voronoi
    /// </summary>
    Voronoi = DistortMethod.Sentinel,

    /// <summary>
    /// Inverse
    /// </summary>
    Inverse = 19,

    /// <summary>
    /// Manhattan
    /// </summary>
    Manhattan = 20
  }
}