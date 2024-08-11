// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ImageMagick;

/// <summary>
/// Represents the collection of images.
/// </summary>
public partial interface IMagickImageCollection : IDisposable
{
    /// <summary>
    /// Event that will we raised when a warning is raised by ImageMagick.
    /// </summary>
    event EventHandler<WarningEventArgs> Warning;

    /// <summary>
    /// Adds an image with the specified file name to the collection.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Add(string fileName);

    /// <summary>
    /// Adds the image(s) from the specified byte array to the collection.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AddRange(byte[] data);

    /// <summary>
    /// Adds the image(s) from the specified file name to the collection.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AddRange(string fileName);

    /// <summary>
    /// Adds the image(s) from the specified stream to the collection.
    /// </summary>
    /// <param name="stream">The stream to read the images from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AddRange(Stream stream);

    /// <summary>
    /// Merge a sequence of images. This is useful for GIF animation sequences that have page
    /// offsets and disposal methods.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Coalesce();

    /// <summary>
    /// Perform complex mathematics on an image sequence.
    /// </summary>
    /// <param name="complexSettings">The complex settings.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Complex(IComplexSettings complexSettings);

    /// <summary>
    /// Break down an image sequence into constituent parts. This is useful for creating GIF or
    /// MNG animation sequences.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Deconstruct();

    /// <summary>
    /// Inserts an image with the specified file name into the collection.
    /// </summary>
    /// <param name="index">The index to insert the image.</param>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    void Insert(int index, string fileName);

    /// <summary>
    /// The Morph method requires a minimum of two images. The first image is transformed into
    /// the second by a number of intervening images as specified by frames.
    /// </summary>
    /// <param name="frames">The number of in-between images to generate.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Morph(uint frames);

    /// <summary>
    /// Compares each image the GIF disposed forms of the previous image in the sequence. From
    /// this it attempts to select the smallest cropped image to replace each frame, while
    /// preserving the results of the GIF animation.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Optimize();

    /// <summary>
    /// OptimizePlus is exactly as Optimize, but may also add or even remove extra frames in the
    /// animation, if it improves the total number of pixels in the resulting GIF animation.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void OptimizePlus();

    /// <summary>
    /// Compares each image the GIF disposed forms of the previous image in the sequence. Any
    /// pixel that does not change the displayed result is replaced with transparency.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void OptimizeTransparency();

    /// <summary>
    /// Read only metadata and not the pixel data from all image frames.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Ping(byte[] data);

    /// <summary>
    /// Reads only metadata and not the pixel data from all image frames.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Ping(byte[] data, uint offset, uint count);

    /// <summary>
    /// Read only metadata and not the pixel data from all image frames.
    /// </summary>
    /// <param name="file">The file to read the frames from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Ping(FileInfo file);

    /// <summary>
    /// Read only metadata and not the pixel data from all image frames.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Ping(Stream stream);

    /// <summary>
    /// Read only metadata and not the pixel data from all image frames.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Ping(string fileName);

    /// <summary>
    /// Quantize images (reduce number of colors).
    /// </summary>
    /// <returns>The resulting image of the quantize operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickErrorInfo? Quantize();

    /// <summary>
    /// Quantize images (reduce number of colors).
    /// </summary>
    /// <param name="settings">Quantize settings.</param>
    /// <returns>The resulting image of the quantize operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickErrorInfo? Quantize(IQuantizeSettings settings);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(byte[] data);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(byte[] data, uint offset, uint count);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(byte[] data, uint offset, uint count, MagickFormat format);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(byte[] data, MagickFormat format);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="file">The file to read the frames from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(FileInfo file);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="file">The file to read the frames from.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(FileInfo file, MagickFormat format);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(Stream stream);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(Stream stream, MagickFormat format);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(string fileName);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(string fileName, MagickFormat format);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="file">The file to read the frames from.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(FileInfo file);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="file">The file to read the frames from.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(FileInfo file, CancellationToken cancellationToken);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="file">The file to read the frames from.</param>
    /// <param name="format">The format to use.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(FileInfo file, MagickFormat format);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="file">The file to read the frames from.</param>
    /// <param name="format">The format to use.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(FileInfo file, MagickFormat format, CancellationToken cancellationToken);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(Stream stream);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(Stream stream, CancellationToken cancellationToken);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="format">The format to use.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(Stream stream, MagickFormat format);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="format">The format to use.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(Stream stream, MagickFormat format, CancellationToken cancellationToken);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(string fileName);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(string fileName, CancellationToken cancellationToken);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="format">The format to use.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(string fileName, MagickFormat format);

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="format">The format to use.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(string fileName, MagickFormat format, CancellationToken cancellationToken);

    /// <summary>
    /// Remap image colors with closest color from reference image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Remap(IMagickImage image);

    /// <summary>
    /// Remap image colors with closest color from reference image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="settings">Quantize settings.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Remap(IMagickImage image, IQuantizeSettings settings);

    /// <summary>
    /// Resets the page property of every image in the collection.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ResetPage();

    /// <summary>
    /// Reverses the order of the images in the collection.
    /// </summary>
    void Reverse();

    /// <summary>
    /// Converts this instance to a <see cref="byte"/> array.
    /// </summary>
    /// <returns>A <see cref="byte"/> array.</returns>
    byte[] ToByteArray();

