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
  /// Encapsulation of the DrawableTextAntialias object.
  ///</summary>
  public sealed class DrawableTextAntialias : IDrawableTextAntialias
  {
    ///<summary>
    /// Creates a new DrawableTextAntialias instance.
    ///</summary>
    ///<param name="isEnabled">True if text antialiasing is enabled otherwise false.</param>
    public DrawableTextAntialias(bool isEnabled)
    {
      IsEnabled = isEnabled;
    }

    ///<summary>
    /// True if text antialiasing is enabled otherwise false.
    ///</summary>
    public bool IsEnabled
    {
      get;
      set;
    }
  }
}