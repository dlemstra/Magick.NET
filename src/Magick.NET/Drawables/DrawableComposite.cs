// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if Q8
using System;
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
/// Encapsulation of the DrawableCompositeImage object.
/// </summary>
public sealed class DrawableComposite : IDrawableComposite<QuantumType>, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableComposite"/> class.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="image">The image to draw.</param>
    [Obsolete($"This constructor will be removed in the next major release, use the overload with x, y, compose instead.")]
    public DrawableComposite(double x, double y, IMagickImage<QuantumType> image)
      : this(x, y, CompositeOperator.In, image)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableComposite"/> class.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="image">The image to draw.</param>
    public DrawableComposite(double x, double y, CompositeOperator compose, IMagickImage<QuantumType> image)
      : this(image)
    {
        X = x;
        Y = y;
        Width = Image.Width;
        Height = Image.Height;
        Compose = compose;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableComposite"/> class.
    /// </summary>
    /// <param name="offset">The offset from origin.</param>
    /// <param name="image">The image to draw.</param>
    [Obsolete($"This constructor will be removed in the next major release, use the overload with x, y, width, height, compose instead.")]
    public DrawableComposite(IMagickGeometry offset, IMagickImage<QuantumType> image)
      : this(offset, CompositeOperator.In, image)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableComposite"/> class.
    /// </summary>
    /// <param name="offset">The offset from origin.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="image">The image to draw.</param>
    [Obsolete($"This constructor will be removed in the next major release, use the overload with x, y, width, height, compose instead.")]
    public DrawableComposite(IMagickGeometry offset, CompositeOperator compose, IMagickImage<QuantumType> image)
      : this(image)
    {
        Throw.IfNull(nameof(offset), offset);

        X = offset.X;
        Y = offset.Y;
        Width = offset.Width;
        Height = offset.Height;
        Compose = compose;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableComposite"/> class.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="width">The width to scale the image to.</param>
    /// <param name="height">The height to scale the image to.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="image">The image to draw.</param>
    public DrawableComposite(double x, double y, double width, double height, CompositeOperator compose, IMagickImage<QuantumType> image)
      : this(image)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
        Compose = compose;
    }

    private DrawableComposite(IMagickImage<QuantumType> image)
    {
        Throw.IfNull(nameof(image), image);

        Image = image;
    }

    /// <summary>
    /// Gets or sets the X coordinate.
    /// </summary>
    public double X { get; set; }

    /// <summary>
    /// Gets or sets the Y coordinate.
    /// </summary>
    public double Y { get; set; }

    /// <summary>
    /// Gets or sets the width to scale the image to.
    /// </summary>
    public double Width { get; set; }

    /// <summary>
    /// Gets or sets the height to scale the image to.
    /// </summary>
    public double Height { get; set; }

    /// <summary>
    /// Gets or sets the composition operator.
    /// </summary>
    public CompositeOperator Compose { get; set; }

    /// <summary>
    /// Gets the composite image.
    /// </summary>
    public IMagickImage<QuantumType> Image { get; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.Composite(X, Y, Width, Height, Compose, Image);
}
