// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ImageMagick;

/// <summary>
/// Represents the collection of images.
/// </summary>
/// <typeparam name="TQuantumType">The quantum type.</typeparam>
public partial interface IMagickImageCollection<TQuantumType> : IMagickImageCollection, IList<IMagickImage<TQuantumType>>
    where TQuantumType : struct, IConvertible
{
    /// <summary>
    /// Adds the image(s) from the specified byte array to the collection.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AddRange(byte[] data, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Adds a Clone of the specified images to this collection.
    /// </summary>
    /// <param name="images">The images to add to the collection.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AddRange(IEnumerable<IMagickImage<TQuantumType>> images);

    /// <summary>
    /// Adds the image(s) from the specified file name to the collection.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AddRange(string fileName, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Adds the image(s) from the specified stream to the collection.
    /// </summary>
    /// <param name="stream">The stream to read the images from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AddRange(Stream stream, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Creates a single image, by appending all the images in the collection horizontally (+append).
    /// </summary>
    /// <returns>A single image, by appending all the images in the collection horizontally (+append).</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> AppendHorizontally();

    /// <summary>
    /// Creates a single image, by appending all the images in the collection vertically (-append).
    /// </summary>
    /// <returns>A single image, by appending all the images in the collection vertically (-append).</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> AppendVertically();

    /// <summary>
    /// Creates a clone of the current image collection.
    /// </summary>
    /// <returns>A clone of the current image collection.</returns>
    IMagickImageCollection<TQuantumType> Clone();

    /// <summary>
    /// Combines the images into a single image. The typical ordering would be
    /// image 1 => Red, 2 => Green, 3 => Blue, etc.
    /// </summary>
    /// <returns>The images combined into a single image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Combine();

    /// <summary>
    /// Combines the images into a single image. The grayscale value of the pixels of each image
    /// in the sequence is assigned in order to the specified channels of the combined image.
    /// The typical ordering would be image 1 => Red, 2 => Green, 3 => Blue, etc.
    /// </summary>
    /// <param name="colorSpace">The image colorspace.</param>
    /// <returns>The images combined into a single image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Combine(ColorSpace colorSpace);

    /// <summary>
    /// Evaluate image pixels into a single image. All the images in the collection must be the
    /// same size in pixels.
    /// </summary>
    /// <param name="evaluateOperator">The operator.</param>
    /// <returns>The resulting image of the evaluation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Evaluate(EvaluateOperator evaluateOperator);

    /// <summary>
    /// Flatten this collection into a single image.
    /// Use the virtual canvas size of first image. Images which fall outside this canvas is clipped.
    /// This can be used to 'fill out' a given virtual canvas.
    /// </summary>
    /// <returns>The resulting image of the flatten operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Flatten();

    /// <summary>
    /// Flatten this collection into a single image.
    /// This is useful for combining Photoshop layers into a single image.
    /// </summary>
    /// <param name="backgroundColor">The background color of the output image.</param>
    /// <returns>The resulting image of the flatten operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Flatten(IMagickColor<TQuantumType> backgroundColor);

    /// <summary>
    /// Applies a mathematical expression to the images and returns the result.
    /// </summary>
    /// <param name="expression">The expression to apply.</param>
    /// <returns>The resulting image of the fx operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Fx(string expression);

    /// <summary>
    /// Applies a mathematical expression to the images and returns the result.
    /// </summary>
    /// <param name="expression">The expression to apply.</param>
    /// <param name="channels">The channel(s) to apply the expression to.</param>
    /// <returns>The resulting image of the fx operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Fx(string expression, Channels channels);

    /// <summary>
    /// Merge all layers onto a canvas just large enough to hold all the actual images. The virtual
    /// canvas of the first image is preserved but otherwise ignored.
    /// </summary>
    /// <returns>The resulting image of the merge operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Merge();

    /// <summary>
    /// Create a composite image by combining the images with the specified settings.
    /// </summary>
    /// <param name="settings">The settings to use.</param>
    /// <returns>The resulting image of the montage operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Montage(IMontageSettings<TQuantumType> settings);

    /// <summary>
    /// Start with the virtual canvas of the first image, enlarging left and right edges to contain
    /// all images. Images with negative offsets will be clipped.
    /// </summary>
    /// <returns>The resulting image of the mosaic operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Mosaic();

    /// <summary>
    /// Reads only metadata and not the pixel data from all image frames.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Ping(byte[] data, uint offset, uint count, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Read only metadata and not the pixel data from all image frames.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Ping(byte[] data, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Read only metadata and not the pixel data from all image frames.
    /// </summary>
    /// <param name="file">The file to read the frames from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Ping(FileInfo file, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Read only metadata and not the pixel data from all image frames.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Ping(Stream stream, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Read only metadata and not the pixel data from all image frames.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Ping(string fileName, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Returns a new image where each pixel is the sum of the pixels in the image sequence after applying its
    /// corresponding terms (coefficient and degree pairs).
    /// </summary>
    /// <param name="terms">The list of polynomial coefficients and degree pairs and a constant.</param>
    /// <returns>A new image where each pixel is the sum of the pixels in the image sequence after applying its
    /// corresponding terms (coefficient and degree pairs).</returns>
    IMagickImage<TQuantumType> Polynomial(double[] terms);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(byte[] data, uint offset, uint count, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(byte[] data, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="file">The file to read the frames from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(FileInfo file, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(Stream stream, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(string fileName, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="file">The file to read the frames from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(FileInfo file, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="file">The file to read the frames from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(FileInfo file, IMagickReadSettings<TQuantumType>? readSettings, CancellationToken cancellationToken);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(Stream stream, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(Stream stream, IMagickReadSettings<TQuantumType>? readSettings, CancellationToken cancellationToken);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(string fileName, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(string fileName, IMagickReadSettings<TQuantumType>? readSettings, CancellationToken cancellationToken);

    /// <summary>
    /// Smush images from list into single image in horizontal direction.
    /// </summary>
    /// <param name="offset">Minimum distance in pixels between images.</param>
    /// <returns>The resulting image of the smush operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> SmushHorizontal(uint offset);

    /// <summary>
    /// Smush images from list into single image in vertical direction.
    /// </summary>
    /// <param name="offset">Minimum distance in pixels between images.</param>
    /// <returns>The resulting image of the smush operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> SmushVertical(uint offset);
}