    /// <summary>
    /// Converts this instance to a <see cref="byte"/> array.
    /// </summary>
    /// <param name="defines">The defines to set.</param>
    /// <returns>A <see cref="byte"/> array.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    byte[] ToByteArray(IWriteDefines defines);

    /// <summary>
    /// Converts this instance to a <see cref="byte"/> array.
    /// </summary>
    /// <returns>A <see cref="byte"/> array.</returns>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    byte[] ToByteArray(MagickFormat format);

    /// <summary>
    /// Converts this instance to a base64 <see cref="string"/>.
    /// </summary>
    /// <returns>A base64 <see cref="string"/>.</returns>
    string ToBase64();

    /// <summary>
    /// Converts this instance to a base64 <see cref="string"/>.
    /// </summary>
    /// <param name="format">The format to use.</param>
    /// <returns>A base64 <see cref="string"/>.</returns>
    string ToBase64(MagickFormat format);

    /// <summary>
    /// Converts this instance to a base64 <see cref="string"/>.
    /// </summary>
    /// <param name="defines">The defines to set.</param>
    /// <returns>A base64 <see cref="string"/>.</returns>
    string ToBase64(IWriteDefines defines);

    /// <summary>
    /// Determine the overall bounds of all the image layers just as in <see cref="IMagickImageCollection{TQuantumType}.Merge()"/>,
    /// then adjust the the canvas and offsets to be relative to those bounds,
    /// without overlaying the images.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void TrimBounds();

    /// <summary>
    /// Writes the images to the specified file. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Write(FileInfo file);

    /// <summary>
    /// Writes the images to the specified file. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <param name="defines">The defines to set.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Write(FileInfo file, IWriteDefines defines);

    /// <summary>
    /// Writes the images to the specified file. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Write(FileInfo file, MagickFormat format);

    /// <summary>
    /// Writes the imagse to the specified stream. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="stream">The stream to write the images to.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Write(Stream stream);

    /// <summary>
    /// Writes the imagse to the specified stream. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="stream">The stream to write the images to.</param>
    /// <param name="defines">The defines to set.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Write(Stream stream, IWriteDefines defines);

    /// <summary>
    /// Writes the image to the specified stream.
    /// </summary>
    /// <param name="stream">The stream to write the image data to.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Write(Stream stream, MagickFormat format);

    /// <summary>
    /// Writes the images to the specified file name. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Write(string fileName);

    /// <summary>
    /// Writes the images to the specified file name. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="defines">The defines to set.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Write(string fileName, IWriteDefines defines);

    /// <summary>
    /// Writes the images to the specified file name. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Write(string fileName, MagickFormat format);

    /// <summary>
    /// Writes the images to the specified file. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(FileInfo file);

    /// <summary>
    /// Writes the images to the specified file. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(FileInfo file, CancellationToken cancellationToken);

    /// <summary>
    /// Writes the images to the specified file. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <param name="defines">The defines to set.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(FileInfo file, IWriteDefines defines);

    /// <summary>
    /// Writes the images to the specified file. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <param name="defines">The defines to set.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(FileInfo file, IWriteDefines defines, CancellationToken cancellationToken);

    /// <summary>
    /// Writes the images to the specified file. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <param name="format">The format to use.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(FileInfo file, MagickFormat format);

    /// <summary>
    /// Writes the images to the specified file. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <param name="format">The format to use.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(FileInfo file, MagickFormat format, CancellationToken cancellationToken);

    /// <summary>
    /// Writes the imagse to the specified stream. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="stream">The stream to write the images to.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(Stream stream);

    /// <summary>
    /// Writes the imagse to the specified stream. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="stream">The stream to write the images to.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(Stream stream, CancellationToken cancellationToken);

    /// <summary>
    /// Writes the imagse to the specified stream. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="stream">The stream to write the images to.</param>
    /// <param name="defines">The defines to set.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(Stream stream, IWriteDefines defines);

    /// <summary>
    /// Writes the imagse to the specified stream. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="stream">The stream to write the images to.</param>
    /// <param name="defines">The defines to set.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(Stream stream, IWriteDefines defines, CancellationToken cancellationToken);

    /// <summary>
    /// Writes the image to the specified stream.
    /// </summary>
    /// <param name="stream">The stream to write the image data to.</param>
    /// <param name="format">The format to use.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(Stream stream, MagickFormat format);

    /// <summary>
    /// Writes the image to the specified stream.
    /// </summary>
    /// <param name="stream">The stream to write the image data to.</param>
    /// <param name="format">The format to use.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(Stream stream, MagickFormat format, CancellationToken cancellationToken);

    /// <summary>
    /// Writes the images to the specified file name. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(string fileName);

    /// <summary>
    /// Writes the images to the specified file name. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(string fileName, CancellationToken cancellationToken);

    /// <summary>
    /// Writes the images to the specified file name. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="defines">The defines to set.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(string fileName, IWriteDefines defines);

    /// <summary>
    /// Writes the images to the specified file name. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="defines">The defines to set.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(string fileName, IWriteDefines defines, CancellationToken cancellationToken);

    /// <summary>
    /// Writes the images to the specified file name. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="format">The format to use.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(string fileName, MagickFormat format);

    /// <summary>
    /// Writes the images to the specified file name. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="format">The format to use.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(string fileName, MagickFormat format, CancellationToken cancellationToken);
}
