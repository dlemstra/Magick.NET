// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
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

namespace ImageMagick;

/// <summary>
/// Class that represents an ImageMagick image.
/// </summary>
public sealed partial class MagickImage : IMagickImage<QuantumType>, INativeInstance
{
    private ProgressDelegate? _nativeProgress;
    private EventHandler<ProgressEventArgs>? _progress;
    private EventHandler<WarningEventArgs>? _warning;

    private MagickSettings _settings;

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImage"/> class.
    /// </summary>
    public MagickImage()
    {
        SetSettings(new MagickSettings());
        SetInstance(new NativeMagickImage(Settings));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImage"/> class.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImage(byte[] data)
        : this()
        => Read(data);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImage"/> class.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImage(byte[] data, int offset, int count)
        : this()
        => Read(data, offset, count);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImage"/> class.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImage(byte[] data, int offset, int count, MagickFormat format)
        : this()
        => Read(data, offset, count, format);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImage"/> class.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImage(byte[] data, int offset, int count, IMagickReadSettings<QuantumType> readSettings)
        : this()
        => Read(data, offset, count, readSettings);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImage"/> class.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImage(byte[] data, MagickFormat format)
        : this()
        => Read(data, format);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImage"/> class.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImage(byte[] data, IMagickReadSettings<QuantumType> readSettings)
        : this()
        => Read(data, readSettings);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImage"/> class.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImage(FileInfo file)
        : this()
        => Read(file);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImage"/> class.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImage(FileInfo file, MagickFormat format)
        : this()
        => Read(file, format);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImage"/> class.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImage(FileInfo file, IMagickReadSettings<QuantumType> readSettings)
        : this()
        => Read(file, readSettings);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImage"/> class.
    /// </summary>
    /// <param name="color">The color to fill the image with.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    public MagickImage(IMagickColor<QuantumType> color, int width, int height)
        : this()
        => Read(color, width, height);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImage"/> class.
    /// </summary>
    /// <param name="image">The image to create a copy of.</param>
    public MagickImage(IMagickImage<QuantumType> image)
    {
        Throw.IfNull(nameof(image), image);

        if (image is not MagickImage magickImage)
            throw new NotSupportedException();

        SetSettings(magickImage._settings.Clone());
        SetInstance(new NativeMagickImage(magickImage._nativeInstance.Clone()));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImage"/> class.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImage(Stream stream)
        : this()
        => Read(stream);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImage"/> class.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImage(Stream stream, MagickFormat format)
        : this()
        => Read(stream, format);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImage"/> class.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImage(Stream stream, IMagickReadSettings<QuantumType> readSettings)
        : this()
        => Read(stream, readSettings);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImage"/> class.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImage(string fileName)
        : this()
        => Read(fileName);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImage"/> class.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImage(string fileName, int width, int height)
        : this()
        => Read(fileName, width, height);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImage"/> class.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImage(string fileName, IMagickReadSettings<QuantumType> readSettings)
        : this()
        => Read(fileName, readSettings);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImage"/> class.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImage(string fileName, MagickFormat format)
        : this()
        => Read(fileName, format);

    private MagickImage(NativeMagickImage instance, MagickSettings settings)
    {
        SetSettings(settings);
        SetInstance(instance);
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="MagickImage"/> class.
    /// </summary>
    ~MagickImage()
        => Dispose(false);

    /// <summary>
    /// Event that will be raised when progress is reported by this image.
    /// </summary>
    public event EventHandler<ProgressEventArgs> Progress
    {
        add
        {
            if (_progress is null)
            {
                _nativeProgress = new ProgressDelegate(OnProgress);
                _nativeInstance.SetProgressDelegate(_nativeProgress);
            }

            _progress += value;
        }

        remove
        {
            _progress -= value;

            if (_progress is null)
            {
                _nativeInstance.SetProgressDelegate(null);
                _nativeProgress = null;
            }
        }
    }

    /// <summary>
    /// Event that will we raised when a warning is thrown by ImageMagick.
    /// </summary>
    public event EventHandler<WarningEventArgs> Warning
    {
        add => _warning += value;
        remove => _warning -= value;
    }

    /// <inheritdoc/>
    IntPtr INativeInstance.Instance
        => _nativeInstance.Instance;

    /// <summary>
    /// Gets or sets the time in 1/100ths of a second which must expire before splaying the next image in an
    /// animated sequence.
    /// </summary>
    public int AnimationDelay
    {
        get => _nativeInstance.AnimationDelay;
        set
        {
            if (value >= 0)
                _nativeInstance.AnimationDelay = value;
        }
    }

    /// <summary>
    /// Gets or sets the number of iterations to loop an animation (e.g. Netscape loop extension) for.
    /// </summary>
    public int AnimationIterations
    {
        get => _nativeInstance.AnimationIterations;
        set
        {
            if (value >= 0)
                _nativeInstance.AnimationIterations = value;
        }
    }

    /// <summary>
    /// Gets or sets the ticks per seconds for the animation delay.
    /// </summary>
    public int AnimationTicksPerSecond
    {
        get => _nativeInstance.AnimationTicksPerSecond;
        set
        {
            if (value >= 0)
                _nativeInstance.AnimationTicksPerSecond = value;
        }
    }

    /// <summary>
    /// Gets the names of the artifacts.
    /// </summary>
    public IEnumerable<string> ArtifactNames
    {
        get
        {
            _nativeInstance.ResetArtifactIterator();
            var name = _nativeInstance.GetNextArtifactName();
            while (name is not null)
            {
                yield return name;
                name = _nativeInstance.GetNextArtifactName();
            }
        }
    }

    /// <summary>
    /// Gets the names of the attributes.
    /// </summary>
    public IEnumerable<string> AttributeNames
    {
        get
        {
            _nativeInstance.ResetAttributeIterator();
            var name = _nativeInstance.GetNextAttributeName();
            while (name is not null)
            {
                yield return name;
                name = _nativeInstance.GetNextAttributeName();
            }
        }
    }

    /// <summary>
    /// Gets or sets the background color of the image.
    /// </summary>
    public IMagickColor<QuantumType>? BackgroundColor
    {
        get => _nativeInstance.BackgroundColor;
        set
        {
            _nativeInstance.BackgroundColor = value;
            _settings.BackgroundColor = value;
        }
    }

    /// <summary>
    /// Gets the height of the image before transformations.
    /// </summary>
    public int BaseHeight
        => _nativeInstance.BaseHeight;

    /// <summary>
    /// Gets the width of the image before transformations.
    /// </summary>
    public int BaseWidth
        => _nativeInstance.BaseWidth;

    /// <summary>
    /// Gets or sets a value indicating whether black point compensation should be used.
    /// </summary>
    public bool BlackPointCompensation
    {
        get => _nativeInstance.BlackPointCompensation;
        set => _nativeInstance.BlackPointCompensation = value;
    }

    /// <summary>
    /// Gets or sets the border color of the image.
    /// </summary>
    public IMagickColor<QuantumType>? BorderColor
    {
        get => _nativeInstance.BorderColor;
        set => _nativeInstance.BorderColor = value;
    }

    /// <summary>
    /// Gets the smallest bounding box enclosing non-border pixels. The current fuzz value is used
    /// when discriminating between pixels.
    /// </summary>
    public IMagickGeometry? BoundingBox
    {
        get
        {
            var boundingBox = _nativeInstance.BoundingBox;
            if (boundingBox is null)
                throw new MagickErrorException("Unable to allocate rectangle");

            var geometry = MagickGeometry.FromRectangle(boundingBox);
            if (geometry.Width == 0 || geometry.Height == 0)
                return null;

            return geometry;
        }
    }

    /// <summary>
    /// Gets the number of channels that the image contains.
    /// </summary>
    public int ChannelCount
        => _nativeInstance.ChannelCount;

    /// <summary>
    /// Gets the color and metadata channels of the image.
    /// </summary>
    public IEnumerable<PixelChannel> Channels
    {
        get
        {
            foreach (var channel in new[]
            {
                PixelChannel.Red,
                PixelChannel.Green,
                PixelChannel.Blue,
                PixelChannel.Black,
                PixelChannel.Alpha,
            })
            {
                if (_nativeInstance.HasChannel(channel))
                    yield return channel;
            }

            for (var channel = PixelChannel.Meta0; channel <= PixelChannel.Meta53; channel++)
            {
                if (_nativeInstance.HasChannel(channel))
                    yield return channel;
                else
                    yield break;
            }
        }
    }

    /// <summary>
    /// Gets or sets the chromaticity of the image.
    /// </summary>
    public IChromaticityInfo Chromaticity
    {
        get
        {
            if (_nativeInstance.ChromaRedPrimary is null ||
                _nativeInstance.ChromaGreenPrimary is null ||
                _nativeInstance.ChromaBluePrimary is null ||
                _nativeInstance.ChromaWhitePoint is null)
                throw new MagickErrorException("Unable to allocate primary info");

            return new ChromaticityInfo(
                _nativeInstance.ChromaRedPrimary,
                _nativeInstance.ChromaGreenPrimary,
                _nativeInstance.ChromaBluePrimary,
                _nativeInstance.ChromaWhitePoint);
        }

        set
        {
            _nativeInstance.ChromaRedPrimary = value.Red;
            _nativeInstance.ChromaGreenPrimary = value.Green;
            _nativeInstance.ChromaBluePrimary = value.Blue;
            _nativeInstance.ChromaWhitePoint = value.White;
        }
    }

    /// <summary>
    /// Gets or sets the chromaticity blue primary point.
    /// </summary>
    [Obsolete($"This property will be removed in the next major release, use {nameof(Chromaticity)} instead.")]
    public IPrimaryInfo ChromaBluePrimary
    {
        get
        {
            var primaryInfo = _nativeInstance.ChromaBluePrimary;

            if (primaryInfo is null)
                throw new MagickErrorException("Unable to allocate primary info");

            return primaryInfo;
        }
        set => _nativeInstance.ChromaBluePrimary = value;
    }

    /// <summary>
    /// Gets or sets the chromaticity green primary point.
    /// </summary>
    [Obsolete($"This property will be removed in the next major release, use {nameof(Chromaticity)} instead.")]
    public IPrimaryInfo ChromaGreenPrimary
    {
        get
        {
            var primaryInfo = _nativeInstance.ChromaGreenPrimary;

            if (primaryInfo is null)
                throw new MagickErrorException("Unable to allocate primary info");

            return primaryInfo;
        }
        set => _nativeInstance.ChromaGreenPrimary = value;
    }

    /// <summary>
    /// Gets or sets the chromaticity red primary point.
    /// </summary>
    [Obsolete($"This property will be removed in the next major release, use {nameof(Chromaticity)} instead.")]
    public IPrimaryInfo ChromaRedPrimary
    {
        get
        {
            var primaryInfo = _nativeInstance.ChromaRedPrimary;

            if (primaryInfo is null)
                throw new MagickErrorException("Unable to allocate primary info");

            return primaryInfo;
        }
        set => _nativeInstance.ChromaRedPrimary = value;
    }

    /// <summary>
    /// Gets or sets the chromaticity white primary point.
    /// </summary>
    [Obsolete($"This property will be removed in the next major release, use {nameof(Chromaticity)} instead.")]
    public IPrimaryInfo ChromaWhitePoint
    {
        get
        {
            var primaryInfo = _nativeInstance.ChromaWhitePoint;

            if (primaryInfo is null)
                throw new MagickErrorException("Unable to allocate primary info");

            return primaryInfo;
        }
        set => _nativeInstance.ChromaWhitePoint = value;
    }

    /// <summary>
    /// Gets or sets the image class (DirectClass or PseudoClass)
    /// NOTE: Setting a DirectClass image to PseudoClass will result in the loss of color information
    /// if the number of colors in the image is greater than the maximum palette size (either 256 (Q8)
    /// or 65536 (Q16).
    /// </summary>
    public ClassType ClassType
    {
        get => _nativeInstance.ClassType;
        set => _nativeInstance.ClassType = value;
    }

    /// <summary>
    /// Gets or sets the distance where colors are considered equal.
    /// </summary>
    public Percentage ColorFuzz
    {
        get => PercentageHelper.FromQuantum(_nativeInstance.ColorFuzz);
        set
        {
            var newValue = PercentageHelper.ToQuantum(value);
            _nativeInstance.ColorFuzz = newValue;
            _settings.ColorFuzz = newValue;
        }
    }

    /// <summary>
    /// Gets or sets the colormap size (number of colormap entries).
    /// </summary>
    public int ColormapSize
    {
        get => _nativeInstance.ColormapSize;
        set => _nativeInstance.ColormapSize = value;
    }

    /// <summary>
    /// Gets or sets the color space of the image.
    /// </summary>
    public ColorSpace ColorSpace
    {
        get => _nativeInstance.ColorSpace;
        set => _nativeInstance.ColorSpace = value;
    }

    /// <summary>
    /// Gets or sets the color type of the image.
    /// </summary>
    public ColorType ColorType
    {
        get
        {
            if (_settings.ColorType != ColorType.Undefined)
                return _settings.ColorType;

            return _nativeInstance.ColorType;
        }

        set => _nativeInstance.ColorType = value;
    }

    /// <summary>
    /// Gets or sets the comment text of the image.
    /// </summary>
    public string? Comment
    {
        get => GetAttribute("comment");
        set
        {
            if (value is not null)
                SetAttribute("comment", value);
            else
                RemoveAttribute("comment");
        }
    }

    /// <summary>
    /// Gets or sets the composition operator to be used when composition is implicitly used (such as for image flattening).
    /// </summary>
    public CompositeOperator Compose
    {
        get => _nativeInstance.Compose;
        set => _nativeInstance.Compose = value;
    }

    /// <summary>
    /// Gets the compression method of the image.
    /// </summary>
    public CompressionMethod Compression
        => _nativeInstance.Compression;

    /// <summary>
    /// Gets or sets the vertical and horizontal resolution in pixels of the image.
    /// </summary>
    public Density Density
    {
        get => new Density(_nativeInstance.ResolutionX, _nativeInstance.ResolutionY, _nativeInstance.ResolutionUnits);
        set
        {
            if (value is null)
                return;

            _nativeInstance.ResolutionX = value.X;
            _nativeInstance.ResolutionY = value.Y;
            _nativeInstance.ResolutionUnits = value.Units;
        }
    }

    /// <summary>
    /// Gets or sets the depth (bits allocated to red/green/blue components).
    /// </summary>
    public int Depth
    {
        get => _nativeInstance.Depth;
        set => _nativeInstance.Depth = value;
    }

    /// <summary>
    /// Gets the preferred size of the image when encoding.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    [Obsolete("Will be removed in the next major release.")]
    public IMagickGeometry? EncodingGeometry
        => MagickGeometry.FromString(_nativeInstance.EncodingGeometry);

    /// <summary>
    /// Gets or sets the endianness (little like Intel or big like SPARC) for image formats which support
    /// endian-specific options.
    /// </summary>
    public Endian Endian
    {
        get => _nativeInstance.Endian;
        set => _nativeInstance.Endian = value;
    }

    /// <summary>
    /// Gets the original file name of the image (only available if read from disk).
    /// </summary>
    public string? FileName
        => _nativeInstance.FileName;

    /// <summary>
    /// Gets or sets the filter to use when resizing image.
    /// </summary>
    public FilterType FilterType
    {
        get => _nativeInstance.FilterType;
        set => _nativeInstance.FilterType = value;
    }

    /// <summary>
    /// Gets or sets the format of the image.
    /// </summary>
    public MagickFormat Format
    {
        get => EnumHelper.Parse(_nativeInstance.Format, MagickFormat.Unknown);
        set
        {
            _nativeInstance.Format = Enum.GetName(value.GetType(), value);
            _settings.Format = value;
        }
    }

    /// <summary>
    /// Gets the information about the format of the image.
    /// </summary>
    [Obsolete("Will be removed in the next major release.")]
    public IMagickFormatInfo? FormatInfo
        => MagickFormatInfo.Create(Format);

    /// <summary>
    /// Gets the gamma level of the image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public double Gamma
        => _nativeInstance.Gamma;

    /// <summary>
    /// Gets or sets the gif disposal method.
    /// </summary>
    public GifDisposeMethod GifDisposeMethod
    {
        get => _nativeInstance.GifDisposeMethod;
        set => _nativeInstance.GifDisposeMethod = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the image supports transparency (alpha channel).
    /// </summary>
    public bool HasAlpha
    {
        get => _nativeInstance.HasAlpha;
        set
        {
            if (_nativeInstance.HasAlpha != value)
            {
                if (value)
                    Alpha(AlphaOption.Opaque);
                _nativeInstance.HasAlpha = value;
            }
        }
    }

    /// <summary>
    /// Gets the height of the image.
    /// </summary>
    public int Height
        => _nativeInstance.Height;

    /// <summary>
    /// Gets or sets the type of interlacing to use.
    /// </summary>
    public Interlace Interlace
    {
        get => _nativeInstance.Interlace;
        [Obsolete($"The setter will be removed in the next major release, use Settings.{nameof(Interlace)} instead.")]
        set
        {
            _nativeInstance.Interlace = value;
            _settings.Interlace = value;
        }
    }

    /// <summary>
    /// Gets or sets the pixel color interpolate method to use.
    /// </summary>
    public PixelInterpolateMethod Interpolate
    {
        get => _nativeInstance.Interpolate;
        set => _nativeInstance.Interpolate = value;
    }

    /// <summary>
    /// Gets a value indicating whether the instance is disposed.
    /// </summary>
    [Obsolete("Will be removed in the next major release.")]
    public bool IsDisposed
        => _nativeInstance.IsDisposed;

    /// <summary>
    /// Gets a value indicating whether none of the pixels in the image have an alpha value other
    /// than OpaqueAlpha (QuantumRange).
    /// </summary>
    public bool IsOpaque
        => _nativeInstance.IsOpaque;

    /// <summary>
    /// Gets or sets the label of the image.
    /// </summary>
    public string? Label
    {
        get => GetAttribute("label");
        set
        {
            if (value is not null)
                SetAttribute("label", value);
            else
                RemoveAttribute("label");
        }
    }

    /// <summary>
    /// Gets or sets the matte color.
    /// </summary>
    public IMagickColor<QuantumType>? MatteColor
    {
        get => _nativeInstance.MatteColor;
        set => _nativeInstance.MatteColor = value;
    }

    /// <summary>
    /// Gets or sets the photo orientation of the image.
    /// </summary>
    public OrientationType Orientation
    {
        get => _nativeInstance.Orientation;
        set => _nativeInstance.Orientation = value;
    }

    /// <summary>
    /// Gets or sets the preferred size and location of an image canvas.
    /// </summary>
    public IMagickGeometry Page
    {
        get
        {
            var page = _nativeInstance.Page;
            if (page is null)
                throw new MagickErrorException("Unable to allocate rectangle");

            return MagickGeometry.FromRectangle(page);
        }

        set
        {
            _nativeInstance.Page = MagickRectangle.FromGeometry(value, this);
        }
    }

    /// <summary>
    /// Gets the names of the profiles.
    /// </summary>
    public IEnumerable<string> ProfileNames
    {
        get
        {
            _nativeInstance.ResetProfileIterator();
            var name = _nativeInstance.GetNextProfileName();
            while (name is not null)
            {
                yield return name;
                name = _nativeInstance.GetNextProfileName();
            }
        }
    }

    /// <summary>
    /// Gets or sets the JPEG/MIFF/PNG compression level (default 75).
    /// </summary>
    public int Quality
    {
        get => _nativeInstance.Quality;
        set
        {
            var quality = value < 1 ? 1 : value;
            quality = quality > 100 ? 100 : quality;

            _nativeInstance.Quality = quality;
            _settings.Quality = quality;
        }
    }

    /// <summary>
    /// Gets or sets the type of rendering intent.
    /// </summary>
    public RenderingIntent RenderingIntent
    {
        get => _nativeInstance.RenderingIntent;
        set => _nativeInstance.RenderingIntent = value;
    }

    /// <summary>
    /// Gets the settings for this MagickImage instance.
    /// </summary>
    public IMagickSettings<QuantumType> Settings
        => _settings;

    /// <summary>
    /// Gets the signature of this image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public string Signature
        => _nativeInstance.Signature;

    /// <summary>
    /// Gets the number of colors in the image.
    /// </summary>
    public int TotalColors
        => _nativeInstance.TotalColors;

    /// <summary>
    /// Gets or sets the virtual pixel method.
    /// </summary>
    public VirtualPixelMethod VirtualPixelMethod
    {
        get => _nativeInstance.VirtualPixelMethod;
        set => _nativeInstance.VirtualPixelMethod = value;
    }

    /// <summary>
    /// Gets the width of the image.
    /// </summary>
    public int Width
        => _nativeInstance.Width;

    /// <summary>
    /// Determines whether the first <see cref="MagickImage"/> is more than the second <see cref="MagickImage"/>.
    /// </summary>
    /// <param name="left">The first <see cref="MagickImage"/> to compare.</param>
    /// <param name="right">The second <see cref="MagickImage"/> to compare.</param>
    public static bool operator >(MagickImage? left, MagickImage? right)
    {
        if (left is null)
            return right is null;

        return left.CompareTo(right) == 1;
    }

    /// <summary>
    /// Determines whether the first <see cref="MagickImage"/> is less than the second <see cref="MagickImage"/>.
    /// </summary>
    /// <param name="left">The first <see cref="MagickImage"/> to compare.</param>
    /// <param name="right">The second <see cref="MagickImage"/> to compare.</param>
    public static bool operator <(MagickImage? left, MagickImage? right)
    {
        if (left is null)
            return right is not null;

        return left.CompareTo(right) == -1;
    }

    /// <summary>
    /// Determines whether the first <see cref="MagickImage"/> is more than or equal to the second <see cref="MagickImage"/>.
    /// </summary>
    /// <param name="left">The first <see cref="MagickImage"/> to compare.</param>
    /// <param name="right">The second <see cref="MagickImage"/> to compare.</param>
    public static bool operator >=(MagickImage? left, MagickImage? right)
    {
        if (left is null)
            return right is null;

        return left.CompareTo(right) >= 0;
    }

    /// <summary>
    /// Determines whether the first <see cref="MagickImage"/> is less than or equal to the second <see cref="MagickImage"/>.
    /// </summary>
    /// <param name="left">The first <see cref="MagickImage"/> to compare.</param>
    /// <param name="right">The second <see cref="MagickImage"/> to compare.</param>
    public static bool operator <=(MagickImage? left, MagickImage? right)
    {
        if (left is null)
            return right is not null;

        return left.CompareTo(right) <= 0;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImage"/> class using the specified base64 string.
    /// </summary>
    /// <param name="value">The base64 string to load the image from.</param>
    /// <returns>A new instance of the <see cref="MagickImage"/> class.</returns>
    public static IMagickImage<QuantumType> FromBase64(string value)
    {
        var data = Convert.FromBase64String(value);
        return new MagickImage(data);
    }

    /// <summary>
    /// Adaptive-blur image with the default blur factor (0x1).
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AdaptiveBlur()
        => AdaptiveBlur(0.0, 1.0);

    /// <summary>
    /// Adaptive-blur image with specified blur factor.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AdaptiveBlur(double radius)
        => AdaptiveBlur(radius, 1.0);

    /// <summary>
    /// Adaptive-blur image with specified blur factor.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AdaptiveBlur(double radius, double sigma)
        => _nativeInstance.AdaptiveBlur(radius, sigma);

    /// <summary>
    /// Resize using mesh interpolation. It works well for small resizes of less than +/- 50%
    /// of the original image size. For larger resizing on images a full filtered and slower resize
    /// function should be used instead.
    /// <para />
    /// Resize will fit the image into the requested size. It does NOT fill, the requested box size.
    /// Use the <see cref="IMagickGeometry"/> overload for more control over the resulting size.
    /// </summary>
    /// <param name="width">The new width.</param>
    /// <param name="height">The new height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AdaptiveResize(int width, int height)
    {
        var geometry = new MagickGeometry(width, height);
        AdaptiveResize(geometry);
    }

    /// <summary>
    /// Resize using mesh interpolation. It works well for small resizes of less than +/- 50%
    /// of the original image size. For larger resizing on images a full filtered and slower resize
    /// function should be used instead.
    /// </summary>
    /// <param name="geometry">The geometry to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AdaptiveResize(IMagickGeometry geometry)
    {
        Throw.IfNull(nameof(geometry), geometry);

        _nativeInstance.AdaptiveResize(geometry.ToString());
    }

    /// <summary>
    /// Adaptively sharpens the image by sharpening more intensely near image edges and less
    /// intensely far from edges.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AdaptiveSharpen()
        => AdaptiveSharpen(0.0, 1.0);

    /// <summary>
    /// Adaptively sharpens the image by sharpening more intensely near image edges and less
    /// intensely far from edges.
    /// </summary>
    /// <param name="channels">The channel(s) that should be sharpened.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AdaptiveSharpen(Channels channels)
        => AdaptiveSharpen(0.0, 1.0, channels);

    /// <summary>
    /// Adaptively sharpens the image by sharpening more intensely near image edges and less
    /// intensely far from edges.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AdaptiveSharpen(double radius, double sigma)
        => AdaptiveSharpen(radius, sigma, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Adaptively sharpens the image by sharpening more intensely near image edges and less
    /// intensely far from edges.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <param name="channels">The channel(s) that should be sharpened.</param>
    public void AdaptiveSharpen(double radius, double sigma, Channels channels)
        => _nativeInstance.AdaptiveSharpen(radius, sigma, channels);

    /// <summary>
    /// Local adaptive threshold image.
    /// http://www.dai.ed.ac.uk/HIPR2/adpthrsh.htm.
    /// </summary>
    /// <param name="width">The width of the pixel neighborhood.</param>
    /// <param name="height">The height of the pixel neighborhood.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AdaptiveThreshold(int width, int height)
        => AdaptiveThreshold(width, height, 0, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Local adaptive threshold image.
    /// http://www.dai.ed.ac.uk/HIPR2/adpthrsh.htm.
    /// </summary>
    /// <param name="width">The width of the pixel neighborhood.</param>
    /// <param name="height">The height of the pixel neighborhood.</param>
    /// <param name="channels">The channel(s) that should be thresholded.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AdaptiveThreshold(int width, int height, Channels channels)
        => AdaptiveThreshold(width, height, 0, channels);

    /// <summary>
    /// Local adaptive threshold image.
    /// http://www.dai.ed.ac.uk/HIPR2/adpthrsh.htm.
    /// </summary>
    /// <param name="width">The width of the pixel neighborhood.</param>
    /// <param name="height">The height of the pixel neighborhood.</param>
    /// <param name="bias">Constant to subtract from pixel neighborhood mean (+/-)(0-QuantumRange).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AdaptiveThreshold(int width, int height, double bias)
        => AdaptiveThreshold(width, height, bias, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Local adaptive threshold image.
    /// http://www.dai.ed.ac.uk/HIPR2/adpthrsh.htm.
    /// </summary>
    /// <param name="width">The width of the pixel neighborhood.</param>
    /// <param name="height">The height of the pixel neighborhood.</param>
    /// <param name="bias">Constant to subtract from pixel neighborhood mean (+/-)(0-QuantumRange).</param>
    /// <param name="channels">The channel(s) that should be thresholded.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AdaptiveThreshold(int width, int height, double bias, Channels channels)
        => _nativeInstance.AdaptiveThreshold(width, height, bias, channels);

    /// <summary>
    /// Local adaptive threshold image.
    /// http://www.dai.ed.ac.uk/HIPR2/adpthrsh.htm.
    /// </summary>
    /// <param name="width">The width of the pixel neighborhood.</param>
    /// <param name="height">The height of the pixel neighborhood.</param>
    /// <param name="biasPercentage">Constant to subtract from pixel neighborhood mean.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AdaptiveThreshold(int width, int height, Percentage biasPercentage)
        => AdaptiveThreshold(width, height, PercentageHelper.ToQuantum(biasPercentage), ImageMagick.Channels.Undefined);

    /// <summary>
    /// Local adaptive threshold image.
    /// http://www.dai.ed.ac.uk/HIPR2/adpthrsh.htm.
    /// </summary>
    /// <param name="width">The width of the pixel neighborhood.</param>
    /// <param name="height">The height of the pixel neighborhood.</param>
    /// <param name="biasPercentage">Constant to subtract from pixel neighborhood mean.</param>
    /// <param name="channels">The channel(s) that should be thresholded.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AdaptiveThreshold(int width, int height, Percentage biasPercentage, Channels channels)
        => AdaptiveThreshold(width, height, PercentageHelper.ToQuantum(biasPercentage), channels);

    /// <summary>
    /// Add noise to image with the specified noise type.
    /// </summary>
    /// <param name="noiseType">The type of noise that should be added to the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AddNoise(NoiseType noiseType)
        => AddNoise(noiseType, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Add noise to the specified channel of the image with the specified noise type.
    /// </summary>
    /// <param name="noiseType">The type of noise that should be added to the image.</param>
    /// <param name="channels">The channel(s) where the noise should be added.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AddNoise(NoiseType noiseType, Channels channels)
        => AddNoise(noiseType, 1.0, channels);

    /// <summary>
    /// Add noise to image with the specified noise type.
    /// </summary>
    /// <param name="noiseType">The type of noise that should be added to the image.</param>
    /// <param name="attenuate">Attenuate the random distribution.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AddNoise(NoiseType noiseType, double attenuate)
        => AddNoise(noiseType, attenuate, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Add noise to the specified channel of the image with the specified noise type.
    /// </summary>
    /// <param name="noiseType">The type of noise that should be added to the image.</param>
    /// <param name="attenuate">Attenuate the random distribution.</param>
    /// <param name="channels">The channel(s) where the noise should be added.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AddNoise(NoiseType noiseType, double attenuate, Channels channels)
        => _nativeInstance.AddNoise(noiseType, attenuate, channels);

    /// <summary>
    /// Affine Transform image.
    /// </summary>
    /// <param name="affineMatrix">The affine matrix to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AffineTransform(IDrawableAffine affineMatrix)
    {
        Throw.IfNull(nameof(affineMatrix), affineMatrix);

        _nativeInstance.AffineTransform(affineMatrix.ScaleX, affineMatrix.ScaleY, affineMatrix.ShearX, affineMatrix.ShearY, affineMatrix.TranslateX, affineMatrix.TranslateY);
    }

    /// <summary>
    /// Applies the specified alpha option.
    /// </summary>
    /// <param name="value">The option to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Alpha(AlphaOption value)
        => _nativeInstance.SetAlpha(value);

    /// <summary>
    /// Annotate using specified text, and bounding area.
    /// </summary>
    /// <param name="text">The text to use.</param>
    /// <param name="boundingArea">The bounding area.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Annotate(string text, IMagickGeometry boundingArea)
        => Annotate(text, boundingArea, Gravity.Undefined, 0.0);

    /// <summary>
    /// Annotate using specified text, bounding area, and placement gravity.
    /// </summary>
    /// <param name="text">The text to use.</param>
    /// <param name="boundingArea">The bounding area.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Annotate(string text, IMagickGeometry boundingArea, Gravity gravity)
        => Annotate(text, boundingArea, gravity, 0.0);

    /// <summary>
    /// Annotate using specified text, bounding area, and placement gravity.
    /// </summary>
    /// <param name="text">The text to use.</param>
    /// <param name="boundingArea">The bounding area.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="angle">The rotation.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Annotate(string text, IMagickGeometry boundingArea, Gravity gravity, double angle)
    {
        Throw.IfNullOrEmpty(nameof(text), text);
        Throw.IfNull(nameof(boundingArea), boundingArea);

        _nativeInstance.Annotate(_settings.Drawing, text, boundingArea.ToString(), gravity, angle);
    }

    /// <summary>
    /// Annotate with text (bounding area is entire image) and placement gravity.
    /// </summary>
    /// <param name="text">The text to use.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Annotate(string text, Gravity gravity)
    {
        Throw.IfNullOrEmpty(nameof(text), text);

        _nativeInstance.AnnotateGravity(_settings.Drawing, text, gravity);
    }

    /// <summary>
    /// Extracts the 'mean' from the image and adjust the image to try make set its gamma appropriatally.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AutoGamma()
        => AutoGamma(ImageMagick.Channels.Composite);

    /// <summary>
    /// Extracts the 'mean' from the image and adjust the image to try make set its gamma appropriatally.
    /// </summary>
    /// <param name="channels">The channel(s) to set the gamma for.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AutoGamma(Channels channels)
        => _nativeInstance.AutoGamma(channels);

    /// <summary>
    /// Adjusts the levels of a particular image channel by scaling the minimum and maximum values
    /// to the full quantum range.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AutoLevel()
        => AutoLevel(ImageMagick.Channels.Undefined);

    /// <summary>
    /// Adjusts the levels of a particular image channel by scaling the minimum and maximum values
    /// to the full quantum range.
    /// </summary>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AutoLevel(Channels channels)
        => _nativeInstance.AutoLevel(channels);

    /// <summary>
    /// Adjusts an image so that its orientation is suitable for viewing.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AutoOrient()
        => _nativeInstance.AutoOrient();

    /// <summary>
    /// Automatically selects a threshold and replaces each pixel in the image with a black pixel if
    /// the image intentsity is less than the selected threshold otherwise white.
    /// </summary>
    /// <param name="method">The threshold method.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AutoThreshold(AutoThresholdMethod method)
        => _nativeInstance.AutoThreshold(method);

    /// <summary>
    /// Applies a non-linear, edge-preserving, and noise-reducing smoothing filter.
    /// </summary>
    /// <param name="width">The width of the neighborhood in pixels.</param>
    /// <param name="height">The height of the neighborhood in pixels.</param>
    public void BilateralBlur(int width, int height)
    {
        var intensitySigma = Math.Sqrt((width * width) + (height * height));
        BilateralBlur(width, height, intensitySigma, intensitySigma * 0.25);
    }

    /// <summary>
    /// Applies a non-linear, edge-preserving, and noise-reducing smoothing filter.
    /// </summary>
    /// <param name="width">The width of the neighborhood in pixels.</param>
    /// <param name="height">The height of the neighborhood in pixels.</param>
    /// <param name="intensitySigma">The sigma in the intensity space.</param>
    /// <param name="spatialSigma">The sigma in the coordinate space.</param>
    public void BilateralBlur(int width, int height, double intensitySigma, double spatialSigma)
       => _nativeInstance.BilateralBlur(width, height, intensitySigma, spatialSigma);

    /// <summary>
    /// Forces all pixels below the threshold into black while leaving all pixels at or above
    /// the threshold unchanged.
    /// </summary>
    /// <param name="threshold">The threshold to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void BlackThreshold(Percentage threshold)
        => BlackThreshold(threshold, ImageMagick.Channels.Composite);

    /// <summary>
    /// Forces all pixels below the threshold into black while leaving all pixels at or above
    /// the threshold unchanged.
    /// </summary>
    /// <param name="threshold">The threshold to use.</param>
    /// <param name="channels">The channel(s) to make black.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void BlackThreshold(Percentage threshold, Channels channels)
    {
        Throw.IfNegative(nameof(threshold), threshold);

        _nativeInstance.BlackThreshold(threshold.ToString(), channels);
    }

    /// <summary>
    /// Simulate a scene at nighttime in the moonlight.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void BlueShift()
        => BlueShift(1.5);

    /// <summary>
    /// Simulate a scene at nighttime in the moonlight.
    /// </summary>
    /// <param name="factor">The factor to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void BlueShift(double factor)
        => _nativeInstance.BlueShift(factor);

    /// <summary>
    /// Blur image with the default blur factor (0x1).
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Blur()
        => Blur(0.0, 1.0);

    /// <summary>
    /// Blur image the specified channel of the image with the default blur factor (0x1).
    /// </summary>
    /// <param name="channels">The channel(s) that should be blurred.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Blur(Channels channels)
        => Blur(0.0, 1.0, channels);

    /// <summary>
    /// Blur image with specified blur factor.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Blur(double radius, double sigma)
        => Blur(radius, sigma, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Blur image with specified blur factor and channel.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <param name="channels">The channel(s) that should be blurred.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Blur(double radius, double sigma, Channels channels)
        => _nativeInstance.Blur(radius, sigma, channels);

    /// <summary>
    /// Border image (add border to image).
    /// </summary>
    /// <param name="size">The size of the border.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Border(int size)
        => Border(size, size);

    /// <summary>
    /// Border image (add border to image).
    /// </summary>
    /// <param name="width">The width of the border.</param>
    /// <param name="height">The height of the border.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Border(int width, int height)
    {
        var rectangle = new MagickRectangle(0, 0, width, height);
        _nativeInstance.Border(rectangle);
    }

    /// <summary>
    /// Border image (add border to image).
    /// </summary>
    /// <param name="percentage">The size of the border.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Border(Percentage percentage)
        => Border(Width * percentage, Height * percentage);

    /// <summary>
    /// Changes the brightness and/or contrast of an image. It converts the brightness and
    /// contrast parameters into slope and intercept and calls a polynomical function to apply
    /// to the image.
    /// </summary>
    /// <param name="brightness">The brightness.</param>
    /// <param name="contrast">The contrast.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void BrightnessContrast(Percentage brightness, Percentage contrast)
        => BrightnessContrast(brightness, contrast, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Changes the brightness and/or contrast of an image. It converts the brightness and
    /// contrast parameters into slope and intercept and calls a polynomical function to apply
    /// to the image.
    /// </summary>
    /// <param name="brightness">The brightness.</param>
    /// <param name="contrast">The contrast.</param>
    /// <param name="channels">The channel(s) that should be changed.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void BrightnessContrast(Percentage brightness, Percentage contrast, Channels channels)
        => _nativeInstance.BrightnessContrast(brightness.ToDouble(), contrast.ToDouble(), channels);

    /// <summary>
    /// Uses a multi-stage algorithm to detect a wide range of edges in images.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void CannyEdge()
        => CannyEdge(0.0, 1.0, new Percentage(10), new Percentage(30));

    /// <summary>
    /// Uses a multi-stage algorithm to detect a wide range of edges in images.
    /// </summary>
    /// <param name="radius">The radius of the gaussian smoothing filter.</param>
    /// <param name="sigma">The sigma of the gaussian smoothing filter.</param>
    /// <param name="lower">Percentage of edge pixels in the lower threshold.</param>
    /// <param name="upper">Percentage of edge pixels in the upper threshold.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void CannyEdge(double radius, double sigma, Percentage lower, Percentage upper)
        => _nativeInstance.CannyEdge(radius, sigma, lower.ToDouble() / 100, upper.ToDouble() / 100);

    /// <summary>
    /// Charcoal effect image (looks like charcoal sketch).
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Charcoal()
        => Charcoal(0.0, 1.0);

    /// <summary>
    /// Charcoal effect image (looks like charcoal sketch).
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Charcoal(double radius, double sigma)
        => _nativeInstance.Charcoal(radius, sigma);

    /// <summary>
    /// Chop image (remove vertical or horizontal subregion of image) using the specified geometry.
    /// </summary>
    /// <param name="geometry">The geometry to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Chop(IMagickGeometry geometry)
        => _nativeInstance.Chop(MagickRectangle.FromGeometry(geometry, this));

    /// <summary>
    /// Chop image (remove horizontal subregion of image).
    /// </summary>
    /// <param name="offset">The X offset from origin.</param>
    /// <param name="width">The width of the part to chop horizontally.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void ChopHorizontal(int offset, int width)
    {
        var geometry = new MagickGeometry(offset, 0, width, 0);
        Chop(geometry);
    }

    /// <summary>
    /// Chop image (remove horizontal subregion of image).
    /// </summary>
    /// <param name="offset">The Y offset from origin.</param>
    /// <param name="height">The height of the part to chop vertically.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void ChopVertical(int offset, int height)
    {
        var geometry = new MagickGeometry(0, offset, 0, height);
        Chop(geometry);
    }

    /// <summary>
    /// A variant of adaptive histogram equalization in which the contrast amplification is limited,
    /// so as to reduce this problem of noise amplification.
    /// </summary>
    /// <param name="xTiles">The percentage of tile divisions to use in horizontal direction.</param>
    /// <param name="yTiles">The percentage of tile divisions to use in vertical direction.</param>
    /// <param name="numberBins">The number of bins for histogram ("dynamic range").</param>
    /// <param name="clipLimit">The contrast limit for localised changes in contrast. A limit less than 1
    /// results in standard non-contrast limited AHE.</param>
    public void Clahe(Percentage xTiles, Percentage yTiles, int numberBins, double clipLimit)
        => Clahe(Width * xTiles, Height * yTiles, numberBins, clipLimit);

    /// <summary>
    /// A variant of adaptive histogram equalization in which the contrast amplification is limited,
    /// so as to reduce this problem of noise amplification.
    /// </summary>
    /// <param name="xTiles">The number of tile divisions to use in horizontal direction.</param>
    /// <param name="yTiles">The number of tile divisions to use in vertical direction.</param>
    /// <param name="numberBins">The number of bins for histogram ("dynamic range").</param>
    /// <param name="clipLimit">The contrast limit for localised changes in contrast. A limit less than 1
    /// results in standard non-contrast limited AHE.</param>
    public void Clahe(int xTiles, int yTiles, int numberBins, double clipLimit)
        => _nativeInstance.Clahe(xTiles, yTiles, numberBins, clipLimit);

    /// <summary>
    /// Set each pixel whose value is below zero to zero and any the pixel whose value is above
    /// the quantum range to the quantum range (Quantum.Max) otherwise the pixel value
    /// remains unchanged.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Clamp()
        => Clamp(ImageMagick.Channels.Undefined);

    /// <summary>
    /// Set each pixel whose value is below zero to zero and any the pixel whose value is above
    /// the quantum range to the quantum range (Quantum.Max) otherwise the pixel value
    /// remains unchanged.
    /// </summary>
    /// <param name="channels">The channel(s) to clamp.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Clamp(Channels channels)
        => _nativeInstance.Clamp(channels);

    /// <summary>
    /// Sets the image clip mask based on any clipping path information if it exists. The clipping
    /// path can be removed with <see cref="RemoveWriteMask"/>. This operating takes effect inside
    /// the clipping path.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Clip()
        => Clip("#1");

    /// <summary>
    /// Sets the image clip mask based on any clipping path information if it exists. The clipping
    /// path can be removed with <see cref="RemoveWriteMask"/>. This operating takes effect inside
    /// the clipping path.
    /// </summary>
    /// <param name="pathName">Name of clipping path resource. If name is preceded by #, use
    /// clipping path numbered by name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Clip(string pathName)
    {
        Throw.IfNullOrEmpty(nameof(pathName), pathName);

        _nativeInstance.ClipPath(pathName, true);
    }

    /// <summary>
    /// Sets the image clip mask based on any clipping path information if it exists. The clipping
    /// path can be removed with <see cref="RemoveWriteMask"/>. This operating takes effect outside
    /// the clipping path.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void ClipOutside()
        => ClipOutside("#1");

    /// <summary>
    /// Sets the image clip mask based on any clipping path information if it exists. The clipping
    /// path can be removed with <see cref="RemoveWriteMask"/>. This operating takes effect outside
    /// the clipping path.
    /// </summary>
    /// <param name="pathName">Name of clipping path resource. If name is preceded by #, use
    /// clipping path numbered by name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void ClipOutside(string pathName)
    {
        Throw.IfNullOrEmpty(nameof(pathName), pathName);

        _nativeInstance.ClipPath(pathName, false);
    }

    /// <summary>
    /// Creates a clone of the current image.
    /// </summary>
    /// <returns>A clone of the current image.</returns>
    public IMagickImage<QuantumType> Clone()
        => new MagickImage(this);

    /// <summary>
    /// Creates a clone of the current image with the specified geometry.
    /// </summary>
    /// <param name="geometry">The area to clone.</param>
    /// <returns>A clone of the current image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> Clone(IMagickGeometry geometry)
    {
        Throw.IfNull(nameof(geometry), geometry);

        var clone = new MagickImage();
        clone.SetInstance(new NativeMagickImage(_nativeInstance.CloneArea(geometry.Width, geometry.Height)));
        clone.SetSettings(_settings);
        clone.CopyPixels(this, geometry, 0, 0);

        return clone;
    }

    /// <summary>
    /// Creates a clone of the current image.
    /// </summary>
    /// <param name="width">The width of the area to clone.</param>
    /// <param name="height">The height of the area to clone.</param>
    /// <returns>A clone of the current image.</returns>
    public IMagickImage<QuantumType> Clone(int width, int height)
        => Clone(new MagickGeometry(width, height));

    /// <summary>
    /// Creates a clone of the current image.
    /// </summary>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="width">The width of the area to clone.</param>
    /// <param name="height">The height of the area to clone.</param>
    /// <returns>A clone of the current image.</returns>
    public IMagickImage<QuantumType> Clone(int x, int y, int width, int height)
        => Clone(new MagickGeometry(x, y, width, height));

    /// <summary>
    /// Apply a color lookup table (CLUT) to the image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Clut(IMagickImage image)
        => Clut(image, PixelInterpolateMethod.Undefined);

    /// <summary>
    /// Apply a color lookup table (CLUT) to the image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="method">Pixel interpolate method.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Clut(IMagickImage image, PixelInterpolateMethod method)
        => Clut(image, method, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Apply a color lookup table (CLUT) to the image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="method">Pixel interpolate method.</param>
    /// <param name="channels">The channel(s) to clut.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Clut(IMagickImage image, PixelInterpolateMethod method, Channels channels)
    {
        Throw.IfNull(nameof(image), image);

        _nativeInstance.Clut(image, method, channels);
    }

    /// <summary>
    /// Sets the alpha channel to the specified color.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void ColorAlpha(IMagickColor<QuantumType> color)
    {
        Throw.IfNull(nameof(color), color);

        if (!HasAlpha)
            return;

        using var canvas = new MagickImage(color, Width, Height);
        canvas.Composite(this, 0, 0, CompositeOperator.SrcOver);
        SetInstance(new NativeMagickImage(canvas._nativeInstance.Clone()));
    }

    /// <summary>
    /// Applies the color decision list from the specified ASC CDL file.
    /// </summary>
    /// <param name="fileName">The file to read the ASC CDL information from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void ColorDecisionList(string fileName)
    {
        Throw.IfNullOrEmpty(nameof(fileName), fileName);

        var filePath = FileHelper.CheckForBaseDirectory(fileName);
        _nativeInstance.ColorDecisionList(filePath);
    }

    /// <summary>
    /// Colorize image with the specified color, using specified percent alpha.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <param name="alpha">The alpha percentage.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Colorize(IMagickColor<QuantumType> color, Percentage alpha)
    {
        Throw.IfNegative(nameof(alpha), alpha);

        Colorize(color, alpha, alpha, alpha);
    }

    /// <summary>
    /// Colorize image with the specified color, using specified percent alpha for red, green,
    /// and blue quantums.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <param name="alphaRed">The alpha percentage for red.</param>
    /// <param name="alphaGreen">The alpha percentage for green.</param>
    /// <param name="alphaBlue">The alpha percentage for blue.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Colorize(IMagickColor<QuantumType> color, Percentage alphaRed, Percentage alphaGreen, Percentage alphaBlue)
    {
        Throw.IfNull(nameof(color), color);
        Throw.IfNegative(nameof(alphaRed), alphaRed);
        Throw.IfNegative(nameof(alphaGreen), alphaGreen);
        Throw.IfNegative(nameof(alphaBlue), alphaBlue);

        var blend = string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2}", alphaRed.ToInt32(), alphaGreen.ToInt32(), alphaBlue.ToInt32());

        _nativeInstance.Colorize(color, blend);
    }

    /// <summary>
    /// Apply a color matrix to the image channels.
    /// </summary>
    /// <param name="matrix">The color matrix to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void ColorMatrix(IMagickColorMatrix matrix)
    {
        Throw.IfNull(nameof(matrix), matrix);

        _nativeInstance.ColorMatrix(matrix);
    }

    /// <summary>
    /// Forces all pixels in the color range to white otherwise black.
    /// </summary>
    /// <param name="startColor">The start color of the color range.</param>
    /// <param name="stopColor">The stop color of the color range.</param>
    public void ColorThreshold(IMagickColor<QuantumType> startColor, IMagickColor<QuantumType> stopColor)
    {
        Throw.IfNull(nameof(startColor), startColor);
        Throw.IfNull(nameof(stopColor), stopColor);

        _nativeInstance.ColorThreshold(startColor, stopColor);
    }

    /// <summary>
    /// Compare current image with another image and returns error information.
    /// </summary>
    /// <param name="image">The other image to compare with this image.</param>
    /// <returns>The error information.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickErrorInfo Compare(IMagickImage image)
    {
        Throw.IfNull(nameof(image), image);

        if (_nativeInstance.SetColorMetric(image))
            return new MagickErrorInfo();

        return CreateErrorInfo(this);
    }

    /// <summary>
    /// Returns the distortion based on the specified metric.
    /// </summary>
    /// <param name="image">The other image to compare with this image.</param>
    /// <param name="metric">The metric to use.</param>
    /// <returns>The distortion based on the specified metric.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public double Compare(IMagickImage image, ErrorMetric metric)
        => Compare(image, metric, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Returns the distortion based on the specified metric.
    /// </summary>
    /// <param name="image">The other image to compare with this image.</param>
    /// <param name="metric">The metric to use.</param>
    /// <param name="channels">The channel(s) to compare.</param>
    /// <returns>The distortion based on the specified metric.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public double Compare(IMagickImage image, ErrorMetric metric, Channels channels)
    {
        Throw.IfNull(nameof(image), image);

        return _nativeInstance.CompareDistortion(image, metric, channels);
    }

    /// <summary>
    /// Returns the distortion based on the specified metric.
    /// </summary>
    /// <param name="image">The other image to compare with this image.</param>
    /// <param name="settings">The settings to use.</param>
    /// <param name="difference">The image that will contain the difference.</param>
    /// <returns>The distortion based on the specified metric.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public double Compare(IMagickImage image, ICompareSettings<QuantumType> settings, IMagickImage difference)
        => Compare(image, settings, difference, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Returns the distortion based on the specified metric.
    /// </summary>
    /// <param name="image">The other image to compare with this image.</param>
    /// <param name="metric">The metric to use.</param>
    /// <param name="difference">The image that will contain the difference.</param>
    /// <returns>The distortion based on the specified metric.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public double Compare(IMagickImage image, ErrorMetric metric, IMagickImage difference)
        => Compare(image, metric, difference, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Returns the distortion based on the specified metric.
    /// </summary>
    /// <param name="image">The other image to compare with this image.</param>
    /// <param name="metric">The metric to use.</param>
    /// <param name="difference">The image that will contain the difference.</param>
    /// <param name="channels">The channel(s) to compare.</param>
    /// <returns>The distortion based on the specified metric.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public double Compare(IMagickImage image, ErrorMetric metric, IMagickImage difference, Channels channels)
        => Compare(image, new CompareSettings { Metric = metric }, difference, channels);

    /// <summary>
    /// Returns the distortion based on the specified metric.
    /// </summary>
    /// <param name="image">The other image to compare with this image.</param>
    /// <param name="settings">The settings to use.</param>
    /// <param name="difference">The image that will contain the difference.</param>
    /// <param name="channels">The channel(s) to compare.</param>
    /// <returns>The distortion based on the specified metric.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public double Compare(IMagickImage image, ICompareSettings<QuantumType> settings, IMagickImage difference, Channels channels)
    {
        Throw.IfNull(nameof(image), image);
        Throw.IfNull(nameof(settings), settings);
        Throw.IfNull(nameof(difference), difference);

        if (difference is not MagickImage differenceImage)
            throw new NotSupportedException();

        using var temporaryDefines = new TemporaryDefines(this);
        temporaryDefines.SetArtifact("compare:highlight-color", settings.HighlightColor);
        temporaryDefines.SetArtifact("compare:lowlight-color", settings.LowlightColor);
        temporaryDefines.SetArtifact("compare:masklight-color", settings.MasklightColor);

        var result = _nativeInstance.Compare(image, settings.Metric, channels, out var distortion);
        if (result != IntPtr.Zero)
            differenceImage._nativeInstance.Instance = result;

        return distortion;
    }

    /// <summary>
    /// Compares the current instance with another image. Only the size of the image is compared.
    /// </summary>
    /// <param name="other">The object to compare this image with.</param>
    /// <returns>A signed number indicating the relative values of this instance and value.</returns>
    public int CompareTo(IMagickImage<QuantumType>? other)
    {
        if (other is null)
            return 1;

        var left = Width * Height;
        var right = other.Width * other.Height;

        if (left == right)
            return 0;

        return left < right ? -1 : 1;
    }

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Composite(IMagickImage image)
        => Composite(image, CompositeOperator.In);

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Composite(IMagickImage image, Channels channels)
        => Composite(image, CompositeOperator.In, channels);

    /// <summary>
    /// Compose an image onto another using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Composite(IMagickImage image, CompositeOperator compose)
        => Composite(image, 0, 0, compose);

    /// <summary>
    /// Compose an image onto another using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Composite(IMagickImage image, CompositeOperator compose, Channels channels)
        => Composite(image, 0, 0, compose, channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Composite(IMagickImage image, CompositeOperator compose, string? args)
        => Composite(image, 0, 0, compose, args);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Composite(IMagickImage image, CompositeOperator compose, string? args, Channels channels)
        => Composite(image, 0, 0, compose, args, channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Composite(IMagickImage image, int x, int y)
        => Composite(image, x, y, CompositeOperator.In);

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Composite(IMagickImage image, int x, int y, Channels channels)
        => Composite(image, x, y, CompositeOperator.In, channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Composite(IMagickImage image, int x, int y, CompositeOperator compose)
        => Composite(image, x, y, compose, null);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Composite(IMagickImage image, int x, int y, CompositeOperator compose, Channels channels)
        => Composite(image, x, y, compose, null, channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Composite(IMagickImage image, int x, int y, CompositeOperator compose, string? args)
        => Composite(image, x, y, compose, args, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Composite(IMagickImage image, int x, int y, CompositeOperator compose, string? args, Channels channels)
    {
        Throw.IfNull(nameof(image), image);

        using var temporaryDefines = new TemporaryDefines(this);
        temporaryDefines.SetArtifact("compose:args", args);

        _nativeInstance.Composite(image, x, y, compose, channels);
    }

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Composite(IMagickImage image, Gravity gravity)
        => Composite(image, gravity, CompositeOperator.In);

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Composite(IMagickImage image, Gravity gravity, Channels channels)
        => Composite(image, gravity, CompositeOperator.In, channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Composite(IMagickImage image, Gravity gravity, CompositeOperator compose)
        => Composite(image, gravity, compose, null);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Composite(IMagickImage image, Gravity gravity, CompositeOperator compose, Channels channels)
        => Composite(image, gravity, compose, null, channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Composite(IMagickImage image, Gravity gravity, CompositeOperator compose, string? args)
        => Composite(image, gravity, compose, args, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Composite(IMagickImage image, Gravity gravity, CompositeOperator compose, string? args, Channels channels)
        => Composite(image, gravity, 0, 0, compose, args, channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Composite(IMagickImage image, Gravity gravity, int x, int y)
        => Composite(image, gravity, x, y, CompositeOperator.In);

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Composite(IMagickImage image, Gravity gravity, int x, int y, Channels channels)
        => Composite(image, gravity, x, y, CompositeOperator.In, channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Composite(IMagickImage image, Gravity gravity, int x, int y, CompositeOperator compose)
        => Composite(image, gravity, x, y, compose, null);

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Composite(IMagickImage image, Gravity gravity, int x, int y, CompositeOperator compose, Channels channels)
        => Composite(image, gravity, x, y, compose, null, channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Composite(IMagickImage image, Gravity gravity, int x, int y, CompositeOperator compose, string? args)
        => Composite(image, gravity, x, y, compose, args, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Composite(IMagickImage image, Gravity gravity, int x, int y, CompositeOperator compose, string? args, Channels channels)
    {
        Throw.IfNull(nameof(image), image);

        if (args is not null)
            _nativeInstance.SetArtifact("compose:args", args);

        try
        {
            _nativeInstance.CompositeGravity(image, gravity, x, y, compose, channels);
        }
        finally
        {
            if (args is not null)
                _nativeInstance.RemoveArtifact("compose:args");
        }
    }

    /// <summary>
    /// Determines the connected-components of the image.
    /// </summary>
    /// <param name="connectivity">How many neighbors to visit, choose from 4 or 8.</param>
    /// <returns>The connected-components of the image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IReadOnlyCollection<IConnectedComponent<QuantumType>> ConnectedComponents(int connectivity)
    {
        var settings = new ConnectedComponentsSettings
        {
            Connectivity = connectivity,
        };
        return ConnectedComponents(settings);
    }

    /// <summary>
    /// Determines the connected-components of the image.
    /// </summary>
    /// <param name="settings">The settings for this operation.</param>
    /// <returns>The connected-components of the image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IReadOnlyCollection<IConnectedComponent<QuantumType>> ConnectedComponents(IConnectedComponentsSettings settings)
    {
        Throw.IfNull(nameof(settings), settings);

        var objects = IntPtr.Zero;

        try
        {
            using var temporaryDefines = new TemporaryDefines(this);
            temporaryDefines.SetArtifact("connected-components:angle-threshold", settings.AngleThreshold);
            temporaryDefines.SetArtifact("connected-components:area-threshold", settings.AreaThreshold);
            temporaryDefines.SetArtifact("connected-components:circularity-threshold", settings.CircularityThreshold);
            temporaryDefines.SetArtifact("connected-components:diameter-threshold", settings.DiameterThreshold);
            temporaryDefines.SetArtifact("connected-components:eccentricity-threshold", settings.EccentricityThreshold);
            temporaryDefines.SetArtifact("connected-components:major-axis-threshold", settings.MajorAxisThreshold);
            temporaryDefines.SetArtifact("connected-components:mean-color", settings.MeanColor);
            temporaryDefines.SetArtifact("connected-components:minor-axis-threshold", settings.MinorAxisThreshold);
            temporaryDefines.SetArtifact("connected-components:perimeter-threshold", settings.PerimeterThreshold);

            _nativeInstance.ConnectedComponents(settings.Connectivity, out objects);

            return ConnectedComponent.Create(objects, ColormapSize);
        }
        finally
        {
            ConnectedComponent.DisposeList(objects);
        }
    }

    /// <summary>
    /// Contrast image (enhance intensity differences in image).
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Contrast()
        => _nativeInstance.Contrast(true);

    /// <summary>
    /// A simple image enhancement technique that attempts to improve the contrast in an image by
    /// 'stretching' the range of intensity values it contains to span a desired range of values.
    /// It differs from the more sophisticated histogram equalization in that it can only apply a
    /// linear scaling function to the image pixel values. As a result the 'enhancement' is less harsh.
    /// </summary>
    /// <param name="blackPoint">The black point.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void ContrastStretch(Percentage blackPoint)
        => ContrastStretch(blackPoint, blackPoint);

    /// <summary>
    /// A simple image enhancement technique that attempts to improve the contrast in an image by
    /// 'stretching' the range of intensity values it contains to span a desired range of values.
    /// It differs from the more sophisticated histogram equalization in that it can only apply a
    /// linear scaling function to the image pixel values. As a result the 'enhancement' is less harsh.
    /// </summary>
    /// <param name="blackPoint">The black point.</param>
    /// <param name="whitePoint">The white point.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void ContrastStretch(Percentage blackPoint, Percentage whitePoint)
        => ContrastStretch(blackPoint, whitePoint, ImageMagick.Channels.Undefined);

    /// <summary>
    /// A simple image enhancement technique that attempts to improve the contrast in an image by
    /// 'stretching' the range of intensity values it contains to span a desired range of values.
    /// It differs from the more sophisticated histogram equalization in that it can only apply a
    /// linear scaling function to the image pixel values. As a result the 'enhancement' is less harsh.
    /// </summary>
    /// <param name="blackPoint">The black point.</param>
    /// <param name="whitePoint">The white point.</param>
    /// <param name="channels">The channel(s) to constrast stretch.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void ContrastStretch(Percentage blackPoint, Percentage whitePoint, Channels channels)
    {
        Throw.IfNegative(nameof(blackPoint), blackPoint);
        Throw.IfNegative(nameof(whitePoint), whitePoint);

        var contrast = CalculateContrastStretch(blackPoint, whitePoint);
        _nativeInstance.ContrastStretch(contrast.X, contrast.Y, channels);
    }

    /// <summary>
    /// Returns the convex hull points of an image canvas.
    /// </summary>
    /// <returns>The convex hull points of an image canvas.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IEnumerable<PointD> ConvexHull()
    {
        var result = _nativeInstance.ConvexHull(out var length);
        using var coordinates = new PointInfoCollection(result, (int)length);
        for (var i = 0; i < coordinates.Count; i++)
        {
            var x = coordinates.GetX(i);
            var y = coordinates.GetY(i);

            yield return new PointD(x, y);
        }
    }

    /// <summary>
    /// Convolve image. Applies a user-specified convolution to the image.
    /// </summary>
    /// <param name="matrix">The convolution matrix.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Convolve(IConvolveMatrix matrix)
    {
        Throw.IfNull(nameof(matrix), matrix);

        _nativeInstance.Convolve(matrix);
    }

    /// <summary>
    /// Copies pixels from the source image to the destination image.
    /// </summary>
    /// <param name="source">The source image to copy the pixels from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void CopyPixels(IMagickImage source)
        => CopyPixels(source, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Copies pixels from the source image to the destination image.
    /// </summary>
    /// <param name="source">The source image to copy the pixels from.</param>
    /// <param name="channels">The channels to copy.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void CopyPixels(IMagickImage source, Channels channels)
    {
        Throw.IfNull(nameof(source), source);

        var geometry = new MagickGeometry(0, 0, Math.Min(source.Width, Width), Math.Min(source.Height, Height));

        CopyPixels(source, geometry, 0, 0, channels);
    }

    /// <summary>
    /// Copies pixels from the source image to the destination image.
    /// </summary>
    /// <param name="source">The source image to copy the pixels from.</param>
    /// <param name="geometry">The geometry to copy.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void CopyPixels(IMagickImage source, IMagickGeometry geometry)
        => CopyPixels(source, geometry, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Copies pixels from the source image to the destination image.
    /// </summary>
    /// <param name="source">The source image to copy the pixels from.</param>
    /// <param name="geometry">The geometry to copy.</param>
    /// <param name="channels">The channels to copy.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void CopyPixels(IMagickImage source, IMagickGeometry geometry, Channels channels)
        => CopyPixels(source, geometry, 0, 0, channels);

    /// <summary>
    /// Copies pixels from the source image as defined by the geometry the destination image at
    /// the specified offset.
    /// </summary>
    /// <param name="source">The source image to copy the pixels from.</param>
    /// <param name="geometry">The geometry to copy.</param>
    /// <param name="x">The X offset to start the copy from.</param>
    /// <param name="y">The Y offset to start the copy from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void CopyPixels(IMagickImage source, IMagickGeometry geometry, int x, int y)
        => CopyPixels(source, geometry, x, y, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Copies pixels from the source image as defined by the geometry the destination image at
    /// the specified offset.
    /// </summary>
    /// <param name="source">The source image to copy the pixels from.</param>
    /// <param name="geometry">The geometry to copy.</param>
    /// <param name="x">The X offset to copy the pixels to.</param>
    /// <param name="y">The Y offset to copy the pixels to.</param>
    /// <param name="channels">The channels to copy.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void CopyPixels(IMagickImage source, IMagickGeometry geometry, int x, int y, Channels channels)
    {
        Throw.IfNull(nameof(source), source);

        _nativeInstance.CopyPixels(source, MagickRectangle.FromGeometry(geometry, this), new OffsetInfo(x, y), channels);
    }

    /// <summary>
    /// Crop image (subregion of original image). RePage should be called unless the Page information
    /// is needed.
    /// </summary>
    /// <param name="width">The width of the subregion.</param>
    /// <param name="height">The height of the subregion.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Crop(int width, int height)
        => Crop(width, height, Gravity.Undefined);

    /// <summary>
    /// Crop image (subregion of original image). RePage should be called unless the Page information
    /// is needed.
    /// </summary>
    /// <param name="width">The width of the subregion.</param>
    /// <param name="height">The height of the subregion.</param>
    /// <param name="gravity">The position where the cropping should start from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Crop(int width, int height, Gravity gravity)
        => Crop(new MagickGeometry(0, 0, width, height), gravity);

    /// <summary>
    /// Crop image (subregion of original image). RePage should be called unless the Page information
    /// is needed.
    /// </summary>
    /// <param name="geometry">The subregion to crop.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Crop(IMagickGeometry geometry)
        => Crop(geometry, Gravity.Undefined);

    /// <summary>
    /// Crop image (subregion of original image). RePage should be called unless the Page information
    /// is needed.
    /// </summary>
    /// <param name="geometry">The subregion to crop.</param>
    /// <param name="gravity">The position where the cropping should start from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Crop(IMagickGeometry geometry, Gravity gravity)
    {
        Throw.IfNull(nameof(geometry), geometry);

        _nativeInstance.Crop(geometry.ToString(), gravity);
    }

    /// <summary>
    /// Creates tiles of the current image in the specified dimension.
    /// </summary>
    /// <param name="width">The width of the tile.</param>
    /// <param name="height">The height of the tile.</param>
    /// <returns>New title of the current image.</returns>
    public IReadOnlyCollection<IMagickImage<QuantumType>> CropToTiles(int width, int height)
        => CropToTiles(new MagickGeometry(width, height));

    /// <summary>
    /// Creates tiles of the current image in the specified dimension.
    /// </summary>
    /// <param name="geometry">The size of the tile.</param>
    /// <returns>New title of the current image.</returns>
    public IReadOnlyCollection<IMagickImage<QuantumType>> CropToTiles(IMagickGeometry geometry)
    {
        Throw.IfNull(nameof(geometry), geometry);

        var images = _nativeInstance.CropToTiles(geometry.ToString());
        return CreateList(images);
    }

    /// <summary>
    /// Displaces an image's colormap by a given number of positions.
    /// </summary>
    /// <param name="amount">Displace the colormap this amount.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void CycleColormap(int amount)
        => _nativeInstance.CycleColormap(amount);

    /// <summary>
    /// Converts cipher pixels to plain pixels.
    /// </summary>
    /// <param name="passphrase">The password that was used to encrypt the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Decipher(string passphrase)
    {
        Throw.IfNullOrEmpty(nameof(passphrase), passphrase);

        _nativeInstance.Decipher(passphrase);
    }

    /// <summary>
    /// Removes skew from the image. Skew is an artifact that occurs in scanned images because of
    /// the camera being misaligned, imperfections in the scanning or surface, or simply because
    /// the paper was not placed completely flat when scanned. The value of threshold ranges
    /// from 0 to QuantumRange.
    /// </summary>
    /// <param name="threshold">The threshold.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    /// <returns>The angle that was used.</returns>
    public double Deskew(Percentage threshold)
        => Deskew(new DeskewSettings() { Threshold = threshold });

    /// <summary>
    /// Removes skew from the image. Skew is an artifact that occurs in scanned images because of
    /// the camera being misaligned, imperfections in the scanning or surface, or simply because
    /// the paper was not placed completely flat when scanned. The value of threshold ranges
    /// from 0 to QuantumRange.
    /// </summary>
    /// <param name="settings">The deskew settings.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    /// <returns>The angle that was used.</returns>
    public double Deskew(IDeskewSettings settings)
    {
        Throw.IfNull(nameof(settings), settings);
        Throw.IfNegative(nameof(settings), settings.Threshold);

        using var temporaryDefines = new TemporaryDefines(this);
        temporaryDefines.SetArtifact("deskew:auto-crop", settings.AutoCrop);

        _nativeInstance.Deskew(PercentageHelper.ToQuantum(settings.Threshold));

        var artifact = GetArtifact("deskew:angle");
        if (!double.TryParse(artifact, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            return 0.0;

        return result;
    }

    /// <summary>
    /// Despeckle image (reduce speckle noise).
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Despeckle()
        => _nativeInstance.Despeckle();

    /// <summary>
    /// Determines the bit depth (bits allocated to red/green/blue components). Use the Depth
    /// property to get the current value.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    /// <returns>The bit depth (bits allocated to red/green/blue components).</returns>
    public int DetermineBitDepth()
        => DetermineBitDepth(ImageMagick.Channels.Undefined);

    /// <summary>
    /// Determines the bit depth (bits allocated to red/green/blue components) of the specified channel.
    /// </summary>
    /// <param name="channels">The channel to get the depth for.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    /// <returns>The bit depth (bits allocated to red/green/blue components) of the specified channel.</returns>
    public int DetermineBitDepth(Channels channels)
        => _nativeInstance.DetermineBitDepth(channels);

    /// <summary>
    /// Determines the color type of the image. This method can be used to automatically make the
    /// type GrayScale.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    /// <returns>The color type of the image.</returns>
    public ColorType DetermineColorType()
        => _nativeInstance.DetermineColorType();

    /// <summary>
    /// Disposes the instance.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Distorts an image using various distortion methods, by mapping color lookups of the source
    /// image to a new destination image of the same size as the source image.
    /// </summary>
    /// <param name="method">The distortion method to use.</param>
    /// <param name="arguments">An array containing the arguments for the distortion.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Distort(DistortMethod method, params double[] arguments)
        => Distort(method, new DistortSettings(), arguments);

    /// <summary>
    /// Distorts an image using various distortion methods, by mapping color lookups of the source
    /// image to a new destination image usually of the same size as the source image, unless
    /// 'bestfit' is set to true.
    /// </summary>
    /// <param name="method">The distortion method to use.</param>
    /// <param name="settings">The settings for the distort operation.</param>
    /// <param name="arguments">An array containing the arguments for the distortion.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Distort(DistortMethod method, IDistortSettings settings, params double[] arguments)
    {
        Throw.IfNull(nameof(settings), settings);
        Throw.IfNullOrEmpty(nameof(arguments), arguments);

        using var temporaryDefines = new TemporaryDefines(this);
        temporaryDefines.SetArtifact("distort:scale", settings.Scale);
        temporaryDefines.SetArtifact("distort:viewport", settings.Viewport);

        _nativeInstance.Distort(method, settings.Bestfit, arguments, arguments.Length);
    }

    /// <summary>
    /// Draw on image using one or more drawables.
    /// </summary>
    /// <param name="drawables">The drawable(s) to draw on the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Draw(IDrawables<QuantumType> drawables)
        => Draw((IEnumerable<IDrawable>)drawables);

    /// <summary>
    /// Draw on image using one or more drawables.
    /// </summary>
    /// <param name="drawables">The drawable(s) to draw on the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Draw(params IDrawable[] drawables)
        => Draw((IEnumerable<IDrawable>)drawables);

    /// <summary>
    /// Draw on image using a collection of drawables.
    /// </summary>
    /// <param name="drawables">The drawables to draw on the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Draw(IEnumerable<IDrawable> drawables)
    {
        Throw.IfNull(nameof(drawables), drawables);

        using var wand = new DrawingWand(this);
        wand.Draw(drawables);
    }

    /// <summary>
    /// Edge image (highlight edges in image).
    /// </summary>
    /// <param name="radius">The radius of the pixel neighborhood.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Edge(double radius)
        => _nativeInstance.Edge(radius);

    /// <summary>
    /// Emboss image (highlight edges with 3D effect) with default value (0x1).
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Emboss()
        => Emboss(0.0, 1.0);

    /// <summary>
    /// Emboss image (highlight edges with 3D effect).
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Emboss(double radius, double sigma)
        => _nativeInstance.Emboss(radius, sigma);

    /// <summary>
    /// Converts pixels to cipher-pixels.
    /// </summary>
    /// <param name="passphrase">The password that to encrypt the image with.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Encipher(string passphrase)
    {
        Throw.IfNullOrEmpty(nameof(passphrase), passphrase);

        _nativeInstance.Encipher(passphrase);
    }

    /// <summary>
    /// Applies a digital filter that improves the quality of a noisy image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Enhance()
        => _nativeInstance.Enhance();

    /// <summary>
    /// Applies a histogram equalization to the image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Equalize()
        => Equalize(ImageMagick.Channels.Undefined);

    /// <summary>
    /// Applies a histogram equalization to the image.
    /// </summary>
    /// <param name="channels">The channel(s) to apply the operator on.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Equalize(Channels channels)
        => _nativeInstance.Equalize(channels);

    /// <summary>
    /// Apply an arithmetic or bitwise operator to the image pixel quantums.
    /// </summary>
    /// <param name="channels">The channel(s) to apply the operator on.</param>
    /// <param name="evaluateFunction">The function.</param>
    /// <param name="arguments">The arguments for the function.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Evaluate(Channels channels, EvaluateFunction evaluateFunction, params double[] arguments)
    {
        Throw.IfNullOrEmpty(nameof(arguments), arguments);

        _nativeInstance.EvaluateFunction(channels, evaluateFunction, arguments, arguments.Length);
    }

    /// <summary>
    /// Apply an arithmetic or bitwise operator to the image pixel quantums.
    /// </summary>
    /// <param name="channels">The channel(s) to apply the operator on.</param>
    /// <param name="evaluateOperator">The operator.</param>
    /// <param name="value">The value.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Evaluate(Channels channels, EvaluateOperator evaluateOperator, double value)
        => _nativeInstance.EvaluateOperator(channels, evaluateOperator, value);

    /// <summary>
    /// Apply an arithmetic or bitwise operator to the image pixel quantums.
    /// </summary>
    /// <param name="channels">The channel(s) to apply the operator on.</param>
    /// <param name="evaluateOperator">The operator.</param>
    /// <param name="percentage">The value.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Evaluate(Channels channels, EvaluateOperator evaluateOperator, Percentage percentage)
        => Evaluate(channels, evaluateOperator, PercentageHelper.ToQuantum(percentage));

    /// <summary>
    /// Apply an arithmetic or bitwise operator to the image pixel quantums.
    /// </summary>
    /// <param name="channels">The channel(s) to apply the operator on.</param>
    /// <param name="geometry">The geometry to use.</param>
    /// <param name="evaluateOperator">The operator.</param>
    /// <param name="value">The value.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Evaluate(Channels channels, IMagickGeometry geometry, EvaluateOperator evaluateOperator, double value)
        => _nativeInstance.EvaluateGeometry(channels, MagickRectangle.FromGeometry(geometry, this), evaluateOperator, value);

    /// <summary>
    /// Apply an arithmetic or bitwise operator to the image pixel quantums.
    /// </summary>
    /// <param name="channels">The channel(s) to apply the operator on.</param>
    /// <param name="geometry">The geometry to use.</param>
    /// <param name="evaluateOperator">The operator.</param>
    /// <param name="percentage">The value.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Evaluate(Channels channels, IMagickGeometry geometry, EvaluateOperator evaluateOperator, Percentage percentage)
        => Evaluate(channels, geometry, evaluateOperator, PercentageHelper.ToQuantum(percentage));

    /// <summary>
    /// Extend the image as defined by the width and height.
    /// </summary>
    /// <param name="width">The width to extend the image to.</param>
    /// <param name="height">The height to extend the image to.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Extent(int width, int height)
        => Extent(new MagickGeometry(width, height));

    /// <summary>
    /// Extend the image as defined by the width and height.
    /// </summary>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="width">The width to extend the image to.</param>
    /// <param name="height">The height to extend the image to.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Extent(int x, int y, int width, int height)
        => Extent(new MagickGeometry(x, y, width, height));

    /// <summary>
    /// Extend the image as defined by the width and height.
    /// </summary>
    /// <param name="width">The width to extend the image to.</param>
    /// <param name="height">The height to extend the image to.</param>
    /// <param name="backgroundColor">The background color to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Extent(int width, int height, IMagickColor<QuantumType> backgroundColor)
    {
        var geometry = new MagickGeometry(width, height);
        Extent(geometry, backgroundColor);
    }

    /// <summary>
    /// Extend the image as defined by the width and height.
    /// </summary>
    /// <param name="width">The width to extend the image to.</param>
    /// <param name="height">The height to extend the image to.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Extent(int width, int height, Gravity gravity)
    {
        var geometry = new MagickGeometry(width, height);
        Extent(geometry, gravity);
    }

    /// <summary>
    /// Extend the image as defined by the width and height.
    /// </summary>
    /// <param name="width">The width to extend the image to.</param>
    /// <param name="height">The height to extend the image to.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="backgroundColor">The background color to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Extent(int width, int height, Gravity gravity, IMagickColor<QuantumType> backgroundColor)
    {
        var geometry = new MagickGeometry(width, height);
        Extent(geometry, gravity, backgroundColor);
    }

    /// <summary>
    /// Extend the image as defined by the rectangle.
    /// </summary>
    /// <param name="geometry">The geometry to extend the image to.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Extent(IMagickGeometry geometry)
        => Extent(geometry, Gravity.Undefined);

    /// <summary>
    /// Extend the image as defined by the geometry.
    /// </summary>
    /// <param name="geometry">The geometry to extend the image to.</param>
    /// <param name="backgroundColor">The background color to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Extent(IMagickGeometry geometry, IMagickColor<QuantumType> backgroundColor)
    {
        Throw.IfNull(nameof(backgroundColor), backgroundColor);

        BackgroundColor = backgroundColor;
        Extent(geometry);
    }

    /// <summary>
    /// Extend the image as defined by the geometry.
    /// </summary>
    /// <param name="geometry">The geometry to extend the image to.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Extent(IMagickGeometry geometry, Gravity gravity)
    {
        Throw.IfNull(nameof(geometry), geometry);

        _nativeInstance.Extent(geometry.ToString(), gravity);
    }

    /// <summary>
    /// Extend the image as defined by the geometry.
    /// </summary>
    /// <param name="geometry">The geometry to extend the image to.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="backgroundColor">The background color to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Extent(IMagickGeometry geometry, Gravity gravity, IMagickColor<QuantumType> backgroundColor)
    {
        Throw.IfNull(nameof(backgroundColor), backgroundColor);

        BackgroundColor = backgroundColor;
        Extent(geometry, gravity);
    }

    /// <summary>
    /// Flip image (reflect each scanline in the vertical direction).
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Flip()
        => _nativeInstance.Flip();

    /// <summary>
    /// Floodfill pixels matching color (within fuzz factor) of target pixel(x,y) with replacement
    /// alpha value using method.
    /// </summary>
    /// <param name="alpha">The alpha to use.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void FloodFill(QuantumType alpha, int x, int y)
        => FloodFill(alpha, x, y, false);

    /// <summary>
    /// Flood-fill color across pixels that match the color of the target pixel and are neighbors
    /// of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void FloodFill(IMagickColor<QuantumType> color, int x, int y)
        => FloodFill(color, x, y, false);

    /// <summary>
    /// Flood-fill color across pixels that match the color of the target pixel and are neighbors
    /// of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="target">The target color.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void FloodFill(IMagickColor<QuantumType> color, int x, int y, IMagickColor<QuantumType> target)
        => FloodFill(color, x, y, target, false);

    /// <summary>
    /// Flood-fill texture across pixels that match the color of the target pixel and are neighbors
    /// of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void FloodFill(IMagickImage<QuantumType> image, int x, int y)
        => FloodFill(image, x, y, false);

    /// <summary>
    /// Flood-fill texture across pixels that match the color of the target pixel and are neighbors
    /// of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="target">The target color.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void FloodFill(IMagickImage<QuantumType> image, int x, int y, IMagickColor<QuantumType> target)
        => FloodFill(image, x, y, target, false);

    /// <summary>
    /// Flop image (reflect each scanline in the horizontal direction).
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Flop()
        => _nativeInstance.Flop();

    /// <summary>
    /// Obtain font metrics for text string given current font, pointsize, and density settings.
    /// </summary>
    /// <param name="text">The text to get the font metrics for.</param>
    /// <returns>The font metrics for text.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public ITypeMetric? FontTypeMetrics(string text)
        => FontTypeMetrics(text, false);

    /// <summary>
    /// Obtain font metrics for text string given current font, pointsize, and density settings.
    /// </summary>
    /// <param name="text">The text to get the font metrics for.</param>
    /// <param name="ignoreNewlines">Specifies if newlines should be ignored.</param>
    /// <returns>The font metrics for text.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public ITypeMetric? FontTypeMetrics(string text, bool ignoreNewlines)
    {
        Throw.IfNullOrEmpty(nameof(text), text);

        var settings = _settings.Drawing;

        settings.Text = text;
        var result = _nativeInstance.FontTypeMetrics(settings, ignoreNewlines);
        settings.Text = null;
        return TypeMetric.CreateInstance(result);
    }

    /// <summary>
    /// Formats the specified expression, more info here: http://www.imagemagick.org/script/escape.php.
    /// </summary>
    /// <param name="expression">The expression, more info here: http://www.imagemagick.org/script/escape.php.</param>
    /// <returns>The result of the expression.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public string? FormatExpression(string expression)
    {
        Throw.IfNullOrEmpty(nameof(expression), expression);

        return _nativeInstance.FormatExpression(Settings, expression);
    }

    /// <summary>
    /// Frame image with the default geometry (25x25+6+6).
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Frame()
        => Frame(new MagickGeometry(6, 6, 25, 25));

    /// <summary>
    /// Frame image with the specified geometry.
    /// </summary>
    /// <param name="geometry">The geometry of the frame.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Frame(IMagickGeometry geometry)
    {
        _nativeInstance.Frame(MagickRectangle.FromGeometry(geometry, this));
    }

    /// <summary>
    /// Frame image with the specified with and height.
    /// </summary>
    /// <param name="width">The width of the frame.</param>
    /// <param name="height">The height of the frame.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Frame(int width, int height)
        => Frame(new MagickGeometry(6, 6, width, height));

    /// <summary>
    /// Frame image with the specified with, height, innerBevel and outerBevel.
    /// </summary>
    /// <param name="width">The width of the frame.</param>
    /// <param name="height">The height of the frame.</param>
    /// <param name="innerBevel">The inner bevel of the frame.</param>
    /// <param name="outerBevel">The outer bevel of the frame.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Frame(int width, int height, int innerBevel, int outerBevel)
        => Frame(new MagickGeometry(innerBevel, outerBevel, width, height));

    /// <summary>
    /// Applies a mathematical expression to the image.
    /// </summary>
    /// <param name="expression">The expression to apply.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Fx(string expression)
        => Fx(expression, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Applies a mathematical expression to the image.
    /// </summary>
    /// <param name="expression">The expression to apply.</param>
    /// <param name="channels">The channel(s) to apply the expression to.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Fx(string expression, Channels channels)
    {
        Throw.IfNullOrEmpty(nameof(expression), expression);

        _nativeInstance.Fx(expression, channels);
    }

    /// <summary>
    /// Gamma correct image.
    /// </summary>
    /// <param name="gamma">The image gamma.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void GammaCorrect(double gamma)
        => GammaCorrect(gamma, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Gamma correct image.
    /// </summary>
    /// <param name="gamma">The image gamma for the channel.</param>
    /// <param name="channels">The channel(s) to gamma correct.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void GammaCorrect(double gamma, Channels channels)
        => _nativeInstance.GammaCorrect(gamma, channels);

    /// <summary>
    /// Gaussian blur image.
    /// </summary>
    /// <param name="radius">The number of neighbor pixels to be included in the convolution.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void GaussianBlur(double radius)
        => GaussianBlur(radius, 1.0);

    /// <summary>
    /// Gaussian blur image.
    /// </summary>
    /// <param name="radius">The number of neighbor pixels to be included in the convolution.</param>
    /// <param name="channels">The channel(s) to blur.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void GaussianBlur(double radius, Channels channels)
        => GaussianBlur(radius, 1.0, channels);

    /// <summary>
    /// Gaussian blur image.
    /// </summary>
    /// <param name="radius">The number of neighbor pixels to be included in the convolution.</param>
    /// <param name="sigma">The standard deviation of the gaussian bell curve.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void GaussianBlur(double radius, double sigma)
        => GaussianBlur(radius, sigma, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Gaussian blur image.
    /// </summary>
    /// <param name="radius">The number of neighbor pixels to be included in the convolution.</param>
    /// <param name="sigma">The standard deviation of the gaussian bell curve.</param>
    /// <param name="channels">The channel(s) to blur.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void GaussianBlur(double radius, double sigma, Channels channels)
        => _nativeInstance.GaussianBlur(radius, sigma, channels);

    /// <summary>
    /// Retrieve the 8bim profile from the image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    /// <returns>The 8bim profile from the image.</returns>
    public IEightBimProfile? Get8BimProfile()
    {
        var info = _nativeInstance.GetProfile("8bim");
        if (info is null || info.Datum is null)
            return null;

        return new EightBimProfile(this, info.Datum);
    }

    /// <summary>
    /// Returns the value of the artifact with the specified name.
    /// </summary>
    /// <param name="name">The name of the artifact.</param>
    /// <returns>The value of the artifact with the specified name.</returns>
    public string? GetArtifact(string name)
    {
        Throw.IfNullOrEmpty(nameof(name), name);

        return _nativeInstance.GetArtifact(name);
    }

    /// <summary>
    /// Returns the value of a named image attribute.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    /// <returns>The value of a named image attribute.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public string? GetAttribute(string name)
    {
        Throw.IfNullOrEmpty(nameof(name), name);

        return _nativeInstance.GetAttribute(name);
    }

    /// <summary>
    /// Returns the default clipping path. Null will be returned if the image has no clipping path.
    /// </summary>
    /// <returns>The default clipping path. Null will be returned if the image has no clipping path.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public string? GetClippingPath()
        => GetClippingPath("#1");

    /// <summary>
    /// Returns the clipping path with the specified name. Null will be returned if the image has no clipping path.
    /// </summary>
    /// <param name="pathName">Name of clipping path resource. If name is preceded by #, use clipping path numbered by name.</param>
    /// <returns>The clipping path with the specified name. Null will be returned if the image has no clipping path.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public string? GetClippingPath(string pathName)
        => GetAttribute("8BIM:1999,2998:" + pathName);

    /// <summary>
    /// Returns the color at colormap position index.
    /// </summary>
    /// <param name="index">The position index.</param>
    /// <returns>The color at colormap position index.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickColor<QuantumType>? GetColormapColor(int index)
        => _nativeInstance.GetColormapColor(index);

    /// <summary>
    /// Retrieve the color profile from the image.
    /// </summary>
    /// <returns>The color profile from the image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IColorProfile? GetColorProfile()
    {
        var profile = GetColorProfile("icc");

        if (profile is not null)
            return profile;

        return GetColorProfile("icm");
    }

    /// <summary>
    /// Retrieve the exif profile from the image.
    /// </summary>
    /// <returns>The exif profile from the image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IExifProfile? GetExifProfile()
    {
        var info = _nativeInstance.GetProfile("exif");
        if (info is null || info.Datum is null)
            return null;

        return new ExifProfile(info.Datum);
    }

    /// <summary>
    /// Retrieve the iptc profile from the image.
    /// </summary>
    /// <returns>The iptc profile from the image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IIptcProfile? GetIptcProfile()
    {
        var info = _nativeInstance.GetProfile("iptc");
        if (info is null || info.Datum is null)
            return null;

        return new IptcProfile(info.Datum);
    }

    /// <summary>
    /// Returns a pixel collection that can be used to read or modify the pixels of this image.
    /// </summary>
    /// <returns>A pixel collection that can be used to read or modify the pixels of this image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IPixelCollection<QuantumType> GetPixels()
    {
        if (_settings.Ping)
            throw new InvalidOperationException("Image contains no pixel data.");

        return new SafePixelCollection(this);
    }

    /// <summary>
    /// Returns a pixel collection that can be used to read or modify the pixels of this image. This instance
    /// will not do any bounds checking and directly call ImageMagick.
    /// </summary>
    /// <returns>A pixel collection that can be used to read or modify the pixels of this image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IUnsafePixelCollection<QuantumType> GetPixelsUnsafe()
    {
        if (_settings.Ping)
            throw new InvalidOperationException("Image contains no pixel data.");

        return new UnsafePixelCollection(this);
    }

    /// <summary>
    /// Retrieve a named profile from the image.
    /// </summary>
    /// <param name="name">The name of the profile (e.g. "ICM", "IPTC", or a generic profile name).</param>
    /// <returns>A named profile from the image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IImageProfile? GetProfile(string name)
    {
        Throw.IfNullOrEmpty(nameof(name), name);

        var info = _nativeInstance.GetProfile(name);
        if (info is null || info.Datum is null)
            return null;

        return new ImageProfile(name, info.Datum);
    }

    /// <summary>
    /// Gets the associated read mask of the image.
    /// </summary>
    /// <returns>The associated read mask of the image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType>? GetReadMask()
        => Create(_nativeInstance.GetReadMask());

    /// <summary>
    /// Gets the associated write mask of the image.
    /// </summary>
    /// <returns>The associated write mask of the image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType>? GetWriteMask()
        => Create(_nativeInstance.GetWriteMask());

    /// <summary>
    /// Retrieve the xmp profile from the image.
    /// </summary>
    /// <returns>The xmp profile from the image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IXmpProfile? GetXmpProfile()
    {
        var info = _nativeInstance.GetProfile("xmp");
        if (info is null || info.Datum is null)
            return null;

        return new XmpProfile(info.Datum);
    }

    /// <summary>
    /// Converts the colors in the image to gray.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Grayscale()
        => Grayscale(PixelIntensityMethod.Undefined);

    /// <summary>
    /// Converts the colors in the image to gray.
    /// </summary>
    /// <param name="method">The pixel intensity method to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Grayscale(PixelIntensityMethod method)
        => _nativeInstance.Grayscale(method);

    /// <summary>
    /// Gets a value indicating whether a profile with the specified name already exists on the image.
    /// </summary>
    /// <param name="name">The name of the profile.</param>
    /// <returns>A value indicating whether a profile with the specified name already exists on the image.</returns>
    public bool HasProfile(string name)
    {
        Throw.IfNullOrEmpty(nameof(name), name);

        return _nativeInstance.HasProfile(name);
    }

    /// <summary>
    /// Apply a color lookup table (Hald CLUT) to the image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void HaldClut(IMagickImage image)
    {
        Throw.IfNull(nameof(image), image);

        _nativeInstance.HaldClut(image);
    }

    /// <summary>
    /// Creates a color histogram.
    /// </summary>
    /// <returns>A color histogram.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IReadOnlyDictionary<IMagickColor<QuantumType>, int> Histogram()
    {
        var histogram = IntPtr.Zero;
        try
        {
            histogram = _nativeInstance.Histogram(out var length);
            return MagickColorCollection.ToDictionary(histogram, (int)length);
        }
        finally
        {
            MagickColorCollection.DisposeList(histogram);
        }
    }

    /// <summary>
    /// Identifies lines in the image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void HoughLine()
        => HoughLine(0, 0, 40);

    /// <summary>
    /// Identifies lines in the image.
    /// </summary>
    /// <param name="width">The width of the neighborhood.</param>
    /// <param name="height">The height of the neighborhood.</param>
    /// <param name="threshold">The line count threshold.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void HoughLine(int width, int height, int threshold)
        => _nativeInstance.HoughLine(width, height, threshold);

    /// <summary>
    /// Implode image (special effect).
    /// </summary>
    /// <param name="amount">The extent of the implosion.</param>
    /// <param name="method">Pixel interpolate method.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Implode(double amount, PixelInterpolateMethod method)
        => _nativeInstance.Implode(amount, method);

    /// <summary>
    /// Import pixels from the specified byte array into the current image.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="settings">The import settings to use when importing the pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void ImportPixels(byte[] data, IPixelImportSettings settings)
    {
        Throw.IfNullOrEmpty(nameof(data), data);

        ImportPixels(data, 0, settings);
    }

    /// <summary>
    /// Import pixels from the specified byte array into the current image.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="settings">The import settings to use when importing the pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void ImportPixels(byte[] data, int offset, IPixelImportSettings settings)
    {
        Throw.IfNullOrEmpty(nameof(data), data);
        Throw.IfTrue(nameof(offset), offset < 0, "The offset should be positive.");
        Throw.IfTrue(nameof(offset), offset >= data.Length, "The offset should not exceed the length of the array.");
        Throw.IfNull(nameof(settings), settings);
        Throw.IfNullOrEmpty(nameof(settings), settings.Mapping, "Pixel storage mapping should be defined.");
        Throw.IfTrue(nameof(settings), settings.StorageType == StorageType.Undefined, "Storage type should not be undefined.");

        var length = data.Length - offset;
        var expectedLength = GetExpectedByteLength(settings);
        Throw.IfTrue(nameof(data), length < expectedLength, "The data length is {0} but should be at least {1}.", data.Length, expectedLength + offset);

        _nativeInstance.ImportPixels(settings.X, settings.Y, settings.Width, settings.Height, settings.Mapping, settings.StorageType, data, offset);
    }

#if !Q8
    /// <summary>
    /// Import pixels from the specified quantum array into the current image.
    /// </summary>
    /// <param name="data">The quantum array to read the pixels from.</param>
    /// <param name="settings">The import settings to use when importing the pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void ImportPixels(QuantumType[] data, IPixelImportSettings settings)
    {
        Throw.IfNullOrEmpty(nameof(data), data);

        ImportPixels(data, 0, settings);
    }

    /// <summary>
    /// Import pixels from the specified quantum array into the current image.
    /// </summary>
    /// <param name="data">The quantum array to read the pixels from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="settings">The import settings to use when importing the pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void ImportPixels(QuantumType[] data, int offset, IPixelImportSettings settings)
    {
        Throw.IfNullOrEmpty(nameof(data), data);
        Throw.IfTrue(nameof(offset), offset < 0, "The offset should be positive.");
        Throw.IfTrue(nameof(offset), offset >= data.Length, "The offset should not exceed the length of the array.");
        Throw.IfNull(nameof(settings), settings);
        Throw.IfNullOrEmpty(nameof(settings), settings.Mapping, "Pixel storage mapping should be defined.");
        Throw.IfTrue(nameof(settings), settings.StorageType != StorageType.Quantum, $"Storage type should be {nameof(StorageType.Quantum)}.");

        var length = data.Length - offset;
        var expectedLength = GetExpectedLength(settings);
        Throw.IfTrue(nameof(data), length < expectedLength, "The data length is {0} but should be at least {1}.", data.Length, expectedLength + offset);

        _nativeInstance.ImportPixels(settings.X, settings.Y, settings.Width, settings.Height, settings.Mapping, settings.StorageType, data, offset);
    }
#endif

    /// <summary>
    /// Returns the sum of values (pixel values) in the image.
    /// </summary>
    /// <returns>The sum of values (pixel values) in the image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType>? Integral()
        => Create(_nativeInstance.Integral());

    /// <summary>
    /// Resize image to specified size using the specified interpolation method.
    /// </summary>
    /// <param name="width">The new width.</param>
    /// <param name="height">The new height.</param>
    /// <param name="method">Pixel interpolate method.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void InterpolativeResize(int width, int height, PixelInterpolateMethod method)
        => InterpolativeResize(new MagickGeometry(width, height), method);

    /// <summary>
    /// Resize image to specified size using the specified interpolation method.
    /// </summary>
    /// <param name="geometry">The geometry to use.</param>
    /// <param name="method">Pixel interpolate method.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void InterpolativeResize(IMagickGeometry geometry, PixelInterpolateMethod method)
    {
        Throw.IfNull(nameof(geometry), geometry);

        _nativeInstance.InterpolativeResize(geometry.ToString(), method);
    }

    /// <summary>
    /// Resize image to specified size using the specified interpolation method.
    /// </summary>
    /// <param name="percentage">The percentage.</param>
    /// <param name="method">Pixel interpolate method.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void InterpolativeResize(Percentage percentage, PixelInterpolateMethod method)
    {
        Throw.IfNegative(nameof(percentage), percentage);

        var geometry = new MagickGeometry(percentage, percentage);
        InterpolativeResize(geometry, method);
    }

    /// <summary>
    /// Resize image to specified size using the specified interpolation method.
    /// </summary>
    /// <param name="percentageWidth">The percentage of the width.</param>
    /// <param name="percentageHeight">The percentage of the height.</param>
    /// <param name="method">Pixel interpolate method.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void InterpolativeResize(Percentage percentageWidth, Percentage percentageHeight, PixelInterpolateMethod method)
    {
        Throw.IfNegative(nameof(percentageWidth), percentageWidth);
        Throw.IfNegative(nameof(percentageHeight), percentageHeight);

        var geometry = new MagickGeometry(percentageWidth, percentageHeight);
        InterpolativeResize(geometry, method);
    }

    /// <summary>
    /// Inverse contrast image (diminish intensity differences in image).
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void InverseContrast()
        => _nativeInstance.Contrast(false);

    /// <summary>
    /// Floodfill pixels not matching color (within fuzz factor) of target pixel(x,y) with
    /// replacement alpha value using method.
    /// </summary>
    /// <param name="alpha">The alpha to use.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void InverseFloodFill(QuantumType alpha, int x, int y)
        => FloodFill(alpha, x, y, true);

    /// <summary>
    /// Flood-fill texture across pixels that do not match the color of the target pixel and are
    /// neighbors of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void InverseFloodFill(IMagickColor<QuantumType> color, int x, int y)
        => FloodFill(color, x, y, true);

    /// <summary>
    /// Flood-fill texture across pixels that do not match the color of the target pixel and are
    /// neighbors of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="target">The target color.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void InverseFloodFill(IMagickColor<QuantumType> color, int x, int y, IMagickColor<QuantumType> target)
        => FloodFill(color, x, y, target, true);

    /// <summary>
    /// Flood-fill texture across pixels that do not match the color of the target pixel and are
    /// neighbors of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void InverseFloodFill(IMagickImage<QuantumType> image, int x, int y)
        => FloodFill(image, x, y, true);

    /// <summary>
    /// Flood-fill texture across pixels that match the color of the target pixel and are neighbors
    /// of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="target">The target color.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void InverseFloodFill(IMagickImage<QuantumType> image, int x, int y, IMagickColor<QuantumType> target)
        => FloodFill(image, x, y, target, true);

    /// <summary>
    /// Applies the reversed level operation to just the specific channels specified. It compresses
    /// the full range of color values, so that they lie between the given black and white points.
    /// Gamma is applied before the values are mapped. Uses a midpoint of 1.0.
    /// </summary>
    /// <param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void InverseLevel(QuantumType blackPoint, QuantumType whitePoint)
        => InverseLevel(blackPoint, whitePoint, 1.0);

    /// <summary>
    /// Applies the reversed level operation to just the specific channels specified. It compresses
    /// the full range of color values, so that they lie between the given black and white points.
    /// Gamma is applied before the values are mapped. Uses a midpoint of 1.0.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void InverseLevel(Percentage blackPointPercentage, Percentage whitePointPercentage)
        => InverseLevel(PercentageHelper.ToQuantumType(blackPointPercentage), PercentageHelper.ToQuantumType(whitePointPercentage));

    /// <summary>
    /// Applies the reversed level operation to just the specific channels specified. It compresses
    /// the full range of color values, so that they lie between the given black and white points.
    /// Gamma is applied before the values are mapped. Uses a midpoint of 1.0.
    /// </summary>
    /// <param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void InverseLevel(QuantumType blackPoint, QuantumType whitePoint, Channels channels)
        => InverseLevel(blackPoint, whitePoint, 1.0, channels);

    /// <summary>
    /// Applies the reversed level operation to just the specific channels specified. It compresses
    /// the full range of color values, so that they lie between the given black and white points.
    /// Gamma is applied before the values are mapped. Uses a midpoint of 1.0.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void InverseLevel(Percentage blackPointPercentage, Percentage whitePointPercentage, Channels channels)
        => InverseLevel(PercentageHelper.ToQuantumType(blackPointPercentage), PercentageHelper.ToQuantumType(whitePointPercentage), channels);

    /// <summary>
    /// Applies the reversed level operation to just the specific channels specified. It compresses
    /// the full range of color values, so that they lie between the given black and white points.
    /// Gamma is applied before the values are mapped.
    /// </summary>
    /// <param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="midpoint">The gamma correction to apply to the image. (Useful range of 0 to 10).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void InverseLevel(QuantumType blackPoint, QuantumType whitePoint, double midpoint)
        => _nativeInstance.Levelize(blackPoint, whitePoint, midpoint, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Applies the reversed level operation to just the specific channels specified. It compresses
    /// the full range of color values, so that they lie between the given black and white points.
    /// Gamma is applied before the values are mapped.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="midpoint">The gamma correction to apply to the image. (Useful range of 0 to 10).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void InverseLevel(Percentage blackPointPercentage, Percentage whitePointPercentage, double midpoint)
        => InverseLevel(PercentageHelper.ToQuantumType(blackPointPercentage), PercentageHelper.ToQuantumType(whitePointPercentage), midpoint);

    /// <summary>
    /// Applies the reversed level operation to just the specific channels specified. It compresses
    /// the full range of color values, so that they lie between the given black and white points.
    /// Gamma is applied before the values are mapped.
    /// </summary>
    /// <param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="midpoint">The gamma correction to apply to the image. (Useful range of 0 to 10).</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void InverseLevel(QuantumType blackPoint, QuantumType whitePoint, double midpoint, Channels channels)
        => _nativeInstance.Levelize(blackPoint, whitePoint, midpoint, channels);

    /// <summary>
    /// Applies the reversed level operation to just the specific channels specified. It compresses
    /// the full range of color values, so that they lie between the given black and white points.
    /// Gamma is applied before the values are mapped.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="midpoint">The gamma correction to apply to the image. (Useful range of 0 to 10).</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void InverseLevel(Percentage blackPointPercentage, Percentage whitePointPercentage, double midpoint, Channels channels)
        => InverseLevel(PercentageHelper.ToQuantumType(blackPointPercentage), PercentageHelper.ToQuantumType(whitePointPercentage), midpoint, channels);

    /// <summary>
    /// Maps the given color to "black" and "white" values, linearly spreading out the colors, and
    /// level values on a channel by channel bases, as per level(). The given colors allows you to
    /// specify different level ranges for each of the color channels separately.
    /// </summary>
    /// <param name="blackColor">The color to map black to/from.</param>
    /// <param name="whiteColor">The color to map white to/from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void InverseLevelColors(IMagickColor<QuantumType> blackColor, IMagickColor<QuantumType> whiteColor)
        => LevelColors(blackColor, whiteColor, true);

    /// <summary>
    /// Maps the given color to "black" and "white" values, linearly spreading out the colors, and
    /// level values on a channel by channel bases, as per level(). The given colors allows you to
    /// specify different level ranges for each of the color channels separately.
    /// </summary>
    /// <param name="blackColor">The color to map black to/from.</param>
    /// <param name="whiteColor">The color to map white to/from.</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void InverseLevelColors(IMagickColor<QuantumType> blackColor, IMagickColor<QuantumType> whiteColor, Channels channels)
        => LevelColors(blackColor, whiteColor, channels, true);

    /// <summary>
    /// Changes any pixel that does not match the target with the color defined by fill.
    /// </summary>
    /// <param name="target">The color to replace.</param>
    /// <param name="fill">The color to replace opaque color with.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void InverseOpaque(IMagickColor<QuantumType> target, IMagickColor<QuantumType> fill)
        => Opaque(target, fill, true);

    /// <summary>
    /// Adjust the image contrast with an inverse non-linear sigmoidal contrast algorithm.
    /// </summary>
    /// <param name="contrast">The contrast.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void InverseSigmoidalContrast(double contrast)
        => InverseSigmoidalContrast(contrast, Quantum.Max * 0.5);

    /// <summary>
    /// Adjust the image contrast with an inverse non-linear sigmoidal contrast algorithm.
    /// </summary>
    /// <param name="contrast">The contrast to use.</param>
    /// <param name="midpoint">The midpoint to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void InverseSigmoidalContrast(double contrast, double midpoint)
         => InverseSigmoidalContrast(contrast, midpoint, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Adjust the image contrast with an inverse non-linear sigmoidal contrast algorithm.
    /// </summary>
    /// <param name="contrast">The contrast to use.</param>
    /// <param name="midpoint">The midpoint to use.</param>
    /// <param name="channels">The channel(s) that should be adjusted.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void InverseSigmoidalContrast(double contrast, double midpoint, Channels channels)
         => _nativeInstance.SigmoidalContrast(false, contrast, midpoint, channels);

    /// <summary>
    /// Adjust the image contrast with an inverse non-linear sigmoidal contrast algorithm.
    /// </summary>
    /// <param name="contrast">The contrast to use.</param>
    /// <param name="midpointPercentage">The midpoint to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void InverseSigmoidalContrast(double contrast, Percentage midpointPercentage)
        => InverseSigmoidalContrast(contrast, PercentageHelper.ToQuantum(midpointPercentage));

    /// <summary>
    /// Add alpha channel to image, setting pixels that don't match the specified color to transparent.
    /// </summary>
    /// <param name="color">The color that should not be made transparent.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void InverseTransparent(IMagickColor<QuantumType> color)
    {
        Throw.IfNull(nameof(color), color);

        _nativeInstance.Transparent(color, true);
    }

    /// <summary>
    /// Add alpha channel to image, setting pixels that don't lie in between the given two colors to
    /// transparent.
    /// </summary>
    /// <param name="colorLow">The low target color.</param>
    /// <param name="colorHigh">The high target color.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void InverseTransparentChroma(IMagickColor<QuantumType> colorLow, IMagickColor<QuantumType> colorHigh)
    {
        Throw.IfNull(nameof(colorLow), colorLow);
        Throw.IfNull(nameof(colorHigh), colorHigh);

        _nativeInstance.TransparentChroma(colorLow, colorHigh, true);
    }

    /// <summary>
    /// Applies k-means color reduction to an image. This is a colorspace clustering or segmentation technique.
    /// </summary>
    /// <param name="settings">The kmeans settings.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Kmeans(IKmeansSettings settings)
    {
        Throw.IfNull(nameof(settings), settings);
        Throw.IfNegative(nameof(settings), settings.NumberColors);
        Throw.IfNegative(nameof(settings), settings.MaxIterations);

        using var temporaryDefines = new TemporaryDefines(this);
        temporaryDefines.SetArtifact("kmeans:seed-colors", settings.SeedColors);

        _nativeInstance.Kmeans(settings.NumberColors, settings.MaxIterations, settings.Tolerance);
    }

    /// <summary>
    /// An edge preserving noise reduction filter.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Kuwahara()
        => Kuwahara(0.0, 1.0);

    /// <summary>
    /// An edge preserving noise reduction filter.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Kuwahara(double radius, double sigma)
        => _nativeInstance.Kuwahara(radius, sigma);

    /// <summary>
    /// Adjust the levels of the image by scaling the colors falling between specified white and
    /// black points to the full available quantum range. Uses a midpoint of 1.0.
    /// </summary>
    /// <param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Level(QuantumType blackPoint, QuantumType whitePoint)
        => Level(blackPoint, whitePoint, 1.0);

    /// <summary>
    /// Adjust the levels of the image by scaling the colors falling between specified white and
    /// black points to the full available quantum range. Uses a midpoint of 1.0.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Level(Percentage blackPointPercentage, Percentage whitePointPercentage)
        => Level(PercentageHelper.ToQuantumType(blackPointPercentage), PercentageHelper.ToQuantumType(whitePointPercentage));

    /// <summary>
    /// Adjust the levels of the image by scaling the colors falling between specified white and
    /// black points to the full available quantum range. Uses a midpoint of 1.0.
    /// </summary>
    /// <param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Level(QuantumType blackPoint, QuantumType whitePoint, Channels channels)
        => Level(blackPoint, whitePoint, 1.0, channels);

    /// <summary>
    /// Adjust the levels of the image by scaling the colors falling between specified white and
    /// black points to the full available quantum range. Uses a midpoint of 1.0.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Level(Percentage blackPointPercentage, Percentage whitePointPercentage, Channels channels)
        => Level(PercentageHelper.ToQuantumType(blackPointPercentage), PercentageHelper.ToQuantumType(whitePointPercentage), channels);

    /// <summary>
    /// Adjust the levels of the image by scaling the colors falling between specified white and
    /// black points to the full available quantum range.
    /// </summary>
    /// <param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="gamma">The gamma correction to apply to the image. (Useful range of 0 to 10).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Level(QuantumType blackPoint, QuantumType whitePoint, double gamma)
        => Level(blackPoint, whitePoint, gamma, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Adjust the levels of the image by scaling the colors falling between specified white and
    /// black points to the full available quantum range.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="gamma">The gamma correction to apply to the image. (Useful range of 0 to 10).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Level(Percentage blackPointPercentage, Percentage whitePointPercentage, double gamma)
        => Level(PercentageHelper.ToQuantumType(blackPointPercentage), PercentageHelper.ToQuantumType(whitePointPercentage), gamma);

    /// <summary>
    /// Adjust the levels of the image by scaling the colors falling between specified white and
    /// black points to the full available quantum range.
    /// </summary>
    /// <param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="gamma">The gamma correction to apply to the image. (Useful range of 0 to 10).</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Level(QuantumType blackPoint, QuantumType whitePoint, double gamma, Channels channels)
        => _nativeInstance.Level(blackPoint, whitePoint, gamma, channels);

    /// <summary>
    /// Adjust the levels of the image by scaling the colors falling between specified white and
    /// black points to the full available quantum range.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="gamma">The gamma correction to apply to the image. (Useful range of 0 to 10).</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Level(Percentage blackPointPercentage, Percentage whitePointPercentage, double gamma, Channels channels)
        => Level(PercentageHelper.ToQuantumType(blackPointPercentage), PercentageHelper.ToQuantumType(whitePointPercentage), gamma, channels);

    /// <summary>
    /// Maps the given color to "black" and "white" values, linearly spreading out the colors, and
    /// level values on a channel by channel bases, as per level(). The given colors allows you to
    /// specify different level ranges for each of the color channels separately.
    /// </summary>
    /// <param name="blackColor">The color to map black to/from.</param>
    /// <param name="whiteColor">The color to map white to/from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void LevelColors(IMagickColor<QuantumType> blackColor, IMagickColor<QuantumType> whiteColor)
        => LevelColors(blackColor, whiteColor, false);

    /// <summary>
    /// Maps the given color to "black" and "white" values, linearly spreading out the colors, and
    /// level values on a channel by channel bases, as per level(). The given colors allows you to
    /// specify different level ranges for each of the color channels separately.
    /// </summary>
    /// <param name="blackColor">The color to map black to/from.</param>
    /// <param name="whiteColor">The color to map white to/from.</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void LevelColors(IMagickColor<QuantumType> blackColor, IMagickColor<QuantumType> whiteColor, Channels channels)
        => LevelColors(blackColor, whiteColor, channels, false);

    /// <summary>
    /// Discards any pixels below the black point and above the white point and levels the remaining pixels.
    /// </summary>
    /// <param name="blackPoint">The black point.</param>
    /// <param name="whitePoint">The white point.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void LinearStretch(Percentage blackPoint, Percentage whitePoint)
    {
        Throw.IfNegative(nameof(blackPoint), blackPoint);
        Throw.IfNegative(nameof(whitePoint), whitePoint);

        _nativeInstance.LinearStretch(PercentageHelper.ToQuantum(blackPoint), PercentageHelper.ToQuantum(whitePoint));
    }

    /// <summary>
    /// Rescales image with seam carving.
    /// </summary>
    /// <param name="width">The new width.</param>
    /// <param name="height">The new height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void LiquidRescale(int width, int height)
    {
        var geometry = new MagickGeometry(width, height);
        LiquidRescale(geometry);
    }

    /// <summary>
    /// Rescales image with seam carving.
    /// </summary>
    /// <param name="width">The new width.</param>
    /// <param name="height">The new height.</param>
    /// <param name="deltaX">Maximum seam transversal step (0 means straight seams).</param>
    /// <param name="rigidity">Introduce a bias for non-straight seams (typically 0).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void LiquidRescale(int width, int height, double deltaX, double rigidity)
    {
        var geometry = new MagickGeometry(width, height);

        _nativeInstance.LiquidRescale(geometry.ToString(), deltaX, rigidity);
    }

    /// <summary>
    /// Rescales image with seam carving.
    /// </summary>
    /// <param name="geometry">The geometry to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void LiquidRescale(IMagickGeometry geometry)
    {
        Throw.IfNull(nameof(geometry), geometry);

        _nativeInstance.LiquidRescale(geometry.ToString(), geometry.X, geometry.Y);
    }

    /// <summary>
    /// Rescales image with seam carving.
    /// </summary>
    /// <param name="percentage">The percentage.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void LiquidRescale(Percentage percentage)
    {
        Throw.IfNegative(nameof(percentage), percentage);

        var geometry = new MagickGeometry(percentage, percentage);
        LiquidRescale(geometry);
    }

    /// <summary>
    /// Rescales image with seam carving.
    /// </summary>
    /// <param name="percentageWidth">The percentage of the width.</param>
    /// <param name="percentageHeight">The percentage of the height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void LiquidRescale(Percentage percentageWidth, Percentage percentageHeight)
    {
        Throw.IfNegative(nameof(percentageWidth), percentageWidth);
        Throw.IfNegative(nameof(percentageHeight), percentageHeight);

        var geometry = new MagickGeometry(percentageWidth, percentageHeight);
        LiquidRescale(geometry);
    }

    /// <summary>
    /// Rescales image with seam carving.
    /// </summary>
    /// <param name="percentageWidth">The percentage of the width.</param>
    /// <param name="percentageHeight">The percentage of the height.</param>
    /// <param name="deltaX">Maximum seam transversal step (0 means straight seams).</param>
    /// <param name="rigidity">Introduce a bias for non-straight seams (typically 0).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void LiquidRescale(Percentage percentageWidth, Percentage percentageHeight, double deltaX, double rigidity)
    {
        Throw.IfNegative(nameof(percentageWidth), percentageWidth);
        Throw.IfNegative(nameof(percentageHeight), percentageHeight);

        var geometry = new MagickGeometry(percentageWidth, percentageHeight);

        _nativeInstance.LiquidRescale(geometry.ToString(), deltaX, rigidity);
    }

    /// <summary>
    /// Local contrast enhancement.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="strength">The strength of the blur mask.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void LocalContrast(double radius, Percentage strength)
        => LocalContrast(radius, strength, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Local contrast enhancement.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="strength">The strength of the blur mask.</param>
    /// <param name="channels">The channel(s) that should be changed.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void LocalContrast(double radius, Percentage strength, Channels channels)
        => _nativeInstance.LocalContrast(radius, strength.ToDouble(), channels);

    /// <summary>
    /// Lower image (darken the edges of an image to give a 3-D lowered effect).
    /// </summary>
    /// <param name="size">The size of the edges.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Lower(int size)
        => _nativeInstance.RaiseOrLower(size, false);

    /// <summary>
    /// Magnify image by integral size.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Magnify()
        => _nativeInstance.Magnify();

    /// <summary>
    /// Remap image colors with closest color from the specified colors.
    /// </summary>
    /// <param name="colors">The colors to use.</param>
    /// <returns>The error informaton.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickErrorInfo Map(IEnumerable<IMagickColor<QuantumType>> colors)
    {
        Throw.IfNull(nameof(colors), colors);

        return Map(colors, new QuantizeSettings());
    }

    /// <summary>
    /// Remap image colors with closest color from the specified colors.
    /// </summary>
    /// <param name="colors">The colors to use.</param>
    /// <param name="settings">Quantize settings.</param>
    /// <returns>The error informaton.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickErrorInfo Map(IEnumerable<IMagickColor<QuantumType>> colors, IQuantizeSettings settings)
    {
        Throw.IfNull(nameof(colors), colors);

        var colorList = new List<IMagickColor<QuantumType>>(colors);
        if (colorList.Count == 0)
            throw new ArgumentException("Value cannot be empty.", nameof(colors));

        using var images = new MagickImageCollection();
        foreach (var color in colorList)
            images.Add(new MagickImage(color, 1, 1));

        using var image = images.AppendHorizontally();
        return Map(image, settings);
    }

    /// <summary>
    /// Remap image colors with closest color from reference image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <returns>The error informaton.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickErrorInfo Map(IMagickImage image)
        => Map(image, new QuantizeSettings());

    /// <summary>
    /// Remap image colors with closest color from reference image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="settings">Quantize settings.</param>
    /// <returns>The error informaton.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickErrorInfo Map(IMagickImage image, IQuantizeSettings settings)
    {
        Throw.IfNull(nameof(image), image);
        Throw.IfNull(nameof(settings), settings);

        if (_nativeInstance.Map(image, settings))
            return new MagickErrorInfo();

        return CreateErrorInfo(this);
    }

    /// <summary>
    /// Delineate arbitrarily shaped clusters in the image.
    /// </summary>
    /// <param name="size">The width and height of the pixels neighborhood.</param>
    public void MeanShift(int size)
        => MeanShift(size, size);

    /// <summary>
    /// Delineate arbitrarily shaped clusters in the image.
    /// </summary>
    /// <param name="size">The width and height of the pixels neighborhood.</param>
    /// <param name="colorDistance">The color distance.</param>
    public void MeanShift(int size, Percentage colorDistance)
        => MeanShift(size, size, colorDistance);

    /// <summary>
    /// Delineate arbitrarily shaped clusters in the image.
    /// </summary>
    /// <param name="width">The width of the pixels neighborhood.</param>
    /// <param name="height">The height of the pixels neighborhood.</param>
    public void MeanShift(int width, int height)
        => MeanShift(width, height, new Percentage(10));

    /// <summary>
    /// Delineate arbitrarily shaped clusters in the image.
    /// </summary>
    /// <param name="width">The width of the pixels neighborhood.</param>
    /// <param name="height">The height of the pixels neighborhood.</param>
    /// <param name="colorDistance">The color distance.</param>
    public void MeanShift(int width, int height, Percentage colorDistance)
        => _nativeInstance.MeanShift(width, height, PercentageHelper.ToQuantum(colorDistance));

    /// <summary>
    /// Filter image by replacing each pixel component with the median color in a circular neighborhood.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void MedianFilter()
        => MedianFilter(0);

    /// <summary>
    /// Filter image by replacing each pixel component with the median color in a circular neighborhood.
    /// </summary>
    /// <param name="radius">The radius of the pixel neighborhood.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void MedianFilter(int radius)
        => Statistic(StatisticType.Median, radius, radius);

    /// <summary>
    /// Reduce image by integral size.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Minify()
        => _nativeInstance.Minify();

    /// <summary>
    /// Returns the points that form the minimum bounding box around the image foreground objects with
    /// the "Rotating Calipers" algorithm. he method also returns these properties: minimum-bounding-box:area,
    /// minimum-bounding-box:width, minimum-bounding-box:height, and minimum-bounding-box:angle.
    /// </summary>
    /// <returns>The points that form the minimum bounding box around the image foreground objects.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IEnumerable<PointD> MinimumBoundingBox()
    {
        var result = _nativeInstance.MinimumBoundingBox(out var length);
        using var coordinates = new PointInfoCollection(result, (int)length);
        for (var i = 0; i < coordinates.Count; i++)
        {
            var x = coordinates.GetX(i);
            var y = coordinates.GetY(i);

            yield return new PointD(x, y);
        }
    }

    /// <summary>
    /// Modulate percent brightness of an image.
    /// </summary>
    /// <param name="brightness">The brightness percentage.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Modulate(Percentage brightness)
        => Modulate(brightness, new Percentage(100), new Percentage(100));

    /// <summary>
    /// Modulate percent saturation and brightness of an image.
    /// </summary>
    /// <param name="brightness">The brightness percentage.</param>
    /// <param name="saturation">The saturation percentage.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Modulate(Percentage brightness, Percentage saturation)
        => Modulate(brightness, saturation, new Percentage(100));

    /// <summary>
    /// Modulate percent hue, saturation, and brightness of an image.
    /// </summary>
    /// <param name="brightness">The brightness percentage.</param>
    /// <param name="saturation">The saturation percentage.</param>
    /// <param name="hue">The hue percentage.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Modulate(Percentage brightness, Percentage saturation, Percentage hue)
    {
        Throw.IfNegative(nameof(brightness), brightness);
        Throw.IfNegative(nameof(saturation), saturation);
        Throw.IfNegative(nameof(hue), hue);

        var modulate = string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2}", brightness.ToDouble(), saturation.ToDouble(), hue.ToDouble());

        _nativeInstance.Modulate(modulate);
    }

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="kernel">Built-in kernel.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Morphology(MorphologyMethod method, Kernel kernel)
        => Morphology(method, kernel, string.Empty);

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="kernel">Built-in kernel.</param>
    /// <param name="channels">The channels to apply the kernel to.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Morphology(MorphologyMethod method, Kernel kernel, Channels channels)
        => Morphology(method, kernel, string.Empty, channels);

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="kernel">Built-in kernel.</param>
    /// <param name="channels">The channels to apply the kernel to.</param>
    /// <param name="iterations">The number of iterations.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Morphology(MorphologyMethod method, Kernel kernel, Channels channels, int iterations)
        => Morphology(method, kernel, string.Empty, channels, iterations);

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="kernel">Built-in kernel.</param>
    /// <param name="iterations">The number of iterations.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Morphology(MorphologyMethod method, Kernel kernel, int iterations)
        => Morphology(method, kernel, string.Empty, ImageMagick.Channels.Undefined, iterations);

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="kernel">Built-in kernel.</param>
    /// <param name="arguments">Kernel arguments.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Morphology(MorphologyMethod method, Kernel kernel, string? arguments)
        => Morphology(method, kernel, arguments, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="kernel">Built-in kernel.</param>
    /// <param name="arguments">Kernel arguments.</param>
    /// <param name="channels">The channels to apply the kernel to.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Morphology(MorphologyMethod method, Kernel kernel, string? arguments, Channels channels)
        => Morphology(method, kernel, arguments, channels, 1);

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="kernel">Built-in kernel.</param>
    /// <param name="arguments">Kernel arguments.</param>
    /// <param name="channels">The channels to apply the kernel to.</param>
    /// <param name="iterations">The number of iterations.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Morphology(MorphologyMethod method, Kernel kernel, string? arguments, Channels channels, int iterations)
    {
        var newKernel = Enum.GetName(kernel.GetType(), kernel).ToLowerInvariant() + ":" + arguments;

        Morphology(method, newKernel, channels, iterations);
    }

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="kernel">Built-in kernel.</param>
    /// <param name="arguments">Kernel arguments.</param>
    /// <param name="iterations">The number of iterations.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Morphology(MorphologyMethod method, Kernel kernel, string? arguments, int iterations)
        => Morphology(method, kernel, arguments, ImageMagick.Channels.Undefined, iterations);

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="userKernel">User suplied kernel.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Morphology(MorphologyMethod method, string userKernel)
        => Morphology(method, userKernel, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="userKernel">User suplied kernel.</param>
    /// <param name="channels">The channels to apply the kernel to.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Morphology(MorphologyMethod method, string userKernel, Channels channels)
        => Morphology(method, userKernel, channels, 1);

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="userKernel">User suplied kernel.</param>
    /// <param name="channels">The channels to apply the kernel to.</param>
    /// <param name="iterations">The number of iterations.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Morphology(MorphologyMethod method, string userKernel, Channels channels, int iterations)
        => _nativeInstance.Morphology(method, userKernel, channels, iterations);

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="userKernel">User suplied kernel.</param>
    /// <param name="iterations">The number of iterations.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Morphology(MorphologyMethod method, string userKernel, int iterations)
        => Morphology(method, userKernel, ImageMagick.Channels.Undefined, iterations);

    /// <summary>
    /// Applies a kernel to the image according to the given mophology settings.
    /// </summary>
    /// <param name="settings">The morphology settings.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Morphology(IMorphologySettings settings)
    {
        Throw.IfNull(nameof(settings), settings);

        using var temporaryDefines = new TemporaryDefines(this);
        temporaryDefines.SetArtifact("convolve:bias", settings.ConvolveBias);
        temporaryDefines.SetArtifact("convolve:scale", settings.ConvolveScale);

        if (settings.UserKernel is not null && settings.UserKernel.Length > 0)
            Morphology(settings.Method, settings.UserKernel, settings.Channels, settings.Iterations);
        else
            Morphology(settings.Method, settings.Kernel, settings.KernelArguments, settings.Channels, settings.Iterations);
    }

    /// <summary>
    /// Returns the normalized moments of one or more image channels.
    /// </summary>
    /// <returns>The normalized moments of one or more image channels.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMoments Moments()
    {
        var list = _nativeInstance.Moments();
        try
        {
            return new Moments(this, list);
        }
        finally
        {
            ImageMagick.Moments.DisposeList(list);
        }
    }

    /// <summary>
    /// Motion blur image with specified blur factor.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <param name="angle">The angle the object appears to be comming from (zero degrees is from the right).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void MotionBlur(double radius, double sigma, double angle)
        => _nativeInstance.MotionBlur(radius, sigma, angle);

    /// <summary>
    /// Negate colors in image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Negate()
        => Negate(ImageMagick.Channels.Undefined);

    /// <summary>
    /// Negate colors in image for the specified channel.
    /// </summary>
    /// <param name="channels">The channel(s) that should be negated.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Negate(Channels channels)
        => _nativeInstance.Negate(false, channels);

    /// <summary>
    /// Negate the grayscale colors in image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void NegateGrayscale()
        => NegateGrayscale(ImageMagick.Channels.Undefined);

    /// <summary>
    /// Negate the grayscale colors in image for the specified channel.
    /// </summary>
    /// <param name="channels">The channel(s) that should be negated.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void NegateGrayscale(Channels channels)
        => _nativeInstance.Negate(true, channels);

    /// <summary>
    /// Normalize image (increase contrast by normalizing the pixel values to span the full range
    /// of color values).
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Normalize()
        => _nativeInstance.Normalize();

    /// <summary>
    /// Oilpaint image (image looks like oil painting).
    /// </summary>
    public void OilPaint()
        => OilPaint(3.0, 1.0);

    /// <summary>
    /// Oilpaint image (image looks like oil painting).
    /// </summary>
    /// <param name="radius">The radius of the circular neighborhood.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void OilPaint(double radius, double sigma)
        => _nativeInstance.OilPaint(radius, sigma);

    /// <summary>
    /// Changes any pixel that matches target with the color defined by fill.
    /// </summary>
    /// <param name="target">The color to replace.</param>
    /// <param name="fill">The color to replace opaque color with.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Opaque(IMagickColor<QuantumType> target, IMagickColor<QuantumType> fill)
        => Opaque(target, fill, false);

    /// <summary>
    /// Perform a ordered dither based on a number of pre-defined dithering threshold maps, but over
    /// multiple intensity levels.
    /// </summary>
    /// <param name="thresholdMap">A string containing the name of the threshold dither map to use,
    /// followed by zero or more numbers representing the number of color levels tho dither between.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void OrderedDither(string thresholdMap)
    {
        Throw.IfNullOrEmpty(nameof(thresholdMap), thresholdMap);

        OrderedDither(thresholdMap, ImageMagick.Channels.Undefined);
    }

    /// <summary>
    /// Perform a ordered dither based on a number of pre-defined dithering threshold maps, but over
    /// multiple intensity levels.
    /// </summary>
    /// <param name="thresholdMap">A string containing the name of the threshold dither map to use,
    /// followed by zero or more numbers representing the number of color levels tho dither between.</param>
    /// <param name="channels">The channel(s) to dither.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void OrderedDither(string thresholdMap, Channels channels)
    {
        Throw.IfNullOrEmpty(nameof(thresholdMap), thresholdMap);

        _nativeInstance.OrderedDither(thresholdMap, channels);
    }

    /// <summary>
    /// Set each pixel whose value is less than epsilon to epsilon or -epsilon (whichever is closer)
    /// otherwise the pixel value remains unchanged.
    /// </summary>
    /// <param name="epsilon">The epsilon threshold.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Perceptible(double epsilon)
        => Perceptible(epsilon, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Set each pixel whose value is less than epsilon to epsilon or -epsilon (whichever is closer)
    /// otherwise the pixel value remains unchanged.
    /// </summary>
    /// <param name="epsilon">The epsilon threshold.</param>
    /// <param name="channels">The channel(s) to perceptible.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Perceptible(double epsilon, Channels channels)
        => _nativeInstance.Perceptible(epsilon, channels);

    /// <summary>
    /// Returns the perceptual hash of this image.
    /// </summary>
    /// <returns>The perceptual hash of this image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IPerceptualHash? PerceptualHash()
    {
        using var temporaryDefines = new TemporaryDefines(this);
        temporaryDefines.SetArtifact("phash:colorspaces", "sRGB,HCLp");
        var list = _nativeInstance.PerceptualHash();

        try
        {
            var hash = new PerceptualHash(this, list);
            if (!hash.Isvalid)
                return null;

            return hash;
        }
        finally
        {
            ImageMagick.PerceptualHash.DisposeList(list);
        }
    }

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="data">The byte array to read the information from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Ping(byte[] data)
        => Ping(data, null);

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Ping(byte[] data, int offset, int count)
        => Ping(data, offset, count, null);

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Ping(byte[] data, int offset, int count, IMagickReadSettings<QuantumType>? readSettings)
    {
        Throw.IfNullOrEmpty(nameof(data), data);
        Throw.IfTrue(nameof(offset), offset < 0, "The offset should be positive.");
        Throw.IfTrue(nameof(count), count < 1, "The number of bytes should be at least 1.");
        Throw.IfTrue(nameof(offset), offset >= data.Length, "The offset should not exceed the length of the array.");
        Throw.IfTrue(nameof(count), offset + count > data.Length, "The number of bytes should not exceed the length of the array.");

        Read(data, offset, count, readSettings, true);
    }

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="data">The byte array to read the information from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Ping(byte[] data, IMagickReadSettings<QuantumType>? readSettings)
    {
        Throw.IfNullOrEmpty(nameof(data), data);

        Ping(data, 0, data.Length, readSettings);
    }

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Ping(FileInfo file) => Ping(file, null);

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Ping(FileInfo file, IMagickReadSettings<QuantumType>? readSettings)
    {
        Throw.IfNull(nameof(file), file);

        Read(file.FullName, readSettings, true);
    }

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Ping(Stream stream)
        => Ping(stream, null);

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Ping(Stream stream, IMagickReadSettings<QuantumType>? readSettings)
        => Read(stream, readSettings, true);

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Ping(string fileName)
        => Ping(fileName, null);

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Ping(string fileName, IMagickReadSettings<QuantumType>? readSettings)
        => Read(fileName, readSettings, true);

    /// <summary>
    /// Simulates a polaroid picture.
    /// </summary>
    /// <param name="caption">The caption to put on the image.</param>
    /// <param name="angle">The angle of image.</param>
    /// <param name="method">Pixel interpolate method.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Polaroid(string caption, double angle, PixelInterpolateMethod method)
    {
        Throw.IfNull(nameof(caption), caption);

        _nativeInstance.Polaroid(_settings.Drawing, caption, angle, method);
    }

    /// <summary>
    /// Reduces the image to a limited number of colors for a "poster" effect.
    /// </summary>
    /// <param name="levels">Number of color levels allowed in each channel.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Posterize(int levels)
        => Posterize(levels, DitherMethod.No);

    /// <summary>
    /// Reduces the image to a limited number of colors for a "poster" effect.
    /// </summary>
    /// <param name="levels">Number of color levels allowed in each channel.</param>
    /// <param name="channels">The channel(s) to posterize.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Posterize(int levels, Channels channels)
        => Posterize(levels, DitherMethod.No, channels);

    /// <summary>
    /// Reduces the image to a limited number of colors for a "poster" effect.
    /// </summary>
    /// <param name="levels">Number of color levels allowed in each channel.</param>
    /// <param name="method">Dither method to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Posterize(int levels, DitherMethod method)
        => Posterize(levels, method, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Reduces the image to a limited number of colors for a "poster" effect.
    /// </summary>
    /// <param name="levels">Number of color levels allowed in each channel.</param>
    /// <param name="method">Dither method to use.</param>
    /// <param name="channels">The channel(s) to posterize.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Posterize(int levels, DitherMethod method, Channels channels)
        => _nativeInstance.Posterize(levels, method, channels);

    /// <summary>
    /// Sets an internal option to preserve the color type.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    [SuppressMessage("Usage", "CA2245:Do not assign a property to itself.", Justification = "False positive.")]
    public void PreserveColorType()
    {
        ColorType = ColorType;
        SetAttribute("colorspace:auto-grayscale", "false");
    }

    /// <summary>
    /// Quantize image (reduce number of colors).
    /// </summary>
    /// <param name="settings">Quantize settings.</param>
    /// <returns>The error information.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickErrorInfo? Quantize(IQuantizeSettings settings)
    {
        Throw.IfNull(nameof(settings), settings);

        _nativeInstance.Quantize(settings);

        if (settings.MeasureErrors)
            return CreateErrorInfo(this);
        else
            return null;
    }

    /// <summary>
    /// Raise image (lighten the edges of an image to give a 3-D raised effect).
    /// </summary>
    /// <param name="size">The size of the edges.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Raise(int size) =>
        _nativeInstance.RaiseOrLower(size, true);

    /// <summary>
    /// Changes the value of individual pixels based on the intensity of each pixel compared to a
    /// random threshold. The result is a low-contrast, two color image.
    /// </summary>
    /// <param name="percentageLow">The low threshold.</param>
    /// <param name="percentageHigh">The high threshold.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void RandomThreshold(Percentage percentageLow, Percentage percentageHigh)
        => RandomThreshold(percentageLow, percentageHigh, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Changes the value of individual pixels based on the intensity of each pixel compared to a
    /// random threshold. The result is a low-contrast, two color image.
    /// </summary>
    /// <param name="percentageLow">The low threshold.</param>
    /// <param name="percentageHigh">The high threshold.</param>
    /// <param name="channels">The channel(s) to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void RandomThreshold(Percentage percentageLow, Percentage percentageHigh, Channels channels)
        => RandomThreshold(PercentageHelper.ToQuantumType(percentageLow), PercentageHelper.ToQuantumType(percentageHigh), channels);

    /// <summary>
    /// Changes the value of individual pixels based on the intensity of each pixel compared to a
    /// random threshold. The result is a low-contrast, two color image.
    /// </summary>
    /// <param name="low">The low threshold.</param>
    /// <param name="high">The high threshold.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void RandomThreshold(QuantumType low, QuantumType high)
        => RandomThreshold(low, high, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Changes the value of individual pixels based on the intensity of each pixel compared to a
    /// random threshold. The result is a low-contrast, two color image.
    /// </summary>
    /// <param name="low">The low threshold.</param>
    /// <param name="high">The high threshold.</param>
    /// <param name="channels">The channel(s) to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void RandomThreshold(QuantumType low, QuantumType high, Channels channels)
        => _nativeInstance.RandomThreshold(low, high, channels);

    /// <summary>
    /// Applies soft and hard thresholding.
    /// </summary>
    /// <param name="percentageLowBlack">Defines the minimum black threshold value.</param>
    /// <param name="percentageLowWhite">Defines the minimum white threshold value.</param>
    /// <param name="percentageHighWhite">Defines the maximum white threshold value.</param>
    /// <param name="percentageHighBlack">Defines the maximum black threshold value.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void RangeThreshold(Percentage percentageLowBlack, Percentage percentageLowWhite, Percentage percentageHighWhite, Percentage percentageHighBlack)
    {
        Throw.IfNegative(nameof(percentageLowBlack), percentageLowBlack);
        Throw.IfNegative(nameof(percentageLowWhite), percentageLowWhite);
        Throw.IfNegative(nameof(percentageHighWhite), percentageHighWhite);
        Throw.IfNegative(nameof(percentageHighBlack), percentageHighBlack);

        RangeThreshold(PercentageHelper.ToQuantumType(percentageLowBlack), PercentageHelper.ToQuantumType(percentageLowWhite), PercentageHelper.ToQuantumType(percentageHighWhite), PercentageHelper.ToQuantumType(percentageHighBlack));
    }

    /// <summary>
    /// Applies soft and hard thresholding.
    /// </summary>
    /// <param name="lowBlack">Defines the minimum black threshold value.</param>
    /// <param name="lowWhite">Defines the minimum white threshold value.</param>
    /// <param name="highWhite">Defines the maximum white threshold value.</param>
    /// <param name="highBlack">Defines the maximum black threshold value.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void RangeThreshold(QuantumType lowBlack, QuantumType lowWhite, QuantumType highWhite, QuantumType highBlack)
        => _nativeInstance.RangeThreshold(lowBlack, lowWhite, highWhite, highBlack);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(byte[] data)
        => Read(data, null);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(byte[] data, int offset, int count)
        => Read(data, offset, count, null);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(byte[] data, int offset, int count, MagickFormat format)
        => Read(data, offset, count, new MagickReadSettings { Format = format });

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(byte[] data, int offset, int count, IMagickReadSettings<QuantumType>? readSettings)
    {
        Throw.IfNullOrEmpty(nameof(data), data);
        Throw.IfTrue(nameof(offset), offset < 0, "The offset should be positive.");
        Throw.IfTrue(nameof(count), count < 1, "The number of bytes should be at least 1.");
        Throw.IfTrue(nameof(offset), offset >= data.Length, "The offset should not exceed the length of the array.");
        Throw.IfTrue(nameof(count), offset + count > data.Length, "The number of bytes should not exceed the length of the array.");

        Read(data, offset, count, readSettings, false);
    }

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(byte[] data, MagickFormat format)
    {
        Throw.IfNullOrEmpty(nameof(data), data);

        Read(data, 0, data.Length, new MagickReadSettings { Format = format }, false);
    }

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(byte[] data, IMagickReadSettings<QuantumType>? readSettings)
    {
        Throw.IfNullOrEmpty(nameof(data), data);

        Read(data, 0, data.Length, readSettings, false);
    }

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(FileInfo file)
        => Read(file, null);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(FileInfo file, int width, int height)
    {
        Throw.IfNull(nameof(file), file);

        Read(file.FullName, width, height);
    }

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(FileInfo file, MagickFormat format)
    {
        Throw.IfNull(nameof(file), file);

        Read(file.FullName, new MagickReadSettings { Format = format });
    }

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(FileInfo file, IMagickReadSettings<QuantumType>? readSettings)
    {
        Throw.IfNull(nameof(file), file);

        Read(file.FullName, readSettings);
    }

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="color">The color to fill the image with.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(IMagickColor<QuantumType> color, int width, int height)
    {
        Throw.IfNull(nameof(color), color);

        Read("xc:" + color.ToShortString(), width, height);
        BackgroundColor = color;
    }

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(Stream stream)
        => Read(stream, MagickFormat.Unknown);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(Stream stream, MagickFormat format)
        => Read(stream, new MagickReadSettings { Format = format });

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(Stream stream, IMagickReadSettings<QuantumType>? readSettings)
        => Read(stream, readSettings, false);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(string fileName)
        => Read(fileName, null);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(string fileName, int width, int height)
    {
        Throw.IfTrue(nameof(width), width <= 0, "The width should be positive.");
        Throw.IfTrue(nameof(height), height <= 0, "The height should be positive.");

        var settings = new MagickReadSettings(_settings)
        {
            Width = width,
            Height = height,
        };

        Read(fileName, settings);
    }

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(string fileName, MagickFormat format)
        => Read(fileName, new MagickReadSettings { Format = format });

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(string fileName, IMagickReadSettings<QuantumType>? readSettings)
        => Read(fileName, readSettings, false);

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

        var bytes = await FileHelper.ReadAllBytesAsync(fileName, cancellationToken).ConfigureAwait(false);

        cancellationToken.ThrowIfCancellationRequested();
        Read(bytes, 0, bytes.Length, readSettings, false, filePath);
    }

    /// <summary>
    /// Read single image frame from pixel data.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void ReadPixels(byte[] data, IPixelReadSettings<QuantumType> settings)
    {
        Throw.IfNullOrEmpty(nameof(data), data);

        ReadPixels(data, 0, data.Length, settings);
    }

    /// <summary>
    /// Read single image frame from pixel data.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void ReadPixels(byte[] data, int offset, int count, IPixelReadSettings<QuantumType> settings)
    {
        Throw.IfNullOrEmpty(nameof(data), data);
        Throw.IfTrue(nameof(offset), offset < 0, "The offset should be positive.");
        Throw.IfTrue(nameof(count), count < 1, "The number of bytes should be at least 1.");
        Throw.IfTrue(nameof(offset), offset >= data.Length, "The offset should not exceed the length of the array.");
        Throw.IfTrue(nameof(count), offset + count > data.Length, "The number of bytes should not exceed the length of the array.");
        Throw.IfNull(nameof(settings), settings);
        Throw.IfNullOrEmpty(nameof(settings), settings.Mapping, "Pixel storage mapping should be defined.");
        Throw.IfTrue(nameof(settings), settings.StorageType == StorageType.Undefined, "Storage type should not be undefined.");

        var newReadSettings = CreateReadSettings(settings.ReadSettings);
        SetSettings(newReadSettings);

        var expectedLength = GetExpectedByteLength(settings);
        Throw.IfTrue(nameof(count), count < expectedLength, "The count is {0} but should be at least {1}.", count, expectedLength);

        _nativeInstance.ReadPixels(settings.ReadSettings.Width!.Value, settings.ReadSettings.Height!.Value, settings.Mapping, settings.StorageType, data, offset);
    }

#if !Q8
    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="data">The quantum array to read the image data from.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void ReadPixels(QuantumType[] data, IPixelReadSettings<QuantumType> settings)
    {
        Throw.IfNullOrEmpty(nameof(data), data);

        ReadPixels(data, 0, data.Length, settings);
    }

    /// <summary>
    /// Read single image frame from pixel data.
    /// </summary>
    /// <param name="data">The quantum array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of items to read.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void ReadPixels(QuantumType[] data, int offset, int count, IPixelReadSettings<QuantumType> settings)
    {
        Throw.IfNullOrEmpty(nameof(data), data);
        Throw.IfTrue(nameof(offset), offset < 0, "The offset should be positive.");
        Throw.IfTrue(nameof(count), count < 1, "The number of items should be at least 1.");
        Throw.IfTrue(nameof(offset), offset >= data.Length, "The offset should not exceed the length of the array.");
        Throw.IfTrue(nameof(count), offset + count > data.Length, "The number of items should not exceed the length of the array.");
        Throw.IfNull(nameof(settings), settings);
        Throw.IfNullOrEmpty(nameof(settings), settings.Mapping, "Pixel storage mapping should be defined.");
        Throw.IfTrue(nameof(settings), settings.StorageType != StorageType.Quantum, $"Storage type should be {nameof(StorageType.Quantum)}.");

        var newReadSettings = CreateReadSettings(settings.ReadSettings);
        SetSettings(newReadSettings);

        var expectedLength = GetExpectedLength(settings);
        Throw.IfTrue(nameof(count), count < expectedLength, "The count is {0} but should be at least {1}.", count, expectedLength);

        var offsetInBytes = ToByteCount(settings.StorageType, offset);

        _nativeInstance.ReadPixels(settings.ReadSettings.Width!.Value, settings.ReadSettings.Height!.Value, settings.Mapping, settings.StorageType, data, offsetInBytes);
    }
#endif

    /// <summary>
    /// Read single image frame from pixel data.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void ReadPixels(FileInfo file, IPixelReadSettings<QuantumType> settings)
    {
        Throw.IfNull(nameof(file), file);

        ReadPixels(file.FullName, settings);
    }

    /// <summary>
    /// Read single image frame from pixel data.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void ReadPixels(Stream stream, IPixelReadSettings<QuantumType> settings)
    {
        Throw.IfNullOrEmpty(nameof(stream), stream);

        var bytes = Bytes.Create(stream);
        ReadPixels(bytes.GetData(), 0, bytes.Length, settings);
    }

    /// <summary>
    /// Read single image frame from pixel data.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void ReadPixels(string fileName, IPixelReadSettings<QuantumType> settings)
    {
        var filePath = FileHelper.CheckForBaseDirectory(fileName);
        Throw.IfNullOrEmpty(nameof(fileName), filePath);

        var data = File.ReadAllBytes(filePath);
        ReadPixels(data, 0, data.Length, settings);
    }

    /// <summary>
    /// Read single image frame from pixel data.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public Task ReadPixelsAsync(FileInfo file, IPixelReadSettings<QuantumType> settings)
        => ReadPixelsAsync(file, settings, CancellationToken.None);

    /// <summary>
    /// Read single image frame from pixel data.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public Task ReadPixelsAsync(FileInfo file, IPixelReadSettings<QuantumType> settings, CancellationToken cancellationToken)
    {
        Throw.IfNull(nameof(file), file);

        return ReadPixelsAsync(file.FullName, settings, cancellationToken);
    }

    /// <summary>
    /// Read single image frame from pixel data.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public Task ReadPixelsAsync(Stream stream, IPixelReadSettings<QuantumType> settings)
        => ReadPixelsAsync(stream, settings, CancellationToken.None);

    /// <summary>
    /// Read single image frame from pixel data.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public async Task ReadPixelsAsync(Stream stream, IPixelReadSettings<QuantumType> settings, CancellationToken cancellationToken)
    {
        Throw.IfNullOrEmpty(nameof(stream), stream);

        var bytes = await Bytes.CreateAsync(stream, cancellationToken).ConfigureAwait(false);
        ReadPixels(bytes.GetData(), 0, bytes.Length, settings);
    }

    /// <summary>
    /// Read single image frame from pixel data.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public Task ReadPixelsAsync(string fileName, IPixelReadSettings<QuantumType> settings)
        => ReadPixelsAsync(fileName, settings, CancellationToken.None);

    /// <summary>
    /// Read single image frame from pixel data.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public async Task ReadPixelsAsync(string fileName, IPixelReadSettings<QuantumType> settings, CancellationToken cancellationToken)
    {
        var filePath = FileHelper.CheckForBaseDirectory(fileName);
        Throw.IfNullOrEmpty(nameof(fileName), filePath);

        var data = await FileHelper.ReadAllBytesAsync(filePath, cancellationToken).ConfigureAwait(false);

        cancellationToken.ThrowIfCancellationRequested();
        ReadPixels(data, 0, data.Length, settings);
    }

    /// <summary>
    /// Reduce noise in image using a noise peak elimination filter.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void ReduceNoise()
        => ReduceNoise(3);

    /// <summary>
    /// Reduce noise in image using a noise peak elimination filter.
    /// </summary>
    /// <param name="order">The order to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void ReduceNoise(int order)
        => Statistic(StatisticType.Nonpeak, order, order);

    /// <summary>
    /// Associates a mask with the image as defined by the specified region.
    /// </summary>
    /// <param name="geometry">The mask region.</param>
    public void RegionMask(IMagickGeometry geometry)
        => _nativeInstance.RegionMask(MagickRectangle.FromGeometry(geometry, this));

    /// <summary>
    /// Removes the artifact with the specified name.
    /// </summary>
    /// <param name="name">The name of the artifact.</param>
    public void RemoveArtifact(string name)
        => _nativeInstance.RemoveArtifact(name);

    /// <summary>
    /// Removes the attribute with the specified name.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    public void RemoveAttribute(string name)
        => _nativeInstance.RemoveAttribute(name);

    /// <summary>
    /// Removes the region mask of the image.
    /// </summary>
    public void RemoveRegionMask()
        => _nativeInstance.RegionMask(null);

    /// <summary>
    /// Remove a profile from the image.
    /// </summary>
    /// <param name="profile">The profile to remove.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void RemoveProfile(IImageProfile profile)
    {
        Throw.IfNull(nameof(profile), profile);

        RemoveProfile(profile.Name);
    }

    /// <summary>
    /// Remove a named profile from the image.
    /// </summary>
    /// <param name="name">The name of the profile (e.g. "ICM", "IPTC", or a generic profile name).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void RemoveProfile(string name)
    {
        Throw.IfNullOrEmpty(nameof(name), name);

        _nativeInstance.RemoveProfile(name);
    }

    /// <summary>
    /// Removes the associated read mask of the image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void RemoveReadMask()
        => _nativeInstance.SetReadMask(null);

    /// <summary>
    /// Removes the associated write mask of the image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void RemoveWriteMask()
        => _nativeInstance.SetWriteMask(null);

    /// <summary>
    /// Resets the page property of this image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void RePage()
        => Page = new MagickGeometry(0, 0, 0, 0);

    /// <summary>
    /// Resize image in terms of its pixel size.
    /// </summary>
    /// <param name="resolutionX">The new X resolution.</param>
    /// <param name="resolutionY">The new Y resolution.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Resample(double resolutionX, double resolutionY)
    {
        var density = new PointD(resolutionX, resolutionY);
        Resample(density);
    }

    /// <summary>
    /// Resize image in terms of its pixel size.
    /// </summary>
    /// <param name="density">The density to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Resample(PointD density)
        => _nativeInstance.Resample(density.X, density.Y);

    /// <summary>
    /// Resize image to specified size.
    /// <para />
    /// Resize will fit the image into the requested size. It does NOT fill, the requested box size.
    /// Use the <see cref="IMagickGeometry"/> overload for more control over the resulting size.
    /// </summary>
    /// <param name="width">The new width.</param>
    /// <param name="height">The new height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Resize(int width, int height)
        => Resize(new MagickGeometry(width, height));

    /// <summary>
    /// Resize image to specified geometry.
    /// </summary>
    /// <param name="geometry">The geometry to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Resize(IMagickGeometry geometry)
    {
        Throw.IfNull(nameof(geometry), geometry);

        _nativeInstance.Resize(geometry.ToString());
    }

    /// <summary>
    /// Resize image to specified percentage.
    /// </summary>
    /// <param name="percentage">The percentage.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Resize(Percentage percentage)
    {
        Throw.IfNegative(nameof(percentage), percentage);

        var geometry = new MagickGeometry(percentage, percentage);
        Resize(geometry);
    }

    /// <summary>
    /// Resize image to specified percentage.
    /// </summary>
    /// <param name="percentageWidth">The percentage of the width.</param>
    /// <param name="percentageHeight">The percentage of the height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Resize(Percentage percentageWidth, Percentage percentageHeight)
    {
        Throw.IfNegative(nameof(percentageWidth), percentageWidth);
        Throw.IfNegative(nameof(percentageHeight), percentageHeight);

        var geometry = new MagickGeometry(percentageWidth, percentageHeight);
        Resize(geometry);
    }

    /// <summary>
    /// Roll image (rolls image vertically and horizontally).
    /// </summary>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Roll(int x, int y)
        => _nativeInstance.Roll(x, y);

    /// <summary>
    /// Rotate image clockwise by specified number of degrees.
    /// </summary>
    /// <remarks>Specify a negative number for <paramref name="degrees"/> to rotate counter-clockwise.</remarks>
    /// <param name="degrees">The number of degrees to rotate (positive to rotate clockwise, negative to rotate counter-clockwise).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Rotate(double degrees)
        => _nativeInstance.Rotate(degrees);

    /// <summary>
    /// Rotational blur image.
    /// </summary>
    /// <param name="angle">The angle to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void RotationalBlur(double angle)
        => RotationalBlur(angle, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Rotational blur image.
    /// </summary>
    /// <param name="angle">The angle to use.</param>
    /// <param name="channels">The channel(s) to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void RotationalBlur(double angle, Channels channels)
        => _nativeInstance.RotationalBlur(angle, channels);

    /// <summary>
    /// Resize image by using pixel sampling algorithm.
    /// <para />
    /// Resize will fit the image into the requested size. It does NOT fill, the requested box size.
    /// Use the <see cref="IMagickGeometry"/> overload for more control over the resulting size.
    /// </summary>
    /// <param name="width">The new width.</param>
    /// <param name="height">The new height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Sample(int width, int height)
        => Sample(new MagickGeometry(width, height));

    /// <summary>
    /// Resize image by using pixel sampling algorithm.
    /// </summary>
    /// <param name="geometry">The geometry to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Sample(IMagickGeometry geometry)
    {
        Throw.IfNull(nameof(geometry), geometry);

        _nativeInstance.Sample(geometry.ToString());
    }

    /// <summary>
    /// Resize image by using pixel sampling algorithm to the specified percentage.
    /// </summary>
    /// <param name="percentage">The percentage.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Sample(Percentage percentage)
        => Sample(percentage, percentage);

    /// <summary>
    /// Resize image by using pixel sampling algorithm to the specified percentage.
    /// </summary>
    /// <param name="percentageWidth">The percentage of the width.</param>
    /// <param name="percentageHeight">The percentage of the height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Sample(Percentage percentageWidth, Percentage percentageHeight)
    {
        Throw.IfNegative(nameof(percentageWidth), percentageWidth);
        Throw.IfNegative(nameof(percentageHeight), percentageHeight);

        Sample(new MagickGeometry(percentageWidth, percentageHeight));
    }

    /// <summary>
    /// Resize image by using simple ratio algorithm.
    /// <para />
    /// Resize will fit the image into the requested size. It does NOT fill, the requested box size.
    /// Use the <see cref="IMagickGeometry"/> overload for more control over the resulting size.
    /// </summary>
    /// <param name="width">The new width.</param>
    /// <param name="height">The new height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Scale(int width, int height)
        => Scale(new MagickGeometry(width, height));

    /// <summary>
    /// Resize image by using simple ratio algorithm.
    /// </summary>
    /// <param name="geometry">The geometry to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Scale(IMagickGeometry geometry)
    {
        Throw.IfNull(nameof(geometry), geometry);

        _nativeInstance.Scale(geometry.ToString());
    }

    /// <summary>
    /// Resize image by using simple ratio algorithm to the specified percentage.
    /// </summary>
    /// <param name="percentage">The percentage.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Scale(Percentage percentage)
        => Scale(percentage, percentage);

    /// <summary>
    /// Resize image by using simple ratio algorithm to the specified percentage.
    /// </summary>
    /// <param name="percentageWidth">The percentage of the width.</param>
    /// <param name="percentageHeight">The percentage of the height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Scale(Percentage percentageWidth, Percentage percentageHeight)
    {
        Throw.IfNegative(nameof(percentageWidth), percentageWidth);
        Throw.IfNegative(nameof(percentageHeight), percentageHeight);

        Scale(new MagickGeometry(percentageWidth, percentageHeight));
    }

    /// <summary>
    /// Segment (coalesce similar image components) by analyzing the histograms of the color
    /// components and identifying units that are homogeneous with the fuzzy c-means technique.
    /// Also uses QuantizeColorSpace and Verbose image attributes.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Segment()
        => Segment(ColorSpace.Undefined, 1.0, 1.5);

    /// <summary>
    /// Segment (coalesce similar image components) by analyzing the histograms of the color
    /// components and identifying units that are homogeneous with the fuzzy c-means technique.
    /// Also uses QuantizeColorSpace and Verbose image attributes.
    /// </summary>
    /// <param name="quantizeColorSpace">Quantize colorspace.</param>
    /// <param name="clusterThreshold">This represents the minimum number of pixels contained in
    /// a hexahedra before it can be considered valid (expressed as a percentage).</param>
    /// <param name="smoothingThreshold">The smoothing threshold eliminates noise in the second
    /// derivative of the histogram. As the value is increased, you can expect a smoother second
    /// derivative.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Segment(ColorSpace quantizeColorSpace, double clusterThreshold, double smoothingThreshold)
        => _nativeInstance.Segment(quantizeColorSpace, clusterThreshold, smoothingThreshold);

    /// <summary>
    /// Selectively blur pixels within a contrast threshold. It is similar to the unsharpen mask
    /// that sharpens everything with contrast above a certain threshold.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Gaussian, in pixels.</param>
    /// <param name="threshold">Only pixels within this contrast threshold are included in the blur operation.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void SelectiveBlur(double radius, double sigma, double threshold)
        => SelectiveBlur(radius, sigma, threshold, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Selectively blur pixels within a contrast threshold. It is similar to the unsharpen mask
    /// that sharpens everything with contrast above a certain threshold.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Gaussian, in pixels.</param>
    /// <param name="threshold">Only pixels within this contrast threshold are included in the blur operation.</param>
    /// <param name="channels">The channel(s) to blur.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void SelectiveBlur(double radius, double sigma, double threshold, Channels channels)
        => _nativeInstance.SelectiveBlur(radius, sigma, threshold, channels);

    /// <summary>
    /// Selectively blur pixels within a contrast threshold. It is similar to the unsharpen mask
    /// that sharpens everything with contrast above a certain threshold.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Gaussian, in pixels.</param>
    /// <param name="thresholdPercentage">Only pixels within this contrast threshold are included in the blur operation.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void SelectiveBlur(double radius, double sigma, Percentage thresholdPercentage)
        => SelectiveBlur(radius, sigma, thresholdPercentage, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Selectively blur pixels within a contrast threshold. It is similar to the unsharpen mask
    /// that sharpens everything with contrast above a certain threshold.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Gaussian, in pixels.</param>
    /// <param name="thresholdPercentage">Only pixels within this contrast threshold are included in the blur operation.</param>
    /// <param name="channels">The channel(s) to blur.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void SelectiveBlur(double radius, double sigma, Percentage thresholdPercentage, Channels channels)
        => _nativeInstance.SelectiveBlur(radius, sigma, PercentageHelper.ToQuantum(thresholdPercentage), channels);

    /// <summary>
    /// Separates the channels from the image and returns it as grayscale images.
    /// </summary>
    /// <returns>The channels from the image as grayscale images.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IReadOnlyCollection<IMagickImage<QuantumType>> Separate()
        => Separate(ImageMagick.Channels.Undefined);

    /// <summary>
    /// Separates the specified channels from the image and returns it as grayscale images.
    /// </summary>
    /// <param name="channels">The channel(s) to separates.</param>
    /// <returns>The channels from the image as grayscale images.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IReadOnlyCollection<IMagickImage<QuantumType>> Separate(Channels channels)
    {
        var images = _nativeInstance.Separate(channels);
        return CreateList(images);
    }

    /// <summary>
    /// Applies a special effect to the image, similar to the effect achieved in a photo darkroom
    /// by sepia toning.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void SepiaTone()
        => SepiaTone(new Percentage(80));

    /// <summary>
    /// Applies a special effect to the image, similar to the effect achieved in a photo darkroom
    /// by sepia toning.
    /// </summary>
    /// <param name="threshold">The tone threshold.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void SepiaTone(Percentage threshold)
        => _nativeInstance.SepiaTone(PercentageHelper.ToQuantum(threshold));

    /// <summary>
    /// Inserts the artifact with the specified name and value into the artifact tree of the image.
    /// </summary>
    /// <param name="name">The name of the artifact.</param>
    /// <param name="value">The value of the artifact.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void SetArtifact(string name, string value)
    {
        Throw.IfNullOrEmpty(nameof(name), name);
        Throw.IfNull(nameof(value), value);

        _nativeInstance.SetArtifact(name, value);
    }

    /// <summary>
    /// Inserts the artifact with the specified name and value into the artifact tree of the image.
    /// </summary>
    /// <param name="name">The name of the artifact.</param>
    /// <param name="flag">The value of the artifact.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void SetArtifact(string name, bool flag)
    {
        Throw.IfNullOrEmpty(nameof(name), name);

        _nativeInstance.SetArtifact(name, flag ? "true" : "false");
    }

    /// <summary>
    /// Lessen (or intensify) when adding noise to an image.
    /// </summary>
    /// <param name="attenuate">The attenuate value.</param>
    public void SetAttenuate(double attenuate)
        => SetArtifact("attenuate", attenuate.ToString(CultureInfo.InvariantCulture));

    /// <summary>
    /// Sets a named image attribute.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    /// <param name="value">The value of the attribute.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void SetAttribute(string name, string value)
    {
        Throw.IfNullOrEmpty(nameof(name), name);
        Throw.IfNull(nameof(value), value);

        _nativeInstance.SetAttribute(name, value);
    }

    /// <summary>
    /// Sets a named image attribute.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    /// <param name="flag">The value of the attribute.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void SetAttribute(string name, bool flag)
    {
        Throw.IfNullOrEmpty(nameof(name), name);

        _nativeInstance.SetAttribute(name, flag ? "true" : "false");
    }

    /// <summary>
    /// Set the bit depth (bits allocated to red/green/blue components).
    /// </summary>
    /// <param name="value">The depth.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void SetBitDepth(int value)
        => SetBitDepth(value, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Set the bit depth (bits allocated to red/green/blue components) of the specified channel.
    /// </summary>
    /// <param name="value">The depth.</param>
    /// <param name="channels">The channel to set the depth for.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void SetBitDepth(int value, Channels channels)
        => _nativeInstance.SetBitDepth(value, channels);

    /// <summary>
    /// Sets the default clipping path.
    /// </summary>
    /// <param name="value">The clipping path.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void SetClippingPath(string value)
        => SetClippingPath(value, "#1");

    /// <summary>
    /// Sets the clipping path with the specified name.
    /// </summary>
    /// <param name="value">The clipping path.</param>
    /// <param name="pathName">Name of clipping path resource. If name is preceded by #, use clipping path numbered by name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void SetClippingPath(string value, string pathName)
        => SetAttribute("8BIM:1999,2998:" + pathName, value);

    /// <summary>
    /// Sets the compression of the image. This method should only be used when the encoder uses the compression of the image. For
    /// most usecases Setting.Compression should be used instead.
    /// </summary>
    /// <param name="compression">The compression method.</param>
    public void SetCompression(CompressionMethod compression)
        => _nativeInstance.Compression = compression;

    /// <summary>
    /// Set color at colormap position index.
    /// </summary>
    /// <param name="index">The position index.</param>
    /// <param name="color">The color.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void SetColormapColor(int index, IMagickColor<QuantumType> color)
    {
        Throw.IfNull(nameof(color), color);

        _nativeInstance.SetColormapColor(index, color);
    }

    /// <summary>
    /// Set the specified profile of the image. If a profile with the same name already exists it will be overwritten.
    /// </summary>
    /// <param name="profile">The profile to set.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void SetProfile(IImageProfile profile)
    {
        Throw.IfNull(nameof(profile), profile);

        var datum = profile.ToByteArray();
        if (datum is null || datum.Length == 0)
            return;

        _nativeInstance.SetProfile(profile.Name, datum, datum.Length);
    }

    /// <summary>
    /// Set the specified profile of the image. If a profile with the same name already exists it will be overwritten.
    /// </summary>
    /// <param name="profile">The profile to set.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void SetProfile(IColorProfile profile)
        => SetProfile(profile, ColorTransformMode.Quantum);

    /// <summary>
    /// Set the specified profile of the image. If a profile with the same name already exists it will be overwritten.
    /// </summary>
    /// <param name="profile">The profile to set.</param>
    /// <param name="mode">The color transformation mode.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void SetProfile(IColorProfile profile, ColorTransformMode mode)
    {
        Throw.IfNull(nameof(profile), profile);

        var datum = profile.ToByteArray();
        if (datum is null || datum.Length == 0)
            return;

        using var temporaryDefines = new TemporaryDefines(this);
        if (mode == ColorTransformMode.Quantum)
            temporaryDefines.SetArtifact("profile:highres-transform", false);

        _nativeInstance.SetProfile(profile.Name, datum, datum.Length);
    }

    /// <summary>
    /// Sets the associated read mask of the image. The mask must be the same dimensions as the image and
    /// only contain the colors black and white.
    /// </summary>
    /// <param name="image">The image that contains the read mask.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void SetReadMask(IMagickImage image)
    {
        Throw.IfNull(nameof(image), image);

        _nativeInstance.SetReadMask(image);
    }

    /// <summary>
    /// Sets the associated write mask of the image. The mask must be the same dimensions as the image and
    /// only contains the colors black and white or have grayscale values that will cause blended updates of
    /// the image.
    /// </summary>
    /// <param name="image">The image that contains the write mask.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>z
    public void SetWriteMask(IMagickImage image)
    {
        Throw.IfNull(nameof(image), image);

        _nativeInstance.SetWriteMask(image);
    }

    /// <summary>
    /// Shade image using distant light source.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Shade()
        => Shade(30, 30);

    /// <summary>
    /// Shade image using distant light source.
    /// </summary>
    /// <param name="azimuth">The azimuth of the light source direction.</param>
    /// <param name="elevation">The elevation of the light source direction.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Shade(double azimuth, double elevation)
        => Shade(azimuth, elevation, ImageMagick.Channels.RGB);

    /// <summary>
    /// Shade image using distant light source.
    /// </summary>
    /// <param name="azimuth">The azimuth of the light source direction.</param>
    /// <param name="elevation">The elevation of the light source direction.</param>
    /// <param name="channels">The channel(s) that should be shaded.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Shade(double azimuth, double elevation, Channels channels)
        => _nativeInstance.Shade(azimuth, elevation, false, channels);

    /// <summary>
    /// Shade image using distant light source and make it grayscale.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void ShadeGrayscale()
        => ShadeGrayscale(30, 30);

    /// <summary>
    /// Shade image using distant light source and make it grayscale.
    /// </summary>
    /// <param name="azimuth">The azimuth of the light source direction.</param>
    /// <param name="elevation">The elevation of the light source direction.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void ShadeGrayscale(double azimuth, double elevation)
        => ShadeGrayscale(azimuth, elevation, ImageMagick.Channels.RGB);

    /// <summary>
    /// Shade image using distant light source and make it grayscale.
    /// </summary>
    /// <param name="azimuth">The azimuth of the light source direction.</param>
    /// <param name="elevation">The elevation of the light source direction.</param>
    /// <param name="channels">The channel(s) that should be shaded.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void ShadeGrayscale(double azimuth, double elevation, Channels channels)
        => _nativeInstance.Shade(azimuth, elevation, true, channels);

    /// <summary>
    /// Simulate an image shadow.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Shadow()
        => Shadow(5, 5, 0.5, new Percentage(80));

    /// <summary>
    /// Simulate an image shadow.
    /// </summary>
    /// <param name="color">The color of the shadow.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Shadow(IMagickColor<QuantumType> color)
        => Shadow(5, 5, 0.5, new Percentage(80), color);

    /// <summary>
    /// Simulate an image shadow.
    /// </summary>
    /// <param name="x">the shadow x-offset.</param>
    /// <param name="y">the shadow y-offset.</param>
    /// <param name="sigma">The standard deviation of the Gaussian, in pixels.</param>
    /// <param name="alpha">Transparency percentage.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Shadow(int x, int y, double sigma, Percentage alpha)
        => _nativeInstance.Shadow(x, y, sigma, alpha.ToDouble());

    /// <summary>
    /// Simulate an image shadow.
    /// </summary>
    /// <param name="x ">the shadow x-offset.</param>
    /// <param name="y">the shadow y-offset.</param>
    /// <param name="sigma">The standard deviation of the Gaussian, in pixels.</param>
    /// <param name="alpha">Transparency percentage.</param>
    /// <param name="color">The color of the shadow.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Shadow(int x, int y, double sigma, Percentage alpha, IMagickColor<QuantumType> color)
    {
        Throw.IfNull(nameof(color), color);

        var backgroundColor = BackgroundColor;
        BackgroundColor = color;
        _nativeInstance.Shadow(x, y, sigma, alpha.ToDouble());
        BackgroundColor = backgroundColor;
    }

    /// <summary>
    /// Sharpen pixels in image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Sharpen()
        => Sharpen(0.0, 1.0);

    /// <summary>
    /// Sharpen pixels in image.
    /// </summary>
    /// <param name="channels">The channel(s) that should be sharpened.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Sharpen(Channels channels)
        => Sharpen(0.0, 1.0, channels);

    /// <summary>
    /// Sharpen pixels in image.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Sharpen(double radius, double sigma)
        => Sharpen(radius, sigma, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Sharpen pixels in image.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <param name="channels">The channel(s) that should be sharpened.</param>
    public void Sharpen(double radius, double sigma, Channels channels)
        => _nativeInstance.Sharpen(radius, sigma, channels);

    /// <summary>
    /// Shave pixels from image edges.
    /// </summary>
    /// <param name="size">The size of to shave of the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Shave(int size)
        => Shave(size, size);

    /// <summary>
    /// Shave pixels from image edges.
    /// </summary>
    /// <param name="leftRight">The number of pixels to shave left and right.</param>
    /// <param name="topBottom">The number of pixels to shave top and bottom.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Shave(int leftRight, int topBottom)
        => _nativeInstance.Shave(leftRight, topBottom);

    /// <summary>
    /// Shear image (create parallelogram by sliding image by X or Y axis).
    /// </summary>
    /// <param name="xAngle">Specifies the number of x degrees to shear the image.</param>
    /// <param name="yAngle">Specifies the number of y degrees to shear the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Shear(double xAngle, double yAngle)
        => _nativeInstance.Shear(xAngle, yAngle);

    /// <summary>
    /// Adjust the image contrast with a non-linear sigmoidal contrast algorithm.
    /// </summary>
    /// <param name="contrast">The contrast.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void SigmoidalContrast(double contrast)
        => SigmoidalContrast(contrast, Quantum.Max * 0.5);

    /// <summary>
    /// Adjust the image contrast with a non-linear sigmoidal contrast algorithm.
    /// </summary>
    /// <param name="contrast">The contrast to use.</param>
    /// <param name="midpoint">The midpoint to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void SigmoidalContrast(double contrast, double midpoint)
         => SigmoidalContrast(contrast, midpoint, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Adjust the image contrast with a non-linear sigmoidal contrast algorithm.
    /// </summary>
    /// <param name="contrast">The contrast to use.</param>
    /// <param name="midpoint">The midpoint to use.</param>
    /// <param name="channels">The channel(s) that should be adjusted.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void SigmoidalContrast(double contrast, double midpoint, Channels channels)
         => _nativeInstance.SigmoidalContrast(true, contrast, midpoint, channels);

    /// <summary>
    /// Adjust the image contrast with a non-linear sigmoidal contrast algorithm.
    /// </summary>
    /// <param name="contrast">The contrast to use.</param>
    /// <param name="midpointPercentage">The midpoint to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void SigmoidalContrast(double contrast, Percentage midpointPercentage)
        => SigmoidalContrast(contrast, PercentageHelper.ToQuantum(midpointPercentage));

    /// <summary>
    /// Sparse color image, given a set of coordinates, interpolates the colors found at those
    /// coordinates, across the whole image, using various methods.
    /// </summary>
    /// <param name="method">The sparse color method to use.</param>
    /// <param name="args">The sparse color arguments.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void SparseColor(SparseColorMethod method, IEnumerable<ISparseColorArg<QuantumType>> args)
        => SparseColor(ImageMagick.Channels.Composite, method, args);

    /// <summary>
    /// Sparse color image, given a set of coordinates, interpolates the colors found at those
    /// coordinates, across the whole image, using various methods.
    /// </summary>
    /// <param name="method">The sparse color method to use.</param>
    /// <param name="args">The sparse color arguments.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void SparseColor(SparseColorMethod method, params ISparseColorArg<QuantumType>[] args)
        => SparseColor(ImageMagick.Channels.Composite, method, (IEnumerable<ISparseColorArg<QuantumType>>)args);

    /// <summary>
    /// Sparse color image, given a set of coordinates, interpolates the colors found at those
    /// coordinates, across the whole image, using various methods.
    /// </summary>
    /// <param name="channels">The channel(s) to use.</param>
    /// <param name="method">The sparse color method to use.</param>
    /// <param name="args">The sparse color arguments.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void SparseColor(Channels channels, SparseColorMethod method, IEnumerable<ISparseColorArg<QuantumType>> args)
    {
        Throw.IfNull(nameof(args), args);

        var hasRed = EnumHelper.HasFlag(channels, ImageMagick.Channels.Red);
        var hasGreen = EnumHelper.HasFlag(channels, ImageMagick.Channels.Green);
        var hasBlue = EnumHelper.HasFlag(channels, ImageMagick.Channels.Blue);
        var hasAlpha = HasAlpha && EnumHelper.HasFlag(channels, ImageMagick.Channels.Alpha);

        Throw.IfTrue(nameof(channels), !hasRed && !hasGreen && !hasBlue && !hasAlpha, "Invalid channels specified.");

        var arguments = new List<double>();

        foreach (var arg in args)
        {
            arguments.Add(arg.X);
            arguments.Add(arg.Y);
            if (hasRed)
                arguments.Add(Quantum.ScaleToDouble(arg.Color.R));
            if (hasGreen)
                arguments.Add(Quantum.ScaleToDouble(arg.Color.G));
            if (hasBlue)
                arguments.Add(Quantum.ScaleToDouble(arg.Color.B));
            if (hasAlpha)
                arguments.Add(Quantum.ScaleToDouble(arg.Color.A));
        }

        Throw.IfTrue(nameof(args), arguments.Count == 0, "Value cannot be empty");

        _nativeInstance.SparseColor(channels, method, arguments.ToArray(), arguments.Count);
    }

    /// <summary>
    /// Sparse color image, given a set of coordinates, interpolates the colors found at those
    /// coordinates, across the whole image, using various methods.
    /// </summary>
    /// <param name="channels">The channel(s) to use.</param>
    /// <param name="method">The sparse color method to use.</param>
    /// <param name="args">The sparse color arguments.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void SparseColor(Channels channels, SparseColorMethod method, params ISparseColorArg<QuantumType>[] args)
        => SparseColor(channels, method, (IEnumerable<ISparseColorArg<QuantumType>>)args);

    /// <summary>
    /// Simulates a pencil sketch.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Sketch()
        => Sketch(0.0, 1.0, 0.0);

    /// <summary>
    /// Simulates a pencil sketch. We convolve the image with a Gaussian operator of the given
    /// radius and standard deviation (sigma). For reasonable results, radius should be larger than sigma.
    /// Use a radius of 0 and sketch selects a suitable radius for you.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <param name="angle">Apply the effect along this angle.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Sketch(double radius, double sigma, double angle)
        => _nativeInstance.Sketch(radius, sigma, angle);

    /// <summary>
    /// Solarize image (similar to effect seen when exposing a photographic film to light during
    /// the development process).
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Solarize()
        => Solarize(new Percentage(50.0));

    /// <summary>
    /// Solarize image (similar to effect seen when exposing a photographic film to light during
    /// the development process).
    /// </summary>
    /// <param name="factor">The factor to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Solarize(double factor)
        => _nativeInstance.Solarize(factor);

    /// <summary>
    /// Solarize image (similar to effect seen when exposing a photographic film to light during
    /// the development process).
    /// </summary>
    /// <param name="factorPercentage">The factor to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Solarize(Percentage factorPercentage)
        => _nativeInstance.Solarize(PercentageHelper.ToQuantum(factorPercentage));

    /// <summary>
    /// Sort pixels within each scanline in ascending order of intensity.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void SortPixels()
        => _nativeInstance.SortPixels();

    /// <summary>
    /// Splice the background color into the image.
    /// </summary>
    /// <param name="geometry">The geometry to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Splice(IMagickGeometry geometry)
        => _nativeInstance.Splice(MagickRectangle.FromGeometry(geometry, this));

    /// <summary>
    /// Spread pixels randomly within image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Spread()
        => Spread(Interpolate, 3);

    /// <summary>
    /// Spread pixels randomly within image by specified amount.
    /// </summary>
    /// <param name="radius">Choose a random pixel in a neighborhood of this extent.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Spread(double radius)
        => Spread(PixelInterpolateMethod.Undefined, radius);

    /// <summary>
    /// Spread pixels randomly within image by specified amount.
    /// </summary>
    /// <param name="method">Pixel interpolate method.</param>
    /// <param name="radius">Choose a random pixel in a neighborhood of this extent.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Spread(PixelInterpolateMethod method, double radius)
        => _nativeInstance.Spread(method, radius);

    /// <summary>
    /// Makes each pixel the min / max / median / mode / etc. of the neighborhood of the specified width
    /// and height.
    /// </summary>
    /// <param name="type">The statistic type.</param>
    /// <param name="width">The width of the pixel neighborhood.</param>
    /// <param name="height">The height of the pixel neighborhood.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Statistic(StatisticType type, int width, int height)
        => _nativeInstance.Statistic(type, width, height);

    /// <summary>
    /// Returns the image statistics.
    /// </summary>
    /// <returns>The image statistics.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IStatistics Statistics()
        => Statistics(ImageMagick.Channels.All);

    /// <summary>
    /// Returns the image statistics.
    /// </summary>
    /// <returns>The image statistics.</returns>
    /// <param name="channels">The channel(s) that should be used.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IStatistics Statistics(Channels channels)
    {
        var list = _nativeInstance.Statistics(channels);
        try
        {
            return new Statistics(this, list, channels);
        }
        finally
        {
            ImageMagick.Statistics.DisposeList(list);
        }
    }

    /// <summary>
    /// Add a digital watermark to the image (based on second image).
    /// </summary>
    /// <param name="watermark">The image to use as a watermark.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Stegano(IMagickImage watermark)
    {
        Throw.IfNull(nameof(watermark), watermark);

        _nativeInstance.Stegano(watermark);
    }

    /// <summary>
    /// Create an image which appears in stereo when viewed with red-blue glasses (Red image on
    /// left, blue on right).
    /// </summary>
    /// <param name="rightImage">The image to use as the right part of the resulting image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Stereo(IMagickImage rightImage)
    {
        Throw.IfNull(nameof(rightImage), rightImage);

        _nativeInstance.Stereo(rightImage);
    }

    /// <summary>
    /// Strips an image of all profiles and comments.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Strip()
        => _nativeInstance.Strip();

    /// <summary>
    /// Swirl image (image pixels are rotated by degrees).
    /// </summary>
    /// <param name="degrees">The number of degrees.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Swirl(double degrees)
        => Swirl(Interpolate, degrees);

    /// <summary>
    /// Swirl image (image pixels are rotated by degrees).
    /// </summary>
    /// <param name="method">Pixel interpolate method.</param>
    /// <param name="degrees">The number of degrees.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Swirl(PixelInterpolateMethod method, double degrees)
        => _nativeInstance.Swirl(method, degrees);

    /// <summary>
    /// Search for the specified image at EVERY possible location in this image. This is slow!
    /// very very slow.. It returns a similarity image such that an exact match location is
    /// completely white and if none of the pixels match, black, otherwise some gray level in-between.
    /// </summary>
    /// <param name="image">The image to search for.</param>
    /// <returns>The result of the search action.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickSearchResult<QuantumType> SubImageSearch(IMagickImage<QuantumType> image)
        => SubImageSearch(image, ErrorMetric.RootMeanSquared, -1);

    /// <summary>
    /// Search for the specified image at EVERY possible location in this image. This is slow!
    /// very very slow.. It returns a similarity image such that an exact match location is
    /// completely white and if none of the pixels match, black, otherwise some gray level in-between.
    /// </summary>
    /// <param name="image">The image to search for.</param>
    /// <param name="metric">The metric to use.</param>
    /// <returns>The result of the search action.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickSearchResult<QuantumType> SubImageSearch(IMagickImage<QuantumType> image, ErrorMetric metric)
        => SubImageSearch(image, metric, -1);

    /// <summary>
    /// Search for the specified image at EVERY possible location in this image. This is slow!
    /// very very slow.. It returns a similarity image such that an exact match location is
    /// completely white and if none of the pixels match, black, otherwise some gray level in-between.
    /// </summary>
    /// <param name="image">The image to search for.</param>
    /// <param name="metric">The metric to use.</param>
    /// <param name="similarityThreshold">Minimum distortion for (sub)image match.</param>
    /// <returns>The result of the search action.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickSearchResult<QuantumType> SubImageSearch(IMagickImage<QuantumType> image, ErrorMetric metric, double similarityThreshold)
    {
        Throw.IfNull(nameof(image), image);

        var result = _nativeInstance.SubImageSearch(image, metric, similarityThreshold, out var rectangle, out var similarityMetric);

        var geometry = MagickGeometry.FromRectangle(rectangle);
        return new MagickSearchResult(Create(result, GetSettings(image)), geometry, similarityMetric);
    }

    /// <summary>
    /// Channel a texture on image background.
    /// </summary>
    /// <param name="image">The image to use as a texture on the image background.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Texture(IMagickImage image)
    {
        Throw.IfNull(nameof(image), image);

        _nativeInstance.Texture(image);
    }

    /// <summary>
    /// Threshold image.
    /// </summary>
    /// <param name="percentage">The threshold percentage.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Threshold(Percentage percentage)
        => Threshold(percentage, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Threshold image.
    /// </summary>
    /// <param name="percentage">The threshold percentage.</param>
    /// <param name="channels">The channel(s) that should be thresholded.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Threshold(Percentage percentage, Channels channels)
        => _nativeInstance.Threshold(PercentageHelper.ToQuantum(percentage), channels);

    /// <summary>
    /// Resize image to thumbnail size and remove all the image profiles except the icc/icm profile.
    /// <para />
    /// Resize will fit the image into the requested size. It does NOT fill, the requested box size.
    /// Use the <see cref="IMagickGeometry"/> overload for more control over the resulting size.
    /// </summary>
    /// <param name="width">The new width.</param>
    /// <param name="height">The new height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Thumbnail(int width, int height)
    {
        var geometry = new MagickGeometry(width, height);
        Thumbnail(geometry);
    }

    /// <summary>
    /// Resize image to thumbnail size and remove all the image profiles except the icc/icm profile.
    /// </summary>
    /// <param name="geometry">The geometry to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Thumbnail(IMagickGeometry geometry)
    {
        Throw.IfNull(nameof(geometry), geometry);

        _nativeInstance.Thumbnail(geometry.ToString());
    }

    /// <summary>
    /// Resize image to thumbnail size and remove all the image profiles except the icc/icm profile.
    /// </summary>
    /// <param name="percentage">The percentage.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Thumbnail(Percentage percentage)
        => Thumbnail(percentage, percentage);

    /// <summary>
    /// Resize image to thumbnail size and remove all the image profiles except the icc/icm profile.
    /// </summary>
    /// <param name="percentageWidth">The percentage of the width.</param>
    /// <param name="percentageHeight">The percentage of the height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Thumbnail(Percentage percentageWidth, Percentage percentageHeight)
    {
        Throw.IfNegative(nameof(percentageWidth), percentageWidth);
        Throw.IfNegative(nameof(percentageHeight), percentageHeight);

        var geometry = new MagickGeometry(percentageWidth, percentageHeight);
        Thumbnail(geometry);
    }

    /// <summary>
    /// Compose an image repeated across and down the image.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Tile(IMagickImage image, CompositeOperator compose)
        => Tile(image, compose, null);

    /// <summary>
    /// Compose an image repeated across and down the image.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Tile(IMagickImage image, CompositeOperator compose, string? args)
    {
        Throw.IfNull(nameof(image), image);

        for (var y = 0; y < Height; y += image.Height)
        {
            for (var x = 0; x < Width; x += image.Width)
            {
                Composite(image, x, y, compose, args);
            }
        }
    }

    /// <summary>
    /// Applies a color vector to each pixel in the image. The length of the vector is 0 for black
    /// and white and at its maximum for the midtones. The vector weighting function is
    /// f(x)=(1-(4.0*((x-0.5)*(x-0.5)))).
    /// </summary>
    /// <param name="opacity">An opacity value used for tinting.</param>
    /// <param name="color">A color value used for tinting.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Tint(IMagickGeometry opacity, IMagickColor<QuantumType> color)
    {
        Throw.IfNull(nameof(opacity), opacity);
        Throw.IfNull(nameof(color), color);

        _nativeInstance.Tint(opacity.ToString(), color);
    }

    /// <summary>
    /// Converts this instance to a base64 <see cref="string"/>.
    /// </summary>
    /// <returns>A base64 <see cref="string"/>.</returns>
    public string ToBase64()
    {
        var bytes = ToByteArray();
        return ToBase64(bytes);
    }

    /// <summary>
    /// Converts this instance to a base64 <see cref="string"/>.
    /// </summary>
    /// <param name="format">The format to use.</param>
    /// <returns>A base64 <see cref="string"/>.</returns>
    public string ToBase64(MagickFormat format)
    {
        var bytes = ToByteArray(format);
        return ToBase64(bytes);
    }

    /// <summary>
    /// Converts this instance to a base64 <see cref="string"/>.
    /// </summary>
    /// <param name="defines">The defines to set.</param>
    /// <returns>A base64 <see cref="string"/>.</returns>
    public string ToBase64(IWriteDefines defines)
    {
        var bytes = ToByteArray(defines);
        return ToBase64(bytes);
    }

    /// <summary>
    /// Converts this instance to a <see cref="byte"/> array.
    /// </summary>
    /// <returns>A <see cref="byte"/> array.</returns>
    public byte[] ToByteArray()
    {
        _settings.FileName = null;

        var wrapper = new ByteArrayWrapper();
        var writer = new ReadWriteStreamDelegate(wrapper.Write);
        var seeker = new SeekStreamDelegate(wrapper.Seek);
        var teller = new TellStreamDelegate(wrapper.Tell);
        var reader = new ReadWriteStreamDelegate(wrapper.Read);

        _nativeInstance.WriteStream(_settings, writer, seeker, teller, reader);

        return wrapper.GetBytes();
    }

    /// <summary>
    /// Converts this instance to a <see cref="byte"/> array.
    /// </summary>
    /// <param name="defines">The defines to set.</param>
    /// <returns>A <see cref="byte"/> array.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public byte[] ToByteArray(IWriteDefines defines)
    {
        _settings.SetDefines(defines);
        return ToByteArray(defines.Format);
    }

    /// <summary>
    /// Converts this instance to a <see cref="byte"/> array.
    /// </summary>
    /// <param name="format">The format to use.</param>
    /// <returns>A <see cref="byte"/> array.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public byte[] ToByteArray(MagickFormat format)
    {
        using var tempFormat = new TemporaryMagickFormat(this, format);
        return ToByteArray();
    }

    /// <summary>
    /// Returns a string that represents the current image.
    /// </summary>
    /// <returns>A string that represents the current image.</returns>
    public override string ToString()
        => string.Format(CultureInfo.InvariantCulture, "{0} {1}x{2} {3}-bit {4}", Format, Width, Height, Depth, ColorSpace);

    /// <summary>
    /// Transforms the image from the colorspace of the source profile to the target profile. This
    /// requires the image to have a color profile. Nothing will happen if the image has no color profile.
    /// </summary>
    /// <param name="target">The target color profile.</param>
    /// <returns>True when the colorspace was transformed otherwise false.</returns>
    public bool TransformColorSpace(IColorProfile target)
        => TransformColorSpace(target, ColorTransformMode.Quantum);

    /// <summary>
    /// Transforms the image from the colorspace of the source profile to the target profile. This
    /// requires the image to have a color profile. Nothing will happen if the image has no color profile.
    /// </summary>
    /// <param name="target">The target color profile.</param>
    /// <param name="mode">The color transformation mode.</param>
    /// <returns>True when the colorspace was transformed otherwise false.</returns>
    public bool TransformColorSpace(IColorProfile target, ColorTransformMode mode)
    {
        Throw.IfNull(nameof(target), target);

        if (!HasProfile(target.Name))
            return false;

        SetProfile(target, mode);

        return true;
    }

    /// <summary>
    /// Transforms the image from the colorspace of the source profile to the target profile. The
    /// source profile will only be used if the image does not contain a color profile. Nothing
    /// will happen if the source profile has a different colorspace then that of the image.
    /// </summary>
    /// <param name="source">The source color profile.</param>
    /// <param name="target">The target color profile.</param>
    /// <returns>True when the colorspace was transformed otherwise false.</returns>
    public bool TransformColorSpace(IColorProfile source, IColorProfile target)
        => TransformColorSpace(source, target, ColorTransformMode.Quantum);

    /// <summary>
    /// Transforms the image from the colorspace of the source profile to the target profile. The
    /// source profile will only be used if the image does not contain a color profile. Nothing
    /// will happen if the source profile has a different colorspace then that of the image.
    /// </summary>
    /// <param name="source">The source color profile.</param>
    /// <param name="target">The target color profile.</param>
    /// <param name="mode">The color transformation mode.</param>
    /// <returns>True when the colorspace was transformed otherwise false.</returns>
    public bool TransformColorSpace(IColorProfile source, IColorProfile target, ColorTransformMode mode)
    {
        Throw.IfNull(nameof(source), source);
        Throw.IfNull(nameof(target), target);

        if (source.ColorSpace != ColorSpace)
            return false;

        if (!HasProfile(target.Name))
            SetProfile(source);

        SetProfile(target, mode);

        return true;
    }

    /// <summary>
    /// Add alpha channel to image, setting pixels matching color to transparent.
    /// </summary>
    /// <param name="color">The color to make transparent.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Transparent(IMagickColor<QuantumType> color)
    {
        Throw.IfNull(nameof(color), color);

        _nativeInstance.Transparent(color, false);
    }

    /// <summary>
    /// Add alpha channel to image, setting pixels that lie in between the given two colors to
    /// transparent.
    /// </summary>
    /// <param name="colorLow">The low target color.</param>
    /// <param name="colorHigh">The high target color.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void TransparentChroma(IMagickColor<QuantumType> colorLow, IMagickColor<QuantumType> colorHigh)
    {
        Throw.IfNull(nameof(colorLow), colorLow);
        Throw.IfNull(nameof(colorHigh), colorHigh);

        _nativeInstance.TransparentChroma(colorLow, colorHigh, false);
    }

    /// <summary>
    /// Creates a horizontal mirror image by reflecting the pixels around the central y-axis while
    /// rotating them by 90 degrees.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Transpose()
        => _nativeInstance.Transpose();

    /// <summary>
    /// Creates a vertical mirror image by reflecting the pixels around the central x-axis while
    /// rotating them by 270 degrees.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Transverse()
        => _nativeInstance.Transverse();

    /// <summary>
    /// Trim edges that are the background color from the image. The property <see cref="BoundingBox"/> can be used to the
    /// coordinates of the area that will be extracted.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Trim()
        => _nativeInstance.Trim();

    /// <summary>
    /// Trim the specified edges that are the background color from the image.
    /// </summary>
    /// <param name="edges">The edges that need to be trimmed.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Trim(params Gravity[] edges)
    {
        static IEnumerable<string> GravityToEdge(Gravity[] edges)
        {
            foreach (var edge in edges)
            {
                switch (edge)
                {
                    case Gravity.North:
                        yield return "north";
                        break;
                    case Gravity.Northeast:
                        yield return "north";
                        yield return "east";
                        break;
                    case Gravity.Northwest:
                        yield return "north";
                        yield return "west";
                        break;
                    case Gravity.East:
                        yield return "east";
                        break;
                    case Gravity.West:
                        yield return "west";
                        break;
                    case Gravity.South:
                        yield return "south";
                        break;
                    case Gravity.Southeast:
                        yield return "south";
                        yield return "east";
                        break;
                    case Gravity.Southwest:
                        yield return "south";
                        yield return "west";
                        break;
                }
            }
        }

        var artifact = new List<string>();
        foreach (var edge in GravityToEdge(edges))
        {
            if (!artifact.Contains(edge))
            {
                artifact.Add(edge);
            }
        }

        using var temporaryDefines = new TemporaryDefines(this);
        temporaryDefines.SetArtifact("trim:edges", string.Join(",", artifact.ToArray()));
        Trim();
    }

    /// <summary>
    /// Trim edges that are the background color from the image. The property <see cref="BoundingBox"/> can be used to the
    /// coordinates of the area that will be extracted.
    /// </summary>
    /// <param name="percentBackground">The percentage of background pixels permitted in the outer rows and columns.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Trim(Percentage percentBackground)
    {
        using var temporaryDefines = new TemporaryDefines(this);
        temporaryDefines.SetArtifact("trim:percent-background", percentBackground.ToInt32().ToString(CultureInfo.InvariantCulture));
        Trim();
    }

    /// <summary>
    /// Returns the unique colors of an image.
    /// </summary>
    /// <returns>The unique colors of an image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> UniqueColors()
        => Create(_nativeInstance.UniqueColors(), _settings);

    /// <summary>
    /// Replace image with a sharpened version of the original image using the unsharp mask algorithm.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void UnsharpMask(double radius, double sigma)
        => UnsharpMask(radius, sigma, 1.0, 0.05);

    /// <summary>
    /// Replace image with a sharpened version of the original image using the unsharp mask algorithm.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <param name="channels">The channel(s) that should be sharpened.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void UnsharpMask(double radius, double sigma, Channels channels)
        => UnsharpMask(radius, sigma, 1.0, 0.05, channels);

    /// <summary>
    /// Replace image with a sharpened version of the original image using the unsharp mask algorithm.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <param name="amount">The percentage of the difference between the original and the blur image
    /// that is added back into the original.</param>
    /// <param name="threshold">The threshold in pixels needed to apply the diffence amount.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void UnsharpMask(double radius, double sigma, double amount, double threshold)
        => UnsharpMask(radius, sigma, amount, threshold, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Replace image with a sharpened version of the original image using the unsharp mask algorithm.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <param name="amount">The percentage of the difference between the original and the blur image
    /// that is added back into the original.</param>
    /// <param name="threshold">The threshold in pixels needed to apply the diffence amount.</param>
    /// <param name="channels">The channel(s) that should be sharpened.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void UnsharpMask(double radius, double sigma, double amount, double threshold, Channels channels)
        => _nativeInstance.UnsharpMask(radius, sigma, amount, threshold, channels);

    /// <summary>
    /// Softens the edges of the image in vignette style.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Vignette()
        => Vignette(0.0, 1.0, 0, 0);

    /// <summary>
    /// Softens the edges of the image in vignette style.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <param name="x">The x ellipse offset.</param>
    /// <param name="y">the y ellipse offset.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Vignette(double radius, double sigma, int x, int y)
        => _nativeInstance.Vignette(radius, sigma, x, y);

    /// <summary>
    /// Map image pixels to a sine wave.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Wave()
        => Wave(Interpolate, 25.0, 150.0);

    /// <summary>
    /// Map image pixels to a sine wave.
    /// </summary>
    /// <param name="method">Pixel interpolate method.</param>
    /// <param name="amplitude">The amplitude.</param>
    /// <param name="length">The length of the wave.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Wave(PixelInterpolateMethod method, double amplitude, double length)
        => _nativeInstance.Wave(method, amplitude, length);

    /// <summary>
    /// Removes noise from the image using a wavelet transform.
    /// </summary>
    /// <param name="threshold">The threshold for smoothing.</param>
    public void WaveletDenoise(QuantumType threshold)
        => WaveletDenoise(threshold, 0.0);

    /// <summary>
    /// Removes noise from the image using a wavelet transform.
    /// </summary>
    /// <param name="threshold">The threshold for smoothing.</param>
    /// <param name="softness">Attenuate the smoothing threshold.</param>
    public void WaveletDenoise(QuantumType threshold, double softness)
        => _nativeInstance.WaveletDenoise(threshold, softness);

    /// <summary>
    /// Removes noise from the image using a wavelet transform.
    /// </summary>
    /// <param name="thresholdPercentage">The threshold for smoothing.</param>
    public void WaveletDenoise(Percentage thresholdPercentage)
        => WaveletDenoise(PercentageHelper.ToQuantumType(thresholdPercentage));

    /// <summary>
    /// Removes noise from the image using a wavelet transform.
    /// </summary>
    /// <param name="thresholdPercentage">The threshold for smoothing.</param>
    /// <param name="softness">Attenuate the smoothing threshold.</param>
    public void WaveletDenoise(Percentage thresholdPercentage, double softness)
        => WaveletDenoise(PercentageHelper.ToQuantumType(thresholdPercentage), softness);

    /// <summary>
    /// Apply a white balancing to an image according to a grayworld assumption in the LAB colorspace.
    /// </summary>
    public void WhiteBalance()
        => _nativeInstance.WhiteBalance();

    /// <summary>
    /// Apply a white balancing to an image according to a grayworld assumption in the LAB colorspace.
    /// </summary>
    /// <param name="vibrance">The vibrance.</param>
    public void WhiteBalance(Percentage vibrance)
    {
        using var temporaryDefines = new TemporaryDefines(this);
        temporaryDefines.SetArtifact("white-balance:vibrance", vibrance.ToString());
        WhiteBalance();
    }

    /// <summary>
    /// Forces all pixels above the threshold into white while leaving all pixels at or below
    /// the threshold unchanged.
    /// </summary>
    /// <param name="threshold">The threshold to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void WhiteThreshold(Percentage threshold)
        => WhiteThreshold(threshold, ImageMagick.Channels.Undefined);

    /// <summary>
    /// Forces all pixels above the threshold into white while leaving all pixels at or below
    /// the threshold unchanged.
    /// </summary>
    /// <param name="threshold">The threshold to use.</param>
    /// <param name="channels">The channel(s) to make black.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void WhiteThreshold(Percentage threshold, Channels channels)
    {
        Throw.IfNegative(nameof(threshold), threshold);

        _nativeInstance.WhiteThreshold(threshold.ToString(), channels);
    }

    /// <summary>
    /// Writes the image to the specified file.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Write(FileInfo file)
    {
        Throw.IfNull(nameof(file), file);

        Write(file.FullName);
        file.Refresh();
    }

    /// <summary>
    /// Writes the image to the specified file.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <param name="defines">The defines to set.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Write(FileInfo file, IWriteDefines defines)
    {
        _settings.SetDefines(defines);
        Write(file, defines.Format);
    }

    /// <summary>
    /// Writes the image to the specified file.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Write(FileInfo file, MagickFormat format)
    {
        using var tempFormat = new TemporaryMagickFormat(this, format);
        Write(file);
    }

    /// <summary>
    /// Writes the image to the specified stream.
    /// </summary>
    /// <param name="stream">The stream to write the image data to.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Write(Stream stream)
    {
        Throw.IfNull(nameof(stream), stream);

        _settings.FileName = null;

        using var wrapper = StreamWrapper.CreateForWriting(stream);
        var writer = new ReadWriteStreamDelegate(wrapper.Write);
        ReadWriteStreamDelegate? reader = null;
        SeekStreamDelegate? seeker = null;
        TellStreamDelegate? teller = null;

        if (stream.CanSeek)
        {
            seeker = new SeekStreamDelegate(wrapper.Seek);
            teller = new TellStreamDelegate(wrapper.Tell);
        }

        if (stream.CanRead)
            reader = new ReadWriteStreamDelegate(wrapper.Read);

        _nativeInstance.WriteStream(_settings, writer, seeker, teller, reader);
    }

    /// <summary>
    /// Writes the image to the specified stream.
    /// </summary>
    /// <param name="stream">The stream to write the image data to.</param>
    /// <param name="defines">The defines to set.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Write(Stream stream, IWriteDefines defines)
    {
        _settings.SetDefines(defines);
        Write(stream, defines.Format);
    }

    /// <summary>
    /// Writes the image to the specified stream.
    /// </summary>
    /// <param name="stream">The stream to write the image data to.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Write(Stream stream, MagickFormat format)
    {
        using var tempFormat = new TemporaryMagickFormat(this, format);
        Write(stream);
    }

    /// <summary>
    /// Writes the image to the specified file name.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Write(string fileName)
    {
        var filePath = FileHelper.CheckForBaseDirectory(fileName);

        Throw.IfNullOrEmpty(nameof(fileName), filePath);

        _nativeInstance.FileName = filePath;
        _nativeInstance.WriteFile(_settings);
    }

    /// <summary>
    /// Writes the image to the specified file name.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="defines">The defines to set.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Write(string fileName, IWriteDefines defines)
    {
        _settings.SetDefines(defines);
        Write(fileName, defines.Format);
    }

    /// <summary>
    /// Writes the image to the specified file name.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Write(string fileName, MagickFormat format)
    {
        using var tempFormat = new TemporaryMagickFormat(this, format);
        Write(fileName);
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

        var format = EnumHelper.ParseMagickFormatFromExtension(file);
        var bytes = format != MagickFormat.Unknown ? ToByteArray(format) : ToByteArray();
        return FileHelper.WriteAllBytesAsync(file.FullName, bytes, cancellationToken);
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
        return FileHelper.WriteAllBytesAsync(file.FullName, bytes, cancellationToken);
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
        using var tempFormat = new TemporaryMagickFormat(this, format);
        await WriteAsync(stream, cancellationToken).ConfigureAwait(false);
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
        var filePath = FileHelper.CheckForBaseDirectory(fileName);
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
        var filePath = FileHelper.CheckForBaseDirectory(fileName);
        Throw.IfNullOrEmpty(nameof(fileName), filePath);

        var bytes = ToByteArray(format);
        return FileHelper.WriteAllBytesAsync(filePath, bytes, cancellationToken);
    }

    internal static IMagickImage<QuantumType>? Clone(IMagickImage<QuantumType>? image)
        => image?.Clone();

    internal static MagickImage? Create(IntPtr image)
    {
        if (image == IntPtr.Zero)
            return null;

        var instance = new NativeMagickImage(image);
        return new MagickImage(instance, new MagickSettings());
    }

    internal static IMagickImage<QuantumType> Create(IntPtr image, MagickSettings settings)
    {
        if (image == IntPtr.Zero)
            throw new InvalidOperationException();

        var instance = new NativeMagickImage(image);
        return new MagickImage(instance, settings.Clone());
    }

    internal static IMagickErrorInfo CreateErrorInfo(MagickImage image)
        => new MagickErrorInfo(image._nativeInstance.MeanErrorPerPixel, image._nativeInstance.NormalizedMeanError, image._nativeInstance.NormalizedMaximumError);

    internal static IReadOnlyCollection<IMagickImage<QuantumType>> CreateList(IntPtr images, MagickSettings settings)
    {
        var result = new Collection<IMagickImage<QuantumType>>();

        var image = images;
        while (image != IntPtr.Zero)
        {
            var next = NativeMagickImage.GetNext(image);

            var instance = new NativeMagickImage(image);
            instance.SetNext(IntPtr.Zero);

            result.Add(new MagickImage(instance, settings.Clone()));
            image = next;
        }

        return result;
    }

    internal static IntPtr GetInstance(IMagickImage? image)
    {
        if (image is null)
            return IntPtr.Zero;

        if (image is MagickImage magickImage)
            return magickImage._nativeInstance.Instance;

        throw new NotSupportedException();
    }

    internal static MagickSettings GetSettings(IMagickImage<QuantumType> image)
    {
        if (image?.Settings is MagickSettings settings)
            return settings;

        throw new NotSupportedException();
    }

    internal int ChannelOffset(PixelChannel pixelChannel)
    {
        if (!_nativeInstance.HasChannel(pixelChannel))
            return -1;

        return _nativeInstance.ChannelOffset(pixelChannel);
    }

    internal void SetNext(IMagickImage? image)
        => _nativeInstance.SetNext(GetInstance(image));

    private static int GetExpectedByteLength(IPixelReadSettings<QuantumType> settings)
    {
        var length = GetExpectedLength(settings);
        return ToByteCount(settings.StorageType, length);
    }

    private static int GetExpectedByteLength(IPixelImportSettings settings)
    {
        var length = GetExpectedLength(settings);
        return ToByteCount(settings.StorageType, length);
    }

    private static int GetExpectedLength(IPixelImportSettings settings)
        => settings.Width * settings.Height * settings.Mapping!.Length;

    private static int GetExpectedLength(IPixelReadSettings<QuantumType> settings)
    {
        Throw.IfNull(nameof(settings), settings.ReadSettings.Width, "ReadSettings.Width should be defined");
        Throw.IfNull(nameof(settings), settings.ReadSettings.Height, "ReadSettings.Height should be defined.");

        return settings.ReadSettings.Width.Value * settings.ReadSettings.Height.Value * settings.Mapping!.Length;
    }

    private static string ToBase64(byte[] bytes)
    {
        if (bytes is null)
            return string.Empty;

        return Convert.ToBase64String(bytes);
    }

    private static int ToByteCount(StorageType storageType, int length)
        => storageType switch
        {
            StorageType.Char => length,
            StorageType.Double => length * sizeof(double),
            StorageType.Float => length * sizeof(float),
            StorageType.Int32 => length * sizeof(int),
            StorageType.Int64 => length * sizeof(long),
            StorageType.Quantum => length * sizeof(QuantumType),
            StorageType.Short => length * sizeof(ushort),
            _ => throw new NotSupportedException(),
        };

    private PointD CalculateContrastStretch(Percentage blackPoint, Percentage whitePoint)
    {
        var x = blackPoint.ToDouble();
        var y = whitePoint.ToDouble();

        var pixels = Width * Height;
        x *= pixels / 100.0;
        y *= pixels / 100.0;
        y = pixels - y;

        return new PointD(x, y);
    }

    private IReadOnlyCollection<IMagickImage<QuantumType>> CreateList(IntPtr images)
        => CreateList(images, _settings.Clone());

    private MagickReadSettings CreateReadSettings(IMagickReadSettings<QuantumType>? readSettings)
    {
        if (readSettings is not null && readSettings.FrameCount.HasValue)
            Throw.IfFalse(nameof(readSettings), readSettings.FrameCount.Value == 1, "The frame count can only be set to 1 when a single image is being read.");

        MagickReadSettings newReadSettings;
        if (readSettings is null)
            newReadSettings = new MagickReadSettings(_settings);
        else
            newReadSettings = new MagickReadSettings(readSettings);

        newReadSettings.ForceSingleFrame();

        return newReadSettings;
    }

    private void Dispose(bool disposing)
    {
        DisposeInstance();

        if (disposing)
        {
            if (_settings is not null)
                _settings.Artifact -= OnArtifact;
        }
    }

    private void DisposeInstance()
    {
        if (_nativeInstance is null)
            return;

        _nativeInstance.Warning -= OnWarning;
        _nativeInstance.Dispose();
    }

    private void FloodFill(QuantumType alpha, int x, int y, bool invert)
    {
        IMagickColor<QuantumType>? target;
        using var pixels = GetPixelsUnsafe();
        target = pixels.GetPixel(x, y).ToColor();

        if (target is not null)
            target.A = alpha;

        _nativeInstance.FloodFill(_settings.Drawing, x, y, target, invert);
    }

    private void FloodFill(IMagickColor<QuantumType> color, int x, int y, bool invert)
    {
        Throw.IfNull(nameof(color), color);

        IMagickColor<QuantumType>? target;
        using var pixels = GetPixelsUnsafe();
        target = pixels.GetPixel(x, y).ToColor();

        if (target is not null)
            FloodFill(color, x, y, target, invert);
    }

    private void FloodFill(IMagickColor<QuantumType> color, int x, int y, IMagickColor<QuantumType> target, bool invert)
    {
        Throw.IfNull(nameof(color), color);
        Throw.IfNull(nameof(target), target);

        var settings = _settings.Drawing;

        using var fillPattern = settings.FillPattern;
        var fillColor = settings.FillColor;

        settings.FillColor = color;
        settings.FillPattern = null;
        _nativeInstance.FloodFill(settings, x, y, target, invert);

        settings.FillColor = fillColor;
        settings.FillPattern = fillPattern;
    }

    private void FloodFill(IMagickImage<QuantumType> image, int x, int y, bool invert)
    {
        Throw.IfNull(nameof(image), image);

        IMagickColor<QuantumType>? target;
        using var pixels = GetPixelsUnsafe();
        target = pixels.GetPixel(x, y).ToColor();

        if (target is not null)
            FloodFill(image, x, y, target, invert);
    }

    private void FloodFill(IMagickImage<QuantumType> image, int x, int y, IMagickColor<QuantumType> target, bool invert)
    {
        Throw.IfNull(nameof(image), image);
        Throw.IfNull(nameof(target), target);

        var settings = _settings.Drawing;

        using var fillPattern = settings.FillPattern;
        var fillColor = settings.FillColor;

        settings.FillColor = null;
        settings.FillPattern = image;
        _nativeInstance.FloodFill(settings, x, y, target, invert);

        settings.FillColor = fillColor;
        settings.FillPattern = fillPattern;
    }

    private void LevelColors(IMagickColor<QuantumType> blackColor, IMagickColor<QuantumType> whiteColor, bool invert)
        => LevelColors(blackColor, whiteColor, ImageMagick.Channels.RGB, invert);

    private void LevelColors(IMagickColor<QuantumType> blackColor, IMagickColor<QuantumType> whiteColor, Channels channels, bool invert)
    {
        Throw.IfNull(nameof(blackColor), blackColor);
        Throw.IfNull(nameof(whiteColor), whiteColor);

        _nativeInstance.LevelColors(blackColor, whiteColor, channels, invert);
    }

    private void Opaque(IMagickColor<QuantumType> target, IMagickColor<QuantumType> fill, bool invert)
    {
        Throw.IfNull(nameof(target), target);
        Throw.IfNull(nameof(fill), fill);

        _nativeInstance.Opaque(target, fill, invert);
    }

    private ColorProfile? GetColorProfile(string name)
    {
        var info = _nativeInstance.GetProfile(name);
        if (info is null || info.Datum is null)
            return null;

        return new ColorProfile(name, info.Datum);
    }

    private void OnArtifact(object sender, ArtifactEventArgs arguments)
    {
        if (arguments.Value is null)
            RemoveArtifact(arguments.Key);
        else
            SetArtifact(arguments.Key, arguments.Value);
    }

    private bool OnProgress(IntPtr origin, long offset, ulong extent, IntPtr userData)
    {
        if (_progress is null)
            return true;

        var managedOrigin = UTF8Marshaler.NativeToManaged(origin);
        var eventArgs = new ProgressEventArgs(managedOrigin, (int)offset, (int)extent);
        _progress(this, eventArgs);
        return !eventArgs.Cancel;
    }

    private void OnWarning(object sender, WarningEventArgs arguments)
        => _warning?.Invoke(this, arguments);

    private void Read(byte[] data, int offset, int length, IMagickReadSettings<QuantumType>? readSettings, bool ping, string? fileName = null)
    {
        var newReadSettings = CreateReadSettings(readSettings);
        SetSettings(newReadSettings);

        _settings.Ping = ping;
        _settings.FileName = fileName;

        _nativeInstance.ReadBlob(Settings, data, offset, length);

        ResetSettings();
    }

    private void Read(Stream stream, IMagickReadSettings<QuantumType>? readSettings, bool ping)
    {
        Throw.IfNullOrEmpty(nameof(stream), stream);

        var bytes = Bytes.FromStreamBuffer(stream);
        if (bytes is not null)
        {
            Read(bytes.GetData(), 0, bytes.Length, readSettings, ping);
            return;
        }

        var newReadSettings = CreateReadSettings(readSettings);
        SetSettings(newReadSettings);

        _settings.Ping = ping;
        _settings.FileName = null;

        using var wrapper = StreamWrapper.CreateForReading(stream);
        var reader = new ReadWriteStreamDelegate(wrapper.Read);
        SeekStreamDelegate? seeker = null;
        TellStreamDelegate? teller = null;

        if (stream.CanSeek)
        {
            seeker = new SeekStreamDelegate(wrapper.Seek);
            teller = new TellStreamDelegate(wrapper.Tell);
        }

        _nativeInstance.ReadStream(Settings, reader, seeker, teller);

        ResetSettings();
    }

    private void Read(string fileName, IMagickReadSettings<QuantumType>? readSettings, bool ping)
    {
        var filePath = FileHelper.CheckForBaseDirectory(fileName);
        Throw.IfNullOrEmpty(nameof(fileName), filePath);

        var newReadSettings = CreateReadSettings(readSettings);
        SetSettings(newReadSettings);

        _settings.FileName = filePath;
        _settings.Ping = ping;

        _nativeInstance.ReadFile(Settings);
        ResetSettings();
    }

    private void ResetSettings()
        => _settings.Format = MagickFormat.Unknown;

    [MemberNotNull(nameof(_nativeInstance))]
    private void SetInstance(NativeMagickImage instance)
    {
        DisposeInstance();

        _nativeInstance = instance;
        _nativeInstance.Warning += OnWarning;
    }

    [MemberNotNull(nameof(_settings))]
    private void SetSettings(MagickSettings settings)
    {
        if (_settings is not null)
            _settings.Artifact -= OnArtifact;

        _settings = settings;
        _settings.Artifact += OnArtifact;
    }

    private unsafe sealed partial class NativeMagickImage : NativeInstance
    {
        public void ImportPixels(int x, int y, int width, int height, string map, StorageType storageType, byte[] data, int offsetInBytes)
        {
            fixed (byte* dataFixed = data)
            {
                ImportPixels(x, y, width, height, map, storageType, dataFixed, offsetInBytes);
            }
        }

#if !Q8
        public void ImportPixels(int x, int y, int width, int height, string map, StorageType storageType, QuantumType[] data, int offsetInBytes)
        {
            fixed (QuantumType* dataFixed = data)
            {
                ImportPixels(x, y, width, height, map, storageType, dataFixed, offsetInBytes);
            }
        }
#endif

        public void ReadPixels(int width, int height, string map, StorageType storageType, byte[] data, int offsetInBytes)
        {
            fixed (byte* dataFixed = data)
            {
                ReadPixels(width, height, map, storageType, dataFixed, offsetInBytes);
            }
        }

#if !Q8
        public void ReadPixels(int width, int height, string map, StorageType storageType, QuantumType[] data, int offsetInBytes)
        {
            fixed (QuantumType* dataFixed = data)
            {
                ReadPixels(width, height, map, storageType, dataFixed, offsetInBytes);
            }
        }
#endif
    }
}
