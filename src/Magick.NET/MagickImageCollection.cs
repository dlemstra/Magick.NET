// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections;
using System.Collections.Generic;
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
/// Represents the collection of images.
/// </summary>
public sealed partial class MagickImageCollection : IMagickImageCollection<QuantumType>
{
    private readonly List<IMagickImage<QuantumType>> _images;
    private readonly NativeMagickImageCollection _nativeInstance;

    private EventHandler<WarningEventArgs>? _warning;

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImageCollection"/> class.
    /// </summary>
    public MagickImageCollection()
    {
        _images = new List<IMagickImage<QuantumType>>();
        _nativeInstance = new NativeMagickImageCollection();
        _nativeInstance.Warning += OnWarning;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImageCollection"/> class.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImageCollection(byte[] data)
        : this()
        => Read(data);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImageCollection"/> class.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImageCollection(byte[] data, uint offset, uint count)
        : this()
        => Read(data, offset, count);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImageCollection"/> class.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImageCollection(byte[] data, uint offset, uint count, MagickFormat format)
        : this()
        => Read(data, offset, count, format);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImageCollection"/> class.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImageCollection(byte[] data, uint offset, uint count, IMagickReadSettings<QuantumType> readSettings)
        : this()
        => Read(data, offset, count, readSettings);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImageCollection"/> class.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImageCollection(byte[] data, MagickFormat format)
        : this()
        => Read(data, format);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImageCollection"/> class.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImageCollection(byte[] data, IMagickReadSettings<QuantumType> readSettings)
        : this()
        => Read(data, readSettings);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImageCollection"/> class.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImageCollection(FileInfo file)
        : this()
        => Read(file);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImageCollection"/> class.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImageCollection(FileInfo file, MagickFormat format)
        : this()
        => Read(file, format);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImageCollection"/> class.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImageCollection(FileInfo file, IMagickReadSettings<QuantumType> readSettings)
        : this()
        => Read(file, readSettings);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImageCollection"/> class.
    /// </summary>
    /// <param name="images">The images to add to the collection.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImageCollection(IEnumerable<IMagickImage<QuantumType>> images)
        : this()
        => AddRange(images);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImageCollection"/> class.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImageCollection(Stream stream)
        : this()
        => Read(stream);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImageCollection"/> class.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImageCollection(Stream stream, MagickFormat format)
        : this()
        => Read(stream, format);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImageCollection"/> class.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImageCollection(Stream stream, IMagickReadSettings<QuantumType> readSettings)
        : this()
        => Read(stream, readSettings);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImageCollection"/> class.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImageCollection(string fileName)
        : this()
        => Read(fileName);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImageCollection"/> class.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImageCollection(string fileName, MagickFormat format)
        : this()
        => Read(fileName, format);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickImageCollection"/> class.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public MagickImageCollection(string fileName, IMagickReadSettings<QuantumType> readSettings)
        : this()
        => Read(fileName, readSettings);

    /// <summary>
    /// Finalizes an instance of the <see cref="MagickImageCollection"/> class.
    /// </summary>
    ~MagickImageCollection()
        => Dispose(false);

    /// <summary>
    /// Event that will we raised when a warning is raised by ImageMagick.
    /// </summary>
    public event EventHandler<WarningEventArgs> Warning
    {
        add => _warning += value;
        remove => _warning -= value;
    }

    /// <summary>
    /// Gets the number of images in the collection.
    /// </summary>
    public int Count
        => _images.Count;

    /// <summary>
    /// Gets a value indicating whether the collection is read-only.
    /// </summary>
    public bool IsReadOnly
        => false;

    /// <summary>
    /// Gets or sets the image at the specified index.
    /// </summary>
    /// <param name="index">The index of the image to get.</param>
    public IMagickImage<QuantumType> this[int index]
    {
        get => _images[index];
        set
        {
            if (value is null)
                throw new InvalidOperationException("Not allowed to set null value.");

            if (!ReferenceEquals(value, _images[index]))
                CheckDuplicate(value);

            _images[index] = value;
        }
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>An enumerator that iterates through the collection.</returns>
    IEnumerator IEnumerable.GetEnumerator()
        => _images.GetEnumerator();

    /// <summary>
    /// Adds an image to the collection.
    /// </summary>
    /// <param name="item">The image to add.</param>
    public void Add(IMagickImage<QuantumType> item)
    {
        Throw.IfNull(nameof(item), item);

        CheckDuplicate(item);

        _images.Add(item);
    }

    /// <summary>
    /// Adds an image with the specified file name to the collection.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Add(string fileName)
        => _images.Add(new MagickImage(fileName));

    /// <summary>
    /// Adds the image(s) from the specified byte array to the collection.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AddRange(byte[] data)
        => AddRange(data, null);

    /// <summary>
    /// Adds the image(s) from the specified byte array to the collection.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AddRange(byte[] data, IMagickReadSettings<QuantumType>? readSettings)
    {
        Throw.IfNullOrEmpty(nameof(data), data);

        AddImages(data, 0, (uint)data.Length, readSettings, false);
    }

    /// <summary>
    /// Adds the specified images to this collection.
    /// </summary>
    /// <param name="images">The images to add to the collection.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AddRange(IEnumerable<IMagickImage<QuantumType>> images)
    {
        Throw.IfNull(nameof(images), images);
        Throw.IfTrue(nameof(images), images is MagickImageCollection, "Not allowed to add collection.");

        foreach (var image in images)
        {
            Add(image);
        }
    }

    /// <summary>
    /// Adds the image(s) from the specified file name to the collection.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AddRange(string fileName)
        => AddRange(fileName, null);

    /// <summary>
    /// Adds the image(s) from the specified file name to the collection.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AddRange(string fileName, IMagickReadSettings<QuantumType>? readSettings)
        => AddImages(fileName, readSettings, false);

    /// <summary>
    /// Adds the image(s) from the specified stream to the collection.
    /// </summary>
    /// <param name="stream">The stream to read the images from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AddRange(Stream stream)
        => AddRange(stream, null);

    /// <summary>
    /// Adds the image(s) from the specified stream to the collection.
    /// </summary>
    /// <param name="stream">The stream to read the images from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void AddRange(Stream stream, IMagickReadSettings<QuantumType>? readSettings)
        => AddImages(stream, readSettings, false);

    /// <summary>
    /// Creates a single image, by appending all the images in the collection horizontally (+append).
    /// </summary>
    /// <returns>A single image, by appending all the images in the collection horizontally (+append).</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> AppendHorizontally()
    {
        using var imageAttacher = new TemporaryImageAttacher(_images);
        var image = _nativeInstance.Append(_images[0], false);
        return MagickImage.Create(image, GetSettings());
    }

    /// <summary>
    /// Creates a single image, by appending all the images in the collection vertically (-append).
    /// </summary>
    /// <returns>A single image, by appending all the images in the collection vertically (-append).</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> AppendVertically()
    {
        using var imageAttacher = new TemporaryImageAttacher(_images);
        var image = _nativeInstance.Append(_images[0], true);
        return MagickImage.Create(image, GetSettings());
    }

    /// <summary>
    /// Removes all images from the collection.
    /// </summary>
    public void Clear()
    {
        foreach (var image in _images)
        {
            if (image is not null)
                image.Dispose();
        }

        _images.Clear();
    }

    /// <summary>
    /// Creates a clone of the current image collection.
    /// </summary>
    /// <returns>A clone of the current image collection.</returns>
    public IMagickImageCollection<QuantumType> Clone()
    {
        var result = new MagickImageCollection();
        foreach (var image in this)
            result.Add(image.Clone());

        return result;
    }

    /// <summary>
    /// Merge a sequence of images. This is useful for GIF animation sequences that have page
    /// offsets and disposal methods.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Coalesce()
    {
        using var imageAttacher = new TemporaryImageAttacher(_images);
        var images = _nativeInstance.Coalesce(_images[0]);
        ReplaceImages(images);
    }

    /// <summary>
    /// Combines the images into a single image. The typical ordering would be
    /// image 1 => Red, 2 => Green, 3 => Blue, etc.
    /// </summary>
    /// <returns>The images combined into a single image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> Combine()
        => Combine(ColorSpace.sRGB);

    /// <summary>
    /// Combines the images into a single image. The grayscale value of the pixels of each image
    /// in the sequence is assigned in order to the specified channels of the combined image.
    /// The typical ordering would be image 1 => Red, 2 => Green, 3 => Blue, etc.
    /// </summary>
    /// <param name="colorSpace">The image colorspace.</param>
    /// <returns>The images combined into a single image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> Combine(ColorSpace colorSpace)
    {
        using var imageAttacher = new TemporaryImageAttacher(_images);
        var image = _nativeInstance.Combine(_images[0], colorSpace);
        return MagickImage.Create(image, GetSettings());
    }

    /// <summary>
    /// Perform complex mathematics on an image sequence.
    /// </summary>
    /// <param name="complexSettings">The complex settings.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Complex(IComplexSettings complexSettings)
    {
        Throw.IfNull(nameof(complexSettings), complexSettings);

        using var imageAttacher = new TemporaryImageAttacher(_images);
        using var temporaryDefines = new TemporaryDefines(_images[0]);

        if (complexSettings.SignalToNoiseRatio is not null)
            temporaryDefines.SetArtifact("complex:snr", complexSettings.SignalToNoiseRatio.Value.ToString(CultureInfo.InvariantCulture));

        var images = _nativeInstance.Complex(_images[0], complexSettings.ComplexOperator);
        ReplaceImages(images);
    }

    /// <summary>
    /// Determines whether the collection contains the specified image.
    /// </summary>
    /// <param name="item">The image to check.</param>
    /// <returns>True when the collection contains the specified image.</returns>
    public bool Contains(IMagickImage<QuantumType> item)
        => _images.Contains(item);

    /// <summary>
    /// Copies the images to an <see cref="Array"/>, starting at a particular <see cref="Array"/> index.
    /// </summary>
    /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination.</param>
    /// <param name="arrayIndex">The zero-based index in 'destination' at which copying begins.</param>
    public void CopyTo(IMagickImage<QuantumType>[] array, int arrayIndex)
    {
        if (_images.Count == 0)
            return;

        Throw.IfNull(nameof(array), array);
        Throw.IfOutOfRange(nameof(arrayIndex), arrayIndex, (uint)_images.Count);
        Throw.IfOutOfRange(nameof(arrayIndex), arrayIndex, (uint)array.Length);

        var indexI = 0;
        var indexA = arrayIndex;
        var count = Math.Min(array.Length - arrayIndex, _images.Count);
        while (count-- > 0)
        {
            array[indexA++] = _images[indexI++].Clone();
        }
    }

    /// <summary>
    /// Break down an image sequence into constituent parts. This is useful for creating GIF or
    /// MNG animation sequences.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Deconstruct()
    {
        using var imageAttacher = new TemporaryImageAttacher(_images);
        var images = _nativeInstance.Deconstruct(_images[0]);
        ReplaceImages(images);
    }

    /// <summary>
    /// Disposes the <see cref="MagickImageCollection"/> instance.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Evaluate image pixels into a single image. All the images in the collection must be the
    /// same size in pixels.
    /// </summary>
    /// <param name="evaluateOperator">The operator.</param>
    /// <returns>The resulting image of the evaluation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> Evaluate(EvaluateOperator evaluateOperator)
    {
        using var imageAttacher = new TemporaryImageAttacher(_images);
        var image = _nativeInstance.Evaluate(_images[0], evaluateOperator);
        return MagickImage.Create(image, GetSettings());
    }

    /// <summary>
    /// Flatten this collection into a single image.
    /// Use the virtual canvas size of first image. Images which fall outside this canvas is clipped.
    /// This can be used to 'fill out' a given virtual canvas.
    /// </summary>
    /// <returns>The resulting image of the flatten operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> Flatten()
        => Merge(LayerMethod.Flatten);

    /// <summary>
    /// Flatten this collection into a single image.
    /// This is useful for combining Photoshop layers into a single image.
    /// </summary>
    /// <param name="backgroundColor">The background color of the output image.</param>
    /// <returns>The resulting image of the flatten operation.</returns>
    public IMagickImage<QuantumType> Flatten(IMagickColor<QuantumType> backgroundColor)
    {
        var originalColor = _images[0].BackgroundColor;
        _images[0].BackgroundColor = backgroundColor;

        try
        {
            using var imageAttacher = new TemporaryImageAttacher(_images);
            var image = _nativeInstance.Merge(_images[0], LayerMethod.Flatten);
            return MagickImage.Create(image, GetSettings());
        }
        finally
        {
            _images[0].BackgroundColor = originalColor;
        }
    }

    /// <summary>
    /// Applies a mathematical expression to the images and returns the result.
    /// </summary>
    /// <param name="expression">The expression to apply.</param>
    /// <returns>The resulting image of the fx operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> Fx(string expression)
        => Fx(expression, Channels.Undefined);

    /// <summary>
    /// Applies a mathematical expression to the images and returns the result.
    /// </summary>
    /// <param name="expression">The expression to apply.</param>
    /// <param name="channels">The channel(s) to apply the expression to.</param>
    /// <returns>The resulting image of the fx operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> Fx(string expression, Channels channels)
    {
        Throw.IfNullOrEmpty(nameof(expression), expression);

        using var imageAttacher = new TemporaryImageAttacher(_images);
        return MagickImage.Fx(_images[0], expression, channels);
    }

    /// <summary>
    /// Returns an enumerator that iterates through the images.
    /// </summary>
    /// <returns>An enumerator that iterates through the images.</returns>
    public IEnumerator<IMagickImage<QuantumType>> GetEnumerator()
        => _images.GetEnumerator();

    /// <summary>
    /// Determines the index of the specified image.
    /// </summary>
    /// <param name="item">The image to check.</param>
    /// <returns>The index of the specified image.</returns>
    public int IndexOf(IMagickImage<QuantumType> item)
        => _images.IndexOf(item);

    /// <summary>
    /// Inserts an image into the collection.
    /// </summary>
    /// <param name="index">The index to insert the image.</param>
    /// <param name="item">The image to insert.</param>
    public void Insert(int index, IMagickImage<QuantumType> item)
    {
        CheckDuplicate(item);

        _images.Insert(index, item);
    }

    /// <summary>
    /// Inserts an image with the specified file name into the collection.
    /// </summary>
    /// <param name="index">The index to insert the image.</param>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    public void Insert(int index, string fileName)
        => _images.Insert(index, new MagickImage(fileName));

    /// <summary>
    /// Remap image colors with closest color from reference image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Map(IMagickImage<QuantumType> image)
        => Map(image, new QuantizeSettings());

    /// <summary>
    /// Remap image colors with closest color from reference image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="settings">Quantize settings.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Map(IMagickImage<QuantumType> image, IQuantizeSettings settings)
    {
        Throw.IfNull(nameof(image), image);
        Throw.IfNull(nameof(settings), settings);

        using var imageAttacher = new TemporaryImageAttacher(_images);
        _nativeInstance.Map(_images[0], settings, image);
    }

    /// <summary>
    /// Merge all layers onto a canvas just large enough to hold all the actual images. The virtual
    /// canvas of the first image is preserved but otherwise ignored.
    /// </summary>
    /// <returns>The resulting image of the merge operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> Merge()
        => Merge(LayerMethod.Merge);

    /// <summary>
    /// Create a composite image by combining the images with the specified settings.
    /// </summary>
    /// <param name="settings">The settings to use.</param>
    /// <returns>The resulting image of the montage operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> Montage(IMontageSettings<QuantumType> settings)
    {
        Throw.IfNull(nameof(settings), settings);

        if (!string.IsNullOrEmpty(settings.Label))
            _images[0].Label = settings.Label;

        using var imageAttacher = new TemporaryImageAttacher(_images);
        var images = _nativeInstance.Montage(_images[0], settings);

        using var result = new MagickImageCollection();
        result.AddRange(MagickImage.CreateList(images, GetSettings()));
        if (settings.TransparentColor is not null)
        {
            foreach (var image in result)
            {
                image.Transparent(settings.TransparentColor);
            }
        }

        return result.Merge();
    }

    /// <summary>
    /// The Morph method requires a minimum of two images. The first image is transformed into
    /// the second by a number of intervening images as specified by frames.
    /// </summary>
    /// <param name="frames">The number of in-between images to generate.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Morph(uint frames)
    {
        if (_images.Count < 2)
            throw new InvalidOperationException("Operation requires at least two images.");

        using var imageAttacher = new TemporaryImageAttacher(_images);
        var images = _nativeInstance.Morph(_images[0], frames);
        ReplaceImages(images);
    }

    /// <summary>
    /// Start with the virtual canvas of the first image, enlarging left and right edges to contain
    /// all images. Images with negative offsets will be clipped.
    /// </summary>
    /// <returns>The resulting image of the mosaic operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> Mosaic()
        => Merge(LayerMethod.Mosaic);

    /// <summary>
    /// Compares each image the GIF disposed forms of the previous image in the sequence. From
    /// this it attempts to select the smallest cropped image to replace each frame, while
    /// preserving the results of the GIF animation.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Optimize()
    {
        using var imageAttacher = new TemporaryImageAttacher(_images);
        var images = _nativeInstance.Optimize(_images[0]);
        ReplaceImages(images);
    }

    /// <summary>
    /// OptimizePlus is exactly as Optimize, but may also add or even remove extra frames in the
    /// animation, if it improves the total number of pixels in the resulting GIF animation.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void OptimizePlus()
    {
        using var imageAttacher = new TemporaryImageAttacher(_images);
        var images = _nativeInstance.OptimizePlus(_images[0]);
        ReplaceImages(images);
    }

    /// <summary>
    /// Compares each image the GIF disposed forms of the previous image in the sequence. Any
    /// pixel that does not change the displayed result is replaced with transparency.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void OptimizeTransparency()
    {
        using var imageAttacher = new TemporaryImageAttacher(_images);
        _nativeInstance.OptimizeTransparency(_images[0]);
    }

    /// <summary>
    /// Read only metadata and not the pixel data from all image frames.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Ping(byte[] data)
        => Ping(data, null);

    /// <summary>
    /// Reads only metadata and not the pixel data from all image frames.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Ping(byte[] data, uint offset, uint count)
        => Ping(data, offset, count, null);

    /// <summary>
    /// Reads only metadata and not the pixel data from all image frames.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Ping(byte[] data, uint offset, uint count, IMagickReadSettings<QuantumType>? readSettings)
    {
        Throw.IfNullOrEmpty(nameof(data), data);
        Throw.IfTrue(nameof(count), count < 1, "The number of bytes should be at least 1.");
        Throw.IfTrue(nameof(offset), offset >= data.Length, "The offset should not exceed the length of the array.");
        Throw.IfTrue(nameof(count), offset + count > data.Length, "The number of bytes should not exceed the length of the array.");

        Clear();
        AddImages(data, offset, count, readSettings, true);
    }

    /// <summary>
    /// Read only metadata and not the pixel data from all image frames.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Ping(byte[] data, IMagickReadSettings<QuantumType>? readSettings)
    {
        Throw.IfNullOrEmpty(nameof(data), data);

        Clear();
        AddImages(data, 0, (uint)data.Length, readSettings, true);
    }

    /// <summary>
    /// Read only metadata and not the pixel data from all image frames.
    /// </summary>
    /// <param name="file">The file to read the frames from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Ping(FileInfo file)
        => Ping(file, null);

    /// <summary>
    /// Read only metadata and not the pixel data from all image frames.
    /// </summary>
    /// <param name="file">The file to read the frames from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Ping(FileInfo file, IMagickReadSettings<QuantumType>? readSettings)
    {
        Throw.IfNull(nameof(file), file);

        Ping(file.FullName, readSettings);
    }

    /// <summary>
    /// Read only metadata and not the pixel data from all image frames.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Ping(Stream stream)
        => Ping(stream, null);

    /// <summary>
    /// Read only metadata and not the pixel data from all image frames.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Ping(Stream stream, IMagickReadSettings<QuantumType>? readSettings)
    {
        Clear();
        AddImages(stream, readSettings, true);
    }

    /// <summary>
    /// Read only metadata and not the pixel data from all image frames.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Ping(string fileName)
        => Ping(fileName, null);

    /// <summary>
    /// Read only metadata and not the pixel data from all image frames.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Ping(string fileName, IMagickReadSettings<QuantumType>? readSettings)
    {
        Clear();
        AddImages(fileName, readSettings, true);
    }

    /// <summary>
    /// Returns a new image where each pixel is the sum of the pixels in the image sequence after applying its
    /// corresponding terms (coefficient and degree pairs).
    /// </summary>
    /// <param name="terms">The list of polynomial coefficients and degree pairs and a constant.</param>
    /// <returns>A new image where each pixel is the sum of the pixels in the image sequence after applying its
    /// corresponding terms (coefficient and degree pairs).</returns>
    public IMagickImage<QuantumType> Polynomial(double[] terms)
    {
        Throw.IfNullOrEmpty(nameof(terms), terms);

        using var imageAttacher = new TemporaryImageAttacher(_images);
        var image = _nativeInstance.Polynomial(_images[0], terms, (nuint)terms.Length);
        return MagickImage.Create(image, GetSettings());
    }

    /// <summary>
    /// Quantize images (reduce number of colors).
    /// </summary>
    /// <returns>The resulting image of the quantize operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickErrorInfo? Quantize()
        => Quantize(new QuantizeSettings());

    /// <summary>
    /// Quantize images (reduce number of colors).
    /// </summary>
    /// <param name="settings">Quantize settings.</param>
    /// <returns>The resulting image of the quantize operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickErrorInfo? Quantize(IQuantizeSettings settings)
    {
        Throw.IfNull(nameof(settings), settings);

        using var imageAttacher = new TemporaryImageAttacher(_images);
        _nativeInstance.Quantize(_images[0], settings);

        if (settings.MeasureErrors && _images[0] is MagickImage image)
            return MagickImage.CreateErrorInfo(image);
        else
            return null;
    }

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(byte[] data)
        => Read(data, null);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(byte[] data, uint offset, uint count)
        => Read(data, offset, count, null);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(byte[] data, uint offset, uint count, MagickFormat format)
        => Read(data, offset, count, new MagickReadSettings { Format = format });

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(byte[] data, uint offset, uint count, IMagickReadSettings<QuantumType>? readSettings)
    {
        Throw.IfNullOrEmpty(nameof(data), data);
        Throw.IfTrue(nameof(count), count < 1, "The number of bytes should be at least 1.");
        Throw.IfTrue(nameof(offset), offset >= data.Length, "The offset should not exceed the length of the array.");
        Throw.IfTrue(nameof(count), offset + count > data.Length, "The number of bytes should not exceed the length of the array.");

        Clear();
        AddImages(data, offset, count, readSettings, false);
    }

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(byte[] data, MagickFormat format)
        => Read(data, new MagickReadSettings { Format = format });

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(byte[] data, IMagickReadSettings<QuantumType>? readSettings)
    {
        Throw.IfNullOrEmpty(nameof(data), data);

        Clear();
        AddImages(data, 0, (uint)data.Length, readSettings, false);
    }

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="file">The file to read the frames from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(FileInfo file)
        => Read(file, null);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="file">The file to read the frames from.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(FileInfo file, MagickFormat format)
        => Read(file, new MagickReadSettings { Format = format });

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="file">The file to read the frames from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(FileInfo file, IMagickReadSettings<QuantumType>? readSettings)
    {
        Throw.IfNull(nameof(file), file);

        Read(file.FullName, readSettings);
    }

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(Stream stream)
        => Read(stream, null);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(Stream stream, MagickFormat format)
        => Read(stream, new MagickReadSettings { Format = format });

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(Stream stream, IMagickReadSettings<QuantumType>? readSettings)
    {
        Clear();
        AddImages(stream, readSettings, false);
    }

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(string fileName)
        => Read(fileName, null);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(string fileName, MagickFormat format)
        => Read(fileName, new MagickReadSettings { Format = format });

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Read(string fileName, IMagickReadSettings<QuantumType>? readSettings)
    {
        Clear();
        AddImages(fileName, readSettings, false);
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
        var filePath = FileHelper.CheckForBaseDirectory(fileName);
        Throw.IfNullOrEmpty(nameof(fileName), filePath);

        var bytes = await FileHelper.ReadAllBytesAsync(fileName, cancellationToken).ConfigureAwait(false);

        cancellationToken.ThrowIfCancellationRequested();
        Clear();
        AddImages(bytes, 0, (uint)bytes.Length, readSettings, false, filePath);
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
        AddImages(bytes.GetData(), 0, (uint)bytes.Length, readSettings, false);
    }

    /// <summary>
    /// Removes the first occurrence of the specified image from the collection.
    /// </summary>
    /// <param name="item">The image to remove.</param>
    /// <returns>True when the image was found and removed.</returns>
    public bool Remove(IMagickImage<QuantumType> item)
        => _images.Remove(item);

    /// <summary>
    /// Removes the image at the specified index from the collection.
    /// </summary>
    /// <param name="index">The index of the image to remove.</param>
    public void RemoveAt(int index)
        => _images.RemoveAt(index);

    /// <summary>
    /// Resets the page property of every image in the collection.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void RePage()
    {
        foreach (var image in _images)
        {
            image.RePage();
        }
    }

    /// <summary>
    /// Reverses the order of the images in the collection.
    /// </summary>
    public void Reverse()
        => _images.Reverse();

    /// <summary>
    /// Smush images from list into single image in horizontal direction.
    /// </summary>
    /// <param name="offset">Minimum distance in pixels between images.</param>
    /// <returns>The resulting image of the smush operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> SmushHorizontal(uint offset)
        => Smush(offset, false);

    /// <summary>
    /// Smush images from list into single image in vertical direction.
    /// </summary>
    /// <param name="offset">Minimum distance in pixels between images.</param>
    /// <returns>The resulting image of the smush operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public IMagickImage<QuantumType> SmushVertical(uint offset)
        => Smush(offset, true);

    /// <summary>
    /// Converts this instance to a <see cref="byte"/> array.
    /// </summary>
    /// <returns>A <see cref="byte"/> array.</returns>
    public byte[] ToByteArray()
    {
        if (_images.Count == 0)
            return Array.Empty<byte>();

        var settings = GetSettings().Clone();
        settings.FileName = null;

        var wrapper = new ByteArrayWrapper();
        var writer = new ReadWriteStreamDelegate(wrapper.Write);
        var seeker = new SeekStreamDelegate(wrapper.Seek);
        var teller = new TellStreamDelegate(wrapper.Tell);
        var reader = new ReadWriteStreamDelegate(wrapper.Read);

        using var imageAttacher = new TemporaryImageAttacher(_images);
        _nativeInstance.WriteStream(_images[0], settings, writer, seeker, teller, reader);

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
        SetDefines(defines);
        return ToByteArray(defines.Format);
    }

    /// <summary>
    /// Converts this instance to a <see cref="byte"/> array.
    /// </summary>
    /// <returns>A <see cref="byte"/> array.</returns>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public byte[] ToByteArray(MagickFormat format)
    {
        using var tempFormat = new TemporaryMagickFormat(this, format);
        return ToByteArray();
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
    /// Converts this instance to a base64 string.
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
    /// Determine the overall bounds of all the image layers just as in <see cref="Merge()"/>,
    /// then adjust the the canvas and offsets to be relative to those bounds,
    /// without overlaying the images.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void TrimBounds()
    {
        using var imageAttacher = new TemporaryImageAttacher(_images);
        _nativeInstance.Merge(_images[0], LayerMethod.Trimbounds);
    }

    /// <summary>
    /// Writes the images to the specified file. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
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
    /// Writes the images to the specified file. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <param name="defines">The defines to set.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Write(FileInfo file, IWriteDefines defines)
    {
        SetDefines(defines);
        Write(file, defines.Format);
    }

    /// <summary>
    /// Writes the images to the specified file. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
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
    /// Writes the imagse to the specified stream. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="stream">The stream to write the images to.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Write(Stream stream)
    {
        Throw.IfNull(nameof(stream), stream);

        if (_images.Count == 0)
            return;

        var settings = GetSettings().Clone();
        settings.FileName = null;

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

        using var imageAttacher = new TemporaryImageAttacher(_images);
        _nativeInstance.WriteStream(_images[0], settings, writer, seeker, teller, reader);
    }

    /// <summary>
    /// Writes the imagse to the specified stream. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="stream">The stream to write the images to.</param>
    /// <param name="defines">The defines to set.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Write(Stream stream, IWriteDefines defines)
    {
        SetDefines(defines);
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
    /// Writes the images to the specified file name. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Write(string fileName)
    {
        var filePath = FileHelper.CheckForBaseDirectory(fileName);

        Throw.IfNullOrEmpty(nameof(fileName), filePath);

        if (_images.Count == 0)
            return;

        var settings = GetSettings().Clone();
        settings.FileName = fileName;

        using var imageAttacher = new TemporaryImageAttacher(_images);
        _nativeInstance.WriteFile(_images[0], settings);
    }

    /// <summary>
    /// Writes the images to the specified file name. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="defines">The defines to set.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void Write(string fileName, IWriteDefines defines)
    {
        SetDefines(defines);
        Write(fileName, defines.Format);
    }

    /// <summary>
    /// Writes the images to the specified file name. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
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
    public Task WriteAsync(FileInfo file, CancellationToken cancellationToken)
    {
        Throw.IfNull(nameof(file), file);

        if (_images.Count == 0)
            return Task.CompletedTask;

        var formatInfo = MagickFormatInfo.Create(file);
        return WriteAsyncInternal(file.FullName, formatInfo?.Format, cancellationToken);
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
    public Task WriteAsync(FileInfo file, MagickFormat format, CancellationToken cancellationToken)
    {
        Throw.IfNull(nameof(file), file);

        if (_images.Count == 0)
            return Task.CompletedTask;

        return WriteAsyncInternal(file.FullName, format, cancellationToken);
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

        using var imageAttacher = new TemporaryImageAttacher(_images);

        var bytes = ToByteArray();
        await stream.WriteAsync(bytes, 0, bytes.Length, cancellationToken).ConfigureAwait(false);
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
    public Task WriteAsync(string fileName, MagickFormat format, CancellationToken cancellationToken)
    {
        var filePath = FileHelper.CheckForBaseDirectory(fileName);
        Throw.IfNullOrEmpty(nameof(fileName), filePath);

        if (_images.Count == 0)
            return Task.CompletedTask;

        return WriteAsyncInternal(filePath, format, cancellationToken);
    }

    internal static void DisposeList(IntPtr value)
        => NativeMagickImageCollection.Dispose(value);

    private static MagickSettings CreateSettings(IMagickReadSettings<QuantumType>? readSettings)
    {
        if (readSettings is null)
            return new MagickSettings();

        return new MagickReadSettings(readSettings);
    }

    private static string ToBase64(byte[] bytes)
    {
        if (bytes is null)
            return string.Empty;

        return Convert.ToBase64String(bytes);
    }

    private void AddImages(byte[] data, uint offset, uint count, IMagickReadSettings<QuantumType>? readSettings, bool ping, string? fileName = null)
    {
        var settings = CreateSettings(readSettings);
        settings.Ping = ping;
        settings.FileName = fileName;

        var result = _nativeInstance.ReadBlob(settings, data, offset, count);
        AddImages(result, settings);
    }

    private void AddImages(string fileName, IMagickReadSettings<QuantumType>? readSettings, bool ping)
    {
        var filePath = FileHelper.CheckForBaseDirectory(fileName);
        Throw.IfNullOrEmpty(nameof(fileName), filePath);

        var settings = CreateSettings(readSettings);
        settings.FileName = filePath;
        settings.Ping = ping;

        var result = _nativeInstance.ReadFile(settings);
        AddImages(result, settings);
    }

    private void AddImages(Stream stream, IMagickReadSettings<QuantumType>? readSettings, bool ping)
    {
        Throw.IfNullOrEmpty(nameof(stream), stream);

        var bytes = Bytes.FromStreamBuffer(stream);
        if (bytes is not null)
        {
            AddImages(bytes.GetData(), 0, (uint)bytes.Length, readSettings, ping);
            return;
        }

        var settings = CreateSettings(readSettings);
        settings.Ping = ping;
        settings.FileName = null;

        using var wrapper = StreamWrapper.CreateForReading(stream);
        var reader = new ReadWriteStreamDelegate(wrapper.Read);
        SeekStreamDelegate? seeker = null;
        TellStreamDelegate? teller = null;

        if (stream.CanSeek)
        {
            seeker = new SeekStreamDelegate(wrapper.Seek);
            teller = new TellStreamDelegate(wrapper.Tell);
        }

        var result = _nativeInstance.ReadStream(settings, reader, seeker, teller);
        AddImages(result, settings);
    }

    private void AddImages(IntPtr result, MagickSettings settings)
    {
        settings.Format = MagickFormat.Unknown;

        foreach (var image in MagickImage.CreateList(result, settings))
        {
            _images.Add(image);
        }
    }

    private void CheckDuplicate(IMagickImage<QuantumType> item)
    {
        foreach (var image in _images)
        {
            if (ReferenceEquals(image, item))
                throw new InvalidOperationException("Not allowed to add the same image to the collection.");
        }
    }

    private void Dispose(bool disposing)
    {
        if (_nativeInstance is not null)
            _nativeInstance.Warning -= OnWarning;

        if (disposing)
            Clear();
    }

    private MagickSettings GetSettings()
        => MagickImage.GetSettings(_images[0]);

    private IMagickImage<QuantumType> Merge(LayerMethod layerMethod)
    {
        using var imageAttacher = new TemporaryImageAttacher(_images);
        var image = _nativeInstance.Merge(_images[0], layerMethod);
        return MagickImage.Create(image, GetSettings());
    }

    private void ReplaceImages(IntPtr images)
    {
        var settings = GetSettings().Clone();

        Clear();
        foreach (var image in MagickImage.CreateList(images, settings))
            Add(image);
    }

    private void OnWarning(object? sender, WarningEventArgs arguments)
        => _warning?.Invoke(this, arguments);

    private void SetDefines(IWriteDefines defines)
    {
        Throw.IfNull(nameof(defines), defines);

        foreach (var image in _images)
        {
            image.Settings.SetDefines(defines);
        }
    }

    private IMagickImage<QuantumType> Smush(uint offset, bool stack)
    {
        using var imageAttacher = new TemporaryImageAttacher(_images);
        var image = _nativeInstance.Smush(_images[0], offset, stack);
        return MagickImage.Create(image, GetSettings());
    }

    private async Task WriteAsyncInternal(string fileName, MagickFormat? format, CancellationToken cancellationToken)
    {
        if (_images.Count > 1 && format.HasValue && MagickFormatInfo.Create(format.Value)?.SupportsMultipleFrames == false)
        {
            var lastDotIndex = fileName.LastIndexOf('.');
            for (var i = 0; i < _images.Count; i++)
            {
                var newFileName = lastDotIndex == -1 ? fileName + "-" + i : fileName.Substring(0, lastDotIndex) + "-" + i + fileName.Substring(lastDotIndex);
                await _images[i].WriteAsync(newFileName, format.Value, cancellationToken).ConfigureAwait(false);
            }
        }
        else
        {
            var bytes = format.HasValue ? ToByteArray(format.Value) : ToByteArray();
            await FileHelper.WriteAllBytesAsync(fileName, bytes, cancellationToken).ConfigureAwait(false);
        }
    }

    private unsafe sealed partial class NativeMagickImageCollection : NativeHelper
    {
        public IntPtr ReadStream(IMagickSettings<QuantumType>? settings, ReadWriteStreamDelegate reader, SeekStreamDelegate? seeker, TellStreamDelegate? teller)
            => ReadStream(settings, reader, seeker, teller, (void*)null);

        public void WriteStream(IMagickImage image, IMagickSettings<QuantumType>? settings, ReadWriteStreamDelegate writer, SeekStreamDelegate? seeker, TellStreamDelegate? teller, ReadWriteStreamDelegate? reader)
            => WriteStream(image, settings, writer, seeker, teller, reader, (void*)null);
    }
}
