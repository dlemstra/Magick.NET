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
/// Class that represents a YUV color.
/// </summary>
public sealed class ColorYUV : ColorBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ColorYUV"/> class.
    /// </summary>
    /// <param name="y">Y component value of this color.</param>
    /// <param name="u">U component value of this color.</param>
    /// <param name="v">V component value of this color.</param>
    public ColorYUV(double y, double u, double v)
      : base(new MagickColor(0, 0, 0))
    {
        Y = y;
        U = u;
        V = v;
    }

    private ColorYUV(IMagickColor<QuantumType> color)
      : base(color)
    {
        Y = (1.0 / Quantum.Max) * ((0.298839 * color.R) + (0.586811 * color.G) + (0.11435 * color.B));
        U = ((1.0 / Quantum.Max) * ((-0.147 * color.R) - (0.289 * color.G) + (0.436 * color.B))) + 0.5;
        V = ((1.0 / Quantum.Max) * ((0.615 * color.R) - (0.515 * color.G) - (0.1 * color.B))) + 0.5;
    }

    /// <summary>
    /// Gets or sets the U component value of this color. (value beteeen -0.5 and 0.5).
    /// </summary>
    public double U { get; set; }

    /// <summary>
    /// Gets or sets the V component value of this color. (value beteeen -0.5 and 0.5).
    /// </summary>
    public double V { get; set; }

    /// <summary>
    /// Gets or sets the Y component value of this color. (value beteeen 0.0 and 1.0).
    /// </summary>
    public double Y { get; set; }

    /// <summary>
    /// Converts the specified <see cref="MagickColor"/> to an instance of this type.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <returns>A <see cref="ColorYUV"/> instance.</returns>
    public static explicit operator ColorYUV?(MagickColor color)
        => FromMagickColor(color);

    /// <summary>
    /// Converts the specified <see cref="IMagickColor{QuantumType}"/> to an instance of this type.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <returns>A <see cref="ColorYUV"/> instance.</returns>
    public static ColorYUV? FromMagickColor(IMagickColor<QuantumType> color)
    {
        if (color is null)
            return null;

        return new ColorYUV(color);
    }

    /// <summary>
    /// Updates the color value in an inherited class.
    /// </summary>
    protected override void UpdateColor()
    {
        Color.R = Quantum.ScaleToQuantum(Y - (3.945707070708279e-05 * (U - 0.5)) + (1.1398279671717170825 * (V - 0.5)));
        Color.G = Quantum.ScaleToQuantum(Y - (0.3946101641414141437 * (U - 0.5)) - (0.5805003156565656797 * (V - 0.5)));
        Color.B = Quantum.ScaleToQuantum(Y + (2.0319996843434342537 * (U - 0.5)) - (4.813762626262513e-04 * (V - 0.5)));
    }
}
