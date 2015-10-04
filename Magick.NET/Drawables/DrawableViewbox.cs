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

using System.Drawing;
using ImageMagick.Drawables;

namespace ImageMagick
{
  ///<summary>
  /// Encapsulation of the DrawableViewbox object.
  ///</summary>
  public sealed class DrawableViewbox : IDrawableViewbox
  {
    ///<summary>
    /// Creates a new DrawableViewbox instance.
    ///</summary>
    ///<param name="upperLeftX">The upper left X coordinate.</param>
    ///<param name="upperLeftY">The upper left Y coordinate.</param>
    ///<param name="lowerRightX">The lower right X coordinate.</param>
    ///<param name="lowerRightY">The lower right Y coordinate.</param>
    public DrawableViewbox(int upperLeftX, int upperLeftY, int lowerRightX, int lowerRightY)
    {
      UpperLeftX = upperLeftX;
      UpperLeftY = upperLeftY;
      LowerRightX = lowerRightX;
      LowerRightY = lowerRightY;
    }

    ///<summary>
    /// Creates a new DrawableViewbox instance.
    ///</summary>
    ///<param name="rectangle">The rectangle to use.</param>
    public DrawableViewbox(Rectangle rectangle)
    {
      Throw.IfNull("rectangle", rectangle);

      UpperLeftX = rectangle.X;
      UpperLeftY = rectangle.Y;
      LowerRightX = rectangle.Right;
      LowerRightY = rectangle.Bottom;
    }

    ///<summary>
    /// The upper left X coordinate.
    ///</summary>
    public int LowerRightX
    {
      get;
      set;
    }

    ///<summary>
    /// The upper left Y coordinate.
    ///</summary>
    public int LowerRightY
    {
      get;
      set;
    }

    ///<summary>
    /// The upper left X coordinate.
    ///</summary>
    public int UpperLeftX
    {
      get;
      set;
    }

    ///<summary>
    /// The upper left Y coordinate.
    ///</summary>
    public int UpperLeftY
    {
      get;
      set;
    }
  }
}