// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using ImageMagick.ImageOptimizers;

namespace ImageMagick;

/// <summary>
/// Class that can be used to optimize an image.
/// </summary>
public sealed class ImageOptimizer
{
    private readonly Collection<IImageOptimizer> _optimizers = CreateImageOptimizers();

    /// <summary>
    /// Gets or sets a value indicating whether to skip unsupported files instead of throwing an exception.
    /// </summary>
    public bool IgnoreUnsupportedFormats { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether various compression types will be used to find
    /// the smallest file. This process will take extra time because the file has to be written
    /// multiple times.
    /// </summary>
    public bool OptimalCompression { get; set; }

    private string SupportedFormats
    {
        get
        {
            var formats = new List<string>(_optimizers.Count);

            foreach (var optimizer in _optimizers)
            {
                formats.Add(optimizer.Format.ModuleFormat.ToString());
            }

            return string.Join(", ", formats.ToArray());
        }
    }

    /// <summary>
    /// Performs compression on the specified file. With some formats the image will be decoded
    /// and encoded and this will result in a small quality reduction. If the new file size is not
    /// smaller the file won't be overwritten.
    /// </summary>
    /// <param name="file">The image file to compress.</param>
    /// <returns>True when the image could be compressed otherwise false.</returns>
    public bool Compress(FileInfo file)
    {
        Throw.IfNull(nameof(file), file);

        return DoCompress(file);
    }

    /// <summary>
    /// Performs compression on the specified file. With some formats the image will be decoded
    /// and encoded and this will result in a small quality reduction. If the new file size is not
    /// smaller the file won't be overwritten.
    /// </summary>
    /// <param name="fileName">The file name of the image to compress.</param>
    /// <returns>True when the image could be compressed otherwise false.</returns>
    public bool Compress(string fileName)
    {
        var filePath = FileHelper.CheckForBaseDirectory(fileName);
        Throw.IfNullOrEmpty(nameof(fileName), filePath);

        return DoCompress(new FileInfo(filePath));
    }

    /// <summary>
    /// Performs compression on the specified stream. With some formats the image will be decoded
    /// and encoded and this will result in a small quality reduction. If the new size is not
    /// smaller the stream won't be overwritten.
    /// </summary>
    /// <param name="stream">The stream of the image to compress.</param>
    /// <returns>True when the image could be compressed otherwise false.</returns>
    public bool Compress(Stream stream)
    {
        ImageOptimizerHelper.CheckStream(stream);

        var optimizer = GetOptimizer(stream);
        if (optimizer is null)
            return false;

        optimizer.OptimalCompression = OptimalCompression;
        return optimizer.Compress(stream);
    }

    /// <summary>
    /// Returns true when the supplied file name is supported based on the extension of the file.
    /// </summary>
    /// <param name="file">The file to check.</param>
    /// <returns>True when the supplied file name is supported based on the extension of the file.</returns>
    /// <returns>True when the image could be compressed otherwise false.</returns>
    public bool IsSupported(FileInfo file) => IsSupported(ImageOptimizerHelper.GetFormatInformation(file));

    /// <summary>
    /// Returns true when the supplied formation information is supported.
    /// </summary>
    /// <param name="formatInfo">The format information to check.</param>
    /// <returns>True when the supplied formation information is supported.</returns>
    public bool IsSupported(IMagickFormatInfo? formatInfo)
    {
        if (formatInfo is null)
            return false;

        foreach (var optimizer in _optimizers)
        {
            if (optimizer.Format.Format == formatInfo.ModuleFormat)
                return true;
        }

        return false;
    }

    /// <summary>
    /// Returns true when the supplied file name is supported based on the extension of the file.
    /// </summary>
    /// <param name="fileName">The name of the file to check.</param>
    /// <returns>True when the supplied file name is supported based on the extension of the file.</returns>
    public bool IsSupported(string fileName) => IsSupported(ImageOptimizerHelper.GetFormatInformation(fileName));

    /// <summary>
    /// Returns true when the supplied stream is supported.
    /// </summary>
    /// <param name="stream">The stream to check.</param>
    /// <returns>True when the supplied stream is supported.</returns>
    public bool IsSupported(Stream stream)
    {
        Throw.IfNull(nameof(stream), stream);

        if (!stream.CanRead || !stream.CanWrite || !stream.CanSeek)
            return false;

        return IsSupported(ImageOptimizerHelper.GetFormatInformation(stream));
    }

    /// <summary>
    /// Performs lossless compression on the specified file. If the new file size is not smaller
    /// the file won't be overwritten.
    /// </summary>
    /// <param name="file">The image file to compress.</param>
    /// <returns>True when the image could be compressed otherwise false.</returns>
    public bool LosslessCompress(FileInfo file)
    {
        Throw.IfNull(nameof(file), file);

        return DoLosslessCompress(file);
    }

    /// <summary>
    /// Performs lossless compression on the specified file. If the new file size is not smaller
    /// the file won't be overwritten.
    /// </summary>
    /// <param name="fileName">The file name of the image to compress.</param>
    /// <returns>True when the image could be compressed otherwise false.</returns>
    public bool LosslessCompress(string fileName)
    {
        var filePath = FileHelper.CheckForBaseDirectory(fileName);
        Throw.IfNullOrEmpty(nameof(fileName), filePath);

        return DoLosslessCompress(new FileInfo(filePath));
    }

    /// <summary>
    /// Performs lossless compression on the specified stream. If the new stream size is not smaller
    /// the stream won't be overwritten.
    /// </summary>
    /// <param name="stream">The stream of the image to compress.</param>
    /// <returns>True when the image could be compressed otherwise false.</returns>
    public bool LosslessCompress(Stream stream)
    {
        ImageOptimizerHelper.CheckStream(stream);

        var optimizer = GetOptimizer(stream);
        if (optimizer is null)
            return false;

        optimizer.OptimalCompression = OptimalCompression;
        return optimizer.LosslessCompress(stream);
    }

    private static Collection<IImageOptimizer> CreateImageOptimizers()
    {
        return new Collection<IImageOptimizer>
        {
            new JpegOptimizer(),
            new PngOptimizer(),
            new IcoOptimizer(),
            new GifOptimizer(),
        };
    }

    private bool DoLosslessCompress(FileInfo file)
    {
        var optimizer = GetOptimizer(file);
        if (optimizer is null)
            return false;

        optimizer.OptimalCompression = OptimalCompression;
        return optimizer.LosslessCompress(file);
    }

    private bool DoCompress(FileInfo file)
    {
        var optimizer = GetOptimizer(file);
        if (optimizer is null)
            return false;

        optimizer.OptimalCompression = OptimalCompression;
        return optimizer.Compress(file);
    }

    private IImageOptimizer? GetOptimizer(FileInfo file)
    {
        var info = ImageOptimizerHelper.GetFormatInformation(file);
        return GetOptimizer(info);
    }

    private IImageOptimizer? GetOptimizer(Stream stream)
    {
        var info = ImageOptimizerHelper.GetFormatInformation(stream);
        return GetOptimizer(info);
    }

    private IImageOptimizer? GetOptimizer(IMagickFormatInfo? info)
    {
        if (info is null)
            return null;

        foreach (var optimizer in _optimizers)
        {
            if (optimizer.Format.ModuleFormat == info.ModuleFormat)
                return optimizer;
        }

        if (IgnoreUnsupportedFormats)
            return null;

        throw new MagickCorruptImageErrorException("Invalid format, supported formats are: " + SupportedFormats);
    }
}
