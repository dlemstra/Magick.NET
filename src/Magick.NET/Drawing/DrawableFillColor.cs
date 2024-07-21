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

namespace ImageMagick.Drawing;

/// <summary>
/// Sets the fill color to be used for drawing filled objects.
/// </summary>
public sealed partial class DrawableFillColor : IDrawableFillColor<QuantumType>, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableFillColor"/> class.
    /// </summary>
    /// <param name="color">The color to use.</param>
    public DrawableFillColor(IMagickColor<QuantumType> color)
    {
        Throw.IfNull(nameof(color), color);

        Color = color;
    }

    /// <summary>
    /// Gets or sets the color to use.
    /// </summary>
    public IMagickColor<QuantumType> Color { get; set; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.FillColor(Color);
}
