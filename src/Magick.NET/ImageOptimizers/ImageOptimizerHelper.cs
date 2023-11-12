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

namespace ImageMagick;

internal static class ImageOptimizerHelper
{
    public static void CheckFormat(IMagickImage<QuantumType> image, MagickFormat expectedFormat)
    {
        var format = MagickFormatInfo.Create(image.Format)?.ModuleFormat;
        if (format != expectedFormat)
            throw new MagickCorruptImageErrorException("Invalid image format: " + format.ToString());
    }

    public static void CheckStream(Stream stream)
    {
        Throw.IfNullOrEmpty(nameof(stream), stream);
        Throw.IfFalse(nameof(stream), stream.CanRead, "The stream should be readable.");
        Throw.IfFalse(nameof(stream), stream.CanWrite, "The stream should be writeable.");
        Throw.IfFalse(nameof(stream), stream.CanSeek, "The stream should be seekable.");
    }

    public static IMagickFormatInfo? GetFormatInformation(FileInfo file)
    {
        var info = MagickFormatInfo.Create(file);
        if (info is not null)
            return info;

        try
        {
            var imageInfo = new MagickImageInfo(file);
            return MagickFormatInfo.Create(imageInfo.Format);
        }
        catch
        {
            try
            {
                using var stream = file.OpenRead();
                return GetFormatInformationFromHeader(stream);
            }
            catch
            {
                return null;
            }
        }
    }

    public static IMagickFormatInfo? GetFormatInformation(string fileName)
    {
        var info = MagickFormatInfo.Create(fileName);
        if (info is not null)
            return info;

        try
        {
            var imageInfo = new MagickImageInfo(fileName);
            return MagickFormatInfo.Create(imageInfo.Format);
        }
        catch
        {
            try
            {
                using var stream = File.OpenRead(fileName);
                return GetFormatInformationFromHeader(stream);
            }
            catch
            {
                return null;
            }
        }
    }

    public static IMagickFormatInfo? GetFormatInformation(Stream stream)
    {
        var startPosition = stream.Position;

        try
        {
            var info = new MagickImageInfo(stream);
            return MagickFormatInfo.Create(info.Format);
        }
        catch
        {
            stream.Position = startPosition;

            return GetFormatInformationFromHeader(stream);
        }
        finally
        {
            stream.Position = startPosition;
        }
    }

    private static IMagickFormatInfo? GetFormatInformationFromHeader(Stream stream)
    {
        var buffer = new byte[4];
        stream.Read(buffer, 0, buffer.Length);

        if (buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 1 && buffer[3] == 0)
            return MagickFormatInfo.Create(MagickFormat.Ico);

        return null;
    }
}
