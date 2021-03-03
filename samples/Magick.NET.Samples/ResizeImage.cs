// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;

namespace Magick.NET.Samples
{
    public static class ResizeImageSamples
    {
        public static void ResizeAnimatedGif()
        {
            // Read from file
            using (var collection = new MagickImageCollection(SampleFiles.SnakewareGif))
            {
                // This will remove the optimization and change the image to how it looks at that point
                // during the animation. More info here: http://www.imagemagick.org/Usage/anim_basics/#coalesce
                collection.Coalesce();

                // Resize each image in the collection to a width of 200. When zero is specified for the height
                // the height will be calculated with the aspect ratio.
                foreach (var image in collection)
                {
                    image.Resize(200, 0);
                }

                // Save the result
                collection.Write(SampleFiles.OutputDirectory + "Snakeware.resized.gif");
            }
        }

        public static void ResizeToFixedSize()
        {
            // Read from file
            using (var image = new MagickImage(SampleFiles.SnakewarePng))
            {
                var size = new MagickGeometry(100, 100);
                // This will resize the image to a fixed size without maintaining the aspect ratio.
                // Normally an image will be resized to fit inside the specified size.
                size.IgnoreAspectRatio = true;

                image.Resize(size);

                // Save the result
                image.Write(SampleFiles.OutputDirectory + "Snakeware.100x100.png");
            }
        }
    }
}
