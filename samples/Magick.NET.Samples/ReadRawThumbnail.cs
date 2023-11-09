// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;

namespace Magick.NET.Samples;

public static class ReadRawThumbnailSamples
{
    public static void ReadRawThumbnail()
    {
        // Setup DNG read defines
        var defines = new DngReadDefines
        {
            ReadThumbnail = true
        };

        // Create empty image
        using var image = new MagickImage();

        // Copy the defines to the settings
        image.Settings.SetDefines(defines);

        // Read only meta data of the image
        image.Ping(SampleFiles.StillLifeCR2);

        // Get thumbnail data
        var thumbnailData = image.GetProfile("dng:thumbnail")?.GetData();

        if (thumbnailData != null)
        {
            // Read the thumbnail image
            using var thumbnail = new MagickImage(thumbnailData);
        }
    }
}
