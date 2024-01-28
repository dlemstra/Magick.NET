// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Sets the pattern used for stroking object outlines. Only local URLs("#identifier") are
/// supported at this time. These local URLs are normally created by defining a named stroke
/// pattern with DrawablePushPattern/DrawablePopPattern.
/// </summary>
public interface IDrawableStrokePatternUrl : IDrawable
{
    /// <summary>
    /// Gets the url specifying pattern ID (e.g. "#pattern_id").
    /// </summary>
    string Url { get; }
}
