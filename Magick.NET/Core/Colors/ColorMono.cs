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

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
	using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick
{
  /// <summary>
  /// Class that represents a monochrome color.
  /// </summary>
  public sealed class ColorMono : ColorBase
  {
    private ColorMono(MagickColor color)
      : base(color)
    {
    }

    /// <summary>
    /// Updates the color value in an inherited class.
    /// </summary>
    protected override void UpdateValue()
    {
      QuantumType color = (IsBlack ? (QuantumType)0.0 : Quantum.Max);
      Value.R = color;
      Value.G = color;
      Value.B = color;
    }

    /// <summary>
    /// Initializes a new instance of the ColorMono class.
    /// </summary>
    /// <param name="isBlack">Specifies if the color is black or white.</param>
    public ColorMono(bool isBlack)
      : base(new MagickColor(0, 0, 0))
    {
      IsBlack = isBlack;
    }

    /// <summary>
    /// Specifies if the color is black or white.
    /// </summary>
    public bool IsBlack
    {
      get;
      set;
    }

    /// <summary>
    /// Converts the specified MagickColor to an instance of this type.
    /// </summary>
    public static implicit operator ColorMono(MagickColor color)
    {
      return FromMagickColor(color);
    }

    /// <summary>
    /// Converts the specified MagickColor to an instance of this type.
    /// </summary>
    public static ColorMono FromMagickColor(MagickColor color)
    {
      if (color == null)
        return null;

      return new ColorMono(color);
    }
  }
}