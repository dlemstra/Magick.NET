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
  /// Associates a named clipping path with the image. Only the areas drawn on by the clipping path
  /// will be modified as ssize_t as it remains in effect.
  /// </summary>
  public sealed class DrawableClipPath : IDrawable, IDrawingWand
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableClipPath"/> class.
    /// </summary>
    /// <param name="clipPath">The ID of the clip path.</param>
    public DrawableClipPath(string clipPath)
    {
      Throw.IfNullOrEmpty(nameof(clipPath), clipPath);

      ClipPath = clipPath;
    }

    /// <summary>
    /// Gets or sets the ID of the clip path.
    /// </summary>
    public string ClipPath
    {
      get;
      set;
    }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
    {
      if (wand != null)
        wand.ClipPath(ClipPath);
    }
  }
}