// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick.ImageOptimizers;

/// <summary>
/// Class that can be used to optimize ico files.
/// </summary>
public sealed class IcoOptimizer : IImageOptimizer
{
    /// <summary>
    /// Gets the format that the optimizer supports.
    /// </summary>
    public IMagickFormatInfo Format
        => MagickFormatInfo.Create(MagickFormat.Icon)!;

    /// <summary>
    /// Gets or sets a value indicating whether various compression types will be used to find
    /// the smallest file. This process will take extra time because the file has to be written
    /// multiple times.
    /// </summary>
    public bool OptimalCompression { get; set; }

    /// <summary>
    /// Performs compression on the specified file. With some formats the image will be decoded
    /// and encoded and this will result in a small quality reduction. If the new file size is not
    /// smaller the file won't be overwritten.
    /// </summary>
    /// <param name="file">The ico file to compress.</param>
    /// <returns>True when the image could be compressed otherwise false.</returns>
    public bool Compress(FileInfo file)
        => DoCompress(file, false);

    /// <summary>
    /// Performs compression on the specified file. With some formats the image will be decoded
    /// and encoded and this will result in a small quality reduction. If the new file size is not
    /// smaller the file won't be overwritten.
    /// </summary>
    /// <param name="fileName">The file name of the ico image to compress.</param>
    /// <returns>True when the image could be compressed otherwise false.</returns>
    public bool Compress(string fileName)
    {
        var filePath = FileHelper.CheckForBaseDirectory(fileName);
        Throw.IfNullOrEmpty(nameof(fileName), filePath);

        return DoCompress(new FileInfo(filePath), false);
    }

    /// <summary>
    /// Performs compression on the specified stream. With some formats the image will be decoded
    /// and encoded and this will result in a small quality reduction. If the new size is not
    /// smaller the stream won't be overwritten.
    /// </summary>
    /// <param name="stream">The stream of the ico image to compress.</param>
    /// <returns>True when the image could be compressed otherwise false.</returns>
    public bool Compress(Stream stream)
        => DoCompress(stream, false);

    /// <summary>
    /// Performs lossless compression on the specified file. If the new file size is not smaller
    /// the file won't be overwritten.
    /// </summary>
    /// <param name="file">The ico file to optimize.</param>
    /// <returns>True when the image could be compressed otherwise false.</returns>
    public bool LosslessCompress(FileInfo file)
    {
        Throw.IfNull(nameof(file), file);

        return DoCompress(file, true);
    }

    /// <summary>
    /// Performs lossless compression on the specified file. If the new file size is not smaller
    /// the file won't be overwritten.
    /// </summary>
    /// <param name="fileName">The ico file to optimize.</param>
    /// <returns>True when the image could be compressed otherwise false.</returns>
    public bool LosslessCompress(string fileName)
    {
        var filePath = FileHelper.CheckForBaseDirectory(fileName);
        Throw.IfNullOrEmpty(nameof(fileName), filePath);

        return DoCompress(new FileInfo(filePath), true);
    }

    /// <summary>
    /// Performs lossless compression on the specified stream. If the new stream size is not smaller
    /// the stream won't be overwritten.
    /// </summary>
    /// <param name="stream">The stream of the ico image to compress.</param>
    /// <returns>True when the image could be compressed otherwise false.</returns>
    public bool LosslessCompress(Stream stream)
        => DoCompress(stream, true);

    private static bool CanUseColormap(IMagickImage<QuantumType> image, bool lossless)
    {
        if (image.ClassType == ClassType.Pseudo)
            return true;

        var histogram = image.Histogram();
        if (histogram.Count > 256)
            return false;

        if (!image.HasAlpha)
            return true;

        var max = Quantum.Max;
        var min = (QuantumType)(Quantum.Max * 0.125);
        if (lossless)
            min = 0;
        else
            max -= min;

        var fixAlpha = false;
        foreach (var color in histogram.Keys)
        {
            if (color.A != 0 && color.A != Quantum.Max)
            {
                fixAlpha = true;
                if (color.A > min && color.A < max)
                {
                    return false;
                }
            }
        }

        if (fixAlpha)
            FixAlpha(image, min, max);

        return true;
    }

    private static void FixAlpha(IMagickImage<QuantumType> image, QuantumType min, QuantumType max)
    {
        using var pixels = image.GetPixelsUnsafe();
        var alphaIndex = pixels.GetChannelIndex(PixelChannel.Alpha);
        var channels = pixels.Channels;

        for (var y = 0; y < image.Height; y++)
        {
            var row = pixels.GetArea(0, y, image.Width, 1);
            if (row is null)
                continue;

            for (var i = alphaIndex; i < row.Length; i += channels)
            {
                if (row[i] <= min)
                    row[i] = 0;
                else if (row[i] >= max)
                    row[i] = Quantum.Max;
            }

            pixels.SetArea(0, y, image.Width, 1, row);
        }
    }

    private bool DoCompress(FileInfo file, bool lossless)
    {
        var settings = new MagickReadSettings() { Format = MagickFormat.Ico };
        using var images = new MagickImageCollection(file, settings);
        foreach (var image in images)
        {
            if (image.Width > 255)
            {
                image.Format = MagickFormat.Png;

                var pngHelper = new PngHelper(this);
                var memoryStream = pngHelper.FindBestStreamQuality(image, out var bestQuality);
                image.Format = MagickFormat.Ico;

                if (memoryStream is not null)
                {
                    memoryStream.Dispose();
                    image.Quality = bestQuality;
                }
            }
            else
            {
                if (CanUseColormap(image, lossless))
                    image.ClassType = ClassType.Pseudo;
            }
        }

        using var tempFile = new TemporaryFile();
        images.Write(tempFile.FullName);

        var isCompressed = tempFile.Length < file.Length;
        if (isCompressed)
            tempFile.CopyTo(file);

        return isCompressed;
    }

    private bool DoCompress(Stream stream, bool lossless)
    {
        ImageOptimizerHelper.CheckStream(stream);

        var startPosition = stream.Position;

        using var images = new MagickImageCollection(stream, new MagickReadSettings() { Format = MagickFormat.Ico });
        foreach (var image in images)
        {
            if (image.Width > 255)
            {
                image.Format = MagickFormat.Png;

                var pngHelper = new PngHelper(this);
                var memoryStream = pngHelper.FindBestStreamQuality(image, out var bestQuality);
                image.Format = MagickFormat.Ico;

                if (memoryStream is not null)
                {
                    memoryStream.Dispose();
                    image.Quality = bestQuality;
                }
            }
            else
            {
                if (CanUseColormap(image, lossless))
                    image.ClassType = ClassType.Pseudo;
            }
        }

        using var output = new MemoryStream();
        images.Write(output);

        var isCompressed = output.Length < (stream.Length - startPosition);
        if (isCompressed)
        {
            stream.Position = startPosition;
            output.Position = 0;
            output.CopyTo(stream);
            stream.SetLength(startPosition + output.Length);
        }

        stream.Position = startPosition;

        return isCompressed;
    }
}
