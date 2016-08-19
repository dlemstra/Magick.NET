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
using System.Collections.Generic;

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
  ///<summary>
  /// Class that represents a CMYK color.
  ///</summary>
  public sealed class ColorCMYK : ColorBase
  {
    private ColorCMYK(MagickColor color)
      : base(color)
    {
    }

    private static MagickColor CreateColor(string color)
    {
      Throw.IfNullOrEmpty(nameof(color), color);

      if (color[0] == '#')
      {
        List<QuantumType> colors = HexColor.Parse(color);

        if (colors.Count == 4)
          return new MagickColor(colors[0], colors[1], colors[2], colors[3], Quantum.Max);
      }

      throw new ArgumentException("Invalid color specified", nameof(color));
    }

    ///<summary>
    /// Initializes a new instance of the ColorCMYK class.
    ///</summary>
    ///<param name="cyan">Cyan component value of this color.</param>
    ///<param name="magenta">Magenta component value of this color.</param>
    ///<param name="yellow">Yellow component value of this color.</param>
    ///<param name="key">Key (black) component value of this color.</param>
    public ColorCMYK(Percentage cyan, Percentage magenta, Percentage yellow, Percentage key)
      : base(new MagickColor(cyan.ToQuantum(), magenta.ToQuantum(), yellow.ToQuantum(), key.ToQuantum(), Quantum.Max))
    {
    }

    ///<summary>
    /// Initializes a new instance of the ColorCMYK class.
    ///</summary>
    ///<param name="cyan">Cyan component value of this color.</param>
    ///<param name="magenta">Magenta component value of this color.</param>
    ///<param name="yellow">Yellow component value of this color.</param>
    ///<param name="key">Key (black) component value of this color.</param>
    ///<param name="alpha">Key (black) component value of this color.</param>
    public ColorCMYK(Percentage cyan, Percentage magenta, Percentage yellow, Percentage key, Percentage alpha)
      : base(new MagickColor(cyan.ToQuantum(), magenta.ToQuantum(), yellow.ToQuantum(), key.ToQuantum(), alpha.ToQuantum()))
    {
    }

    ///<summary>
    /// Initializes a new instance of the ColorCMYK class.
    ///</summary>
    ///<param name="cyan">Cyan component value of this color.</param>
    ///<param name="magenta">Magenta component value of this color.</param>
    ///<param name="yellow">Yellow component value of this color.</param>
    ///<param name="key">Key (black) component value of this color.</param>
#if Q16
    [CLSCompliant(false)]
#endif
    public ColorCMYK(QuantumType cyan, QuantumType magenta, QuantumType yellow, QuantumType key)
      : base(new MagickColor(cyan, magenta, yellow, key, Quantum.Max))
    {
    }

    ///<summary>
    /// Initializes a new instance of the ColorCMYK class.
    ///</summary>
    ///<param name="cyan">Cyan component value of this color.</param>
    ///<param name="magenta">Magenta component value of this color.</param>
    ///<param name="yellow">Yellow component value of this color.</param>
    ///<param name="key">Key (black) component value of this color.</param>
    ///<param name="alpha">Key (black) component value of this color.</param>
#if Q16
    [CLSCompliant(false)]
#endif
    public ColorCMYK(QuantumType cyan, QuantumType magenta, QuantumType yellow, QuantumType key, QuantumType alpha)
      : base(new MagickColor(cyan, magenta, yellow, key, alpha))
    {
    }

#if Q8
    ///<summary>
    /// Initializes a new instance of the MagickColor class using the specified CMYK hex string or
    /// name of the color (http://www.imagemagick.org/script/color.php).
    /// For example: #F000, #FF000000
    ///</summary>
    ///<param name="color">The RGBA/CMYK hex string or name of the color.</param>
#elif Q16 || Q16HDRI
    ///<summary>
    /// Initializes a new instance of the MagickColor class using the specified CMYK hex string or
    /// name of the color (http://www.imagemagick.org/script/color.php).
    /// For example: #F000, #FF000000, #FFFF000000000000
    ///</summary>
    ///<param name="color">The RGBA/CMYK hex string or name of the color.</param>
#else
#error Not implemented!
#endif
    public ColorCMYK(string color)
      : base(CreateColor(color))
    {
    }

    ///<summary>
    /// Alpha component value of this color.
    ///</summary>
#if Q16
    [CLSCompliant(false)]
#endif
    public QuantumType A
    {
      get
      {
        return Value.A;
      }
      set
      {
        Value.A = value;
      }
    }

    ///<summary>
    /// Cyan component value of this color.
    ///</summary>
#if Q16
    [CLSCompliant(false)]
#endif
    public QuantumType C
    {
      get
      {
        return Value.R;
      }
      set
      {
        Value.R = value;
      }
    }

    ///<summary>
    /// Key (black) component value of this color.
    ///</summary>
#if Q16
    [CLSCompliant(false)]
#endif
    public QuantumType K
    {
      get
      {
        return Value.K;
      }
      set
      {
        Value.K = value;
      }
    }

    ///<summary>
    /// Magenta component value of this color.
    ///</summary>
#if Q16
    [CLSCompliant(false)]
#endif
    public QuantumType M
    {
      get
      {
        return Value.G;
      }
      set
      {
        Value.G = value;
      }
    }

    ///<summary>
    /// Yellow component value of this color.
    ///</summary>
#if Q16
    [CLSCompliant(false)]
#endif
    public QuantumType Y
    {
      get
      {
        return Value.B;
      }
      set
      {
        Value.B = value;
      }
    }

    ///<summary>
    /// Converts the specified MagickColor to an instance of this type.
    ///</summary>
    public static implicit operator ColorCMYK(MagickColor color)
    {
      return FromMagickColor(color);
    }

    ///<summary>
    /// Converts the specified MagickColor to an instance of this type.
    ///</summary>
    public static ColorCMYK FromMagickColor(MagickColor color)
    {
      if (color == null)
        return null;

      return new ColorCMYK(color);
    }
  }
}