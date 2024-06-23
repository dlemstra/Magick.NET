// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick.Formats;

/// <summary>
/// Class for defines that are used when a <see cref="MagickFormat.Dng"/> image is read.
/// </summary>
public sealed class DngReadDefines : IReadDefines
{
    /// <summary>
    /// Gets or sets a value indicating whether auto brightness should be used (dng:no-auto-bright).
    /// </summary>
    public bool? DisableAutoBrightness { get; set; }

    /// <summary>
    /// Gets the format where the defines are for.
    /// </summary>
    public MagickFormat Format
        => MagickFormat.Dng;

    /// <summary>
    /// Gets or sets a value indicating the interpolation quality (dng:interpolation-quality).
    /// </summary>
    public DngInterpolation? InterpolationQuality { get; set; }

    /// <summary>
    /// Gets or sets the output color (dng:output-color).
    /// </summary>
    public DngOutputColor? OutputColor { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the embedded thumbnail should be read (dng:read-thumbnail). This profile can be
    /// read by calling <see cref="IMagickImage.GetProfile(string)"/> with dng:thumbnail as the name of the profile.
    /// </summary>
    public bool? ReadThumbnail { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether auto white balance should be used (dng:use-auto-wb).
    /// </summary>
    public bool? UseAutoWhitebalance { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the white balance of the camera should be used (dng:use-camera-wb).
    /// </summary>
    public bool? UseCameraWhitebalance { get; set; }

    /// <summary>
    /// Gets the defines that should be set as a define on an image.
    /// </summary>
    public IEnumerable<IDefine> Defines
    {
        get
        {
            if (InterpolationQuality.HasValue)
                yield return new MagickDefine(Format, "interpolation-quality", (int)InterpolationQuality.Value);

            if (DisableAutoBrightness.HasValue)
                yield return new MagickDefine(Format, "no-auto-bright", DisableAutoBrightness.Value);

            if (OutputColor.HasValue)
                yield return new MagickDefine(Format, "output-color", (int)OutputColor.Value);

            if (ReadThumbnail.HasValue)
                yield return new MagickDefine(Format, "read-thumbnail", ReadThumbnail.Value);

            if (UseCameraWhitebalance.HasValue)
                yield return new MagickDefine(Format, "use-camera-wb", UseCameraWhitebalance.Value);

            if (UseAutoWhitebalance.HasValue)
                yield return new MagickDefine(Format, "use-auto-wb", UseAutoWhitebalance.Value);
        }
    }
}
