// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

/// <summary>
/// Specifies the different IFDs that can be found in an Exif profile.
/// </summary>
[Flags]
public enum ExifIfds
{
    /// <summary>
    /// None.
    /// </summary>
    None = 0,

    /// <summary>
    /// Primary IFD.
    /// </summary>
    Ifd0 = 1,

    /// <summary>
    /// Thumbnail IFD.
    /// </summary>
    Exif = 4,

    /// <summary>
    /// GPS IFD.
    /// </summary>
    Gps = 8,

    /// <summary>
    /// All.
    /// </summary>
    All = Ifd0 | Exif | Gps,
}
