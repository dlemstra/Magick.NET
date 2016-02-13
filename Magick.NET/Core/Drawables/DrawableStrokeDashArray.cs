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

using System;
using ImageMagick.Drawables;

namespace ImageMagick
{
  ///<summary>
  /// Specifies the pattern of dashes and gaps used to stroke paths. The stroke dash array
  /// represents an array of numbers that specify the lengths of alternating dashes and gaps in
  /// pixels. If an odd number of values is provided, then the list of values is repeated to yield
  /// an even number of values. To remove an existing dash array, pass a null dasharray. A typical
  /// stroke dash array might contain the members 5 3 2.
  ///</summary>
  public sealed class DrawableStrokeDashArray : IDrawable
  {
    private double[] _Dash;

    void IDrawable.Draw(IDrawingWand wand)
    {
      if (wand != null)
        wand.StrokeDashArray(_Dash);
    }

    ///<summary>
    /// Creates a new DrawableDashArray instance.
    ///</summary>
    ///<param name="dash">An array containing the dash information.</param>
    public DrawableStrokeDashArray(double[] dash)
    {
      _Dash = dash;
    }
  }
}