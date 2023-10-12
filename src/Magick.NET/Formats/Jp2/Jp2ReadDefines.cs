// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick.Formats;

/// <summary>
/// Class for defines that are used when a <see cref="MagickFormat.Jp2"/> image is read.
/// </summary>
public sealed class Jp2ReadDefines : IReadDefines
{
    /// <summary>
    /// Gets the format where the defines are for.
    /// </summary>
    public MagickFormat Format
        => MagickFormat.Jp2;

    /// <summary>
    /// Gets or sets the flag that assumes an alpha channel when the image has 2 or 4 channels (jp2:assume-alpha).
    /// </summary>
    public bool? AssumeAlpha { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of quality layers to decode (jp2:quality-layers).
    /// </summary>
    public int? QualityLayers { get; set; }

    /// <summary>
    /// Gets or sets the number of highest resolution levels to be discarded (jp2:reduce-factor).
    /// </summary>
    public int? ReduceFactor { get; set; }

    /// <summary>
    /// Gets the defines that should be set as a define on an image.
    /// </summary>
    public IEnumerable<IDefine> Defines
    {
        get
        {
            if (AssumeAlpha.HasValue)
                yield return new MagickDefine(Format, "assume-alpha", AssumeAlpha.Value);

            if (QualityLayers.HasValue)
                yield return new MagickDefine(Format, "quality-layers", QualityLayers.Value);

            if (ReduceFactor.HasValue)
                yield return new MagickDefine(Format, "reduce-factor", ReduceFactor.Value);
        }
    }
}
