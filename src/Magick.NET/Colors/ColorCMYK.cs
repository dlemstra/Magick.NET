// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

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

namespace ImageMagick.Colors;

/// <summary>
/// Class that represents a CMYK color.
/// </summary>
public sealed class ColorCMYK : ColorBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ColorCMYK"/> class.
    /// </summary>
    /// <param name="cyan">Cyan component value of this color.</param>
    /// <param name="magenta">Magenta component value of this color.</param>
    /// <param name="yellow">Yellow component value of this color.</param>
    /// <param name="key">Key (black) component value of this color.</param>
    public ColorCMYK(Percentage cyan, Percentage magenta, Percentage yellow, Percentage key)
      : base(new MagickColor(PercentageHelper.ToQuantumType(nameof(cyan), cyan), PercentageHelper.ToQuantumType(nameof(magenta), magenta), PercentageHelper.ToQuantumType(nameof(yellow), yellow), PercentageHelper.ToQuantumType(nameof(key), key), Quantum.Max))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorCMYK"/> class.
    /// </summary>
    /// <param name="cyan">Cyan component value of this color.</param>
    /// <param name="magenta">Magenta component value of this color.</param>
    /// <param name="yellow">Yellow component value of this color.</param>
    /// <param name="key">Key (black) component value of this color.</param>
    /// <param name="alpha">Alpha component value of this color.</param>
    public ColorCMYK(Percentage cyan, Percentage magenta, Percentage yellow, Percentage key, Percentage alpha)
      : base(new MagickColor(PercentageHelper.ToQuantumType(nameof(cyan), cyan), PercentageHelper.ToQuantumType(nameof(magenta), magenta), PercentageHelper.ToQuantumType(nameof(yellow), yellow), PercentageHelper.ToQuantumType(nameof(key), key), PercentageHelper.ToQuantumType(nameof(alpha), alpha)))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorCMYK"/> class.
    /// </summary>
    /// <param name="cyan">Cyan component value of this color.</param>
    /// <param name="magenta">Magenta component value of this color.</param>
    /// <param name="yellow">Yellow component value of this color.</param>
    /// <param name="key">Key (black) component value of this color.</param>
    public ColorCMYK(QuantumType cyan, QuantumType magenta, QuantumType yellow, QuantumType key)
      : base(new MagickColor(cyan, magenta, yellow, key, Quantum.Max))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorCMYK"/> class.
    /// </summary>
    /// <param name="cyan">Cyan component value of this color.</param>
    /// <param name="magenta">Magenta component value of this color.</param>
    /// <param name="yellow">Yellow component value of this color.</param>
    /// <param name="key">Key (black) component value of this color.</param>
    /// <param name="alpha">Alpha component value of this color.</param>
    public ColorCMYK(QuantumType cyan, QuantumType magenta, QuantumType yellow, QuantumType key, QuantumType alpha)
      : base(new MagickColor(cyan, magenta, yellow, key, alpha))
    {
    }

#if Q8
    /// <summary>
    /// Initializes a new instance of the <see cref="ColorCMYK"/> class.
    /// </summary>
    /// <param name="color">The CMYK hex string or name of the color (http://www.imagemagick.org/script/color.php).
    /// For example: #F000, #FF000000.</param>
#elif Q16 || Q16HDRI
    /// <summary>
    /// Initializes a new instance of the <see cref="ColorCMYK"/> class.
    /// </summary>
    /// <param name="color">The CMYK hex string or name of the color (http://www.imagemagick.org/script/color.php).
    /// For example: #F000, #FF000000, #FFFF000000000000.</param>
#else
#error Not implemented!
#endif
    public ColorCMYK(string color)
      : base(CreateColor(color))
    {
    }

    private ColorCMYK(IMagickColor<QuantumType> color)
      : base(color)
    {
    }

    /// <summary>
    /// Gets or sets the alpha component value of this color.
    /// </summary>
    public QuantumType A
    {
        get { return Color.A; }
        set { Color.A = value; }
    }

    /// <summary>
    /// Gets or sets the cyan component value of this color.
    /// </summary>
    public QuantumType C
    {
        get { return Color.R; }
        set { Color.R = value; }
    }

    /// <summary>
    /// Gets or sets the key (black) component value of this color.
    /// </summary>
    public QuantumType K
    {
        get { return Color.K; }
        set { Color.K = value; }
    }

    /// <summary>
    /// Gets or sets the magenta component value of this color.
    /// </summary>
    public QuantumType M
    {
        get { return Color.G; }
        set { Color.G = value; }
    }

    /// <summary>
    /// Gets or sets the yellow component value of this color.
    /// </summary>
    public QuantumType Y
    {
        get { return Color.B; }
        set { Color.B = value; }
    }

    /// <summary>
    /// Converts the specified <see cref="MagickColor"/> to an instance of this type.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <returns>A <see cref="ColorCMYK"/> instance.</returns>
    public static explicit operator ColorCMYK?(MagickColor color)
        => FromMagickColor(color);

    /// <summary>
    /// Converts the specified <see cref="IMagickColor{QuantumType}"/> to an instance of this type.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <returns>A <see cref="ColorCMYK"/> instance.</returns>
    public static ColorCMYK? FromMagickColor(IMagickColor<QuantumType> color)
    {
        if (color is null)
            return null;

        return new ColorCMYK(color);
    }

    private static IMagickColor<QuantumType> CreateColor(string color)
    {
        Throw.IfNullOrEmpty(nameof(color), color);

        if (color[0] == '#')
        {
            if (!HexColor.TryParse(color, out var colors))
                throw new ArgumentException("Invalid hex value.", nameof(color));

            if (colors.Count == 4)
                return new MagickColor(colors[0], colors[1], colors[2], colors[3], Quantum.Max);
        }

        throw new ArgumentException("Invalid color specified", nameof(color));
    }
}
