// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETSTANDARD2_1
using System;
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
    /// <content />
    public sealed partial class MagickImageCollection : IMagickImageCollection<QuantumType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MagickImageCollection"/> class.
        /// </summary>
        /// <param name="data">The span of bytes to read the image data from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public MagickImageCollection(ReadOnlySpan<byte> data)
            : this() => Read(data);

        /// <summary>
        /// Initializes a new instance of the <see cref="MagickImageCollection"/> class.
        /// </summary>
        /// <param name="data">The span of bytes to read the image data from.</param>
        /// <param name="format">The format to use.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public MagickImageCollection(ReadOnlySpan<byte> data, MagickFormat format)
            : this() => Read(data, format);

        /// <summary>
        /// Initializes a new instance of the <see cref="MagickImageCollection"/> class.
        /// </summary>
        /// <param name="data">The span of bytes to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public MagickImageCollection(ReadOnlySpan<byte> data, IMagickReadSettings<QuantumType> readSettings)
            : this() => Read(data, readSettings);

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

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="file">The file to read the frames from.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(FileInfo file)
            => ReadAsync(file, null);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="file">The file to read the frames from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(FileInfo file, IMagickReadSettings<QuantumType>? readSettings)
        {
            Throw.IfNull(nameof(file), file);

            return ReadAsync(file.FullName, readSettings);
        }

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="file">The file to read the frames from.</param>
        /// <param name="format">The format to use.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(FileInfo file, MagickFormat format)
            => ReadAsync(file, new MagickReadSettings { Format = format });

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(string fileName)
            => ReadAsync(fileName, null);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public async Task ReadAsync(string fileName, IMagickReadSettings<QuantumType>? readSettings)
        {
            Throw.IfNullOrEmpty(nameof(fileName), fileName);

            var bytes = await File.ReadAllBytesAsync(fileName);

            Clear();
            AddImages(bytes, 0, bytes.Length, readSettings, false);
        }

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="format">The format to use.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(string fileName, MagickFormat format)
            => ReadAsync(fileName, new MagickReadSettings { Format = format });

        private void AddImages(ReadOnlySpan<byte> data, IMagickReadSettings<QuantumType>? readSettings, bool ping)
        {
            var settings = CreateSettings(readSettings);
            settings.Ping = ping;

            var result = _nativeInstance.ReadBlob(settings, data, 0, data.Length);
            AddImages(result, settings);
        }
    }
}

#endif
