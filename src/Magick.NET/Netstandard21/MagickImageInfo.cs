// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETSTANDARD2_1

using System;

namespace ImageMagick
{
    /// <summary>
    /// Class that contains basic information about an image.
    /// </summary>
    public sealed partial class MagickImageInfo : IMagickImageInfo
    {
        /// <summary>
        /// Read basic information about an image.
        /// </summary>
        /// <param name="data">The span of bytes to read the information from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public void Read(ReadOnlySpan<byte> data)
        {
            using (var image = new MagickImage())
            {
                image.Ping(data);
                Initialize(image);
            }
        }
    }
}

#endif
