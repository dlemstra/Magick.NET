// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using System.Threading;
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
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(Stream stream)
            => ReadAsync(stream, CancellationToken.None);

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(Stream stream, CancellationToken cancellationToken)
            => ReadAsync(stream, null, cancellationToken);

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="format">The format to use.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(Stream stream, MagickFormat format)
            => ReadAsync(stream, format, CancellationToken.None);

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="format">The format to use.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(Stream stream, MagickFormat format, CancellationToken cancellationToken)
            => ReadAsync(stream, new MagickReadSettings { Format = format }, cancellationToken);

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(Stream stream, IMagickReadSettings<QuantumType>? readSettings)
            => ReadAsync(stream, readSettings, CancellationToken.None);

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public async Task ReadAsync(Stream stream, IMagickReadSettings<QuantumType>? readSettings, CancellationToken cancellationToken)
        {
            Throw.IfNull(nameof(stream), stream);

            var bytes = await Bytes.CreateAsync(stream, cancellationToken).ConfigureAwait(false);
            Read(bytes.GetData(), readSettings);
        }

        /// <summary>
        /// Read single image frame from pixel data.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadPixelsAsync(Stream stream, IPixelReadSettings<QuantumType>? settings)
            => ReadPixelsAsync(stream, settings, CancellationToken.None);

        /// <summary>
        /// Read single image frame from pixel data.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public async Task ReadPixelsAsync(Stream stream, IPixelReadSettings<QuantumType>? settings, CancellationToken cancellationToken)
        {
            Throw.IfNullOrEmpty(nameof(stream), stream);

            var bytes = await Bytes.CreateAsync(stream, cancellationToken).ConfigureAwait(false);
            ReadPixels(bytes.GetData(), 0, bytes.Length, settings);
        }

        /// <summary>
        /// Writes the image to the specified stream.
        /// </summary>
        /// <param name="stream">The stream to write the image data to.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task WriteAsync(Stream stream)
            => WriteAsync(stream, CancellationToken.None);

        /// <summary>
        /// Writes the image to the specified stream.
        /// </summary>
        /// <param name="stream">The stream to write the image data to.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task WriteAsync(Stream stream, CancellationToken cancellationToken)
        {
            Throw.IfNull(nameof(stream), stream);

            var bytes = ToByteArray();
            return stream.WriteAsync(bytes, 0, bytes.Length, cancellationToken);
        }

        /// <summary>
        /// Writes the image to the specified stream.
        /// </summary>
        /// <param name="stream">The stream to write the image data to.</param>
        /// <param name="defines">The defines to set.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task WriteAsync(Stream stream, IWriteDefines defines)
            => WriteAsync(stream, defines, CancellationToken.None);

        /// <summary>
        /// Writes the image to the specified stream.
        /// </summary>
        /// <param name="stream">The stream to write the image data to.</param>
        /// <param name="defines">The defines to set.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task WriteAsync(Stream stream, IWriteDefines defines, CancellationToken cancellationToken)
        {
            _settings.SetDefines(defines);
            return WriteAsync(stream, defines.Format, cancellationToken);
        }

        /// <summary>
        /// Writes the image to the specified stream.
        /// </summary>
        /// <param name="stream">The stream to write the image data to.</param>
        /// <param name="format">The format to use.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task WriteAsync(Stream stream, MagickFormat format)
            => WriteAsync(stream, format, CancellationToken.None);

        /// <summary>
        /// Writes the image to the specified stream.
        /// </summary>
        /// <param name="stream">The stream to write the image data to.</param>
        /// <param name="format">The format to use.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public async Task WriteAsync(Stream stream, MagickFormat format, CancellationToken cancellationToken)
        {
            using (_ = new TemporaryMagickFormat(this, format))
            {
                await WriteAsync(stream, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
