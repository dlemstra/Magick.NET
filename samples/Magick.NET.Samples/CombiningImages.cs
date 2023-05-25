// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;

namespace Magick.NET.Samples;

public static class CombiningImagesSamples
{
    public static void MergeMultipleImages()
    {
        using var images = new MagickImageCollection();

        // Add the first image
        var first = new MagickImage(SampleFiles.SnakewarePng);
        images.Add(first);

        // Add the second image
        var second = new MagickImage(SampleFiles.SnakewarePng);
        images.Add(second);

        // Create a mosaic from both images
        using var result = images.Mosaic();

        // Save the result
        result.Write(SampleFiles.OutputDirectory + "Mosaic.png");
    }

    public static void CreateAnimatedGif()
    {
        using var images = new MagickImageCollection();

        // Add the first image, set the animation delay to 100ms, and set the disposal method
        images.Add(SampleFiles.SnakewarePng);
        images[0].AnimationDelay = 100;
        images[0].GifDisposeMethod = GifDisposeMethod.Previous; // Prevents frames with transparent backgrounds from overlapping each other

        // Add the second image, set the animation delay to 100ms, set the disposal method, and flip the image
        images.Add(SampleFiles.SnakewarePng);
        images[1].AnimationDelay = 100;
        images[1].GifDisposeMethod = GifDisposeMethod.Previous;
        images[1].Flip();

        // Optionally reduce colors
        var settings = new QuantizeSettings
        {
            Colors = 256
        };
        images.Quantize(settings);

        // Optionally optimize the images (images should have the same size).
        images.Optimize();

        // Save gif
        images.Write(SampleFiles.OutputDirectory + "Snakeware.Animated.gif");
    }
}
