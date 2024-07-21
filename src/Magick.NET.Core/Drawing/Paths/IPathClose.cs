// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Adds a path element to the current path which closes the current subpath by drawing a straight
/// line from the current point to the current subpath's most recent starting point (usually, the
/// most recent moveto point).
/// </summary>
public interface IPathClose : IPath
{
}
