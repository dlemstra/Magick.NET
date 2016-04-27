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
  /// Sets the interpretation of clip path units.
  ///</summary>
  public sealed class DrawableClipUnits : IDrawable
  {
    void IDrawable.Draw(IDrawingWand wand)
    {
      if (wand != null)
        wand.ClipUnits(Units);
    }

    ///<summary>
    /// Creates a new DrawableClipUnits instance.
    ///</summary>
    ///<param name="units">The clip path units.</param>
    public DrawableClipUnits(ClipPathUnit units)
    {
      Units = units;
    }

    ///<summary>
    /// The clip path units.
    ///</summary>
    public ClipPathUnit Units
    {
      get;
      set;
    }
  }
}