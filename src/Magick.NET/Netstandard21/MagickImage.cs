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

/// <content/>
public sealed partial class MagickImage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImage"/> class.
    /// </summary>
    /// <param name="data">The span of bytes to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImage(ReadOnlySequence<byte> data)
        : this()
        => Read(data);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImage"/> class.
    /// </summary>
    /// <param name="data">The span of bytes to read the image data from.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImage(ReadOnlySequence<byte> data, MagickFormat format)
        : this()
        => Read(data, format);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImage"/> class.
    /// </summary>
    /// <param name="data">The span of bytes to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImage(ReadOnlySequence<byte> data, IMagickReadSettings<QuantumType> readSettings)
        : this()
        => Read(data, readSettings);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImage"/> class.
    /// </summary>
    /// <param name="data">The span of bytes to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImage(ReadOnlySpan<byte> data)
        : this()
        => Read(data);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImage"/> class.
    /// </summary>
    /// <param name="data">The span of bytes to read the image data from.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImage(ReadOnlySpan<byte> data, MagickFormat format)
        : this()
        => Read(data, format);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImage"/> class.
    /// </summary>
    /// <param name="data">The span of bytes to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImage(ReadOnlySpan<byte> data, IMagickReadSettings<QuantumType> readSettings)
        : this()
        => Read(data, readSettings);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImage"/> class.
    /// </summary>
    /// <param name="data">The span of bytes to read the image data from.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImage(ReadOnlySpan<byte> data, IPixelReadSettings<QuantumType> settings)
        : this()
        => ReadPixels(data, settings);

    /// <summary>
    /// Import pixels from the specified byte array into the current image.
    /// </summary>
    /// <param name="data">The quantum array to read the pixels from.</param>
    /// <param name="settings">The import settings to use when importing the pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void ImportPixels(ReadOnlySpan<byte> data, IPixelImportSettings settings)
    {
        Throw.IfEmpty(nameof(data), data);
        Throw.IfNull(nameof(settings), settings);
        Throw.IfNullOrEmpty(nameof(settings), settings.Mapping, "Pixel storage mapping should be defined.");
        Throw.IfTrue(nameof(settings), settings.StorageType == StorageType.Undefined, "Storage type should not be undefined.");

        var length = data.Length;
        var expectedLength = GetExpectedLength(settings);
        Throw.IfTrue(nameof(data), length < expectedLength, "The data length is {0} but should be at least {1}.", length, expectedLength);

        _nativeInstance.ImportPixels(settings.X, settings.Y, settings.Width, settings.Height, settings.Mapping, settings.StorageType, data, 0);
    }

#if !Q8
    /// <summary>
    /// Import pixels from the specified quantum array into the current image.
    /// </summary>
    /// <param name="data">The quantum array to read the pixels from.</param>
    /// <param name="settings">The import settings to use when importing the pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void ImportPixels(ReadOnlySpan<QuantumType> data, IPixelImportSettings settings)
    {
        Throw.IfEmpty(nameof(data), data);
        Throw.IfNull(nameof(settings), settings);
        Throw.IfNullOrEmpty(nameof(settings), settings.Mapping, "Pixel storage mapping should be defined.");
        Throw.IfTrue(nameof(settings), settings.StorageType != StorageType.Quantum, $"Storage type should be {nameof(StorageType.Quantum)}.");

        var length = data.Length;
        var expectedLength = GetExpectedLength(settings);
        Throw.IfTrue(nameof(data), length < expectedLength, "The data length is {0} but should be at least {1}.", length, expectedLength);

        _nativeInstance.ImportPixels(settings.X, settings.Y, settings.Width, settings.Height, settings.Mapping, settings.StorageType, data, 0);
    }
#endif

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="data">The sequence of bytes to read the information from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Ping(ReadOnlySequence<byte> data)
        => Ping(data, null);

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="data">The sequence of bytes to read the information from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Ping(ReadOnlySequence<byte> data, IMagickReadSettings<QuantumType>? readSettings)
    {
        Throw.IfEmpty(nameof(data), data);

        Read(data, readSettings, true);
    }

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="data">The span of bytes to read the information from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Ping(ReadOnlySpan<byte> data)
        => Ping(data, null);

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="data">The span of bytes to read the information from.</param>
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
    /// <param name="data">The sequence of bytes to read the information from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(ReadOnlySequence<byte> data)
        => Read(data, null);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="data">The sequence of bytes to read the information from.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(ReadOnlySequence<byte> data, MagickFormat format)
        => Read(data, new MagickReadSettings { Format = format });

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="data">The sequence of bytes to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(ReadOnlySequence<byte> data, IMagickReadSettings<QuantumType>? readSettings)
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
    /// <param name="data">The span of bytes to read the image data from.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void ReadPixels(ReadOnlySpan<byte> data, IPixelReadSettings<QuantumType> settings)
    {
        Throw.IfEmpty(nameof(data), data);
        Throw.IfNull(nameof(settings), settings);
        Throw.IfNullOrEmpty(nameof(settings), settings.Mapping, "Pixel storage mapping should be defined.");
        Throw.IfTrue(nameof(settings), settings.StorageType == StorageType.Undefined, "Storage type should not be undefined.");

        var newReadSettings = CreateReadSettings(settings.ReadSettings);
        SetSettings(newReadSettings);

        var length = data.Length;
        var expectedLength = GetExpectedByteLength(settings);
        Throw.IfTrue(nameof(data), length < expectedLength, "The data length is {0} but should be at least {1}.", length, expectedLength);

        _nativeInstance.ReadPixels(settings.ReadSettings.Width!.Value, settings.ReadSettings.Height!.Value, settings.Mapping, settings.StorageType, data, 0);
    }

