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

namespace ImageMagick.Drawables
{
  ///<summary>
  /// Encapsulates a 3-by-3 affine matrix that represents a geometric transform.
  ///</summary>
  public interface IDrawableAffine : IDrawable
  {
    ///<summary>
    /// The X coordinate scaling element.
    ///</summary>
    double ScaleX
    {
      get;
    }

    ///<summary>
    /// The Y coordinate scaling element.
    ///</summary>
    double ScaleY
    {
      get;
    }

    ///<summary>
    /// The X coordinate shearing element.
    ///</summary>
    double ShearX
    {
      get;
    }

    ///<summary>
    /// The Y coordinate shearing element.
    ///</summary>
    double ShearY
    {
      get;
    }

    ///<summary>
    /// The X coordinate of the translation element.
    ///</summary>
    double TranslateX
    {
      get;
      set;
    }

    ///<summary>
    /// The Y coordinate of the translation element.
    ///</summary>
    double TranslateY
    {
      get;
    }
  }
}