﻿// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;

namespace ImageMagick.Formats;

/// <summary>
/// Class for defines that are used when a <see cref="MagickFormat.Tiff"/> image is read.
/// </summary>
public sealed class TiffReadDefines : IReadDefines
{
    /// <summary>
    /// Gets the format where the defines are for.
    /// </summary>
    public MagickFormat Format
        => MagickFormat.Tiff;

    /// <summary>
    /// Gets or sets a value indicating whether the exif profile should be ignored (tiff:exif-properties).
    /// </summary>
    [Obsolete($"This property will be removed in the next major release, use {nameof(IgnoreExifProperties)} instead.")]
    public bool? IgnoreExifPoperties
    {
        get => IgnoreExifProperties;
        set => IgnoreExifProperties = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the exif profile should be ignored (tiff:exif-properties).
    /// </summary>
    public bool? IgnoreExifProperties { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the layers should be ignored (tiff:ignore-layers).
    /// </summary>
    public bool? IgnoreLayers { get; set; }

    /// <summary>
    /// Gets or sets the tiff tags that should be ignored (tiff:ignore-tags).
    /// </summary>
    public IEnumerable<string>? IgnoreTags { get; set; }

    /// <summary>
    /// Gets the defines that should be set as a define on an image.
    /// </summary>
    public IEnumerable<IDefine> Defines
    {
        get
        {
            if (IgnoreExifProperties.Equals(true))
                yield return new MagickDefine(Format, "exif-properties", false);

            if (IgnoreLayers is not null)
                yield return new MagickDefine(Format, "ignore-layers", IgnoreLayers.Value);

            var ignoreTags = MagickDefine.Create(Format, "ignore-tags", IgnoreTags);
            if (ignoreTags is not null)
                yield return ignoreTags;
        }
    }
}
