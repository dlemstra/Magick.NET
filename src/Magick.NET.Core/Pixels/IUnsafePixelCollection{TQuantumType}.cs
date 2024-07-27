// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

/// <summary>
/// Interface that can be used to access the individual pixels of an image.
/// </summary>
/// <typeparam name="TQuantumType">The quantum type.</typeparam>
public interface IUnsafePixelCollection<TQuantumType> : IPixelCollection<TQuantumType>
    where TQuantumType : struct, IConvertible
{
    /// <summary>
    /// Returns a pointer to the pixels of the specified area.
    /// </summary>
    /// <param name="x">The X coordinate of the area.</param>
    /// <param name="y">The Y coordinate of the area.</param>
    /// <param name="width">The width of the area.</param>
    /// <param name="height">The height of the area.</param>
    /// <returns>A pointer to the pixels of the specified area.</returns>
    IntPtr GetAreaPointer(int x, int y, uint width, uint height);

    /// <summary>
    /// Returns a pointer to the pixels of the specified area.
    /// </summary>
    /// <param name="geometry">The geometry of the area.</param>
    /// <returns>A pointer to the pixels of the specified area.</returns>
    IntPtr GetAreaPointer(IMagickGeometry geometry);
}
