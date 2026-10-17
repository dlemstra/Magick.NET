// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick.Formats;

/// <summary>
/// Class for defines that are used when a <see cref="MagickFormat.Jxl"/> image is written.
/// </summary>
public sealed class JxlWriteDefines : IWriteDefines
{
    /// <summary>
    /// Gets or sets the JPEG XL decoding speed, from 0 (slowest) to 4 (fastest) (jxl:decoding-speed).
    /// </summary>
    public int? DecodingSpeed { get; set; }

    /// <summary>
    /// Gets or sets the JPEG XL encoder frame distance, from 0 to 25 (jxl:distance).
    /// When set, this takes precedence over <see cref="Quality"/>.
    /// </summary>
    public double? Distance { get; set; }

    /// <summary>
    /// Gets or sets the jpeg-xl encoding effort. Valid values are in the range of 3 (falcon) to 9 (tortois) (jxl:effort).
    /// </summary>
    public int? Effort { get; set; }

    /// <summary>
    /// Gets the format where the defines are for.
    /// </summary>
    public MagickFormat Format
        => MagickFormat.Jxl;

    /// <summary>
    /// Gets or sets the JPEG XL encoder quality, from 0 to 100 (jxl:quality).
    /// This is used only when <see cref="Distance"/> is not set.
    /// </summary>
    public double? Quality { get; set; }

    /// <summary>
    /// Gets the defines that should be set as a define on an image.
    /// </summary>
    public IEnumerable<IDefine> Defines
    {
        get
        {
            if (DecodingSpeed.HasValue)
                yield return new MagickDefine(Format, "decoding-speed", DecodingSpeed.Value);

            if (Distance.HasValue)
                yield return new MagickDefine(Format, "distance", Distance.Value);

            if (Effort.HasValue)
                yield return new MagickDefine(Format, "effort", Effort.Value);

            if (Quality.HasValue)
                yield return new MagickDefine(Format, "quality", Quality.Value);
        }
    }
}
