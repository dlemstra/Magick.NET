// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// indicates that subsequent commands up to a DrawablePopPattern command comprise the definition
/// of a named pattern. The pattern space is assigned top left corner coordinates, a width and
/// height, and becomes its own drawing space. Anything which can be drawn may be used in a
/// pattern definition. Named patterns may be used as stroke or brush definitions.
/// </summary>
public interface IDrawablePushPattern : IDrawable
{
    /// <summary>
    /// Gets the ID of the pattern.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the X coordinate.
    /// </summary>
    double X { get; }

    /// <summary>
    /// Gets the Y coordinate.
    /// </summary>
    double Y { get; }

    /// <summary>
    /// Gets the width.
    /// </summary>
    double Width { get; }

    /// <summary>
    /// Gets the height.
    /// </summary>
    double Height { get; }
}
