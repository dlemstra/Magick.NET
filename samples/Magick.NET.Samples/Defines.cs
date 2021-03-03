// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;

namespace Magick.NET.Samples
{
    public static class CommandLineOptionDefineSamples
    {
        public static void CommandLineOptionDefine()
        {
            // Read image from file
            using (var image = new MagickImage(SampleFiles.SnakewarePng))
            {
                // Tells the dds coder to use dxt1 compression when writing the image
                image.Settings.SetDefine(MagickFormat.Dds, "compression", "dxt1");
                // Save image as dds file
                image.Write(SampleFiles.OutputDirectory + "Snakeware.dds");
            }
        }

        public static void DefinesThatNeedToBeSetBeforeReadingAnImage()
        {
            var settings = new MagickReadSettings();
            // Set define that tells the jpeg coder that the output image will be 32x32
            settings.SetDefine(MagickFormat.Jpeg, "size", "32x32");

            // Read image from file
            using (var image = new MagickImage(SampleFiles.SnakewareJpg))
            {
                // Create thumnail that is 32 pixels wide and 32 pixels high
                image.Thumbnail(32, 32);
                // Save image as tiff
                image.Write(SampleFiles.OutputDirectory + "Snakeware.tiff");
            }
        }
    }
}
