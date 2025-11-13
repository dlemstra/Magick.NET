// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;

namespace ImageMagick.Formats;

/// <summary>
/// Class for defines that are used when a video image is read.
/// </summary>
public sealed class VideoReadDefines : IReadDefines
{
    private static readonly List<MagickFormat> AllowedFormats = [MagickFormat.ThreeGp, MagickFormat.ThreeG2, MagickFormat.APng, MagickFormat.Avi, MagickFormat.Flv, MagickFormat.Mkv, MagickFormat.Mov, MagickFormat.Mpeg, MagickFormat.Mpg, MagickFormat.Mp4, MagickFormat.M2v, MagickFormat.M4v, MagickFormat.WebM, MagickFormat.Wmv];

    /// <summary>
    /// Initializes a new instance of the <see cref="VideoReadDefines"/> class.
    /// </summary>
    /// <param name="format">The video format.</param>
    public VideoReadDefines(MagickFormat format)
        => Format = CheckFormat(format);

    /// <summary>
    /// Gets the format where the defines are for.
    /// </summary>
    public MagickFormat Format { get; }

    /// <summary>
    /// Gets or sets the video pixel format (video:pixel-format).
    /// </summary>
    public string? PixelFormat { get; set; }

    /// <summary>
    /// Gets or sets the video read mode (video:intermediate-format).
    /// </summary>
    public VideoReadMode? ReadMode { get; set; }

    /// <summary>
    /// Gets or sets the video sync (video:vsync).
    /// </summary>
    public VideoSync? VideoSync { get; set; }

    /// <summary>
    /// Gets the defines that should be set as a define on an image.
    /// </summary>
    public IEnumerable<IDefine> Defines
    {
        get
        {
            if (PixelFormat?.Length > 0)
                yield return new MagickDefine("video:pixel-format", PixelFormat);

            if (ReadMode == VideoReadMode.ByDuration)
                yield return new MagickDefine("video:intermediate-format", "webp");
            else if (ReadMode == VideoReadMode.ByFrame)
                yield return new MagickDefine("video:intermediate-format", "pam");

            if (VideoSync is not null)
                yield return new MagickDefine("video:vsync", VideoSync.Value);
        }
    }

    private static MagickFormat CheckFormat(MagickFormat format)
    {
        if (!AllowedFormats.Contains(format))
            throw new ArgumentException("The specified format is not a video format.", nameof(format));

        return format;
    }
}
