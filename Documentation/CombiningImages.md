# Combining images

## Merge multiple images

#### C#
```C#
using (MagickImageCollection images = new MagickImageCollection())
{
    // Add the first image
    MagickImage first = new MagickImage("Snakeware.png");
    images.Add(first);

    // Add the second image
    MagickImage second = new MagickImage("Snakeware.png");
    images.Add(second);

    // Create a mosaic from both images
    using(IMagickImage result = images.Mosaic())
    {
      // Save the result
      result.Write("Mosaic.png");
   }
}
```

#### VB.NET
```VB.NET
Using images As New MagickImageCollection()
    ' Add the first image
    Dim first As New MagickImage("Snakeware.png")
    images.Add(first)

    ' Add the second image
    Dim second As New MagickImage("Snakeware.png")
    images.Add(second)

    ' Create a mosaic from both images
    Using result As IMagickImage = images.Mosaic()
      ' Save the result
      result.Write("Mosaic.png")
    End Using
End Using
```

## Create animated gif

#### C#
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
    QuantizeSettings settings = new QuantizeSettings();
    settings.Colors = 256;
    collection.Quantize(settings);

    // Optionally optimize the images (images should have the same size).
    collection.Optimize();

    // Save gif
    collection.Write("Snakeware.Animated.gif");
}
```

#### VB.NET
```VB.NET
Using collection As New MagickImageCollection()
    ' Add first image and set the animation delay to 100ms
    collection.Add("Snakeware.png")
    collection(0).AnimationDelay = 100   ' in this example delay is 1000ms/1sec

    ' Add second image, set the animation delay (in 1/100th of a second) and flip the image
    collection.Add("Snakeware.png")
    collection(1).AnimationDelay = 100   ' in this example delay is 1000ms/1sec
    collection(1).Flip()

    ' Optionally reduce colors
    Dim settings As New QuantizeSettings()
    settings.Colors = 256
    collection.Quantize(settings)

    ' Optionally optimize the images (images should have the same size).
    collection.Optimize()

    ' Save gif
    collection.Write("Snakeware.Animated.gif")
End Using
```
