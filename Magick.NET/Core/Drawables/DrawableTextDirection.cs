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
  ///<summary>
  /// Specifies the direction to be used when annotating with text.
  ///</summary>
  public sealed class DrawableTextDirection : IDrawable
  {
    void IDrawable.Draw(IDrawingWand wand)
    {
      if (wand != null)
        wand.TextDirection(Direction);
    }

    ///<summary>
    /// Creates a new DrawableTextDirection instance.
    ///</summary>
    ///<param name="direction">Direction to use.</param>
    public DrawableTextDirection(TextDirection direction)
    {
      Direction = direction;
    }

    ///<summary>
    /// Direction to use.
    ///</summary>
    public TextDirection Direction
    {
      get;
      set;
    }
  }
}