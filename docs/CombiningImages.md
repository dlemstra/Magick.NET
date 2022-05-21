# Combining images

## Merge multiple images

```C#
using (var images = new MagickImageCollection())
{
    // Add the first image
    var first = new MagickImage("c:\path\to\Snakeware.png");
    images.Add(first);

    // Add the second image
    var second = new MagickImage("c:\path\to\Snakeware.png");
    images.Add(second);

    // Create a mosaic from both images
    using(var result = images.Mosaic())
    {
      // Save the result
      result.Write("Mosaic.png");
   }
}
```

## Create animated gif

```C#
using (var images = new MagickImageCollection())
{
    // Add first image and set the animation delay (in 1/100th of a second) 
    images.Add("c:\path\to\Snakeware.png");
    images[0].AnimationDelay = 100; // in this example delay is 1000ms/1sec

    // Add second image, set the animation delay (in 1/100th of a second) and flip the image
    images.Add("c:\path\to\Snakeware.png");
    images[1].AnimationDelay = 100; // in this example delay is 1000ms/1sec
    images[1].Flip();

    // Optionally reduce colors
    var settings = new QuantizeSettings();
    settings.Colors = 256;
    images.Quantize(settings);

    // Optionally optimize the images (images should have the same size).
    images.Optimize();

    // Save gif
    images.Write("c:\path\to\Snakeware.Animated.gif");
}
```
