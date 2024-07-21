// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Text;

namespace ImageMagick.Drawing;

/// <summary>
/// Encapsulation of the DrawableTextEncoding object.
/// </summary>
public sealed class DrawableTextEncoding : IDrawableTextEncoding, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableTextEncoding"/> class.
    /// </summary>
    /// <param name="encoding">Encoding to use.</param>
    public DrawableTextEncoding(Encoding encoding)
    {
        Throw.IfNull(nameof(encoding), encoding);

        Encoding = encoding;
    }

    /// <summary>
    /// Gets the encoding of the text.
    /// </summary>
    public Encoding Encoding { get; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.TextEncoding(Encoding);
}
