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
    /// <content />
    public sealed partial class MagickImageCollection : IMagickImageCollection<QuantumType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MagickImageCollection"/> class.
        /// </summary>
        /// <param name="data">The sequence of bytes to read the image data from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public MagickImageCollection(ReadOnlySequence<byte> data)
            : this()
            => Read(data);

        /// <summary>
        /// Initializes a new instance of the <see cref="MagickImageCollection"/> class.
        /// </summary>
        /// <param name="data">The sequence of bytes to read the image data from.</param>
        /// <param name="format">The format to use.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public MagickImageCollection(ReadOnlySequence<byte> data, MagickFormat format)
            : this()
            => Read(data, format);

        /// <summary>
        /// Initializes a new instance of the <see cref="MagickImageCollection"/> class.
        /// </summary>
        /// <param name="data">The sequence of bytes to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public MagickImageCollection(ReadOnlySequence<byte> data, IMagickReadSettings<QuantumType> readSettings)
            : this()
            => Read(data, readSettings);

        /// <summary>
        /// Initializes a new instance of the <see cref="MagickImageCollection"/> class.
        /// </summary>
        /// <param name="data">The span of bytes to read the image data from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public MagickImageCollection(ReadOnlySpan<byte> data)
            : this()
            => Read(data);

        /// <summary>
        /// Initializes a new instance of the <see cref="MagickImageCollection"/> class.
        /// </summary>
        /// <param name="data">The span of bytes to read the image data from.</param>
        /// <param name="format">The format to use.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public MagickImageCollection(ReadOnlySpan<byte> data, MagickFormat format)
            : this()
            => Read(data, format);

        /// <summary>
        /// Initializes a new instance of the <see cref="MagickImageCollection"/> class.
        /// </summary>
        /// <param name="data">The span of bytes to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public MagickImageCollection(ReadOnlySpan<byte> data, IMagickReadSettings<QuantumType> readSettings)
            : this()
            => Read(data, readSettings);

        /// <summary>
        /// Read only metadata and not the pixel data from all image frames.
        /// </summary>
        /// <param name="data">The sequence of bytes to read the image data from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public void Ping(ReadOnlySequence<byte> data)
            => Ping(data, null);

        /// <summary>
        /// Read only metadata and not the pixel data from all image frames.
        /// </summary>
        /// <param name="data">The sequence of bytes to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public void Ping(ReadOnlySequence<byte> data, IMagickReadSettings<QuantumType>? readSettings)
        {
            Throw.IfEmpty(nameof(data), data);

            Clear();
            AddImages(data, readSettings, true);
        }

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
        /// <param name="data">The sequence of bytes to read the image data from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public void Read(ReadOnlySequence<byte> data)
            => Read(data, null);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="data">The sequence of bytes to read the image data from.</param>
        /// <param name="format">The format to use.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public void Read(ReadOnlySequence<byte> data, MagickFormat format)
            => Read(data, new MagickReadSettings { Format = format });

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="data">The sequence of bytes to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public void Read(ReadOnlySequence<byte> data, IMagickReadSettings<QuantumType>? readSettings)
        {
            Throw.IfEmpty(nameof(data), data);

            Clear();
            AddImages(data, readSettings, false);
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
            => ReadAsync(file, CancellationToken.None);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="file">The file to read the frames from.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(FileInfo file, CancellationToken cancellationToken)
            => ReadAsync(file, null, cancellationToken);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="file">The file to read the frames from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(FileInfo file, IMagickReadSettings<QuantumType>? readSettings)
            => ReadAsync(file, readSettings, CancellationToken.None);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="file">The file to read the frames from.</param>
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
        /// Read all image frames.
        /// </summary>
        /// <param name="file">The file to read the frames from.</param>
        /// <param name="format">The format to use.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(FileInfo file, MagickFormat format)
            => ReadAsync(file, format, CancellationToken.None);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="file">The file to read the frames from.</param>
        /// <param name="format">The format to use.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(FileInfo file, MagickFormat format, CancellationToken cancellationToken)
            => ReadAsync(file, new MagickReadSettings { Format = format }, cancellationToken);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(string fileName)
            => ReadAsync(fileName, CancellationToken.None);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(string fileName, CancellationToken cancellationToken)
            => ReadAsync(fileName, null, cancellationToken);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(string fileName, IMagickReadSettings<QuantumType>? readSettings)
            => ReadAsync(fileName, readSettings, CancellationToken.None);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public async Task ReadAsync(string fileName, IMagickReadSettings<QuantumType>? readSettings, CancellationToken cancellationToken)
        {
            Throw.IfNullOrEmpty(nameof(fileName), fileName);

            var bytes = await File.ReadAllBytesAsync(fileName, cancellationToken).ConfigureAwait(false);

            cancellationToken.ThrowIfCancellationRequested();
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
            => ReadAsync(fileName, format, CancellationToken.None);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="format">The format to use.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task ReadAsync(string fileName, MagickFormat format, CancellationToken cancellationToken)
            => ReadAsync(fileName, new MagickReadSettings { Format = format }, cancellationToken);

        /// <summary>
        /// Writes the images to the specified buffer writter. If the output image's file format does not
        /// allow multi-image files multiple files will be written.
        /// </summary>
        /// <param name="bufferWriter">The buffer writer to write the images to.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public void Write(IBufferWriter<byte> bufferWriter)
        {
            Throw.IfNull(nameof(bufferWriter), bufferWriter);

            if (_images.Count == 0)
                return;

            var settings = GetSettings().Clone();
            settings.FileName = null;

            try
            {
                AttachImages();

                var wrapper = new BufferWriterWrapper(bufferWriter);
                var writer = new ReadWriteStreamDelegate(wrapper.Write);

                _nativeInstance.WriteStream(_images[0], settings, writer, null, null, null);
            }
            finally
            {
                DetachImages();
            }
        }

        /// <summary>
        /// Writes the images to the specified buffer writter. If the output image's file format does not
        /// allow multi-image files multiple files will be written.
        /// </summary>
        /// <param name="bufferWriter">The buffer writer to write the images to.</param>
        /// <param name="defines">The defines to set.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public void Write(IBufferWriter<byte> bufferWriter, IWriteDefines defines)
        {
            SetDefines(defines);
            Write(bufferWriter, defines.Format);
        }

        /// <summary>
        /// Writes the images to the specified buffer writter. If the output image's file format does not
        /// allow multi-image files multiple files will be written.
        /// </summary>
        /// <param name="bufferWriter">The buffer writer to write the images to.</param>
        /// <param name="format">The format to use.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public void Write(IBufferWriter<byte> bufferWriter, MagickFormat format)
        {
            using (_ = new TemporaryMagickFormat(this, format))
            {
                Write(bufferWriter);
            }
        }

        /// <summary>
        /// Writes the images to the specified file. If the output image's file format does not
        /// allow multi-image files multiple files will be written.
        /// </summary>
        /// <param name="file">The file to write the image to.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task WriteAsync(FileInfo file)
            => WriteAsync(file, CancellationToken.None);

        /// <summary>
        /// Writes the images to the specified file. If the output image's file format does not
        /// allow multi-image files multiple files will be written.
        /// </summary>
        /// <param name="file">The file to write the image to.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public async Task WriteAsync(FileInfo file, CancellationToken cancellationToken)
        {
            Throw.IfNull(nameof(file), file);

            if (_images.Count == 0)
                return;

            var formatInfo = MagickFormatInfo.Create(file);

            try
            {
                AttachImages();

                var bytes = formatInfo is not null ? ToByteArray(formatInfo.Format) : ToByteArray();
                await File.WriteAllBytesAsync(file.FullName, bytes, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                DetachImages();
            }
        }

        /// <summary>
        /// Writes the images to the specified file. If the output image's file format does not
        /// allow multi-image files multiple files will be written.
        /// </summary>
        /// <param name="file">The file to write the image to.</param>
        /// <param name="defines">The defines to set.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task WriteAsync(FileInfo file, IWriteDefines defines)
            => WriteAsync(file, defines, CancellationToken.None);

        /// <summary>
        /// Writes the images to the specified file. If the output image's file format does not
        /// allow multi-image files multiple files will be written.
        /// </summary>
        /// <param name="file">The file to write the image to.</param>
        /// <param name="defines">The defines to set.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task WriteAsync(FileInfo file, IWriteDefines defines, CancellationToken cancellationToken)
        {
            SetDefines(defines);
            return WriteAsync(file, defines.Format, cancellationToken);
        }

        /// <summary>
        /// Writes the images to the specified file. If the output image's file format does not
        /// allow multi-image files multiple files will be written.
        /// </summary>
        /// <param name="file">The file to write the image to.</param>
        /// <param name="format">The format to use.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task WriteAsync(FileInfo file, MagickFormat format)
            => WriteAsync(file, format, CancellationToken.None);

        /// <summary>
        /// Writes the images to the specified file. If the output image's file format does not
        /// allow multi-image files multiple files will be written.
        /// </summary>
        /// <param name="file">The file to write the image to.</param>
        /// <param name="format">The format to use.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public async Task WriteAsync(FileInfo file, MagickFormat format, CancellationToken cancellationToken)
        {
            Throw.IfNull(nameof(file), file);

            if (_images.Count == 0)
                return;

            try
            {
                AttachImages();

                var bytes = ToByteArray(format);
                await File.WriteAllBytesAsync(file.FullName, bytes, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                DetachImages();
            }
        }

        /// <summary>
        /// Writes the images to the specified file name. If the output image's file format does not
        /// allow multi-image files multiple files will be written.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task WriteAsync(string fileName)
            => WriteAsync(fileName, CancellationToken.None);

        /// <summary>
        /// Writes the images to the specified file name. If the output image's file format does not
        /// allow multi-image files multiple files will be written.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task WriteAsync(string fileName, CancellationToken cancellationToken)
        {
            var filePath = FileHelper.CheckForBaseDirectory(fileName);
            Throw.IfNullOrEmpty(nameof(fileName), filePath);

            return WriteAsync(new FileInfo(filePath), cancellationToken);
        }

        /// <summary>
        /// Writes the images to the specified file name. If the output image's file format does not
        /// allow multi-image files multiple files will be written.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="defines">The defines to set.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task WriteAsync(string fileName, IWriteDefines defines)
            => WriteAsync(fileName, defines, CancellationToken.None);

        /// <summary>
        /// Writes the images to the specified file name. If the output image's file format does not
        /// allow multi-image files multiple files will be written.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="defines">The defines to set.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task WriteAsync(string fileName, IWriteDefines defines, CancellationToken cancellationToken)
        {
            SetDefines(defines);
            return WriteAsync(fileName, defines.Format, cancellationToken);
        }

        /// <summary>
        /// Writes the images to the specified file name. If the output image's file format does not
        /// allow multi-image files multiple files will be written.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="format">The format to use.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public Task WriteAsync(string fileName, MagickFormat format)
            => WriteAsync(fileName, format, CancellationToken.None);

        /// <summary>
        /// Writes the images to the specified file name. If the output image's file format does not
        /// allow multi-image files multiple files will be written.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="format">The format to use.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public async Task WriteAsync(string fileName, MagickFormat format, CancellationToken cancellationToken)
        {
            var filePath = FileHelper.CheckForBaseDirectory(fileName);
            Throw.IfNullOrEmpty(nameof(fileName), filePath);

            if (_images.Count == 0)
                return;

            try
            {
                AttachImages();

                var bytes = ToByteArray(format);
                await File.WriteAllBytesAsync(filePath, bytes, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                DetachImages();
            }
        }

        private void AddImages(ReadOnlySequence<byte> data, IMagickReadSettings<QuantumType>? readSettings, bool ping)
        {
            if (data.IsSingleSegment)
            {
                AddImages(data.FirstSpan, readSettings, ping);
                return;
            }

            var settings = CreateSettings(readSettings);
            settings.Ping = ping;
            settings.FileName = null;

            var wrapper = new ReadOnlySequenceWrapper(data);
            var reader = new ReadWriteStreamDelegate(wrapper.Read);
            var seeker = new SeekStreamDelegate(wrapper.Seek);
            var teller = new TellStreamDelegate(wrapper.Tell);

            var result = _nativeInstance.ReadStream(settings, reader, seeker, teller);
            AddImages(result, settings);
        }

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
