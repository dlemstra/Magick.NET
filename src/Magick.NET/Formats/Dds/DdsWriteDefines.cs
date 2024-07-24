// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;

namespace ImageMagick.Formats;

/// <summary>
/// Class for defines that are used when a <see cref="MagickFormat.Dds"/> image is written.
/// </summary>
public sealed class DdsWriteDefines : IWriteDefines
{
    /// <summary>
    /// Gets or sets a value indicating whether cluser fit is enabled or disabled (dds:cluster-fit).
    /// </summary>
    public bool? ClusterFit { get; set; }

    /// <summary>
    /// Gets or sets the compression that will be used instead of the default compression. Only a limited set of the
    /// compression methods are available for this. (dds:compression).
    /// </summary>
    public DdsCompression? Compression { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the mipmaps should be resized faster but with a lower quality (dds:fast-mipmaps).
    /// </summary>
    public bool? FastMipmaps { get; set; }

    /// <summary>
    /// Gets the format where the defines are for.
    /// </summary>
    public MagickFormat Format
        => MagickFormat.Dds;

    /// <summary>
    /// Gets or sets the the number of mipmaps, zero will disable writing mipmaps (dds:mipmaps).
    /// </summary>
    public int? MipmapCount { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the mipmaps should be created from the images in the collection (dds:mipmaps=fromlist).
    /// </summary>
    public bool? MipmapsFromCollection { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether only the raw pixels should be written (dds:raw).
    /// </summary>
    public bool? Raw { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether weight by alpha is enabled or disabled when cluster fit is used (dds:weight-by-alpha).
    /// </summary>
    public bool? WeightByAlpha { get; set; }

    /// <summary>
    /// Gets the defines that should be set as a define on an image.
    /// </summary>
    public IEnumerable<IDefine> Defines
    {
        get
        {
            if (ClusterFit.HasValue)
                yield return new MagickDefine(Format, "cluster-fit", ClusterFit.Value);

            if (Compression.HasValue)
                yield return new MagickDefine(Format, "compression", Compression.Value);

            if (FastMipmaps.HasValue)
                yield return new MagickDefine(Format, "fast-mipmaps", FastMipmaps.Value);

            if (MipmapsFromCollection == true)
                yield return new MagickDefine(Format, "mipmaps", "fromlist");
            else if (MipmapCount.HasValue)
                yield return new MagickDefine(Format, "mipmaps", MipmapCount.Value);

            if (Raw.HasValue)
                yield return new MagickDefine(Format, "raw", Raw.Value);

            if (WeightByAlpha.HasValue)
                yield return new MagickDefine(Format, "weight-by-alpha", WeightByAlpha.Value);
        }
    }
}
