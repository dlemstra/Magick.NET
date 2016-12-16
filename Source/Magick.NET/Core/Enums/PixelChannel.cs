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

using System.Diagnostics.CodeAnalysis;

namespace ImageMagick
{
  /// <summary>
  /// Specifies the pixel channels.
  /// </summary>
  [SuppressMessage("Microsoft.Design", "CA1027:MarkEnumsWithFlags", Justification = "Flags won't work in this situation.")]
  public enum PixelChannel
  {
    /// <summary>
    /// Red
    /// </summary>
    Red = 0,

    /// <summary>
    /// Cyan
    /// </summary>
    Cyan = 0,

    /// <summary>
    /// Gray
    /// </summary>
    Gray = 0,

    /// <summary>
    /// Green
    /// </summary>
    Green = 1,

    /// <summary>
    /// Magenta
    /// </summary>
    Magenta = 1,

    /// <summary>
    /// Blue
    /// </summary>
    Blue = 2,

    /// <summary>
    /// Yellow
    /// </summary>
    Yellow = 2,

    /// <summary>
    /// Black
    /// </summary>
    Black = 3,

    /// <summary>
    /// Alpha
    /// </summary>
    Alpha = 4,

    /// <summary>
    /// Index
    /// </summary>
    Index = 5,

    /// <summary>
    /// Composite
    /// </summary>
    Composite = 32
  }
}