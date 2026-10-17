// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;

namespace ImageMagick.Formats;

/// <summary>
/// Class for defines that are used when a <see cref="MagickFormat.Heic"/> image is written.
/// </summary>
public sealed class HeicWriteDefines : IWriteDefines
{
    /// <summary>
    /// Gets or sets the chroma parameter (heic:chroma).
    /// </summary>
    public HeicChroma? Chroma { get; set; }

    /// <summary>
    /// Gets or sets a value which chroma downsampling method should be used (heic:chroma-downsampling).
    /// </summary>
    public HeicChromaDownsampling? ChromaDownsampling { get; set; }

    /// <summary>
    /// Gets or sets the color primaries, transfer characteristics, matrix coefficients, and full range flag (heic:cicp).
    /// The values are separated by slashes, for example "1/13/6/1" for full range BT.709.
    /// Explicit values with matrix coefficient 0 require chroma 444.
    /// </summary>
    public string? Cicp { get; set; }

    /// <summary>
    /// Gets or sets the content light level information as "MaxCLL,MaxFALL" (heic:clli).
    /// Both values must be in the range of 0 to 65535.
    /// </summary>
    public string? Clli { get; set; }

    /// <summary>
    /// Gets the format where the defines are for.
    /// </summary>
    public MagickFormat Format
        => MagickFormat.Heic;

    /// <summary>
    /// Gets or sets a value indicating whether the source CICP color information should be preserved (heic:preserve-cicp).
    /// The default is true. An explicit CICP value takes precedence, and an ICC profile prevents automatic preservation.
    /// </summary>
    public bool? PreserveCicp { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the source content light level information should be preserved (heic:preserve-clli).
    /// The default is true. An explicit CLLI value takes precedence.
    /// </summary>
    public bool? PreserveClli { get; set; }

    /// <summary>
    /// Gets or sets the encoding speed. Valid values are in the range of 0 to 9. The default is 5 (heic:speed).
    /// </summary>
    public int? Speed { get; set; }

    /// <summary>
    /// Gets the defines that should be set as a define on an image.
    /// </summary>
    public IEnumerable<IDefine> Defines
    {
        get
        {
            var chroma = GetChroma();
            if (chroma is not null)
                yield return new MagickDefine(Format, "chroma", chroma);

            var chromaDownsampling = GetChromaDownsampling();
            if (chromaDownsampling is not null)
                yield return new MagickDefine(Format, "chroma-downsampling", chromaDownsampling);

            if (Cicp?.Length > 0)
                yield return new MagickDefine(Format, "cicp", Cicp);

            if (Clli?.Length > 0)
                yield return new MagickDefine(Format, "clli", Clli);

            if (PreserveCicp.HasValue)
                yield return new MagickDefine(Format, "preserve-cicp", PreserveCicp.Value);

            if (PreserveClli.HasValue)
                yield return new MagickDefine(Format, "preserve-clli", PreserveClli.Value);

            if (Speed.HasValue)
                yield return new MagickDefine(Format, "speed", Speed.Value);
        }
    }

    private string? GetChroma()
    {
        if (!Chroma.HasValue)
            return null;

        return Chroma.Value switch
        {
            HeicChroma.Chroma420 => "420",
            HeicChroma.Chroma422 => "422",
            HeicChroma.Chroma444 => "444",
            _ => throw new NotImplementedException(Chroma.Value.ToString()),
        };
    }

    private string? GetChromaDownsampling()
    {
        if (!ChromaDownsampling.HasValue)
            return null;

        return ChromaDownsampling.Value switch
        {
            HeicChromaDownsampling.Average => "average",
            HeicChromaDownsampling.NearestNeighbor => "nearest-neighbor",
            HeicChromaDownsampling.SharpYUV => "sharp-yuv",
            _ => throw new NotImplementedException(ChromaDownsampling.Value.ToString()),
        };
    }
}
