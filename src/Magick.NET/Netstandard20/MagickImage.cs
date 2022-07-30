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
    }
}
