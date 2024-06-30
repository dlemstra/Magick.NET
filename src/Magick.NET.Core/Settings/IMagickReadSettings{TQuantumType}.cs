// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

/// <summary>
/// Class that contains setting for when an image is being read.
/// </summary>
/// <typeparam name="TQuantumType">The quantum type.</typeparam>
public interface IMagickReadSettings<TQuantumType> : IMagickSettings<TQuantumType>
    where TQuantumType : struct, IConvertible
{
    /// <summary>
    /// Gets or sets the defines that should be set before the image is read.
    /// </summary>
    IReadDefines? Defines { get; set; }

    /// <summary>
    /// Gets or sets the specified area to extract from the image.
    /// </summary>
    IMagickGeometry? ExtractArea { get; set; }

    /// <summary>
    /// Gets or sets the index of the image to read from a multi layer/frame image.
    /// </summary>
    int? FrameIndex { get; set; }

    /// <summary>
    /// Gets or sets the number of images to read from a multi layer/frame image.
    /// </summary>
    int? FrameCount { get; set; }

    /// <summary>
    /// Gets or sets the height.
    /// </summary>
    int? Height { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the exif profile should be used to update some of the
    /// properties of the image (e.g. <see cref="IMagickImage.Density"/>, <see cref="IMagickImage.Orientation"/>).
    /// </summary>
    bool SyncImageWithExifProfile { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the monochrome reader shoul be used. This is
    /// supported by: <see cref="MagickFormat.Pcl"/>, <see cref="MagickFormat.Pdf"/> ,
    /// <see cref="MagickFormat.Ps"/>  and <see cref="MagickFormat.Xps"/>.
    /// </summary>
    bool UseMonochrome { get; set; }

    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    int? Width { get; set; }
}
