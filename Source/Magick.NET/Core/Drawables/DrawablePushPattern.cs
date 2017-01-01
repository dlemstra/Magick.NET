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
  /// indicates that subsequent commands up to a DrawablePopPattern command comprise the definition
  /// of a named pattern. The pattern space is assigned top left corner coordinates, a width and
  /// height, and becomes its own drawing space. Anything which can be drawn may be used in a
  /// pattern definition. Named patterns may be used as stroke or brush definitions.
  /// </summary>
  public sealed class DrawablePushPattern : IDrawable
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawablePushPattern"/> class.
    /// </summary>
    /// <param name="id">The ID of the pattern.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    public DrawablePushPattern(string id, double x, double y, double width, double height)
    {
      ID = id;
      X = x;
      Y = y;
      Width = width;
      Height = height;
    }

    /// <summary>
    /// Gets or sets the ID of the pattern.
    /// </summary>
    public string ID
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the height
    /// </summary>
    public double Height
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the width
    /// </summary>
    public double Width
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the X coordinate.
    /// </summary>
    public double X
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the Y coordinate.
    /// </summary>
    public double Y
    {
      get;
      set;
    }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawable.Draw(IDrawingWand wand)
    {
      if (wand != null)
        wand.PushPattern(ID, X, Y, Width, Height);
    }
  }
}