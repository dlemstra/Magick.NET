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

using ImageMagick.Drawables;

namespace ImageMagick
{
  ///<summary>
  /// Encapsulation of the DrawableLine object.
  ///</summary>
  public sealed class DrawableLine : IDrawableLine
  {
    ///<summary>
    /// Creates a new DrawableLine instance.
    ///</summary>
    ///<param name="startX">The starting X coordinate.</param>
    ///<param name="startY">The starting Y coordinate.</param>
    ///<param name="endX">The ending X coordinate.</param>
    ///<param name="endY">The ending Y coordinate.</param>
    public DrawableLine(double startX, double startY, double endX, double endY)
    {
      StartX = startX;
      StartY = startY;
      EndX = endX;
      EndY = endY;
    }

    ///<summary>
    /// The ending X coordinate.
    ///</summary>
    public double EndX
    {
      get;
      set;
    }

    ///<summary>
    /// The ending Y coordinate.
    ///</summary>
    public double EndY
    {
      get;
      set;
    }

    ///<summary>
    /// The starting X coordinate.
    ///</summary>
    public double StartX
    {
      get;
      set;
    }

    ///<summary>
    /// The starting Y coordinate.
    ///</summary>
    public double StartY
    {
      get;
      set;
    }
  }
}