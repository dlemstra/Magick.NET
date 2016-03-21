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
using System.Globalization;

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
  public sealed partial class MagickColor : IEquatable<MagickColor>, IComparable<MagickColor>
  {
    private bool _IsCmyk = false;

    private MagickColor(NativeMagickColor instance)
    {
      Initialize(instance);
    }

    private NativeMagickColor CreateNativeInstance()
    {
      NativeMagickColor instance = new NativeMagickColor();
      instance.Red = R;
      instance.Green = G;
      instance.Blue = B;
      instance.Alpha = A;
      instance.Black = K;

      return instance;
    }

    private void Initialize(NativeMagickColor instance)
    {
      R = instance.Red;
      G = instance.Green;
      B = instance.Blue;
      A = instance.Alpha;
      K = instance.Black;

      Count = (int)instance.Count;
    }

#if !(Q8)
    private void Initialize(byte red, byte green, byte blue, byte alpha)
    {
      R = Quantum.Convert(red);
      G = Quantum.Convert(green);
      B = Quantum.Convert(blue);
      A = Quantum.Convert(alpha);
      K = 0;
    }
#endif

    private void Initialize(QuantumType red, QuantumType green, QuantumType blue, QuantumType alpha)
    {
      R = red;
      G = green;
      B = blue;
      A = alpha;
      K = 0;
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
          result += (QuantumType)(k * (c - '0'));
        else if (c >= 'a' && c <= 'f')
          result += (QuantumType)(k * (c - 'a' + '\n'));
        else if (c >= 'A' && c <= 'F')
          result += (QuantumType)(k * (c - 'A' + '\n'));
        else
          throw new ArgumentException("Invalid character: " + c + ".");

        i--;
        k = (QuantumType)(k * 16);
      }

      return result;
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
        QuantumType red = ParseHex(color, 1, 4);
        QuantumType green = ParseHex(color, 5, 4);
        QuantumType blue = ParseHex(color, 9, 4);
        QuantumType alpha = Quantum.Max;

        if (color.Length == 17)
          alpha = ParseHex(color, 13, 4);

        Initialize(red, green, blue, alpha);
      }
      else
        throw new ArgumentException("Invalid hex value.");
    }
