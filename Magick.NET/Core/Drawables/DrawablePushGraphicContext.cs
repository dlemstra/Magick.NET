//=================================================================================================
// Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
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
  /// Clones the current drawing wand to create a new drawing wand. The original drawing wand(s)
  /// may be returned to by invoking DrawablePopGraphicContext. The drawing wands are stored on a
  /// drawing wand stack. For every Pop there must have already been an equivalent Push.
  /// </summary>
  public sealed class DrawablePushGraphicContext : IDrawable
  {
    void IDrawable.Draw(IDrawingWand wand)
    {
      if (wand != null)
        wand.PushGraphicContext();
    }

    /// <summary>
    /// Creates a new DrawablePushGraphicContext instance.
    /// </summary>
    public DrawablePushGraphicContext()
    {
    }
  }
}