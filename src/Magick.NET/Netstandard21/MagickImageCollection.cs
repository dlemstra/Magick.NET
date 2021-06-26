// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETSTANDARD2_1
using System;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick
{
    /// <content />
    public sealed partial class MagickImageCollection : IMagickImageCollection<QuantumType>
    {
        /// <summary>
        /// Read only metadata and not the pixel data from all image frames.
        /// </summary>
        /// <param name="data">The span of bytes to read the image data from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public void Ping(ReadOnlySpan<byte> data)
            => Ping(data, null);

        /// <summary>
        /// Read only metadata and not the pixel data from all image frames.
        /// </summary>
        /// <param name="data">The span of bytes to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public void Ping(ReadOnlySpan<byte> data, IMagickReadSettings<QuantumType>? readSettings)
        {
            Throw.IfEmpty(nameof(data), data);

            Clear();
            AddImages(data, readSettings, true);
        }

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="data">The span of bytes to read the image data from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public void Read(ReadOnlySpan<byte> data)
            => Read(data, null);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="data">The span of bytes to read the image data from.</param>
        /// <param name="format">The format to use.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public void Read(ReadOnlySpan<byte> data, MagickFormat format)
            => Read(data, new MagickReadSettings { Format = format });

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="data">The span of bytes to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public void Read(ReadOnlySpan<byte> data, IMagickReadSettings<QuantumType>? readSettings)
        {
            Throw.IfEmpty(nameof(data), data);

            Clear();
            AddImages(data, readSettings, false);
        }

        private void AddImages(ReadOnlySpan<byte> data, IMagickReadSettings<byte>? readSettings, bool ping)
        {
            var settings = CreateSettings(readSettings);
            settings.Ping = ping;

            var result = _nativeInstance.ReadBlob(settings, data, 0, data.Length);
            AddImages(result, settings);
        }
    }
}

#endif
