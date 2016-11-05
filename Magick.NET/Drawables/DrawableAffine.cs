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

using System.Drawing.Drawing2D;

namespace ImageMagick
{
  /// <content>
  /// Contains code that is not compatible with .NET Core.
  /// </content>
  public sealed partial class DrawableAffine
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableAffine"/> class.
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    public DrawableAffine(Matrix matrix)
    {
      Throw.IfNull(nameof(matrix), matrix);

      ScaleX = matrix.Elements[0];
      ScaleY = matrix.Elements[1];
      ShearX = matrix.Elements[2];
      ShearY = matrix.Elements[3];
      TranslateX = matrix.Elements[4];
      TranslateY = matrix.Elements[5];
    }
  }
}