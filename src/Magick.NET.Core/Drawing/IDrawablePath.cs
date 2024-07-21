// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick;

/// <summary>
/// Draws a set of paths.
/// </summary>
public interface IDrawablePath : IDrawable
{
    /// <summary>
    /// Gets the paths to use.
    /// </summary>
    IReadOnlyList<IPath> Paths { get; }
}
