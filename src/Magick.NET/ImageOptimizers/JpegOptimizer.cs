// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using System.Threading;

namespace ImageMagick.ImageOptimizers;

/// <summary>
/// Class that can be used to optimize jpeg files.
/// </summary>
public sealed partial class JpegOptimizer : IImageOptimizer
{
    /// <summary>
    /// Gets the format that the optimizer supports.
    /// </summary>
    public IMagickFormatInfo Format
        => MagickFormatInfo.Create(MagickFormat.Jpeg)!;

    /// <summary>
    /// Gets or sets a value indicating whether various compression types will be used to find
    /// the smallest file. This process will take extra time because the file has to be written
    /// multiple times.
    /// </summary>
    public bool OptimalCompression { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether a progressive jpeg file will be created.
    /// </summary>
    public bool Progressive { get; set; } = true;

    /// <summary>
    /// Performs compression on the specified file. With some formats the image will be decoded
    /// and encoded and this will result in a small quality reduction. If the new file size is not
    /// smaller the file won't be overwritten.
    /// </summary>
    /// <param name="file">The jpeg file to compress.</param>
    /// <returns>True when the image could be compressed otherwise false.</returns>
    public bool Compress(FileInfo file)
        => Compress(file, 0);

    /// <summary>
    /// Performs compression on the specified file. With some formats the image will be decoded
    /// and encoded and this will result in a small quality reduction. If the new file size is not
    /// smaller the file won't be overwritten.
    /// </summary>
    /// <param name="file">The jpeg file to compress.</param>
    /// <param name="quality">The jpeg quality.</param>
    /// <returns>True when the image could be compressed otherwise false.</returns>
    public bool Compress(FileInfo file, uint quality)
    {
        Throw.IfNull(nameof(file), file);

        return DoCompress(file, false, quality);
    }

    /// <summary>
    /// Performs compression on the specified file. With some formats the image will be decoded
    /// and encoded and this will result in a small quality reduction. If the new file size is not
    /// smaller the file won't be overwritten.
    /// </summary>
    /// <param name="fileName">The file name of the jpeg image to compress.</param>
    /// <returns>True when the image could be compressed otherwise false.</returns>
    public bool Compress(string fileName)
        => Compress(fileName, 0);

    /// <summary>
    /// Performs compression on the specified file. With some formats the image will be decoded
    /// and encoded and this will result in a small quality reduction. If the new file size is not
    /// smaller the file won't be overwritten.
    /// </summary>
    /// <param name="fileName">The file name of the jpeg image to compress.</param>
    /// <param name="quality">The jpeg quality.</param>
    /// <returns>True when the image could be compressed otherwise false.</returns>
    public bool Compress(string fileName, uint quality)
    {
        var filePath = FileHelper.CheckForBaseDirectory(fileName);
        Throw.IfNullOrEmpty(nameof(fileName), filePath);

        return DoCompress(new FileInfo(fileName), false, quality);
    }

    /// <summary>
    /// Performs compression on the specified stream. With some formats the image will be decoded
    /// and encoded and this will result in a small quality reduction. If the new size is not
    /// smaller the stream won't be overwritten.
    /// </summary>
    /// <param name="stream">The stream of the jpeg image to compress.</param>
    /// <returns>True when the image could be compressed otherwise false.</returns>
    public bool Compress(Stream stream)
        => Compress(stream, 0);

    /// <summary>
    /// Performs compression on the specified file. With some formats the image will be decoded
    /// and encoded and this will result in a small quality reduction. If the new file size is not
    /// smaller the file won't be overwritten.
    /// </summary>
    /// <param name="stream">The stream of the jpeg image to compress.</param>
    /// <param name="quality">The jpeg quality.</param>
    /// <returns>True when the image could be compressed otherwise false.</returns>
    public bool Compress(Stream stream, uint quality)
        => DoCompress(stream, false, quality);

    /// <summary>
    /// Performs lossless compression on the specified file. If the new file size is not smaller
    /// the file won't be overwritten.
    /// </summary>
    /// <param name="file">The jpeg file to compress.</param>
    /// <returns>True when the image could be compressed otherwise false.</returns>
    public bool LosslessCompress(FileInfo file)
    {
        Throw.IfNull(nameof(file), file);

        return DoCompress(file, true, 0);
    }

    /// <summary>
    /// Performs lossless compression on the specified file. If the new file size is not smaller
    /// the file won't be overwritten.
    /// </summary>
    /// <param name="fileName">The file name of the jpeg image to compress.</param>
    /// <returns>True when the image could be compressed otherwise false.</returns>
    public bool LosslessCompress(string fileName)
    {
        var filePath = FileHelper.CheckForBaseDirectory(fileName);
        Throw.IfNullOrEmpty(nameof(fileName), filePath);

        return DoCompress(new FileInfo(fileName), true, 0);
    }

    /// <summary>
    /// Performs lossless compression on the specified stream. If the new stream size is not smaller
    /// the stream won't be overwritten.
    /// </summary>
    /// <param name="stream">The stream of the jpeg image to compress.</param>
    /// <returns>True when the image could be compressed otherwise false.</returns>
    public bool LosslessCompress(Stream stream)
        => DoCompress(stream, true, 0);

    private static void DoNativeCompress(string filename, string outputFilename, bool progressive, bool lossless, uint quality)
        => NativeJpegOptimizer.CompressFile(filename, outputFilename, progressive, lossless, quality);

    private static void DoNativeCompress(Stream input, Stream output, bool progressive, bool lossless, uint quality)
    {
        using var readWrapper = StreamWrapper.CreateForReading(input);
        using var writeWrapper = StreamWrapper.CreateForWriting(output);
        var reader = new ReadWriteStreamDelegate(readWrapper.Read);
        var writer = new ReadWriteStreamDelegate(writeWrapper.Write);

        NativeJpegOptimizer.CompressStream(reader, writer, progressive, lossless, quality);
    }

    private bool DoCompress(FileInfo file, bool lossless, uint quality)
    {
        using var tempFile = new TemporaryFile();
        DoNativeCompress(file.FullName, tempFile.FullName, Progressive, lossless, quality);

        if (OptimalCompression)
        {
            using var tempFileOptimal = new TemporaryFile();
            DoNativeCompress(file.FullName, tempFileOptimal.FullName, !Progressive, lossless, quality);

            if (tempFileOptimal.Length < tempFile.Length)
                tempFileOptimal.CopyTo(tempFile);
        }

        if (tempFile.Length >= file.Length)
            return false;

        tempFile.CopyTo(file);
        return true;
    }

    private bool DoCompress(Stream stream, bool lossless, uint quality)
    {
        ImageOptimizerHelper.CheckStream(stream);

        var isCompressed = false;
        var startPosition = stream.Position;

        var memStream = new MemoryStream();

        try
        {
            DoNativeCompress(stream, memStream, Progressive, lossless, quality);

            if (OptimalCompression)
            {
                stream.Position = startPosition;

                var memStreamOptimal = new MemoryStream();

                try
                {
                    DoNativeCompress(stream, memStreamOptimal, !Progressive, lossless, quality);

                    if (memStreamOptimal.Length < memStream.Length)
                        memStreamOptimal = Interlocked.Exchange(ref memStream, memStreamOptimal);
                }
                finally
                {
                    memStreamOptimal.Dispose();
                }
            }

            if (memStream.Length < (stream.Length - startPosition))
            {
                isCompressed = true;
                stream.Position = startPosition;
                memStream.Position = 0;
                memStream.CopyTo(stream);
                stream.SetLength(startPosition + memStream.Length);
            }

            stream.Position = startPosition;
        }
        finally
        {
            memStream.Dispose();
        }

        return isCompressed;
    }
}