#if !Q8
    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="data">The span of quantum to read the image data from.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void ReadPixels(ReadOnlySpan<QuantumType> data, IPixelReadSettings<QuantumType> settings)
    {
        Throw.IfEmpty(nameof(data), data);
        Throw.IfNull(nameof(settings), settings);
        Throw.IfNullOrEmpty(nameof(settings), settings.Mapping, "Pixel storage mapping should be defined.");
        Throw.IfTrue(nameof(settings), settings.StorageType != StorageType.Quantum, $"Storage type should be {nameof(StorageType.Quantum)}.");

        var newReadSettings = CreateReadSettings(settings.ReadSettings);
        SetSettings(newReadSettings);

        var length = data.Length;
        var expectedLength = GetExpectedLength(settings);
        Throw.IfTrue(nameof(data), length < expectedLength, "The data length is {0} but should be at least {1}.", length, expectedLength);

        _nativeInstance.ReadPixels(settings.ReadSettings.Width!.Value, settings.ReadSettings.Height!.Value, settings.Mapping, settings.StorageType, data, 0);
    }
#endif

    /// <summary>
    /// Writes the image to the specified file.
    /// </summary>
    /// <param name="bufferWriter">The buffer writer to write the image to.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Write(IBufferWriter<byte> bufferWriter)
    {
        Throw.IfNull(nameof(bufferWriter), bufferWriter);

        _settings.FileName = null;

        var wrapper = new BufferWriterWrapper(bufferWriter);
        var writer = new ReadWriteStreamDelegate(wrapper.Write);

        _nativeInstance.WriteStream(_settings, writer, null, null, null);
    }

    /// <summary>
    /// Writes the image to the specified file.
    /// </summary>
    /// <param name="bufferWriter">The buffer writer to write the image to.</param>
    /// <param name="defines">The defines to set.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Write(IBufferWriter<byte> bufferWriter, IWriteDefines defines)
    {
        _settings.SetDefines(defines);
        Write(bufferWriter, defines.Format);
    }

    /// <summary>
    /// Writes the image to the specified file.
    /// </summary>
    /// <param name="bufferWriter">The buffer writer to write the image to.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Write(IBufferWriter<byte> bufferWriter, MagickFormat format)
    {
        using var tempFormat = new TemporaryMagickFormat(this, format);
        Write(bufferWriter);
    }

    private void Read(ReadOnlySequence<byte> data, IMagickReadSettings<QuantumType>? readSettings, bool ping)
    {
        if (data.IsSingleSegment)
        {
            Read(data.FirstSpan, readSettings, ping);
            return;
        }

        var newReadSettings = CreateReadSettings(readSettings);
        SetSettings(newReadSettings);

        _settings.Ping = ping;
        _settings.FileName = null;

        var wrapper = new ReadOnlySequenceWrapper(data);
        var reader = new ReadWriteStreamDelegate(wrapper.Read);
        var seeker = new SeekStreamDelegate(wrapper.Seek);
        var teller = new TellStreamDelegate(wrapper.Tell);

        _nativeInstance.ReadStream(_settings, reader, seeker, teller);

        ResetSettings();
    }

    private void Read(ReadOnlySpan<byte> data, IMagickReadSettings<QuantumType>? readSettings, bool ping, string? fileName = null)
    {
        var newReadSettings = CreateReadSettings(readSettings);
        SetSettings(newReadSettings);

        _settings.Ping = ping;
        _settings.FileName = fileName;

        _nativeInstance.ReadBlob(_settings, data, 0, data.Length);

        ResetSettings();
    }

    private unsafe sealed partial class NativeMagickImage : NativeInstance
    {
        public void ImportPixels(int x, int y, int width, int height, string map, StorageType storageType, ReadOnlySpan<byte> data, int offsetInBytes)
        {
            fixed (byte* dataFixed = data)
            {
                ImportPixels(x, y, width, height, map, storageType, dataFixed, offsetInBytes);
            }
        }

        public void ReadPixels(int width, int height, string map, StorageType storageType, ReadOnlySpan<byte> data, int offsetInBytes)
        {
            fixed (byte* dataFixed = data)
            {
                ReadPixels(width, height, map, storageType, dataFixed, offsetInBytes);
            }
        }

#if !Q8
        public void ImportPixels(int x, int y, int width, int height, string map, StorageType storageType, ReadOnlySpan<QuantumType> data, int offsetInBytes)
        {
            fixed (QuantumType* dataFixed = data)
            {
                ImportPixels(x, y, width, height, map, storageType, dataFixed, offsetInBytes);
            }
        }

        public void ReadPixels(int width, int height, string map, StorageType storageType, ReadOnlySpan<QuantumType> data, int offsetInBytes)
        {
            fixed (QuantumType* dataFixed = data)
            {
                ReadPixels(width, height, map, storageType, dataFixed, offsetInBytes);
            }
        }
#endif

    }
}

#endif
