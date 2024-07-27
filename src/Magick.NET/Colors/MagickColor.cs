// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

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

namespace ImageMagick;

/// <summary>
/// Class that represents a color.
/// </summary>
public sealed partial class MagickColor : IMagickColor<QuantumType>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MagickColor"/> class.
    /// </summary>
    public MagickColor()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickColor"/> class.
    /// </summary>
    /// <param name="color">The color to use.</param>
    public MagickColor(IMagickColor<QuantumType> color)
    {
        Throw.IfNull(nameof(color), color);

        R = color.R;
        G = color.G;
        B = color.B;
        A = color.A;
        K = color.K;
        IsCmyk = color.IsCmyk;
    }

#if Q8
    /// <summary>
    /// Initializes a new instance of the <see cref="MagickColor"/> class.
    /// </summary>
    /// <param name="red">Red component value of this color (0-255).</param>
    /// <param name="green">Green component value of this color (0-255).</param>
    /// <param name="blue">Blue component value of this color (0-255).</param>
#elif Q16 || Q16HDRI
    /// <summary>
    /// Initializes a new instance of the <see cref="MagickColor"/> class.
    /// </summary>
    /// <param name="red">Red component value of this color (0-65535).</param>
    /// <param name="green">Green component value of this color (0-65535).</param>
    /// <param name="blue">Blue component value of this color (0-65535).</param>
#endif
    public MagickColor(QuantumType red, QuantumType green, QuantumType blue)
    {
        Initialize(red, green, blue, Quantum.Max);
    }

#if Q8
    /// <summary>
    /// Initializes a new instance of the <see cref="MagickColor"/> class.
    /// </summary>
    /// <param name="red">Red component value of this color (0-255).</param>
    /// <param name="green">Green component value of this color (0-255).</param>
    /// <param name="blue">Blue component value of this color (0-255).</param>
    /// <param name="alpha">Alpha component value of this color (0-255).</param>
#elif Q16 || Q16HDRI
    /// <summary>
    /// Initializes a new instance of the <see cref="MagickColor"/> class.
    /// </summary>
    /// <param name="red">Red component value of this color (0-65535).</param>
    /// <param name="green">Green component value of this color (0-65535).</param>
    /// <param name="blue">Blue component value of this color (0-65535).</param>
    /// <param name="alpha">Alpha component value of this color (0-65535).</param>
#endif
    public MagickColor(QuantumType red, QuantumType green, QuantumType blue, QuantumType alpha)
    {
        Initialize(red, green, blue, alpha);
    }

#if Q8
    /// <summary>
    /// Initializes a new instance of the <see cref="MagickColor"/> class.
    /// </summary>
    /// <param name="cyan">Cyan component value of this color (0-255).</param>
    /// <param name="magenta">Magenta component value of this color (0-255).</param>
    /// <param name="yellow">Yellow component value of this color (0-255).</param>
    /// <param name="black">Black component value of this color (0-255).</param>
    /// <param name="alpha">Alpha component value of this color (0-255).</param>
#elif Q16 || Q16HDRI
    /// <summary>
    /// Initializes a new instance of the <see cref="MagickColor"/> class.
    /// </summary>
    /// <param name="cyan">Cyan component value of this color (0-65535).</param>
    /// <param name="magenta">Magenta component value of this color (0-65535).</param>
    /// <param name="yellow">Yellow component value of this color (0-65535).</param>
    /// <param name="black">Black component value of this color (0-65535).</param>
    /// <param name="alpha">Alpha component value of this color (0-65535).</param>
