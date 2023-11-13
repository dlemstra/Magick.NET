// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Specifies endian.
/// </summary>
public enum Endian
{
    /// <summary>
    /// Undefined.
    /// </summary>
    Undefined,

    /// <summary>
    /// Least significant bit, byte 0 is least significant.
    /// </summary>
    LSB,

    /// <summary>
    /// Most significant bit, byte 0 is most significant.
    /// </summary>
    MSB,
}