#endif

    private void ParseQ8HexColor(string color)
    {
      byte red;
      byte green;
      byte blue;
      byte alpha = 255;

      if (color.Length == 3)
      {
        red = green = blue = (byte)ParseHex(color, 1, 2);
      }
      else if (color.Length == 4 || color.Length == 5)
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

      Initialize(Quantum.Convert(red), Quantum.Convert(green), Quantum.Convert(blue), Quantum.Convert(alpha));
    }

    internal int Count
    {
      get;
      private set;
    }

    internal static MagickColor Clone(MagickColor value)
    {
      if (value == null)
        return value;

      MagickColor clone = new MagickColor();

      clone.R = value.R;
      clone.G = value.G;
      clone.B = value.B;
      clone.A = value.A;
      clone.K = value.K;
      clone._IsCmyk = value._IsCmyk;

      return clone;
    }

    internal string ToShortString()
    {
      if (A != Quantum.Max)
        return ToString();

      if (_IsCmyk)
        return string.Format(CultureInfo.InvariantCulture, "cmyk({0},{1},{2},{3})",
          R, G, B, K);
#if (Q8)
      return string.Format(CultureInfo.InvariantCulture, "#{0:X2}{1:X2}{2:X2}",
        R, G, B);
#elif (Q16) || (Q16HDRI)
      return string.Format(CultureInfo.InvariantCulture, "#{0:X4}{1:X4}{2:X4}",
        (ushort)R, (ushort)G, (ushort)B);
#else
#error Not implemented!
#endif
    }

    internal static string ToString(MagickColor value)
    {
      return value == null ? null : value.ToString();
    }

    ///<summary>
    /// Initializes a new instance of the MagickColor class.
    ///</summary>
    public MagickColor()
    {
    }

    ///<summary>
    /// Initializes a new instance of the ColorBase class using the specified color.
    ///</summary>
    ///<param name="color">The color to use.</param>
    public MagickColor(MagickColor color)
    {
      Throw.IfNull("color", color);

      R = color.R;
      G = color.G;
      B = color.B;
      A = color.A;
      K = color.K;
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
      Initialize(red, green, blue, Quantum.Max);
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
      Initialize(red, green, blue, alpha);
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
      Initialize(cyan, magenta, yellow, alpha);
      K = black;
      _IsCmyk = true;
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
        Initialize(Quantum.Max, Quantum.Max, Quantum.Max, 0);
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

      using (NativeMagickColor instance = new NativeMagickColor())
      {
        Throw.IfFalse("color", instance.Initialize(color), "Invalid color specified");
        Initialize(instance);
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
      return Equals(left, right);
    }

    /// <summary>
    /// Determines whether the specified MagickColor instances are not considered equal.
    /// </summary>
    /// <param name="left">The first MagickColor to compare.</param>
    /// <param name="right"> The second MagickColor to compare.</param>
    /// <returns></returns>
    public static bool operator !=(MagickColor left, MagickColor right)
    {
      return !Equals(left, right);
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
    /// Alpha component value of this color.
    ///</summary>
#if Q16
    [CLSCompliant(false)]
#endif
    public QuantumType A
    {
      get;
      set;
    }

    ///<summary>
    /// Blue component value of this color.
    ///</summary>
#if Q16
    [CLSCompliant(false)]
#endif
    public QuantumType B
    {
      get;
      set;
    }

    ///<summary>
    /// Green component value of this color.
    ///</summary>
#if Q16
    [CLSCompliant(false)]
#endif
    public QuantumType G
    {
      get;
      set;
    }

    ///<summary>
    /// Key (black) component value of this color.
    ///</summary>
#if Q16
    [CLSCompliant(false)]
#endif
    public QuantumType K
    {
      get;
      set;
    }

    ///<summary>
    /// Red component value of this color.
    ///</summary>
#if Q16
    [CLSCompliant(false)]
#endif
    public QuantumType R
    {
      get;
      set;
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

      if (ReferenceEquals(this, other))
        return true;

      return
        _IsCmyk == other._IsCmyk &&
        A == other.A &&
        B == other.B &&
        G == other.G &&
        R == other.R &&
        K == other.K;
    }

    /// <summary>
    /// Creates a new MagickColor instance from the specified 8-bit color values (red, green,
    /// and blue). The alpha value is implicitly 255 (fully opaque).
    ///<param name="red">Red component value of this color.</param>
    ///<param name="green">Green component value of this color.</param>
    ///<param name="blue">Blue component value of this color.</param>
    /// </summary>
    public static MagickColor FromRgb(byte red, byte green, byte blue)
    {
      MagickColor color = new MagickColor();
      color.Initialize(red, green, blue, 255);
      return color;
    }

    /// <summary>
    /// Creates a new MagickColor instance from the specified 8-bit color values (red, green,
    /// blue and alpha).
    ///<param name="red">Red component value of this color.</param>
    ///<param name="green">Green component value of this color.</param>
    ///<param name="blue">Blue component value of this color.</param>
    ///<param name="alpha">Alpha component value of this color.</param>
    /// </summary>
    public static MagickColor FromRgba(byte red, byte green, byte blue, byte alpha)
    {
      MagickColor color = new MagickColor();
      color.Initialize(red, green, blue, alpha);
      return color;
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

      using (NativeMagickColor instance = CreateNativeInstance())
      {
        return instance.FuzzyEquals(other, fuzz.ToQuantum());
      }
    }

    ///<summary>
    /// Serves as a hash of this type.
    ///</summary>
    public override int GetHashCode()
    {
      return
        _IsCmyk.GetHashCode() ^
        A.GetHashCode() ^
        B.GetHashCode() ^
        G.GetHashCode() ^
        K.GetHashCode() ^
        R.GetHashCode();
    }

    ///<summary>
    /// Converts the value of this instance to a hexadecimal string.
    ///</summary>
    public override string ToString()
    {
      if (_IsCmyk)
        return string.Format(CultureInfo.InvariantCulture, "cmyka({0},{1},{2},{3},{4:0.0###})",
          R, G, B, K, (double)A / Quantum.Max);
#if (Q8)
      return string.Format(CultureInfo.InvariantCulture, "#{0:X2}{1:X2}{2:X2}{3:X2}",
        R, G, B, A);
#elif (Q16) || (Q16HDRI)
      return string.Format(CultureInfo.InvariantCulture, "#{0:X4}{1:X4}{2:X4}{3:X4}",
        (ushort)R, (ushort)G, (ushort)B, (ushort)A);
#else
#error Not implemented!
#endif
    }
  }
}
