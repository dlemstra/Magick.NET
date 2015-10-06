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

using System;
using System.Drawing;

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
  /// Class that represents a color.
  ///</summary>
  public sealed class MagickColor : IEquatable<MagickColor>, IComparable<MagickColor>
  {
    private Wrapper.MagickColor _Instance;

    private MagickColor(Wrapper.MagickColor instance)
    {
      _Instance = instance;
    }

    private static QuantumType ParseHex(string color, int offset, int length)
    {
      QuantumType result = 0;
      QuantumType k = 1;

      int i = length - 1;
      while (i >= 0)
      {
        char c = color[offset + i];

        if (c >= '0' && c <= '9')
          result += (QuantumType)(k * ((char)c - '0'));
        else if (c >= 'a' && c <= 'f')
          result += (QuantumType)(k * ((char)c - 'a' + '\n'));
        else if (c >= 'A' && c <= 'F')
          result += (QuantumType)(k * ((char)c - 'A' + '\n'));
        else
          throw new ArgumentException("Invalid character: " + c + ".");

        i--;
        k = (QuantumType)(k * 16);
      }

      return result;
    }

    private void ParseQ8HexColor(string color)
    {
      byte red;
      byte green;
      byte blue;
      byte alpha = 255;

      if (color.Length == 4 || color.Length == 5)
      {
        red = (byte)ParseHex(color, 1, 1);
        red += (byte)(red * 16);
        green = (byte)ParseHex(color, 2, 1);
        green += (byte)(green * 16);
        blue = (byte)ParseHex(color, 3, 1);
        blue += (byte)(blue * 16);

        if (color.Length == 5)
        {
          alpha = (byte)ParseHex(color, 4, 1);
          alpha += (byte)(alpha * 16);
        }
      }
      else if (color.Length == 7 || color.Length == 9)
      {
        red = (byte)ParseHex(color, 1, 2);
        green = (byte)ParseHex(color, 3, 2);
        blue = (byte)ParseHex(color, 5, 2);

        if (color.Length == 9)
          alpha = (byte)ParseHex(color, 7, 2);
      }
      else
        throw new ArgumentException("Invalid hex value.");

      _Instance = new Wrapper.MagickColor(Quantum.Convert(red), Quantum.Convert(green), Quantum.Convert(blue), Quantum.Convert(alpha));
    }

#if !Q8
    private void ParseQ16HexColor(string color)
    {
      if (color.Length < 13)
      {
        ParseQ8HexColor(color);
      }
      else if (color.Length == 13 || color.Length == 17)
      {
        QuantumType red = (QuantumType)ParseHex(color, 1, 4);
        QuantumType green = (QuantumType)ParseHex(color, 5, 4);
        QuantumType blue = (QuantumType)ParseHex(color, 9, 4);
        QuantumType alpha = Quantum.Max;

        if (color.Length == 17)
          alpha = (QuantumType)ParseHex(color, 13, 4);

        _Instance = new Wrapper.MagickColor(red, green, blue, alpha);
      }
      else
        throw new ArgumentException("Invalid hex value.");
    }
#endif

    internal MagickColor(MagickColor color)
    {
      _Instance = new Wrapper.MagickColor(color._Instance);
    }

    internal static MagickColor Create(Wrapper.MagickColor value)
    {
      if (value == null)
        return null;

      return new MagickColor(value);
    }

    internal static Wrapper.MagickColor GetInstance(MagickColor value)
    {
      if (value == null)
        return null;

      return value._Instance;
    }

    ///<summary>
    /// Initializes a new instance of the MagickColor class.
    ///</summary>
    public MagickColor()
    {
      _Instance = new Wrapper.MagickColor();
    }

    ///<summary>
    /// Initializes a new instance of the MagickColor class using the specified color.
    ///</summary>
    ///<param name="color">The color to use.</param>
    public MagickColor(Color color)
    {
      _Instance = new Wrapper.MagickColor(color);
    }

    ///<summary>
    /// Initializes a new instance of the MagickColor class.
    ///</summary>
    ///<param name="red">Red component value of this color.</param>
    ///<param name="green">Green component value of this color.</param>
    ///<param name="blue">Blue component value of this color.</param>
#if Q16
    [CLSCompliant(false)]
#endif
    public MagickColor(QuantumType red, QuantumType green, QuantumType blue)
    {
      _Instance = new Wrapper.MagickColor(red, green, blue);
    }

    ///<summary>
    /// Initializes a new instance of the MagickColor class.
    ///</summary>
    ///<param name="red">Red component value of this color.</param>
    ///<param name="green">Green component value of this color.</param>
    ///<param name="blue">Blue component value of this color.</param>
    ///<param name="alpha">Alpha component value of this color.</param>
#if Q16
    [CLSCompliant(false)]
#endif
    public MagickColor(QuantumType red, QuantumType green, QuantumType blue, QuantumType alpha)
    {
      _Instance = new Wrapper.MagickColor(red, green, blue, alpha);
    }

    ///<summary>
    /// Initializes a new instance of the MagickColor class.
    ///</summary>
    ///<param name="cyan">Cyan component value of this color.</param>
    ///<param name="magenta">Magenta component value of this color.</param>
    ///<param name="yellow">Yellow component value of this color.</param>
    ///<param name="black">Black component value of this color.</param>
    ///<param name="alpha">Alpha component value of this color.</param>
#if Q16
    [CLSCompliant(false)]
#endif
    public MagickColor(QuantumType cyan, QuantumType magenta, QuantumType yellow, QuantumType black, QuantumType alpha)
    {
      _Instance = new Wrapper.MagickColor(cyan, magenta, yellow, black, alpha);
    }
#if Q8
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickColor class using the specified RGBA hex string or
		/// name of the color (http://www.imagemagick.org/script/color.php).
		/// For example: #F00, #F00C, #FF0000, #FF0000CC
		///</summary>
		///<param name="color">The RGBA/CMYK hex string or name of the color.</param>
#elif Q16 || Q16HDRI

    ///<summary>
    /// Initializes a new instance of the MagickColor class using the specified RGBA hex string or
    /// name of the color (http://www.imagemagick.org/script/color.php).
    /// For example: #F00, #F00C, #FF0000, #FF0000CC, #FFFF00000000, #FFFF00000000CCCC
    ///</summary>
    ///<param name="color">The RGBA/CMYK hex string or name of the color.</param>
#else
#error Not implemented!
#endif
    public MagickColor(string color)
    {
      Throw.IfNullOrEmpty("color", color);

      if (color.Equals("transparent", StringComparison.OrdinalIgnoreCase))
      {
        _Instance = new Wrapper.MagickColor(Quantum.Max, Quantum.Max, Quantum.Max, 0);
        return;
      }

      if (color[0] == '#')
      {
#if Q8
				ParseQ8HexColor(color);
#elif Q16 || Q16HDRI
        ParseQ16HexColor(color);
#else
#error Not implemented!
#endif
        return;
      }

      _Instance = new Wrapper.MagickColor(color);
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
        return _Instance.A;
      }
      set
      {
        _Instance.A = value;
      }
    }

    ///<summary>
    /// Blue component value of this color.
    ///</summary>
