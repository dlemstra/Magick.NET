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
/// Class that represents a HSL color.
/// </summary>
public sealed class ColorHSL : ColorBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ColorHSL"/> class.
    /// </summary>
    /// <param name="hue">Hue component value of this color.</param>
    /// <param name="saturation">Saturation component value of this color.</param>
    /// <param name="lightness">Lightness component value of this color.</param>
    public ColorHSL(double hue, double saturation, double lightness)
          : base(new MagickColor(0, 0, 0))
    {
        Hue = hue;
        Saturation = saturation;
        Lightness = lightness;
    }

    private ColorHSL(IMagickColor<QuantumType> color)
      : base(color)
    {
        Initialize(color.R, color.G, color.B);
    }

    /// <summary>
    /// Gets or sets the hue component value of this color.
    /// </summary>
    public double Hue { get; set; }

    /// <summary>
    /// Gets or sets the lightness component value of this color.
    /// </summary>
    public double Lightness { get; set; }

    /// <summary>
    /// Gets or sets the saturation component value of this color.
    /// </summary>
    public double Saturation { get; set; }

    /// <summary>
    /// Converts the specified <see cref="MagickColor"/> to an instance of this type.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <returns>A <see cref="ColorHSL"/> instance.</returns>
    public static explicit operator ColorHSL?(MagickColor color)
        => FromMagickColor(color);

    /// <summary>
    /// Converts the specified <see cref="IMagickColor{QuantumType}"/> to an instance of this type.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <returns>A <see cref="ColorHSL"/> instance.</returns>
    public static ColorHSL? FromMagickColor(IMagickColor<QuantumType> color)
    {
        if (color is null)
            return null;

        return new ColorHSL(color);
    }

    /// <summary>
    /// Updates the color value in an inherited class.
    /// </summary>
    protected override void UpdateColor()
    {
        double c;
        var h = Hue * 360.0;
        if (Lightness <= 0.5)
            c = 2.0 * Lightness * Saturation;
        else
            c = (2.0 - (2.0 * Lightness)) * Saturation;
        var min = Lightness - (0.5 * c);
        h -= 360.0 * Math.Floor(h / 360.0);
        h /= 60.0;
        var x = c * (1.0 - Math.Abs(h - (2.0 * Math.Floor(h / 2.0)) - 1.0));
        switch ((int)Math.Floor(h))
        {
            case 0:
            default:
                Color.R = Quantum.ScaleToQuantum(min + c);
                Color.G = Quantum.ScaleToQuantum(min + x);
                Color.B = Quantum.ScaleToQuantum(min);
                break;
            case 1:
                Color.R = Quantum.ScaleToQuantum(min + x);
                Color.G = Quantum.ScaleToQuantum(min + c);
                Color.B = Quantum.ScaleToQuantum(min);
                break;
            case 2:
                Color.R = Quantum.ScaleToQuantum(min);
                Color.G = Quantum.ScaleToQuantum(min + c);
                Color.B = Quantum.ScaleToQuantum(min + x);
                break;
            case 3:
                Color.R = Quantum.ScaleToQuantum(min);
                Color.G = Quantum.ScaleToQuantum(min + x);
                Color.B = Quantum.ScaleToQuantum(min + c);
                break;
            case 4:
                Color.R = Quantum.ScaleToQuantum(min + x);
                Color.G = Quantum.ScaleToQuantum(min);
                Color.B = Quantum.ScaleToQuantum(min + c);
                break;
            case 5:
                Color.R = Quantum.ScaleToQuantum(min + c);
                Color.G = Quantum.ScaleToQuantum(min);
                Color.B = Quantum.ScaleToQuantum(min + x);
                break;
        }
    }

    private void Initialize(double red, double green, double blue)
    {
        var quantumScale = 1.0 / Quantum.Max;
        var max = Math.Max(red, Math.Max(green, blue)) * quantumScale;
        var min = Math.Min(red, Math.Min(green, blue)) * quantumScale;
        var c = max - min;

        Lightness = (max + min) / 2.0;
        if (c <= 0.0)
        {
            Hue = 0.0;
            Saturation = 0.0;
        }
        else
        {
            if (Math.Abs(max - (quantumScale * red)) < double.Epsilon)
            {
                Hue = ((quantumScale * green) - (quantumScale * blue)) / c;
                if ((quantumScale * green) < (quantumScale * blue))
                    Hue += 6.0;
            }
            else if (Math.Abs(max - (quantumScale * green)) < double.Epsilon)
                Hue = 2.0 + (((quantumScale * blue) - (quantumScale * red)) / c);
            else
                Hue = 4.0 + (((quantumScale * red) - (quantumScale * green)) / c);
            Hue *= 60.0 / 360.0;
            if (Lightness <= 0.5)
                Saturation = c / (2.0 * Lightness);
            else
                Saturation = c / (2.0 - (2.0 * Lightness));
        }
    }
}
