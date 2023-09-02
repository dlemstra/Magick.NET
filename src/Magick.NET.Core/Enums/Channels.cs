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
public enum Channels : long
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
    /// Meta0.
    /// </summary>
    Meta0 = 1 << PixelChannel.Meta0,

    /// <summary>
    /// Meta1.
    /// </summary>
    Meta1 = 1 << PixelChannel.Meta1,

    /// <summary>
    /// Meta2.
    /// </summary>
    Meta2 = 1 << PixelChannel.Meta2,

    /// <summary>
    /// Meta3.
    /// </summary>
    Meta3 = 1 << PixelChannel.Meta3,

    /// <summary>
    /// Meta4.
    /// </summary>
    Meta4 = 1 << PixelChannel.Meta4,

    /// <summary>
    /// Meta5.
    /// </summary>
    Meta5 = 1 << PixelChannel.Meta5,

    /// <summary>
    /// Meta6.
    /// </summary>
    Meta6 = 1 << PixelChannel.Meta6,

    /// <summary>
    /// Meta7.
    /// </summary>
    Meta7 = 1 << PixelChannel.Meta7,

    /// <summary>
    /// Meta8.
    /// </summary>
    Meta8 = 1 << PixelChannel.Meta8,

    /// <summary>
    /// Meta9.
    /// </summary>
    Meta9 = 1 << PixelChannel.Meta9,

    /// <summary>
    /// Meta10.
    /// </summary>
    Meta10 = 1 << PixelChannel.Meta10,

    /// <summary>
    /// Meta11.
    /// </summary>
    Meta11 = 1 << PixelChannel.Meta11,

    /// <summary>
    /// Meta12.
    /// </summary>
    Meta12 = 1 << PixelChannel.Meta12,

    /// <summary>
    /// Meta13.
    /// </summary>
    Meta13 = 1 << PixelChannel.Meta13,

    /// <summary>
    /// Meta14.
    /// </summary>
    Meta14 = 1 << PixelChannel.Meta14,

    /// <summary>
    /// Meta15.
    /// </summary>
    Meta15 = 1 << PixelChannel.Meta15,

    /// <summary>
    /// Meta16.
    /// </summary>
    Meta16 = 1 << PixelChannel.Meta16,

    /// <summary>
    /// Meta17.
    /// </summary>
    Meta17 = 1 << PixelChannel.Meta17,

    /// <summary>
    /// Meta18.
    /// </summary>
    Meta18 = 1 << PixelChannel.Meta18,

    /// <summary>
    /// Meta19.
    /// </summary>
    Meta19 = 1 << PixelChannel.Meta19,

    /// <summary>
    /// Meta20.
    /// </summary>
    Meta20 = 1 << PixelChannel.Meta20,

    /// <summary>
    /// Meta21.
    /// </summary>
    Meta21 = 1 << PixelChannel.Meta21,

    /// <summary>
    /// Meta22.
    /// </summary>
    Meta22 = 1 << PixelChannel.Meta22,

    /// <summary>
    /// Meta23.
    /// </summary>
    Meta23 = 1 << PixelChannel.Meta23,

    /// <summary>
    /// Meta24.
    /// </summary>
    Meta24 = 1 << PixelChannel.Meta24,

    /// <summary>
    /// Meta25.
    /// </summary>
    Meta25 = 1 << PixelChannel.Meta25,

    /// <summary>
    /// Meta26.
    /// </summary>
    Meta26 = 1 << PixelChannel.Meta26,

    /// <summary>
    /// Meta27.
    /// </summary>
    Meta27 = 1 << PixelChannel.Meta27,

    /// <summary>
    /// Meta28.
    /// </summary>
    Meta28 = 1 << PixelChannel.Meta28,

    /// <summary>
    /// Meta29.
    /// </summary>
    Meta29 = 1 << PixelChannel.Meta29,

    /// <summary>
    /// Meta30.
    /// </summary>
    Meta30 = 1 << PixelChannel.Meta30,

    /// <summary>
    /// Meta31.
    /// </summary>
    Meta31 = 1 << PixelChannel.Meta31,

    /// <summary>
    /// Meta32.
    /// </summary>
    Meta32 = 1 << PixelChannel.Meta32,

    /// <summary>
    /// Meta33.
    /// </summary>
    Meta33 = 1 << PixelChannel.Meta33,

    /// <summary>
    /// Meta34.
    /// </summary>
    Meta34 = 1 << PixelChannel.Meta34,

    /// <summary>
    /// Meta35.
    /// </summary>
    Meta35 = 1 << PixelChannel.Meta35,

    /// <summary>
    /// Meta36.
    /// </summary>
    Meta36 = 1 << PixelChannel.Meta36,

    /// <summary>
    /// Meta37.
    /// </summary>
    Meta37 = 1 << PixelChannel.Meta37,

    /// <summary>
    /// Meta38.
    /// </summary>
    Meta38 = 1 << PixelChannel.Meta38,

    /// <summary>
    /// Meta39.
    /// </summary>
    Meta39 = 1 << PixelChannel.Meta39,

    /// <summary>
    /// Meta40.
    /// </summary>
    Meta40 = 1 << PixelChannel.Meta40,

    /// <summary>
    /// Meta41.
    /// </summary>
    Meta41 = 1 << PixelChannel.Meta41,

    /// <summary>
    /// Meta42.
    /// </summary>
    Meta42 = 1 << PixelChannel.Meta42,

    /// <summary>
    /// Meta43.
    /// </summary>
    Meta43 = 1 << PixelChannel.Meta43,

    /// <summary>
    /// Meta44.
    /// </summary>
    Meta44 = 1 << PixelChannel.Meta44,

    /// <summary>
    /// Meta45.
    /// </summary>
    Meta45 = 1 << PixelChannel.Meta45,

    /// <summary>
    /// Meta46.
    /// </summary>
    Meta46 = 1 << PixelChannel.Meta46,

    /// <summary>
    /// Meta47.
    /// </summary>
    Meta47 = 1 << PixelChannel.Meta47,

    /// <summary>
    /// Meta48.
    /// </summary>
    Meta48 = 1 << PixelChannel.Meta48,

    /// <summary>
    /// Meta49.
    /// </summary>
    Meta49 = 1 << PixelChannel.Meta49,

    /// <summary>
    /// Meta50.
    /// </summary>
    Meta50 = 1 << PixelChannel.Meta50,

    /// <summary>
    /// Meta51.
    /// </summary>
    Meta51 = 1 << PixelChannel.Meta51,

    /// <summary>
    /// Meta52.
    /// </summary>
    Meta52 = 1 << PixelChannel.Meta52,

    /// <summary>
    /// Meta53.
    /// </summary>
    Meta53 = 1 << PixelChannel.Meta53,

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
