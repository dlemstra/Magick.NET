// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Sets the pattern used for stroking object outlines. Only local URLs("#identifier") are
/// supported at this time. These local URLs are normally created by defining a named stroke
/// pattern with DrawablePushPattern/DrawablePopPattern.
/// </summary>
public sealed class DrawableStrokePatternUrl : IDrawableStrokePatternUrl, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableStrokePatternUrl"/> class.
    /// </summary>
    /// <param name="url">Url specifying pattern ID (e.g. "#pattern_id").</param>
    public DrawableStrokePatternUrl(string url)
    {
        Url = url;
    }

    /// <summary>
    /// Gets or sets the url specifying pattern ID (e.g. "#pattern_id").
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.StrokePatternUrl(Url);
}