#if Q16
    [CLSCompliant(false)]
#endif
    public QuantumType B
    {
      get
      {
        return _Instance.B;
      }
      set
      {
        _Instance.B = value;
      }
    }

    ///<summary>
    /// Green component value of this color.
    ///</summary>
#if Q16
    [CLSCompliant(false)]
#endif
    public QuantumType G
    {
      get
      {
        return _Instance.G;
      }
      set
      {
        _Instance.G = value;
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
        return _Instance.K;
      }
      set
      {
        _Instance.K = value;
      }
    }

    ///<summary>
    /// Red component value of this color.
    ///</summary>
#if Q16
    [CLSCompliant(false)]
#endif
    public QuantumType R
    {
      get
      {
        return _Instance.R;
      }
      set
      {
        _Instance.R = value;
      }
    }

    ///<summary>
    /// Returns a transparent color.
    ///</summary>
    public static MagickColor Transparent
    {
      get
      {
        return new MagickColor(Quantum.Max, Quantum.Max, Quantum.Max, 0);
      }
    }

    /// <summary>
    /// Determines whether the specified MagickColor instances are considered equal.
    /// </summary>
    /// <param name="left">The first MagickColor to compare.</param>
    /// <param name="right"> The second MagickColor to compare.</param>
    /// <returns></returns>
    public static bool operator ==(MagickColor left, MagickColor right)
    {
      return object.Equals(left, right);
    }

    /// <summary>
    /// Determines whether the specified MagickColor instances are not considered equal.
    /// </summary>
    /// <param name="left">The first MagickColor to compare.</param>
    /// <param name="right"> The second MagickColor to compare.</param>
    /// <returns></returns>
    public static bool operator !=(MagickColor left, MagickColor right)
    {
      return !object.Equals(left, right);
    }

    /// <summary>
    /// Determines whether the first MagickColor is more than the second MagickColor.
    /// </summary>
    /// <param name="left">The first MagickColor to compare.</param>
    /// <param name="right"> The second MagickColor to compare.</param>
    /// <returns></returns>
    public static bool operator >(MagickColor left, MagickColor right)
    {
      if (ReferenceEquals(left, null))
        return ReferenceEquals(right, null);

      return left.CompareTo(right) == 1;
    }

    /// <summary>
    /// Determines whether the first MagickColor is less than the second MagickColor.
    /// </summary>
    /// <param name="left">The first MagickColor to compare.</param>
    /// <param name="right"> The second MagickColor to compare.</param>
    /// <returns></returns>
    public static bool operator <(MagickColor left, MagickColor right)
    {
      if (ReferenceEquals(left, null))
        return !ReferenceEquals(right, null);

      return left.CompareTo(right) == -1;
    }

    /// <summary>
    /// Determines whether the first MagickColor is more than or equal to the second MagickColor.
    /// </summary>
    /// <param name="left">The first MagickColor to compare.</param>
    /// <param name="right"> The second MagickColor to compare.</param>
    /// <returns></returns>
    public static bool operator >=(MagickColor left, MagickColor right)
    {
      if (ReferenceEquals(left, null))
        return ReferenceEquals(right, null);

      return left.CompareTo(right) >= 0;
    }

    /// <summary>
    /// Determines whether the first MagickColor is less than or equal to the second MagickColor.
    /// </summary>
    /// <param name="left">The first MagickColor to compare.</param>
    /// <param name="right"> The second MagickColor to compare.</param>
    /// <returns></returns>
    public static bool operator <=(MagickColor left, MagickColor right)
    {
      if (ReferenceEquals(left, null))
        return !ReferenceEquals(right, null);

      return left.CompareTo(right) <= 0;
    }

    ///<summary>
    /// Converts the specified MagickColor to an instance of this type.
    ///</summary>
    public static implicit operator Color(MagickColor color)
    {
      if (ReferenceEquals(color, null))
        return Color.Empty;

      return color.ToColor();
    }

    ///<summary>
    /// Converts the specified color to an MagickColor instance.
    ///</summary>
    public static implicit operator MagickColor(Color color)
    {
      return new MagickColor(color);
    }

    ///<summary>
    /// Compares the current instance with another object of the same type.
    ///</summary>
    ///<param name="other">The color to compare this color with.</param>
    public int CompareTo(MagickColor other)
    {
      if (ReferenceEquals(other, null))
        return 1;

      if (R < other.R)
        return -1;

      if (R > other.R)
        return 1;

      if (G < other.G)
        return -1;

      if (G > other.G)
        return 1;

      if (B < other.B)
        return -1;

      if (B > other.B)
        return 1;

      if (K < other.K)
        return -1;

      if (K > other.K)
        return 1;

      if (A < other.A)
        return -1;

      if (A > other.A)
        return 1;

      return 0;
    }

    ///<summary>
    /// Determines whether the specified object is equal to the current color.
    ///</summary>
    ///<param name="obj">The object to compare this color with.</param>
    public override bool Equals(object obj)
    {
      if (ReferenceEquals(this, obj))
        return true;

      return Equals(obj as MagickColor);
    }

    ///<summary>
    /// Determines whether the specified color is equal to the current color.
    ///</summary>
    ///<param name="other">The color to compare this color with.</param>
    public bool Equals(MagickColor other)
    {
      if (ReferenceEquals(other, null))
        return false;

      return _Instance.Equals(other._Instance);
    }

    ///<summary>
    /// Determines whether the specified geometry is fuzzy equal to the current color.
    ///</summary>
    ///<param name="other">The color to compare this color with.</param>
    /// <param name="fuzz">The fuzz factor.</param>
    public bool FuzzyEquals(MagickColor other, Percentage fuzz)
    {
      if (ReferenceEquals(other, null))
        return false;

      return _Instance.FuzzyEquals(other._Instance, fuzz.ToQuantum());
    }

    ///<summary>
    /// Serves as a hash of this type.
    ///</summary>
    public override int GetHashCode()
    {
      return _Instance.GetHashCode();
    }

    ///<summary>
    /// Converts the value of this instance to an equivalent Color.
    ///</summary>
    public Color ToColor()
    {
      return _Instance.ToColor();
    }

    ///<summary>
    /// Converts the value of this instance to a hexadecimal string.
    ///</summary>
    public override string ToString()
    {
      return _Instance.ToString();
    }
  }
}
