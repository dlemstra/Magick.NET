// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Controls whether text is antialiased. Text is antialiased by default.
/// </summary>
public interface IDrawableTextAntialias : IDrawable
{
    /// <summary>
    /// Gets a value indicating whether text antialiasing is enabled or disabled.
    /// </summary>
    bool IsEnabled { get; }
}
