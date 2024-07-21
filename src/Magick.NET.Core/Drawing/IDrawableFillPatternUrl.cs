// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Sets the URL to use as a fill pattern for filling objects. Only local URLs("#identifier") are
/// supported at this time. These local URLs are normally created by defining a named fill pattern
/// with <see cref="IDrawablePushPattern"/>/<see cref="IDrawablePopPattern"/>.
/// </summary>
public interface IDrawableFillPatternUrl : IDrawable
{
    /// <summary>
    /// Gets the url specifying pattern ID (e.g. "#pattern_id").
    /// </summary>
    string Url { get; }
}
