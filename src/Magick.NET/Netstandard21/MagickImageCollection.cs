// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETSTANDARD2_1
using System;
using System.Buffers;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick;

/// <content />
public sealed partial class MagickImageCollection
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

        var wrapper = new BufferWriterWrapper(bufferWriter);
        var writer = new ReadWriteStreamDelegate(wrapper.Write);

        using var imageAttacher = new TemporaryImageAttacher(_images);
        _nativeInstance.WriteStream(_images[0], settings, writer, null, null, null);
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
        using var tempFormat = new TemporaryMagickFormat(this, format);
        Write(bufferWriter);
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

    private void AddImages(ReadOnlySpan<byte> data, IMagickReadSettings<QuantumType>? readSettings, bool ping, string? fileName = null)
    {
        var settings = CreateSettings(readSettings);
        settings.Ping = ping;
        settings.FileName = fileName;

        var result = _nativeInstance.ReadBlob(settings, data, 0, data.Length);
        AddImages(result, settings);
    }
}

#endif
