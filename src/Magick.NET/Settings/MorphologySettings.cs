// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Class that contains setting for the morphology operation.
/// </summary>
public sealed class MorphologySettings : IMorphologySettings
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MorphologySettings"/> class.
    /// </summary>
    public MorphologySettings()
    {
        Channels = Channels.Composite;
        Iterations = 1;
        KernelArguments = string.Empty;
    }

    /// <summary>
    /// Gets or sets the channels to apply the kernel to.
    /// </summary>
    public Channels Channels { get; set; }

    /// <summary>
    /// Gets or sets the bias to use when the method is <see cref="MorphologyMethod.Convolve"/>.
    /// </summary>
    public Percentage? ConvolveBias { get; set; }

    /// <summary>
    /// Gets or sets the scale to use when the method is <see cref="MorphologyMethod.Convolve"/>.
    /// </summary>
    public IMagickGeometry? ConvolveScale { get; set; }

    /// <summary>
    /// Gets or sets the number of iterations.
    /// </summary>
    public int Iterations { get; set; }

    /// <summary>
    /// Gets or sets built-in kernel.
    /// </summary>
    public Kernel Kernel { get; set; }

    /// <summary>
    /// Gets or sets kernel arguments.
    /// </summary>
    public string KernelArguments { get; set; }

    /// <summary>
    /// Gets or sets the morphology method.
    /// </summary>
    public MorphologyMethod Method { get; set; }

    /// <summary>
    /// Gets or sets user suplied kernel.
    /// </summary>
    public string? UserKernel { get; set; }
}
