# Combining images

## Merge multiple images

```C#
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
result.Write("Mosaic.png");
```

## Create animated gif

```C#
using var images = new MagickImageCollection();

// Add first image and set the animation delay to 100ms
images.Add(SampleFiles.SnakewarePng);
images[0].AnimationDelay = 100;

// Add second image, set the animation delay to 100ms and flip the image
images.Add(SampleFiles.SnakewarePng);
images[1].AnimationDelay = 100;
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
images.Write("Snakeware.Animated.gif");
```
