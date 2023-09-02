// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Diagnostics.CodeAnalysis;

namespace ImageMagick;

/// <summary>
/// Specifies channel types.
/// </summary>
[Flags]
[SuppressMessage("Naming", "CA1724", Justification = "No real collision.")]
public enum Channels : ulong
{
    /// <summary>
    /// Undefined.
    /// </summary>
    Undefined = 0,

    /// <summary>
    /// Red.
    /// </summary>
    Red = 0b1,

    /// <summary>
    /// Gray.
    /// </summary>
    Gray = Red,

    /// <summary>
    /// Cyan.
    /// </summary>
    Cyan = Red,

    /// <summary>
    /// Green.
    /// </summary>
    Green = 0b10,

    /// <summary>
    /// Magenta.
    /// </summary>
    Magenta = Green,

    /// <summary>
    /// Blue.
    /// </summary>
    Blue = 0b100,

    /// <summary>
    /// Yellow.
    /// </summary>
    Yellow = Blue,

    /// <summary>
    /// Black.
    /// </summary>
    Black = 0b1000,

    /// <summary>
    /// Alpha.
    /// </summary>
    Alpha = 0b10000,

    /// <summary>
    /// Opacity.
    /// </summary>
    Opacity = Alpha,

    /// <summary>
    /// Index.
    /// </summary>
    Index = 0b100000,

    /// <summary>
    /// Composite.
    /// </summary>
    Composite = 0b11111,

    /// <summary>
    /// All.
    /// </summary>
    All = 0b0111111111111111111111111111111111111111111111111111111111111111,

    /// <summary>
    /// TrueAlpha.
    /// </summary>
    TrueAlpha = 0b100000000,

    /// <summary>
    /// RGB.
    /// </summary>
    RGB = Red | Green | Blue,

    /// <summary>
    /// CMYK.
    /// </summary>
    CMYK = Cyan | Magenta | Yellow | Black,

    /// <summary>
    /// Grays.
    /// </summary>
    [Obsolete("The value 'Grays' will be removed in a future release.")]
    Grays = 0x0400,

    /// <summary>
    /// Sync.
    /// </summary>
    [Obsolete("The value 'Sync' will be removed in a future release.")]
    Sync = 0x20000,

    /// <summary>
    /// Default.
    /// </summary>
    [Obsolete("The value 'Default' will be removed in a future release use 'All' instead.")]
    Default = All,
}
