// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick.Formats;

/// <summary>
/// Class for defines that are used when a <see cref="MagickFormat.Jp2"/> image is written.
/// </summary>
public sealed class IconWriteDefines : IWriteDefines
{
    /// <summary>
    /// Gets or sets a value indicating whether automatic resizing is enabled (icon:auto-resize).
    /// </summary>
    public bool? AutoResize { get; set; }

    /// <summary>
    /// Gets the format where the defines are for.
    /// </summary>
    public MagickFormat Format
        => MagickFormat.Icon;

    /// <summary>
    /// Gets or sets the minimum image size threshold above which a PNG image is stored instead of a BMP image (icon:png-compression-size).
    /// </summary>
    public uint? PngCompressionSize { get; set; }

    /// <summary>
    /// Gets the defines that should be set as a define on an image.
    /// </summary>
    public IEnumerable<IDefine> Defines
    {
        get
        {
            if (AutoResize.HasValue)
                yield return new MagickDefine(Format, "auto-resize", AutoResize.Value);

            if (PngCompressionSize.HasValue)
                yield return new MagickDefine(Format, "png-compression-size", PngCompressionSize.Value);
        }
    }
}
