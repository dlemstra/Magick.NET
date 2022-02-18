// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETSTANDARD2_1

using System;
using System.Buffers;

namespace ImageMagick
{
    /// <content />
    public static partial class MagickNET
    {
        /// <summary>
        /// Returns the format information. The header of the image in the span of bytes is used to
        /// determine the format.
        /// </summary>
        /// <param name="data">The span of bytes to read the image header from.</param>
        /// <returns>The format information.</returns>
        public static MagickFormatInfo? GetFormatInformation(ReadOnlySpan<byte> data)
            => MagickFormatInfo.Create(data);

        /// <summary>
        /// Returns the format information. The header of the image in the sequence of bytes is used to
        /// determine the format.
        /// </summary>
        /// <param name="data">The sequence of bytes to read the image header from.</param>
        /// <returns>The format information.</returns>
        public static MagickFormatInfo? GetFormatInformation(ReadOnlySequence<byte> data)
            => MagickFormatInfo.Create(data);
    }
}

#endif
