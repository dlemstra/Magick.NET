// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

/// <summary>
/// Specifies the IFD of an Exif tag.
/// </summary>
[Flags]
public enum ExifIfds
{
    /// <summary>
    /// None.
    /// </summary>
    None = 0,

    /// <summary>
    /// The IFD0.
    /// </summary>
    Ifd0 = 1,

    /// <summary>
    /// The Exif IFD.
    /// </summary>
    Exif = 2,

    /// <summary>
    /// The GPS IFD.
    /// </summary>
    Gps = 4,

    /// <summary>
    /// All IFDs.
    /// </summary>
    All = Ifd0 | Exif | Gps,
}
