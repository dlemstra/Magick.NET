// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;

namespace ImageMagick.Formats;

/// <summary>
/// Class for defines that are used when a <see cref="MagickFormat.Heic"/> image is read.
/// </summary>
public sealed class HeicReadDefines : IReadDefines
{
    /// <summary>
    /// Gets or sets a value which chroma upsampling method should be used (heic:chroma-upsampling).
    /// </summary>
    public HeicChromaUpsampling? ChromaUpsampling { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the depth image should be read (heic:depth-image).
    /// </summary>
    public bool? DepthImage { get; set; }

    /// <summary>
    /// Gets the format where the defines are for.
    /// </summary>
    public MagickFormat Format
        => MagickFormat.Heic;

    /// <summary>
    /// Gets or sets the maximum bayer pattern size (heic:max-bayer-pattern-pixels).
    /// </summary>
    public uint? MaxBayerPatternPixels { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of children per box (heic:max-children-per-box).
    /// </summary>
    public uint? MaxChildrenPerBox { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of components (heic:max-components).
    /// </summary>
    public uint? MaxComponents { get; set; }

    /// <summary>
    /// Gets or sets themaximum number of extents in iloc box (heic:max-iloc-extents-per-item).
    /// </summary>
    public uint? MaxIlocExtentsPerItem { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of items in a box (heic:max-items).
    /// </summary>
    public uint? MaxItems { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of tiles (heic:max-number-of-tiles).
    /// </summary>
    public ulong? MaxNumberOfTiles { get; set; }

    /// <summary>
    /// Gets or sets the maximum size of an entity group (heic:max-size-entity-group).
    /// </summary>
    public uint? MaxSizeEntityGroup { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the orientation should be preserved (heic:preserve-orientation).
    /// </summary>
    public bool? PreserveOrientation { get; set; }

    /// <summary>
    /// Gets the defines that should be set as a define on an image.
    /// </summary>
    public IEnumerable<IDefine> Defines
    {
        get
        {
            var chromaUpsampling = GetChromaUpsampling();
            if (chromaUpsampling is not null)
                yield return new MagickDefine(Format, "chroma-upsampling", chromaUpsampling);

            if (DepthImage == true)
                yield return new MagickDefine(Format, "depth-image", DepthImage.Value);

            if (MaxBayerPatternPixels is not null)
                yield return new MagickDefine(Format, "max-bayer-pattern-pixels", MaxBayerPatternPixels.Value);

            if (MaxChildrenPerBox is not null)
                yield return new MagickDefine(Format, "max-children-per-box", MaxChildrenPerBox.Value);

            if (MaxComponents is not null)
                yield return new MagickDefine(Format, "max-components", MaxComponents.Value);

            if (MaxIlocExtentsPerItem is not null)
                yield return new MagickDefine(Format, "max-iloc-extents-per-item", MaxIlocExtentsPerItem.Value);

            if (MaxItems is not null)
                yield return new MagickDefine(Format, "max-items", MaxItems.Value);

            if (MaxNumberOfTiles is not null)
                yield return new MagickDefine(Format, "max-number-of-tiles", MaxNumberOfTiles.Value);

            if (MaxSizeEntityGroup is not null)
                yield return new MagickDefine(Format, "max-size-entity-group", MaxSizeEntityGroup.Value);

            if (PreserveOrientation == true)
                yield return new MagickDefine(Format, "preserve-orientation", PreserveOrientation.Value);
        }
    }

    private string? GetChromaUpsampling()
    {
        if (!ChromaUpsampling.HasValue)
            return null;

        return ChromaUpsampling.Value switch
        {
            HeicChromaUpsampling.Bilinear => "bilinear",
            HeicChromaUpsampling.NearestNeighbor => "nearest-neighbor",
            _ => throw new NotImplementedException(ChromaUpsampling.Value.ToString()),
        };
    }
}
