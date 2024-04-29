// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Class that contains setting for the morphology operation.
/// </summary>
public interface IMorphologySettings
{
    /// <summary>
    /// Gets or sets the channels to apply the kernel to.
    /// </summary>
    Channels Channels { get; set; }

    /// <summary>
    /// Gets or sets the bias to use when the method is Convolve.
    /// </summary>
    Percentage? ConvolveBias { get; set; }

    /// <summary>
    /// Gets or sets the scale to use when the method is Convolve.
    /// </summary>
    IMagickGeometry? ConvolveScale { get; set; }

    /// <summary>
    /// Gets or sets the number of iterations.
    /// </summary>
    int Iterations { get; set; }

    /// <summary>
    /// Gets or sets built-in kernel.
    /// </summary>
    Kernel Kernel { get; set; }

    /// <summary>
    /// Gets or sets kernel arguments.
    /// </summary>
    string KernelArguments { get; set; }

    /// <summary>
    /// Gets or sets the morphology method.
    /// </summary>
    MorphologyMethod Method { get; set; }

    /// <summary>
    /// Gets or sets user supplied kernel.
    /// </summary>
    string? UserKernel { get; set; }
}
