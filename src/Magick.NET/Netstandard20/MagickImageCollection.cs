// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETSTANDARD
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
    /// <content />
    public sealed partial class MagickImageCollection : IMagickImageCollection<QuantumType>
    {
        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(Stream stream)
            => ReadAsync(stream, CancellationToken.None);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(Stream stream, CancellationToken cancellationToken)
            => ReadAsync(stream, null);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="format">The format to use.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(Stream stream, MagickFormat format)
            => ReadAsync(stream, format, CancellationToken.None);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="format">The format to use.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(Stream stream, MagickFormat format, CancellationToken cancellationToken)
            => ReadAsync(stream, new MagickReadSettings { Format = format }, cancellationToken);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(Stream stream, IMagickReadSettings<QuantumType>? readSettings)
            => ReadAsync(stream, readSettings, CancellationToken.None);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public async Task ReadAsync(Stream stream, IMagickReadSettings<QuantumType>? readSettings, CancellationToken cancellationToken)
        {
            var bytes = await Bytes.CreateAsync(stream, cancellationToken).ConfigureAwait(false);

            Clear();
            AddImages(bytes.GetData(), 0, bytes.Length, readSettings, false);
        }

        /// <summary>
        /// Writes the imagse to the specified stream. If the output image's file format does not
        /// allow multi-image files multiple files will be written.
        /// </summary>
        /// <param name="stream">The stream to write the images to.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task WriteAsync(Stream stream)
            => WriteAsync(stream, CancellationToken.None);

        /// <summary>
        /// Writes the imagse to the specified stream. If the output image's file format does not
        /// allow multi-image files multiple files will be written.
        /// </summary>
        /// <param name="stream">The stream to write the images to.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public async Task WriteAsync(Stream stream, CancellationToken cancellationToken)
        {
            Throw.IfNull(nameof(stream), stream);

            if (_images.Count == 0)
                return;

            try
            {
                AttachImages();

                var bytes = ToByteArray();
                await stream.WriteAsync(bytes, 0, bytes.Length, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                DetachImages();
            }
        }

        /// <summary>
        /// Writes the imagse to the specified stream. If the output image's file format does not
        /// allow multi-image files multiple files will be written.
        /// </summary>
        /// <param name="stream">The stream to write the images to.</param>
        /// <param name="defines">The defines to set.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task WriteAsync(Stream stream, IWriteDefines defines)
            => WriteAsync(stream, defines, CancellationToken.None);

        /// <summary>
        /// Writes the imagse to the specified stream. If the output image's file format does not
        /// allow multi-image files multiple files will be written.
        /// </summary>
        /// <param name="stream">The stream to write the images to.</param>
        /// <param name="defines">The defines to set.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task WriteAsync(Stream stream, IWriteDefines defines, CancellationToken cancellationToken)
        {
            SetDefines(defines);
            SetFormat(defines.Format);
            return WriteAsync(stream, cancellationToken);
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
        public Task WriteAsync(Stream stream, MagickFormat format, CancellationToken cancellationToken)
        {
            SetFormat(format);
            return WriteAsync(stream, cancellationToken);
        }
    }
}

#endif
