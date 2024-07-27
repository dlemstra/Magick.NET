// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

/// <summary>
/// Encapsulation of the ImageMagick connected component object.
/// </summary>
/// <typeparam name="TQuantumType">The quantum type.</typeparam>
public interface IConnectedComponent<TQuantumType>
    where TQuantumType : struct, IConvertible
{
    /// <summary>
    /// Gets the pixel count of the area.
    /// </summary>
    uint Area { get; }

    /// <summary>
    /// Gets the centroid of the area.
    /// </summary>
    PointD Centroid { get; }

    /// <summary>
    /// Gets the color of the area.
    /// </summary>
    IMagickColor<TQuantumType>? Color { get; }

    /// <summary>
    /// Gets the height of the area.
    /// </summary>
    uint Height { get; }

    /// <summary>
    /// Gets the id of the area.
    /// </summary>
    int Id { get; }

    /// <summary>
    /// Gets the width of the area.
    /// </summary>
    uint Width { get; }

    /// <summary>
    /// Gets the X offset from origin.
    /// </summary>
    int X { get; }

    /// <summary>
    /// Gets the Y offset from origin.
    /// </summary>
    int Y { get; }

    /// <summary>
    /// Returns the geometry of the area of this connected component.
    /// </summary>
    /// <returns>The geometry of the area of this connected component.</returns>
    IMagickGeometry ToGeometry();

    /// <summary>
    /// Returns the geometry of the area of this connected component.
    /// </summary>
    /// <param name="extent">The number of pixels to extent the image with.</param>
    /// <returns>The geometry of the area of this connected component.</returns>
    IMagickGeometry ToGeometry(uint extent);
}
