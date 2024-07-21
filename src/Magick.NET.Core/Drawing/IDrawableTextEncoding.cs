// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Text;

namespace ImageMagick.Drawing;

/// <summary>
/// Encapsulation of the DrawableTextEncoding object.
/// </summary>
public interface IDrawableTextEncoding : IDrawable
{
    /// <summary>
    /// Gets the encoding of the text.
    /// </summary>
    Encoding Encoding { get; }
}
