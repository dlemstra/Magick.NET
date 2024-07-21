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
///  Class that represents a gray color.
/// </summary>
public sealed class ColorGray : ColorBase
{
    private double _shade;

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorGray"/> class.
    /// </summary>
    /// <param name="shade">Value between 0.0 - 1.0.</param>
    public ColorGray(double shade)
      : base(new MagickColor(0, 0, 0))
    {
        Throw.IfTrue(nameof(shade), shade < 0.0 || shade > 1.0, "Invalid shade specified");

        _shade = shade;
    }

    private ColorGray(IMagickColor<QuantumType> color)
      : base(color)
    {
        _shade =
            (0.212656 * Quantum.ScaleToDouble(color.R)) +
            (0.715158 * Quantum.ScaleToDouble(color.G)) +
            (0.072186 * Quantum.ScaleToDouble(color.B));
    }

    /// <summary>
    /// Gets or sets the shade of this color (value between 0.0 - 1.0).
    /// </summary>
    public double Shade
    {
        get => _shade;

        set
        {
            if (value < 0.0 || value > 1.0)
                return;

            _shade = value;
        }
    }

    /// <summary>
    /// Converts the specified <see cref="MagickColor"/> to an instance of this type.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <returns>A <see cref="ColorGray"/> instance.</returns>
    public static explicit operator ColorGray?(MagickColor color)
        => FromMagickColor(color);

    /// <summary>
    /// Converts the specified <see cref="MagickColor"/> to an instance of this type.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <returns>A <see cref="ColorGray"/> instance.</returns>
    public static ColorGray? FromMagickColor(MagickColor color)
    {
        if (color is null)
            return null;

        return new ColorGray(color);
    }

    /// <summary>
    /// Updates the color value in an inherited class.
    /// </summary>
    protected override void UpdateColor()
    {
        var gray = Quantum.ScaleToQuantum(_shade);
        Color.R = gray;
        Color.G = gray;
        Color.B = gray;
    }
}
