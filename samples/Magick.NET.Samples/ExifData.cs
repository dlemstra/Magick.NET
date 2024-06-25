// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;

namespace Magick.NET.Samples;

public static class ExifDataSamples
{
    public static void ReadExifData()
    {
        // Read image from file
        using var image = new MagickImage(SampleFiles.FujiFilmFinePixS1ProJpg);

        // Retrieve the exif information
        var profile = image.GetExifProfile();

        // Check if image contains an exif profile
        if (profile is null)
        {
            Console.WriteLine("Image does not contain exif information.");
        }
        else
        {
            // Write all values to the console
            foreach (var value in profile.Values)
            {
                Console.WriteLine("{0}({1}): {2}", value.Tag, value.DataType, value.ToString());
            }
        }
    }

    public static void CreateThumbnailFromExifData()
    {
        // Read image from file
        using var image = new MagickImage(SampleFiles.FujiFilmFinePixS1ProJpg);

        // Retrieve the exif information
        var profile = image.GetExifProfile();

        if (profile is not null)
        {
            // Create thumbnail from exif information
            using var thumbnail = profile.CreateThumbnail();

            // Check if exif profile contains thumbnail and save it
            if (thumbnail is not null)
            {
                thumbnail.Write(SampleFiles.OutputDirectory + "FujiFilmFinePixS1Pro.thumb.jpg");
            }
        }
    }
}
