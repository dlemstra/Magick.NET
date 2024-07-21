// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick.Drawing;

/// <summary>
/// Draws a set of paths.
/// </summary>
public sealed class DrawablePath : IDrawablePath, IDrawingWand
{
    private readonly List<IPath> _paths;

    /// <summary>
    /// Initializes a new instance of the <see cref="DrawablePath"/> class.
    /// </summary>
    /// <param name="paths">The paths to use.</param>
    public DrawablePath(params IPath[] paths)
        => _paths = new List<IPath>(paths);

    /// <summary>
    /// Initializes a new instance of the <see cref="DrawablePath"/> class.
    /// </summary>
    /// <param name="paths">The paths to use.</param>
    public DrawablePath(IEnumerable<IPath> paths)
        => _paths = new List<IPath>(paths);

    /// <summary>
    /// Gets the paths to use.
    /// </summary>
    public IReadOnlyList<IPath> Paths
        => _paths;

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
    {
        if (wand is null)
            return;

        wand.PathStart();
        foreach (var path in _paths)
            ((IDrawingWand)path).Draw(wand);
        wand.PathFinish();
    }
}
