// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

/// <summary>
/// Encapsulation of the ImageMagick geometry object.
/// </summary>
public interface IMagickGeometry : IEquatable<IMagickGeometry?>, IComparable<IMagickGeometry?>
{
    /// <summary>
    /// Gets a value indicating whether the value is an aspect ratio.
    /// </summary>
    bool AspectRatio { get; }

    /// <summary>
    /// Gets or sets a value indicating whether the image is resized based on the smallest fitting dimension (^).
    /// </summary>
    bool FillArea { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the image is resized if image is greater than size (&gt;).
    /// </summary>
    bool Greater { get; set; }

    /// <summary>
    /// Gets or sets the height of the geometry.
    /// </summary>
    uint Height { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the image is resized without preserving aspect ratio (!).
    /// </summary>
    bool IgnoreAspectRatio { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the width and height are expressed as percentages.
    /// </summary>
    bool IsPercentage { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the image is resized if the image is less than size (&lt;).
    /// </summary>
    bool Less { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the image is resized using a pixel area count limit (@).
    /// </summary>
    bool LimitPixels { get; set; }

    /// <summary>
    /// Gets or sets the width of the geometry.
    /// </summary>
    uint Width { get; set; }

    /// <summary>
    /// Gets or sets the X offset from origin.
    /// </summary>
    int X { get; set; }

    /// <summary>
    /// Gets or sets the Y offset from origin.
    /// </summary>
    int Y { get; set; }

    /// <summary>
    /// Initializes the geometry using the specified value.
    /// </summary>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    void Initialize(int x, int y, uint width, uint height);

    /// <summary>
    /// Returns a string that represents the current <see cref="IMagickGeometry"/>.
    /// </summary>
    /// <returns>A string that represents the current <see cref="IMagickGeometry"/>.</returns>
    string ToString();
}
