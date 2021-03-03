// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Diagnostics.CodeAnalysis;

namespace ImageMagick
{
    /// <summary>
    /// Specifies the pixel channels.
    /// </summary>
    public enum PixelChannel
    {
        /// <summary>
        /// Red.
        /// </summary>
        Red = 0,

        /// <summary>
        /// Cyan.
        /// </summary>
        Cyan = Red,

        /// <summary>
        /// Gray.
        /// </summary>
        Gray = Red,

        /// <summary>
        /// Green.
        /// </summary>
        Green = 1,

        /// <summary>
        /// Magenta.
        /// </summary>
        Magenta = Green,

        /// <summary>
        /// Blue.
        /// </summary>
        Blue = 2,

        /// <summary>
        /// Yellow.
        /// </summary>
        Yellow = Blue,

        /// <summary>
        /// Black.
        /// </summary>
        Black = 3,

        /// <summary>
        /// Alpha.
        /// </summary>
        Alpha = 4,

        /// <summary>
        /// Index.
        /// </summary>
        Index = 5,

        /// <summary>
        /// Composite.
        /// </summary>
        Composite = 64,
    }
}