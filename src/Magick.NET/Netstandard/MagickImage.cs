// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETSTANDARD
using System.IO;
using System.Threading.Tasks;

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
    /// <content/>
    public sealed partial class MagickImage : IMagickImage<QuantumType>, INativeInstance
    {
        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task ReadAsync(Stream stream)
            => ReadAsync(stream, null);

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="format">The format to use.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task ReadAsync(Stream stream, MagickFormat format)
            => ReadAsync(stream, new MagickReadSettings { Format = format });

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task ReadAsync(Stream stream, IMagickReadSettings<QuantumType>? readSettings)
        {
            Throw.IfNull(nameof(stream), stream);

            var bytes = await Bytes.CreateAsync(stream);
            Read(bytes.GetData(), readSettings);
        }

        /// <summary>
        /// Read single image frame from pixel data.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task ReadPixelsAsync(Stream stream, IPixelReadSettings<QuantumType>? settings)
        {
            Throw.IfNullOrEmpty(nameof(stream), stream);

            var bytes = await Bytes.CreateAsync(stream);
            ReadPixels(bytes.GetData(), 0, bytes.Length, settings);
        }

        /// <summary>
        /// Writes the image to the specified stream.
        /// </summary>
        /// <param name="stream">The stream to write the image data to.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task WriteAsync(Stream stream)
        {
            Throw.IfNull(nameof(stream), stream);

            var bytes = ToByteArray();
            return stream.WriteAsync(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Writes the image to the specified stream.
        /// </summary>
        /// <param name="stream">The stream to write the image data to.</param>
        /// <param name="defines">The defines to set.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task WriteAsync(Stream stream, IWriteDefines defines)
        {
            _settings.SetDefines(defines);
            _settings.Format = defines.Format;
            return WriteAsync(stream);
        }

        /// <summary>
        /// Writes the image to the specified stream.
        /// </summary>
        /// <param name="stream">The stream to write the image data to.</param>
        /// <param name="format">The format to use.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task WriteAsync(Stream stream, MagickFormat format)
        {
            var currentFormat = Format;
            _settings.Format = format;
            await WriteAsync(stream).ConfigureAwait(false);
            Format = currentFormat;
        }
    }
}

#endif
