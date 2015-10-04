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
  /// Encapsulation of the DrawableRoundRectangle object.
  ///</summary>
  public interface IDrawableRoundRectangle : IDrawable
  {
    ///<summary>
    /// The center X coordinate.
    ///</summary>
    double CenterX
    {
      get;
    }

    ///<summary>
    /// The center Y coordinate.
    ///</summary>
    double CenterY
    {
      get;
    }

    ///<summary>
    /// The corner height.
    ///</summary>
    double CornerHeight
    {
      get;
    }

    ///<summary>
    /// The corner width.
    ///</summary>
    double CornerWidth
    {
      get;
    }

    ///<summary>
    /// The height.
    ///</summary>
    double Height
    {
      get;
    }

    ///<summary>
    /// The width.
    ///</summary>
    double Width
    {
      get;
    }
  }
}