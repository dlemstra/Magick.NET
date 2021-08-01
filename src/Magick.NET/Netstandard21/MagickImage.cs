// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETSTANDARD2_1

using System;
using System.Buffers;
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
        /// Initializes a new instance of the <see cref="MagickImage"/> class.
        /// </summary>
        /// <param name="data">The span of bytes to read the image data from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public MagickImage(ReadOnlySpan<byte> data)
            : this() => Read(data);

        /// <summary>
        /// Initializes a new instance of the <see cref="MagickImage"/> class.
        /// </summary>
        /// <param name="data">The span of bytes to read the image data from.</param>
        /// <param name="format">The format to use.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public MagickImage(ReadOnlySpan<byte> data, MagickFormat format)
            : this() => Read(data, format);

        /// <summary>
        /// Initializes a new instance of the <see cref="MagickImage"/> class.
        /// </summary>
        /// <param name="data">The span of bytes to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public MagickImage(ReadOnlySpan<byte> data, IMagickReadSettings<QuantumType> readSettings)
            : this() => Read(data, readSettings);

        /// <summary>
        /// Initializes a new instance of the <see cref="MagickImage"/> class.
        /// </summary>
        /// <param name="data">The span of bytes to read the image data from.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public MagickImage(ReadOnlySpan<byte> data, IPixelReadSettings<QuantumType> settings)
            : this() => ReadPixels(data, settings);

        /// <summary>
        /// Reads only metadata and not the pixel data.
        /// </summary>
        /// <param name="data">The span of byte to read the information from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public void Ping(ReadOnlySequence<byte> data)
            => Ping(data, null);

        /// <summary>
        /// Reads only metadata and not the pixel data.
        /// </summary>
        /// <param name="data">The span of byte to read the information from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public void Ping(ReadOnlySequence<byte> data, IMagickReadSettings<QuantumType>? readSettings)
        {
            Throw.IfEmpty(nameof(data), data);

            Read(data, readSettings, false);
        }

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

            Read(data, readSettings, true);
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
            => ReadAsync(file, CancellationToken.None);

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(FileInfo file, CancellationToken cancellationToken)
            => ReadAsync(file, null, cancellationToken);

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <param name="format">The format to use.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(FileInfo file, MagickFormat format)
            => ReadAsync(file, format, CancellationToken.None);

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <param name="format">The format to use.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(FileInfo file, MagickFormat format, CancellationToken cancellationToken)
            => ReadAsync(file, new MagickReadSettings { Format = format }, cancellationToken);

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(FileInfo file, IMagickReadSettings<QuantumType>? readSettings)
            => ReadAsync(file, readSettings, CancellationToken.None);

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(FileInfo file, IMagickReadSettings<QuantumType>? readSettings, CancellationToken cancellationToken)
        {
            Throw.IfNull(nameof(file), file);

            return ReadAsync(file.FullName, readSettings, cancellationToken);
        }

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(string fileName)
            => ReadAsync(fileName, CancellationToken.None);

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(string fileName, CancellationToken cancellationToken)
            => ReadAsync(fileName, null, cancellationToken);

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="format">The format to use.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(string fileName, MagickFormat format)
            => ReadAsync(fileName, format, CancellationToken.None);

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="format">The format to use.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(string fileName, MagickFormat format, CancellationToken cancellationToken)
            => ReadAsync(fileName, new MagickReadSettings { Format = format }, cancellationToken);

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(string fileName, IMagickReadSettings<QuantumType>? readSettings)
            => ReadAsync(fileName, readSettings, CancellationToken.None);

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public async Task ReadAsync(string fileName, IMagickReadSettings<QuantumType>? readSettings, CancellationToken cancellationToken)
        {
            var filePath = FileHelper.CheckForBaseDirectory(fileName);
            Throw.IfNullOrEmpty(nameof(fileName), filePath);

            var bytes = await File.ReadAllBytesAsync(fileName, cancellationToken).ConfigureAwait(false);

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
            => ReadPixelsAsync(file, settings, CancellationToken.None);

        /// <summary>
        /// Read single image frame from pixel data.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadPixelsAsync(FileInfo file, IPixelReadSettings<QuantumType>? settings, CancellationToken cancellationToken)
        {
            Throw.IfNull(nameof(file), file);

            return ReadPixelsAsync(file.FullName, settings, cancellationToken);
        }

        /// <summary>
        /// Read single image frame from pixel data.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadPixelsAsync(string fileName, IPixelReadSettings<QuantumType>? settings)
            => ReadPixelsAsync(fileName, settings, CancellationToken.None);

        /// <summary>
        /// Read single image frame from pixel data.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public async Task ReadPixelsAsync(string fileName, IPixelReadSettings<QuantumType>? settings, CancellationToken cancellationToken)
        {
            string filePath = FileHelper.CheckForBaseDirectory(fileName);
            Throw.IfNullOrEmpty(nameof(fileName), filePath);

            var data = await File.ReadAllBytesAsync(filePath, cancellationToken).ConfigureAwait(false);
            ReadPixels(data, 0, data.Length, settings);
        }

        /// <summary>
        /// Writes the image to the specified file.
        /// </summary>
        /// <param name="file">The file to write the image to.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task WriteAsync(FileInfo file)
            => WriteAsync(file, CancellationToken.None);

        /// <summary>
        /// Writes the image to the specified file.
        /// </summary>
        /// <param name="file">The file to write the image to.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task WriteAsync(FileInfo file, CancellationToken cancellationToken)
        {
            Throw.IfNull(nameof(file), file);

            var formatInfo = MagickFormatInfo.Create(file);

            var bytes = formatInfo != null ? ToByteArray(formatInfo.Format) : ToByteArray();
            return File.WriteAllBytesAsync(file.FullName, bytes, cancellationToken);
        }

        /// <summary>
        /// Writes the image to the specified file.
        /// </summary>
        /// <param name="file">The file to write the image to.</param>
        /// <param name="defines">The defines to set.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task WriteAsync(FileInfo file, IWriteDefines defines)
            => WriteAsync(file, defines, CancellationToken.None);

        /// <summary>
        /// Writes the image to the specified file.
        /// </summary>
        /// <param name="file">The file to write the image to.</param>
        /// <param name="defines">The defines to set.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task WriteAsync(FileInfo file, IWriteDefines defines, CancellationToken cancellationToken)
        {
            _settings.SetDefines(defines);
            return WriteAsync(file, defines.Format, cancellationToken);
        }

        /// <summary>
        /// Writes the image to the specified file.
        /// </summary>
        /// <param name="file">The file to write the image to.</param>
        /// <param name="format">The format to use.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task WriteAsync(FileInfo file, MagickFormat format)
            => WriteAsync(file, format, CancellationToken.None);

        /// <summary>
        /// Writes the image to the specified file.
        /// </summary>
        /// <param name="file">The file to write the image to.</param>
        /// <param name="format">The format to use.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task WriteAsync(FileInfo file, MagickFormat format, CancellationToken cancellationToken)
        {
            Throw.IfNull(nameof(file), file);

            var bytes = ToByteArray(format);
            return File.WriteAllBytesAsync(file.FullName, bytes, cancellationToken);
        }

        /// <summary>
        /// Writes the image to the specified file name.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task WriteAsync(string fileName)
            => WriteAsync(fileName, CancellationToken.None);

        /// <summary>
        /// Writes the image to the specified file name.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task WriteAsync(string fileName, CancellationToken cancellationToken)
        {
            string filePath = FileHelper.CheckForBaseDirectory(fileName);
            Throw.IfNullOrEmpty(nameof(fileName), filePath);

            return WriteAsync(new FileInfo(filePath), cancellationToken);
        }

        /// <summary>
        /// Writes the image to the specified file name.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="defines">The defines to set.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task WriteAsync(string fileName, IWriteDefines defines)
            => WriteAsync(fileName, defines, CancellationToken.None);

        /// <summary>
        /// Writes the image to the specified file name.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="defines">The defines to set.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task WriteAsync(string fileName, IWriteDefines defines, CancellationToken cancellationToken)
        {
            _settings.SetDefines(defines);
            return WriteAsync(fileName, defines.Format, cancellationToken);
        }

        /// <summary>
        /// Writes the image to the specified file name.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="format">The format to use.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task WriteAsync(string fileName, MagickFormat format)
            => WriteAsync(fileName, format, CancellationToken.None);

        /// <summary>
        /// Writes the image to the specified file name.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="format">The format to use.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task WriteAsync(string fileName, MagickFormat format, CancellationToken cancellationToken)
        {
            string filePath = FileHelper.CheckForBaseDirectory(fileName);
            Throw.IfNullOrEmpty(nameof(fileName), filePath);

            var bytes = ToByteArray(format);
            return File.WriteAllBytesAsync(filePath, bytes, cancellationToken);
        }

        private void Read(ReadOnlySequence<byte> data, IMagickReadSettings<QuantumType>? readSettings, bool ping)
        {
            if (data.IsSingleSegment)
            {
                Read(data.FirstSpan, readSettings, ping);
                return;
            }

            throw new NotSupportedException();
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
