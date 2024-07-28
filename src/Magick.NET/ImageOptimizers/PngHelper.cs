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

internal sealed class PngHelper
{
    private readonly bool _optimalCompression;

    public PngHelper(IImageOptimizer optimizer)
    {
        _optimalCompression = optimizer.OptimalCompression;
    }

    public TemporaryFile? FindBestFileQuality(IMagickImage<QuantumType> image, out uint bestQuality)
    {
        bestQuality = 0;

        CheckTransparency(image);

        TemporaryFile? bestFile = null;

        foreach (var quality in GetQualityList())
        {
            TemporaryFile? tempFile = null;

            try
            {
                tempFile = new TemporaryFile();

                image.Quality = quality;
                image.Write(tempFile.FullName);

                if (bestFile is null || bestFile.Length > tempFile.Length)
                {
                    if (bestFile is not null)
                        bestFile.Dispose();

                    bestFile = tempFile;
                    bestQuality = quality;
                    tempFile = null;
                }
            }
            finally
            {
                if (tempFile is not null)
                    tempFile.Dispose();
            }
        }

        return bestFile;
    }

    public MemoryStream? FindBestStreamQuality(IMagickImage<QuantumType> image, out uint bestQuality)
    {
        bestQuality = 0;

        CheckTransparency(image);

        MemoryStream? bestStream = null;

        foreach (var quality in GetQualityList())
        {
            var memStream = new MemoryStream();

            try
            {
                image.Quality = quality;
                image.Write(memStream);

                if (bestStream is null || memStream.Length < bestStream.Length)
                {
                    if (bestStream is not null)
                        bestStream.Dispose();

                    bestStream = memStream;
                    bestQuality = quality;
                    memStream = null;
                }
            }
            finally
            {
                if (memStream is not null)
                    memStream.Dispose();
            }
        }

        return bestStream;
    }

    private static void CheckTransparency(IMagickImage<QuantumType> image)
    {
        if (!image.HasAlpha)
            return;

        if (image.IsOpaque)
            image.HasAlpha = false;
    }

    private uint[] GetQualityList()
    {
        if (_optimalCompression)
            return [91, 94, 95, 97];
        else
            return [90];
    }
}
