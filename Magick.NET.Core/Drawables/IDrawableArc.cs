//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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

namespace ImageMagick.Drawables
{
  ///<summary>
  /// Encapsulation of the DrawableArc object.
  ///</summary>
  public interface IDrawableArc : IDrawable
  {
    ///<summary>
    /// The ending degrees of rotation.
    ///</summary>
    double EndDegrees
    {
      get;
    }

    ///<summary>
    /// The ending X coordinate of the bounding rectangle.
    ///</summary>
    double EndX
    {
      get;
    }

    ///<summary>
    /// The ending Y coordinate of the bounding rectangle.
    ///</summary>
    double EndY
    {
      get;
    }

    ///<summary>
    /// The starting degrees of rotation.
    ///</summary>
    double StartDegrees
    {
      get;
    }

    ///<summary>
    /// The starting X coordinate of the bounding rectangle.
    ///</summary>
    double StartX
    {
      get;
    }

    ///<summary>
    /// The starting Y coordinate of the bounding rectangle.
    ///</summary>
    double StartY
    {
      get;
    }
  }
}