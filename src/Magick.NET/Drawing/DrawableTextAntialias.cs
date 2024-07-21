// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Controls whether text is antialiased. Text is antialiased by default.
/// </summary>
public sealed class DrawableTextAntialias : IDrawableTextAntialias, IDrawingWand
{
    private DrawableTextAntialias(bool isEnabled)
    {
        IsEnabled = isEnabled;
    }

    /// <summary>
    /// Gets a new instance of the <see cref="DrawableTextAntialias"/> class that is disabled.
    /// </summary>
    public static DrawableTextAntialias Disabled
        => new DrawableTextAntialias(false);

    /// <summary>
    /// Gets a new instance of the <see cref="DrawableTextAntialias"/> class that is enabled.
    /// </summary>
    public static DrawableTextAntialias Enabled
        => new DrawableTextAntialias(true);

    /// <summary>
    /// Gets a value indicating whether text antialiasing is enabled or disabled.
    /// </summary>
    public bool IsEnabled { get; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.TextAntialias(IsEnabled);
}
