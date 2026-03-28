// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick.Formats;

/// <summary>
/// Class for defines that are used when a <see cref="MagickFormat.Dcm"/> image is read.
/// </summary>
public sealed class DcmReadDefines : IReadDefines
{
    /// <summary>
    /// Gets the format where the defines are for.
    /// </summary>
    public MagickFormat Format
        => MagickFormat.Dcm;

    /// <summary>
    /// Gets or sets a value indicating whether the interpretation of the rescale slope should be set (dcm:rescale).
    /// </summary>
    public bool? Rescale { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the display range should be set to the
    /// minimum and maximum pixel values (dcm:display-range=reset).
    /// </summary>
    public bool? ResetDisplayRange { get; set; }

    /// <summary>
    /// Gets or sets the window center (dcm:window).
    /// </summary>
    public double? WindowCenter { get; set; }

    /// <summary>
    /// Gets or sets the window center (dcm:window).
    /// </summary>
    public double? WindowWidth { get; set; }

    /// <summary>
    /// Gets the defines that should be set as a define on an image.
    /// </summary>
    public IEnumerable<IDefine> Defines
    {
        get
        {
            if (Rescale.HasValue)
                yield return new MagickDefine(Format, "rescale", Rescale.Value);

            if (ResetDisplayRange.HasValue && ResetDisplayRange.Value)
                yield return new MagickDefine(Format, "display-range", "reset");

            if (WindowCenter.HasValue && !WindowWidth.HasValue)
                yield return new MagickDefine(Format, "window", $"{WindowCenter}x");
            else if (WindowWidth.HasValue && !WindowCenter.HasValue)
                yield return new MagickDefine(Format, "window", $"x{WindowWidth}");
            else if (WindowCenter.HasValue && WindowWidth.HasValue)
                yield return new MagickDefine(Format, "window", $"{WindowCenter}x{WindowWidth}");
        }
    }
}
