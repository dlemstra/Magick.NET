// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Diagnostics.CodeAnalysis;

namespace ImageMagick
{
    /// <summary>
    /// Specifies channel types.
    /// </summary>
    [Flags]
    [SuppressMessage("Naming", "CA1724", Justification = "No real collision.")]
    public enum Channels
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        None = 0x0000,

        /// <summary>
        /// Red.
        /// </summary>
        Red = 0x0001,

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
        Green = 0x0002,

        /// <summary>
        /// Magenta.
        /// </summary>
        Magenta = Green,

        /// <summary>
        /// Blue.
        /// </summary>
        Blue = 0x0004,

        /// <summary>
        /// Yellow.
        /// </summary>
        Yellow = Blue,

        /// <summary>
        /// Black.
        /// </summary>
        Black = 0x0008,

        /// <summary>
        /// Alpha.
        /// </summary>
        Alpha = 0x0010,

        /// <summary>
        /// Opacity.
        /// </summary>
        Opacity = Alpha,

        /// <summary>
        /// Index.
        /// </summary>
        Index = 0x0020,

        /// <summary>
        /// Composite.
        /// </summary>
        Composite = 0x001F,

        /// <summary>
        /// All.
        /// </summary>
        All = 0x7ffffff,

        /// <summary>
        /// TrueAlpha.
        /// </summary>
        TrueAlpha = 0x0100,

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
        Grays = 0x0400,

        /// <summary>
        /// Sync.
        /// </summary>
        Sync = 0x20000,

        /// <summary>
        /// Default.
        /// </summary>
        Default = All,
    }
}