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
/// Class that represents a monochrome color.
/// </summary>
public sealed class ColorMono : ColorBase
{
    private ColorMono(bool isBlack)
      : base(isBlack ? MagickColors.Black : MagickColors.White)
    {
        IsBlack = isBlack;
    }

    private ColorMono(IMagickColor<QuantumType> color)
      : base(color)
    {
        if (color.Equals(MagickColors.Black))
            IsBlack = true;
        else if (color.Equals(MagickColors.White))
            IsBlack = false;
        else
            throw new ArgumentException("Invalid color specified.", nameof(color));
    }

    /// <summary>
    /// Gets a new instance of the <see cref="ColorMono"/> class that is black.
    /// </summary>
    public static ColorMono Black
        => new ColorMono(true);

    /// <summary>
    /// Gets a new instance of the <see cref="ColorMono"/> class that is white.
    /// </summary>
    public static ColorMono White
        => new ColorMono(false);

    /// <summary>
    /// Gets or sets a value indicating whether the color is black or white.
    /// </summary>
    public bool IsBlack { get; set; }

    /// <summary>
    /// Converts the specified <see cref="MagickColor"/> to an instance of this type.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <returns>A <see cref="ColorMono"/> instance.</returns>
    public static explicit operator ColorMono?(MagickColor color)
        => FromMagickColor(color);

    /// <summary>
    /// Converts the specified <see cref="IMagickColor{QuantumType}"/> to an instance of this type.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <returns>A <see cref="ColorMono"/> instance.</returns>
    public static ColorMono? FromMagickColor(IMagickColor<QuantumType> color)
    {
        if (color is null)
            return null;

        return new ColorMono(color);
    }

    /// <summary>
    /// Updates the color value in an inherited class.
    /// </summary>
    protected override void UpdateColor()
    {
        var color = IsBlack ? (QuantumType)0.0 : Quantum.Max;
        Color.R = color;
        Color.G = color;
        Color.B = color;
    }
}
