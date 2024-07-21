// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick.Colors;

/// <summary>
/// Class that represents a RGB color.
/// </summary>
public sealed partial class ColorRGB : ColorBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ColorRGB"/> class.
    /// </summary>
    /// <param name="value">The color to use.</param>
    public ColorRGB(IMagickColor<QuantumType> value)
      : base(value)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorRGB"/> class.
    /// </summary>
    /// <param name="red">Red component value of this color.</param>
    /// <param name="green">Green component value of this color.</param>
    /// <param name="blue">Blue component value of this color.</param>
    public ColorRGB(QuantumType red, QuantumType green, QuantumType blue)
      : base(new MagickColor(red, green, blue))
    {
    }

    /// <summary>
    /// Gets or sets the blue component value of this color.
    /// </summary>
    public QuantumType B
    {
        get => Color.B;
        set => Color.B = value;
    }

    /// <summary>
    /// Gets or sets the green component value of this color.
    /// </summary>
    public QuantumType G
    {
        get => Color.G;
        set => Color.G = value;
    }

    /// <summary>
    /// Gets or sets the red component value of this color.
    /// </summary>
    public QuantumType R
    {
        get => Color.R;
        set => Color.R = value;
    }

    /// <summary>
    /// Converts the specified <see cref="MagickColor"/> to an instance of this type.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <returns>A <see cref="ColorRGB"/> instance.</returns>
    public static explicit operator ColorRGB?(MagickColor color)
        => FromMagickColor(color);

    /// <summary>
    /// Converts the specified <see cref="IMagickColor{QuantumType}"/> to an instance of this type.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <returns>A <see cref="ColorRGB"/> instance.</returns>
    public static ColorRGB? FromMagickColor(IMagickColor<QuantumType> color)
    {
        if (color is null)
            return null;

        return new ColorRGB(color);
    }

    /// <summary>
    /// Returns the complementary color for this color.
    /// </summary>
    /// <returns>A <see cref="ColorRGB"/> instance.</returns>
    public ColorRGB? ComplementaryColor()
    {
        var hsv = ColorHSV.FromMagickColor(ToMagickColor());
        if (hsv is null)
            return null;

        hsv.HueShift(180);
        return new ColorRGB(hsv.ToMagickColor());
    }
}
