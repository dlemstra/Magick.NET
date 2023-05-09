// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Formats;

/// <summary>
/// Specifies the video read modes.
/// </summary>
public enum VideoReadMode
{
    /// <summary>
    /// Read the video with frames that have a duration (webp).
    /// </summary>
    ByDuration,

    /// <summary>
    /// Read the video frame by frame (pam).
    /// </summary>
    ByFrame,
}
