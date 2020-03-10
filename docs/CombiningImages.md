# Combining images

## Merge multiple images

```C#
using (var images = new MagickImageCollection())
{
    // Add the first image
    var first = new MagickImage("Snakeware.png");
    images.Add(first);

    // Add the second image
    var second = new MagickImage("Snakeware.png");
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
using (MagickImageCollection collection = new MagickImageCollection())
{
    // Add first image and set the animation delay (in 1/100th of a second) 
    collection.Add("Snakeware.png");
    collection[0].AnimationDelay = 100; // in this example delay is 1000ms/1sec

    // Add second image, set the animation delay (in 1/100th of a second) and flip the image
    collection.Add("Snakeware.png");
    collection[1].AnimationDelay = 100; // in this example delay is 1000ms/1sec
    collection[1].Flip();

    // Optionally reduce colors
    var settings = new QuantizeSettings();
    settings.Colors = 256;
    collection.Quantize(settings);

    // Optionally optimize the images (images should have the same size).
    collection.Optimize();

    // Save gif
    collection.Write("Snakeware.Animated.gif");
}
```