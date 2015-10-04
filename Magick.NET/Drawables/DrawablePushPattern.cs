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
  /// Encapsulation of the DrawablePushPattern object.
  ///</summary>
  public sealed class DrawablePushPattern : IDrawablePushPattern
  {
    ///<summary>
    /// Creates a new DrawablePushPattern instance.
    ///</summary>
    ///<param name="id">The ID of the pattern.</param>
    ///<param name="x">The X coordinate.</param>
    ///<param name="y">The Y coordinate.</param>
    ///<param name="width">The width.</param>
    ///<param name="height">The height.</param>
    public DrawablePushPattern(string id, int x, int y, int width, int height)
    {
      ID = id;
      X = x;
      Y = y;
      Width = width;
      Height = height;
    }

    /// <summary>
    /// The ID of the pattern.
    /// </summary>
    public string ID
    {
      get;
      set;
    }

    /// <summary>
    /// The height
    /// </summary>
    public int Height
    {
      get;
      set;
    }

    /// <summary>
    /// The width
    /// </summary>
    public int Width
    {
      get;
      set;
    }

    /// <summary>
    /// The X coordinate.
    /// </summary>
    public int X
    {
      get;
      set;
    }

    /// <summary>
    /// The Y coordinate.
    /// </summary>
    public int Y
    {
      get;
      set;
    }
  }
}