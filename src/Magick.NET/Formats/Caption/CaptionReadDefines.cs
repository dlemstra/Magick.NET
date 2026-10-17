// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick.Formats;

/// <summary>
/// Class for defines that are used when a <see cref="MagickFormat.Caption"/> image is read.
/// </summary>
public sealed class CaptionReadDefines : IReadDefines
{
    /// <summary>
    /// Gets or sets a value indicating whether text should be split when needed to fit the caption canvas (caption:split).
    /// Text is not split by default.
    /// </summary>
    public bool? Split { get; set; }

    /// <summary>
    /// Gets the format where the defines are for.
    /// </summary>
    public MagickFormat Format
        => MagickFormat.Caption;

    /// <summary>
    /// Gets or sets a the maximum font pointsize (caption:max-pointsize).
    /// </summary>
    public double? MaxFontPointsize { get; set; }

    /// <summary>
    /// Gets or sets a the start font pointsize (caption:start-pointsize).
    /// </summary>
    public double? StartFontPointsize { get; set; }

    /// <summary>
    /// Gets the defines that should be set as a define on an image.
    /// </summary>
    public IEnumerable<IDefine> Defines
    {
        get
        {
            if (MaxFontPointsize.HasValue)
                yield return new MagickDefine(Format, "max-pointsize", MaxFontPointsize.Value);

            if (StartFontPointsize.HasValue)
                yield return new MagickDefine(Format, "start-pointsize", StartFontPointsize.Value);

            if (Split.HasValue)
                yield return new MagickDefine(Format, "split", Split.Value);
        }
    }
}
