// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// PrimaryInfo information.
/// </summary>
public partial class ChromaticityInfo : IChromaticityInfo
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ChromaticityInfo"/> class.
    /// </summary>
    /// <param name="red">The chromaticity red primary point.</param>
    /// <param name="green">The chromaticity green primary point.</param>
    /// <param name="blue">The chromaticity blue primary point.</param>
    /// <param name="white">The chromaticity white primary point.</param>
    public ChromaticityInfo(IPrimaryInfo red, IPrimaryInfo green, IPrimaryInfo blue, IPrimaryInfo white)
    {
        Red = red;
        Green = green;
        Blue = blue;
        White = white;
    }

    /// <summary>
    /// Gets the chromaticity blue primary point.
    /// </summary>
    public IPrimaryInfo Blue { get; }

    /// <summary>
    /// Gets the chromaticity green primary point.
    /// </summary>
    public IPrimaryInfo Green { get; }

    /// <summary>
    /// Gets the chromaticity red primary point.
    /// </summary>
    public IPrimaryInfo Red { get; }

    /// <summary>
    /// Gets the chromaticity white primary point.
    /// </summary>
    public IPrimaryInfo White { get; }
}
