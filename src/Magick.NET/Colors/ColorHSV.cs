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
/// Class that represents a HSV color.
/// </summary>
public sealed class ColorHSV : ColorBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ColorHSV"/> class.
    /// </summary>
    /// <param name="hue">Hue component value of this color.</param>
    /// <param name="saturation">Saturation component value of this color.</param>
    /// <param name="value">Value component value of this color.</param>
    public ColorHSV(double hue, double saturation, double value)
          : base(new MagickColor(0, 0, 0))
    {
        Hue = hue;
        Saturation = saturation;
        Value = value;
    }

    private ColorHSV(IMagickColor<QuantumType> color)
      : base(color)
    {
        Initialize(color.R, color.G, color.B);
    }

    /// <summary>
    /// Gets or sets the hue component value of this color.
    /// </summary>
    public double Hue { get; set; }

    /// <summary>
    /// Gets or sets the saturation component value of this color.
    /// </summary>
    public double Saturation { get; set; }

    /// <summary>
    /// Gets or sets the value component value of this color.
    /// </summary>
    public double Value { get; set; }

    /// <summary>
    /// Converts the specified <see cref="MagickColor"/> to an instance of this type.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <returns>A <see cref="ColorHSV"/> instance.</returns>
    public static explicit operator ColorHSV?(MagickColor color)
        => FromMagickColor(color);

    /// <summary>
    /// Converts the specified <see cref="IMagickColor{QuantumType}"/> to an instance of this type.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <returns>A <see cref="ColorHSV"/> instance.</returns>
    public static ColorHSV? FromMagickColor(IMagickColor<QuantumType> color)
    {
        if (color is null)
            return null;

        return new ColorHSV(color);
    }

    /// <summary>
    /// Performs a hue shift with the specified degrees.
    /// </summary>
    /// <param name="degrees">The degrees.</param>
    public void HueShift(double degrees)
    {
        Hue += degrees / 360.0;

        while (Hue >= 1.0)
            Hue -= 1.0;

        while (Hue < 0.0)
            Hue += 1.0;
    }

    /// <summary>
    /// Updates the color value in an inherited class.
    /// </summary>
    protected override void UpdateColor()
    {
        if (Math.Abs(Saturation) < double.Epsilon)
        {
            Color.R = Color.G = Color.B = Quantum.ScaleToQuantum(Value);
            return;
        }

        var h = 6.0 * (Hue - Math.Floor(Hue));
        var f = h - Math.Floor(h);
        var p = Value * (1.0 - Saturation);
        var q = Value * (1.0 - (Saturation * f));
        var t = Value * (1.0 - (Saturation * (1.0 - f)));
        switch ((int)h)
        {
            case 0:
            default:
                Color.R = Quantum.ScaleToQuantum(Value);
                Color.G = Quantum.ScaleToQuantum(t);
                Color.B = Quantum.ScaleToQuantum(p);
                break;
            case 1:
                Color.R = Quantum.ScaleToQuantum(q);
                Color.G = Quantum.ScaleToQuantum(Value);
                Color.B = Quantum.ScaleToQuantum(p);
                break;
            case 2:
                Color.R = Quantum.ScaleToQuantum(p);
                Color.G = Quantum.ScaleToQuantum(Value);
                Color.B = Quantum.ScaleToQuantum(t);
                break;
            case 3:
                Color.R = Quantum.ScaleToQuantum(p);
                Color.G = Quantum.ScaleToQuantum(q);
                Color.B = Quantum.ScaleToQuantum(Value);
                break;
            case 4:
                Color.R = Quantum.ScaleToQuantum(t);
                Color.G = Quantum.ScaleToQuantum(p);
                Color.B = Quantum.ScaleToQuantum(Value);
                break;
            case 5:
                Color.R = Quantum.ScaleToQuantum(Value);
                Color.G = Quantum.ScaleToQuantum(p);
                Color.B = Quantum.ScaleToQuantum(q);
                break;
        }
    }

    private void Initialize(double red, double green, double blue)
    {
        Hue = 0.0;
        Saturation = 0.0;
        Value = 0.0;

        var min = Math.Min(Math.Min(red, green), blue);
        var max = Math.Max(Math.Max(red, green), blue);

        if (Math.Abs(max) < double.Epsilon)
            return;
        var delta = max - min;
        Saturation = delta / max;
        Value = (1.0 / Quantum.Max) * max;
        if (Math.Abs(delta) < double.Epsilon)
            return;
        if (Math.Abs(red - max) < double.Epsilon)
            Hue = (green - blue) / delta;
        else if (Math.Abs(green - max) < double.Epsilon)
            Hue = 2.0 + ((blue - red) / delta);
        else
            Hue = 4.0 + ((red - green) / delta);
        Hue /= 6.0;
        if (Hue < 0.0)
            Hue += 1.0;
    }
}
