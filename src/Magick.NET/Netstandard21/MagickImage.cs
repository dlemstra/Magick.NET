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
    /// <content/>
    public sealed partial class MagickImage : IMagickImage<QuantumType>, INativeInstance
    {
        /// <summary>
        /// Reads only metadata and not the pixel data.
        /// </summary>
        /// <param name="data">The span of byte to read the information from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public void Ping(ReadOnlySpan<byte> data)
            => Ping(data, null);

        /// <summary>
        /// Reads only metadata and not the pixel data.
        /// </summary>
        /// <param name="data">The span of byte to read the information from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public void Ping(ReadOnlySpan<byte> data, IMagickReadSettings<QuantumType>? readSettings)
        {
            Throw.IfEmpty(nameof(data), data);

            Read(data, readSettings, false);
        }

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="data">The span of bytes to read the image data from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public void Read(ReadOnlySpan<byte> data)
            => Read(data, null);

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="data">The span of bytes to read the image data from.</param>
        /// <param name="format">The format to use.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public void Read(ReadOnlySpan<byte> data, MagickFormat format)
            => Read(data, new MagickReadSettings { Format = format });

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="data">The span of bytes to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public void Read(ReadOnlySpan<byte> data, IMagickReadSettings<QuantumType>? readSettings)
        {
            Throw.IfEmpty(nameof(data), data);

            Read(data, readSettings, false);
        }

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(FileInfo file)
            => ReadAsync(file, null);

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <param name="format">The format to use.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(FileInfo file, MagickFormat format)
            => ReadAsync(file, new MagickReadSettings { Format = format });

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(FileInfo file, IMagickReadSettings<QuantumType>? readSettings)
        {
            Throw.IfNull(nameof(file), file);

            return ReadAsync(file.FullName, readSettings);
        }

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(string fileName)
            => ReadAsync(fileName, null);

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="format">The format to use.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(string fileName, MagickFormat format)
            => ReadAsync(fileName, new MagickReadSettings { Format = format });

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public async Task ReadAsync(string fileName, IMagickReadSettings<QuantumType>? readSettings)
        {
            var filePath = FileHelper.CheckForBaseDirectory(fileName);
            Throw.IfNullOrEmpty(nameof(fileName), filePath);

            var bytes = await File.ReadAllBytesAsync(fileName);

            Read(bytes, readSettings, false);
        }

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="data">The span of bytes to read the image data from.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public void ReadPixels(ReadOnlySpan<byte> data, IPixelReadSettings<QuantumType>? settings)
        {
            Throw.IfEmpty(nameof(data), data);
            Throw.IfNull(nameof(settings), settings);

            var newReadSettings = CreateReadSettings(settings.ReadSettings);
            SetSettings(newReadSettings);

            var count = data.Length;
            var expectedLength = GetExpectedLength(settings);
            Throw.IfTrue(nameof(data), count < expectedLength, "The array count is " + count + " but should be at least " + expectedLength + ".");

            _nativeInstance.ReadPixels(settings.ReadSettings.Width!.Value, settings.ReadSettings.Height!.Value, settings.Mapping, settings.StorageType, data, 0);
        }

        /// <summary>
        /// Read single image frame from pixel data.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadPixelsAsync(FileInfo file, IPixelReadSettings<QuantumType>? settings)
        {
            Throw.IfNull(nameof(file), file);

            return ReadPixelsAsync(file.FullName, settings);
        }

        /// <summary>
        /// Read single image frame from pixel data.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public async Task ReadPixelsAsync(string fileName, IPixelReadSettings<QuantumType>? settings)
        {
            string filePath = FileHelper.CheckForBaseDirectory(fileName);
            Throw.IfNullOrEmpty(nameof(fileName), filePath);

            var data = await File.ReadAllBytesAsync(filePath);
            ReadPixels(data, 0, data.Length, settings);
        }

        private void Read(ReadOnlySpan<byte> data, IMagickReadSettings<QuantumType>? readSettings, bool ping)
        {
            var newReadSettings = CreateReadSettings(readSettings);
            SetSettings(newReadSettings);

            _settings.Ping = ping;

            _nativeInstance.ReadBlob(Settings, data, 0, data.Length);

            ResetSettings();
        }
    }
}

#endif
