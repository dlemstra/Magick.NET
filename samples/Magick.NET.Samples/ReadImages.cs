// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;

namespace Magick.NET.Samples;

public static class ReadImageSamples
{
    private static MemoryStream LoadMemoryStreamImage()
        => new MemoryStream(LoadImageBytes());

    private static byte[] LoadImageBytes()
        => File.ReadAllBytes(SampleFiles.SnakewarePng);

    public static void ReadImage()
    {
        // Read from file.
        using var imageFromFile = new MagickImage(SampleFiles.SnakewareJpg);

        // Read from stream.
        using var memStream = LoadMemoryStreamImage();
        using var imageFromStream = new MagickImage(memStream);

        // Read from byte array.
        var data = LoadImageBytes();
        using var imageFromBytes = new MagickImage(data);

        // Read image that has no predefined dimensions.
        var settings = new MagickReadSettings();
        settings.Width = 800;
        settings.Height = 600;
        using var yellow = new MagickImage("xc:yellow", settings);

        using var image = new MagickImage();
        image.Read(SampleFiles.SnakewareJpg);
        image.Read(data);
        image.Read("xc:yellow", settings);

        using var stream = LoadMemoryStreamImage();
        image.Read(stream);
    }

    public static void ReadBasicImageInformation()
    {
        // Read from file
        var info = new MagickImageInfo(SampleFiles.SnakewarePng);

        // Read from stream
        using var memStream = LoadMemoryStreamImage();
        info = new MagickImageInfo(memStream);

        // Read from byte array
        var data = LoadImageBytes();
        info = new MagickImageInfo(data);

        info = new MagickImageInfo();
        info.Read(SampleFiles.SnakewarePng);
        using var stream = LoadMemoryStreamImage();
        info.Read(stream);
        info.Read(data);

        Console.WriteLine(info.Width);
        Console.WriteLine(info.Height);
        Console.WriteLine(info.ColorSpace);
        Console.WriteLine(info.Format);
        if (info.Density is not null)
        {
            Console.WriteLine(info.Density.X);
            Console.WriteLine(info.Density.Y);
            Console.WriteLine(info.Density.Units);
        }
    }

    public static void ReadImageWithMultipleFrames()
    {
        // Read from file
        using var imagesFromFile = new MagickImageCollection(SampleFiles.SnakewareJpg);

        // Read from stream
        using var memStream = LoadMemoryStreamImage();
        using var imagesFromStream = new MagickImageCollection(memStream);

        // Read from byte array
        var data = LoadImageBytes();
        using var imagesFromByteArray = new MagickImageCollection(data);

        // Read pdf with custom density.
        var settings = new MagickReadSettings();
        settings.Density = new Density(144);

        using var pdf = new MagickImageCollection(SampleFiles.SnakewarePdf, settings);

        using var images = new MagickImageCollection();
        images.Read(SampleFiles.SnakewareJpg);
        using var stream = LoadMemoryStreamImage();
        images.Read(stream);
        images.Read(data);
        images.Read(SampleFiles.SnakewarePdf, settings);
    }
}
