// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Class that can be used to create <see cref="IMagickGeometry"/> instances.
/// </summary>
public interface IMagickGeometryFactory
{
    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickGeometry"/>.
    /// </summary>
    /// <returns>A new <see cref="IMagickGeometry"/> instance.</returns>
    IMagickGeometry Create();

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickGeometry"/>.
    /// </summary>
    /// <param name="widthAndHeight">The width and height.</param>
    /// <returns>A new <see cref="IMagickGeometry"/> instance.</returns>
    IMagickGeometry Create(int widthAndHeight);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickGeometry"/>.
    /// </summary>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <returns>A new <see cref="IMagickGeometry"/> instance.</returns>
    IMagickGeometry Create(int width, int height);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickGeometry"/>.
    /// </summary>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <returns>A new <see cref="IMagickGeometry"/> instance.</returns>
    IMagickGeometry Create(int x, int y, int width, int height);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickGeometry"/>.
    /// </summary>
    /// <param name="percentageWidth">The percentage of the width.</param>
    /// <param name="percentageHeight">The percentage of the height.</param>
    /// <returns>A new <see cref="IMagickGeometry"/> instance.</returns>
    IMagickGeometry Create(Percentage percentageWidth, Percentage percentageHeight);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickGeometry"/>.
    /// </summary>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="percentageWidth">The percentage of the width.</param>
    /// <param name="percentageHeight">The percentage of the height.</param>
    /// <returns>A new <see cref="IMagickGeometry"/> instance.</returns>
    IMagickGeometry Create(int x, int y, Percentage percentageWidth, Percentage percentageHeight);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickGeometry"/>.
    /// </summary>
    /// <param name="value">Geometry specifications in the form: &lt;width&gt;x&lt;height&gt;
    /// {+-}&lt;xoffset&gt;{+-}&lt;yoffset&gt; (where width, height, xoffset, and yoffset are numbers).</param>
    /// <returns>A new <see cref="IMagickGeometry"/> instance.</returns>
    IMagickGeometry Create(string value);

    /// <summary>
    /// Initializes a new <see cref="IMagickGeometry"/> instance using the specified page size.
    /// </summary>
    /// <param name="pageSize">The page size.</param>
    /// <returns>A <see cref="IMagickGeometry"/> instance that represents the specified page size at 72 dpi.</returns>
    IMagickGeometry CreateFromPageSize(string pageSize);
}
