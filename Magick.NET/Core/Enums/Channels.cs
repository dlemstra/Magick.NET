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
using System.Diagnostics.CodeAnalysis;

namespace ImageMagick
{
  /// <summary>
  /// Specifies channel types.
  /// </summary>
  [SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces"), Flags]
  public enum Channels
  {
    /// <summary>
    /// Undefined
    /// </summary>
    Undefined = 0x0000,

    /// <summary>
    /// Red
    /// </summary>
    Red = 0x0001,

    /// <summary>
    /// Gray
    /// </summary>
    Gray = 0x0001,

    /// <summary>
    /// Cyan
    /// </summary>
    Cyan = 0x0001,

    /// <summary>
    /// Green
    /// </summary>
    Green = 0x0002,

    /// <summary>
    /// Magenta
    /// </summary>
    Magenta = 0x0002,

    /// <summary>
    /// Blue
    /// </summary>
    Blue = 0x0004,

    /// <summary>
    /// Yellow
    /// </summary>
    Yellow = 0x0004,

    /// <summary>
    /// Black
    /// </summary>
    Black = 0x0008,

    /// <summary>
    /// Alpha
    /// </summary>
    Alpha = 0x0010,

    /// <summary>
    /// Opacity
    /// </summary>
    Opacity = 0x0010,

    /// <summary>
    /// Index
    /// </summary>
    Index = 0x0020,

    /// <summary>
    /// Index
    /// </summary>
    Composite = 0x001F,

    /// <summary>
    /// All
    /// </summary>
    All = 0x7ffffff,

    /// <summary>
    /// TrueAlpha
    /// </summary>
    TrueAlpha = 0x0100,

    /// <summary>
    /// RGB
    /// </summary>
    RGB = Red | Green | Blue,

    /// <summary>
    /// Grays
    /// </summary>
    Grays = 0x0400,

    /// <summary>
    /// Sync
    /// </summary>
    Sync = 0x20000,

    /// <summary>
    /// Default
    /// </summary>
    Default = All
  }
}