#endif
    public MagickColor(QuantumType cyan, QuantumType magenta, QuantumType yellow, QuantumType black, QuantumType alpha)
    {
        Initialize(cyan, magenta, yellow, alpha);
        K = black;
        IsCmyk = true;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickColor"/> class.
    /// </summary>
    /// <param name="color">The RGBA/CMYK hex string or name of the color (http://www.imagemagick.org/script/color.php).
    /// For example: #F000, #FF000000, #FFFF000000000000.</param>
    public MagickColor(string color)
    {
        Throw.IfNullOrEmpty(nameof(color), color);

        if (color.Equals("transparent", StringComparison.OrdinalIgnoreCase))
        {
            Initialize(Quantum.Max, Quantum.Max, Quantum.Max, 0);
            return;
        }

        if (color[0] == '#')
        {
            ParseHexColor(color);
            return;
        }

        using var instance = NativeMagickColor.Create();
        Throw.IfFalse(nameof(color), instance.Initialize(color), "Invalid color specified");
        Initialize(instance);
    }

    private MagickColor(NativeMagickColor nativeInstance)
        => Initialize(nativeInstance);

    /// <summary>
    /// Gets or sets the alpha component value of this color.
    /// </summary>
    public QuantumType A { get; set; }

    /// <summary>
    /// Gets or sets the blue component value of this color.
    /// </summary>
    public QuantumType B { get; set; }

    /// <summary>
    /// Gets or sets the green component value of this color.
    /// </summary>
    public QuantumType G { get; set; }

    /// <summary>
    /// Gets a value indicating whether the color is a CMYK color.
    /// </summary>
    public bool IsCmyk { get; private set; }

    /// <summary>
    /// Gets or sets the key (black) component value of this color.
    /// </summary>
    public QuantumType K { get; set; }

    /// <summary>
    /// Gets or sets the red component value of this color.
    /// </summary>
    public QuantumType R { get; set; }

    /// <summary>
    /// Determines whether the specified <see cref="MagickColor"/> instances are considered equal.
    /// </summary>
    /// <param name="left">The first <see cref="MagickColor"/> to compare.</param>
    /// <param name="right">The second <see cref="MagickColor"/> to compare.</param>
    public static bool operator ==(MagickColor? left, MagickColor? right)
        => Equals(left, right);

    /// <summary>
    /// Determines whether the specified <see cref="MagickColor"/> instances are not considered equal.
    /// </summary>
    /// <param name="left">The first <see cref="MagickColor"/> to compare.</param>
    /// <param name="right">The second <see cref="MagickColor"/> to compare.</param>
    public static bool operator !=(MagickColor? left, MagickColor? right)
        => !Equals(left, right);

    /// <summary>
    /// Determines whether the first <see cref="MagickColor"/> is more than the second <see cref="MagickColor"/>.
    /// </summary>
    /// <param name="left">The first <see cref="MagickColor"/> to compare.</param>
    /// <param name="right">The second <see cref="MagickColor"/> to compare.</param>
    public static bool operator >(MagickColor? left, MagickColor? right)
    {
        if (left is null)
            return right is null;

        return left.CompareTo(right) == 1;
    }

    /// <summary>
    /// Determines whether the first <see cref="MagickColor"/> is less than the second <see cref="MagickColor"/>.
    /// </summary>
    /// <param name="left">The first <see cref="MagickColor"/> to compare.</param>
    /// <param name="right">The second <see cref="MagickColor"/> to compare.</param>
    public static bool operator <(MagickColor? left, MagickColor? right)
    {
        if (left is null)
            return right is not null;

        return left.CompareTo(right) == -1;
    }

    /// <summary>
    /// Determines whether the first <see cref="MagickColor"/> is more than or equal to the second <see cref="MagickColor"/>.
    /// </summary>
    /// <param name="left">The first <see cref="MagickColor"/> to compare.</param>
    /// <param name="right">The second <see cref="MagickColor"/> to compare.</param>
    public static bool operator >=(MagickColor? left, MagickColor? right)
    {
        if (left is null)
            return right is null;

        return left.CompareTo(right) >= 0;
    }

    /// <summary>
    /// Determines whether the first <see cref="MagickColor"/> is less than or equal to the second <see cref="MagickColor"/>.
    /// </summary>
    /// <param name="left">The first <see cref="MagickColor"/> to compare.</param>
    /// <param name="right">The second <see cref="MagickColor"/> to compare.</param>
    public static bool operator <=(MagickColor? left, MagickColor? right)
    {
        if (left is null)
            return right is not null;

        return left.CompareTo(right) <= 0;
    }

    /// <summary>
    /// Multiplies the value of all non alpha channels in this <see cref="MagickColor"/> with the specified <see cref="Percentage"/>.
    /// </summary>
    /// <param name="color">The <see cref="MagickColor"/> to multiply.</param>
    /// <param name="percentage">The <see cref="Percentage"/> that should be used.</param>
    /// <returns>The <see cref="MagickColor"/> multiplied with the percentage.</returns>
    public static MagickColor? operator *(MagickColor? color, Percentage percentage)
    {
        if (color is null)
            return null;

        var red = Quantum.Convert(percentage.Multiply((double)color.R));
        var green = Quantum.Convert(percentage.Multiply((double)color.G));
        var blue = Quantum.Convert(percentage.Multiply((double)color.B));

        if (!color.IsCmyk)
            return new MagickColor(red, green, blue, color.A);

        var key = Quantum.Convert(percentage.Multiply((double)color.K));

        return new MagickColor(red, green, blue, key, color.A);
    }

    /// <summary>
    /// Creates a new <see cref="MagickColor"/> instance from the specified 8-bit color values (red, green,
    /// and blue). The alpha value is implicitly 255 (fully opaque).
    /// </summary>
    /// <param name="red">Red component value of this color.</param>
    /// <param name="green">Green component value of this color.</param>
    /// <param name="blue">Blue component value of this color.</param>
    /// <returns>A <see cref="MagickColor"/> instance.</returns>
    public static MagickColor FromRgb(byte red, byte green, byte blue)
    {
        var color = new MagickColor();
        color.SetFromBytes(red, green, blue, 255);
        return color;
    }

    /// <summary>
    /// Creates a new <see cref="MagickColor"/> instance from the specified 8-bit color values (red, green,
    /// blue and alpha).
    /// </summary>
    /// <param name="red">Red component value of this color.</param>
    /// <param name="green">Green component value of this color.</param>
    /// <param name="blue">Blue component value of this color.</param>
    /// <param name="alpha">Alpha component value of this color.</param>
    /// <returns>A <see cref="MagickColor"/> instance.</returns>
    public static MagickColor FromRgba(byte red, byte green, byte blue, byte alpha)
    {
        var color = new MagickColor();
        color.SetFromBytes(red, green, blue, alpha);
        return color;
    }

    /// <summary>
    /// Compares the current instance with another object of the same type.
    /// </summary>
    /// <param name="other">The color to compare this color with.</param>
    /// <returns>A signed number indicating the relative values of this instance and value.</returns>
    public int CompareTo(IMagickColor<QuantumType>? other)
    {
        if (other is null)
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

    /// <summary>
    /// Determines whether the specified object is equal to the current color.
    /// </summary>
    /// <param name="obj">The object to compare this color with.</param>
    /// <returns>True when the specified object is equal to the current color.</returns>
    public override bool Equals(object? obj)
        => Equals(obj as IMagickColor<QuantumType>);

    /// <summary>
    /// Determines whether the specified color is equal to the current color.
    /// </summary>
    /// <param name="other">The color to compare this color with.</param>
    /// <returns>True when the specified color is equal to the current color.</returns>
    public bool Equals(IMagickColor<QuantumType>? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return
          IsCmyk == other.IsCmyk &&
          A == other.A &&
          B == other.B &&
          G == other.G &&
          R == other.R &&
          K == other.K;
    }

    /// <summary>
    /// Determines whether the specified color is fuzzy equal to the current color.
    /// </summary>
    /// <param name="other">The color to compare this color with.</param>
    /// <param name="fuzz">The fuzz factor.</param>
    /// <returns>True when the specified color is fuzzy equal to the current instance.</returns>
    public bool FuzzyEquals(IMagickColor<QuantumType> other, Percentage fuzz)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        using var instance = CreateNativeInstance(this);
        return instance.FuzzyEquals(other, PercentageHelper.ToQuantumType(nameof(fuzz), fuzz));
    }

    /// <summary>
    /// Serves as a hash of this type.
    /// </summary>
    /// <returns>A hash code for the current instance.</returns>
    public override int GetHashCode()
    {
        return
          IsCmyk.GetHashCode() ^
          A.GetHashCode() ^
          B.GetHashCode() ^
          G.GetHashCode() ^
          K.GetHashCode() ^
          R.GetHashCode();
    }

    /// <summary>
    /// Initializes the color with the specified bytes.
    /// </summary>
    /// <param name="red">Red component value of this color.</param>
    /// <param name="green">Green component value of this color.</param>
    /// <param name="blue">Blue component value of this color.</param>
    /// <param name="alpha">Alpha component value of this color.</param>
    public void SetFromBytes(byte red, byte green, byte blue, byte alpha)
    {
        R = Quantum.Convert(red);
        G = Quantum.Convert(green);
        B = Quantum.Convert(blue);
        A = Quantum.Convert(alpha);
        K = 0;
        IsCmyk = false;
    }

    /// <summary>
    /// Converts the value of this instance to a <see cref="byte"/> array (RGBA or CMYKA).
    /// </summary>
    /// <returns>The <see cref="byte"/> array.</returns>
    public byte[] ToByteArray()
    {
        if (IsCmyk)
            return new[] { Quantum.ScaleToByte(R), Quantum.ScaleToByte(G), Quantum.ScaleToByte(B), Quantum.ScaleToByte(K), Quantum.ScaleToByte(A) };
        else
            return new[] { Quantum.ScaleToByte(R), Quantum.ScaleToByte(G), Quantum.ScaleToByte(B), Quantum.ScaleToByte(A) };
    }

    /// <summary>
    /// Converts the value of this instance to a hexadecimal string that will not include the alpha channel if it is opaque.
    /// </summary>
    /// <returns>The <see cref="string"/>.</returns>
    public string ToHexString()
    {
        if (IsCmyk)
            throw new NotSupportedException("This method only works for non cmyk colors.");

        var r = Quantum.ScaleToByte(R);
        var g = Quantum.ScaleToByte(G);
        var b = Quantum.ScaleToByte(B);
        if (A == Quantum.Max)
            return string.Format(CultureInfo.InvariantCulture, "#{0:X2}{1:X2}{2:X2}", r, g, b);

        var a = Quantum.ScaleToByte(A);
        return string.Format(CultureInfo.InvariantCulture, "#{0:X2}{1:X2}{2:X2}{3:X2}", r, g, b, a);
    }

    /// <summary>
    /// Converts the value of this instance to a string representation that will not include the alpha channel if it is opaque.
    /// </summary>
    /// <returns>The <see cref="string"/>.</returns>
    public string ToShortString()
    {
        if (A != Quantum.Max)
            return ToString();

        if (IsCmyk)
        {
            var r = Quantum.ScaleToByte(R);
            var g = Quantum.ScaleToByte(G);
            var b = Quantum.ScaleToByte(B);
            var k = Quantum.ScaleToByte(K);

            return string.Format(CultureInfo.InvariantCulture, "cmyk({0},{1},{2},{3})", r, g, b, k);
        }

#if Q8
        return string.Format(CultureInfo.InvariantCulture, "#{0:X2}{1:X2}{2:X2}", R, G, B);
#elif Q16 || Q16HDRI
        return string.Format(CultureInfo.InvariantCulture, "#{0:X4}{1:X4}{2:X4}", R, G, B);
#else
#error Not implemented!
#endif
    }

    /// <summary>
    /// Converts the value of this instance to a string representation.
    /// </summary>
    /// <returns>The <see cref="string"/>.</returns>
    public override string ToString()
    {
        if (IsCmyk)
        {
            var r = Quantum.ScaleToByte(R);
            var g = Quantum.ScaleToByte(G);
            var b = Quantum.ScaleToByte(B);
            var k = Quantum.ScaleToByte(K);

            return string.Format(CultureInfo.InvariantCulture, "cmyka({0},{1},{2},{3},{4:0.0###})", r, g, b, k, (double)A / Quantum.Max);
        }
#if Q8
        return string.Format(CultureInfo.InvariantCulture, "#{0:X2}{1:X2}{2:X2}{3:X2}", R, G, B, A);
#elif Q16 || Q16HDRI
        return string.Format(CultureInfo.InvariantCulture, "#{0:X4}{1:X4}{2:X4}{3:X4}", R, G, B, A);
#else
#error Not implemented!
#endif
    }

    internal static IMagickColor<QuantumType>? Clone(IMagickColor<QuantumType>? value)
    {
        if (value is null)
            return value;

        return new MagickColor
        {
            R = value.R,
            G = value.G,
            B = value.B,
            A = value.A,
            K = value.K,
            IsCmyk = value.IsCmyk,
        };
    }

    private void Initialize(QuantumType red, QuantumType green, QuantumType blue, QuantumType alpha)
    {
        R = red;
        G = green;
        B = blue;
        A = alpha;
        K = 0;
        IsCmyk = false;
    }

    private void ParseHexColor(string color)
    {
        if (!HexColor.TryParse(color, out var colors))
            throw new ArgumentException("Invalid hex value.", nameof(color));

        if (colors.Count == 1)
            Initialize(colors[0], colors[0], colors[0], Quantum.Max);
        else if (colors.Count == 3)
            Initialize(colors[0], colors[1], colors[2], Quantum.Max);
        else
            Initialize(colors[0], colors[1], colors[2], colors[3]);
    }
}
