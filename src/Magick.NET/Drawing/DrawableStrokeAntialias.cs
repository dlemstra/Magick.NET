// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Controls whether stroked outlines are antialiased. Stroked outlines are antialiased by default.
/// When antialiasing is disabled stroked pixels are thresholded to determine if the stroke color
/// or underlying canvas color should be used.
/// </summary>
public sealed class DrawableStrokeAntialias : IDrawableStrokeAntialias, IDrawingWand
{
    private DrawableStrokeAntialias(bool isEnabled)
        => IsEnabled = isEnabled;

    /// <summary>
    /// Gets a new instance of the <see cref="DrawableStrokeAntialias"/> class that is disabled.
    /// </summary>
    public static DrawableStrokeAntialias Disabled
        => new DrawableStrokeAntialias(false);

    /// <summary>
    /// Gets a new instance of the <see cref="DrawableStrokeAntialias"/> class that is enabled.
    /// </summary>
    public static DrawableStrokeAntialias Enabled
        => new DrawableStrokeAntialias(true);

    /// <summary>
    /// Gets or sets a value indicating whether stroke antialiasing is enabled or disabled.
    /// </summary>
    public bool IsEnabled { get; set; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.StrokeAntialias(IsEnabled);
}
