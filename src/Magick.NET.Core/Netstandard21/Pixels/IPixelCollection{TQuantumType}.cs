// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETSTANDARD2_1

using System;

namespace ImageMagick;

/// <content />
public partial interface IPixelCollection<TQuantumType>
{
    /// <summary>
    /// Returns the pixels at the specified area.
    /// </summary>
    /// <param name="x">The X coordinate of the area.</param>
    /// <param name="y">The Y coordinate of the area.</param>
    /// <param name="width">The width of the area.</param>
    /// <param name="height">The height of the area.</param>
    /// <returns>A <typeparamref name="TQuantumType"/> array.</returns>
    ReadOnlySpan<TQuantumType> GetReadOnlyArea(int x, int y, uint width, uint height);

    /// <summary>
    /// Returns the pixels of the specified area.
    /// </summary>
    /// <param name="geometry">The geometry of the area.</param>
    /// <returns>A <typeparamref name="TQuantumType"/> array.</returns>
    ReadOnlySpan<TQuantumType> GetReadOnlyArea(IMagickGeometry geometry);

    /// <summary>
    /// Changes the values of the specified pixels.
    /// </summary>
    /// <param name="x">The X coordinate of the area.</param>
    /// <param name="y">The Y coordinate of the area.</param>
    /// <param name="width">The width of the area.</param>
    /// <param name="height">The height of the area.</param>
    /// <param name="values">The values of the pixels.</param>
    void SetArea(int x, int y, uint width, uint height, ReadOnlySpan<TQuantumType> values);

    /// <summary>
    /// Changes the values of the specified pixels.
    /// </summary>
    /// <param name="geometry">The geometry of the area.</param>
    /// <param name="values">The values of the pixels.</param>
    void SetArea(IMagickGeometry geometry, ReadOnlySpan<TQuantumType> values);

    /// <summary>
    /// Changes the value of the specified pixel.
    /// </summary>
    /// <param name="x">The X coordinate of the pixel.</param>
    /// <param name="y">The Y coordinate of the pixel.</param>
    /// <param name="value">The value of the pixel.</param>
    void SetPixel(int x, int y, ReadOnlySpan<TQuantumType> value);

    /// <summary>
    /// Changes the values of the specified pixels.
    /// </summary>
    /// <param name="values">The values of the pixels.</param>
    void SetPixels(ReadOnlySpan<TQuantumType> values);
}

#endif
