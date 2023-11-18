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
    /// TrueAlpha.
    /// </summary>
    TrueAlpha = 0b100000000,

    /// <summary>
    /// RGB.
    /// </summary>
    RGB = Red | Green | Blue,

    /// <summary>
    /// RGBA.
    /// </summary>
    RGBA = Red | Green | Blue | Alpha,

    /// <summary>
    /// CMYK.
    /// </summary>
    CMYK = Cyan | Magenta | Yellow | Black,

    /// <summary>
    /// CMYKA.
    /// </summary>
    CMYKA = Cyan | Magenta | Yellow | Black | Alpha,

    /// <summary>
    /// Meta 0.
    /// </summary>
    Meta0 = 1UL << PixelChannel.Meta0,

    /// <summary>
    /// Meta 1.
    /// </summary>
    Meta1 = 1UL << PixelChannel.Meta1,

    /// <summary>
    /// Meta  2.
    /// </summary>
    Meta2 = 1UL << PixelChannel.Meta2,

    /// <summary>
    /// Meta 3.
    /// </summary>
    Meta3 = 1UL << PixelChannel.Meta3,

    /// <summary>
    /// Meta 4.
    /// </summary>
    Meta4 = 1UL << PixelChannel.Meta4,

    /// <summary>
    /// Meta 5.
    /// </summary>
    Meta5 = 1UL << PixelChannel.Meta5,

    /// <summary>
    /// Meta 6.
    /// </summary>
    Meta6 = 1UL << PixelChannel.Meta6,

    /// <summary>
    /// Meta 7.
    /// </summary>
    Meta7 = 1UL << PixelChannel.Meta7,

    /// <summary>
    /// Meta 8.
    /// </summary>
    Meta8 = 1UL << PixelChannel.Meta8,

    /// <summary>
    /// Meta 9.
    /// </summary>
    Meta9 = 1UL << PixelChannel.Meta9,

    /// <summary>
    /// Meta 10.
    /// </summary>
    Meta10 = 1UL << PixelChannel.Meta10,

    /// <summary>
    /// Meta 11.
    /// </summary>
    Meta11 = 1UL << PixelChannel.Meta11,

    /// <summary>
    /// Meta 12.
    /// </summary>
    Meta12 = 1UL << PixelChannel.Meta12,

    /// <summary>
    /// Meta 13.
    /// </summary>
    Meta13 = 1UL << PixelChannel.Meta13,

    /// <summary>
    /// Meta 14.
    /// </summary>
    Meta14 = 1UL << PixelChannel.Meta14,

    /// <summary>
    /// Meta 15.
    /// </summary>
    Meta15 = 1UL << PixelChannel.Meta15,

    /// <summary>
    /// Meta 16.
    /// </summary>
    Meta16 = 1UL << PixelChannel.Meta16,

    /// <summary>
    /// Meta 17.
    /// </summary>
    Meta17 = 1UL << PixelChannel.Meta17,

    /// <summary>
    /// Meta 18.
    /// </summary>
    Meta18 = 1UL << PixelChannel.Meta18,

    /// <summary>
    /// Meta 19.
    /// </summary>
    Meta19 = 1UL << PixelChannel.Meta19,

    /// <summary>
    /// Meta 20.
    /// </summary>
    Meta20 = 1UL << PixelChannel.Meta20,

    /// <summary>
    /// Meta 21.
    /// </summary>
    Meta21 = 1UL << PixelChannel.Meta21,

    /// <summary>
    /// Meta 22.
    /// </summary>
    Meta22 = 1UL << PixelChannel.Meta22,

    /// <summary>
    /// Meta 23.
    /// </summary>
    Meta23 = 1UL << PixelChannel.Meta23,

    /// <summary>
    /// Meta 24.
    /// </summary>
    Meta24 = 1UL << PixelChannel.Meta24,

    /// <summary>
    /// Meta 25.
    /// </summary>
    Meta25 = 1UL << PixelChannel.Meta25,

    /// <summary>
    /// Meta 26.
    /// </summary>
    Meta26 = 1UL << PixelChannel.Meta26,

    /// <summary>
    /// Meta 27.
    /// </summary>
    Meta27 = 1UL << PixelChannel.Meta27,

    /// <summary>
    /// Meta 28.
    /// </summary>
    Meta28 = 1UL << PixelChannel.Meta28,

    /// <summary>
    /// Meta 29.
    /// </summary>
    Meta29 = 1UL << PixelChannel.Meta29,

    /// <summary>
    /// Meta 30.
    /// </summary>
    Meta30 = 1UL << PixelChannel.Meta30,

    /// <summary>
    /// Meta 31.
    /// </summary>
    Meta31 = 1UL << PixelChannel.Meta31,

    /// <summary>
    /// Meta 32.
    /// </summary>
    Meta32 = 1UL << PixelChannel.Meta32,

    /// <summary>
    /// Meta 33.
    /// </summary>
    Meta33 = 1UL << PixelChannel.Meta33,

    /// <summary>
    /// Meta 34.
    /// </summary>
    Meta34 = 1UL << PixelChannel.Meta34,

    /// <summary>
    /// Meta 35.
    /// </summary>
    Meta35 = 1UL << PixelChannel.Meta35,

    /// <summary>
    /// Meta 36.
    /// </summary>
    Meta36 = 1UL << PixelChannel.Meta36,

    /// <summary>
    /// Meta 37.
    /// </summary>
    Meta37 = 1UL << PixelChannel.Meta37,

    /// <summary>
    /// Meta 38.
    /// </summary>
    Meta38 = 1UL << PixelChannel.Meta38,

    /// <summary>
    /// Meta 39.
    /// </summary>
    Meta39 = 1UL << PixelChannel.Meta39,

    /// <summary>
    /// Meta 40.
    /// </summary>
    Meta40 = 1UL << PixelChannel.Meta40,

    /// <summary>
    /// Meta 41.
    /// </summary>
    Meta41 = 1UL << PixelChannel.Meta41,

    /// <summary>
    /// Meta 42.
    /// </summary>
    Meta42 = 1UL << PixelChannel.Meta42,

    /// <summary>
    /// Meta 43.
    /// </summary>
    Meta43 = 1UL << PixelChannel.Meta43,

    /// <summary>
    /// Meta 44.
    /// </summary>
    Meta44 = 1UL << PixelChannel.Meta44,

    /// <summary>
    /// Meta 45.
    /// </summary>
    Meta45 = 1UL << PixelChannel.Meta45,

    /// <summary>
    /// Meta 46.
    /// </summary>
    Meta46 = 1UL << PixelChannel.Meta46,

    /// <summary>
    /// Meta 47.
    /// </summary>
    Meta47 = 1UL << PixelChannel.Meta47,

    /// <summary>
    /// Meta 48.
    /// </summary>
    Meta48 = 1UL << PixelChannel.Meta48,

    /// <summary>
    /// Meta 49.
    /// </summary>
    Meta49 = 1UL << PixelChannel.Meta49,

    /// <summary>
    /// Meta 50.
    /// </summary>
    Meta50 = 1UL << PixelChannel.Meta50,

    /// <summary>
    /// Meta 51.
    /// </summary>
    Meta51 = 1UL << PixelChannel.Meta51,

    /// <summary>
    /// Meta 52.
    /// </summary>
    Meta52 = 1UL << PixelChannel.Meta52,

    /// <summary>
    /// All.
    /// </summary>
    All = 0b0111111111111111111111111111111111111111111111111111111111111111,

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
