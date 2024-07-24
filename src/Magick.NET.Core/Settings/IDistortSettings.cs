// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Class that contains setting for the distort operation.
/// </summary>
public interface IDistortSettings
{
    /// <summary>
    /// Gets or sets a value indicating whether distort attempt to 'bestfit' the size of the resulting image.
    /// </summary>
    bool Bestfit { get; set; }

    /// <summary>
    /// Gets the distort method to use.
    /// </summary>
    DistortMethod Method { get; }

    /// <summary>
    /// Gets or sets a value to scale the size of the output canvas by this amount to provide a method of
    /// Zooming, and for super-sampling the results.
    /// </summary>
    double? Scale { get; set; }

    /// <summary>
    /// Gets or sets the viewport that directly set the output image canvas area and offest to use for the
    /// resulting image, rather than use the original images canvas, or a calculated 'bestfit' canvas.
    /// </summary>
    IMagickGeometry? Viewport { get; set; }
}
