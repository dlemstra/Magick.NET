# Defines

## Command line option -define

```C#
// Read image from file
using var image = new MagickImage(SampleFiles.SnakewarePng);

// Tells the dds coder to use dxt1 compression when writing the image
image.Settings.SetDefine(MagickFormat.Dds, "compression", "dxt1");

// Save image as dds file
image.Write("Snakeware.dds");
```

## Defines that need to be set before reading an image

```C#
// Set define that tells the jpeg coder that the output image will be 32x32
var settings = new MagickReadSettings();
settings.SetDefine(MagickFormat.Jpeg, "size", "32x32");

// Read image from file
using var image = new MagickImage(SampleFiles.SnakewareJpg);

// Create thumnail that is 32 pixels wide and 32 pixels high
image.Thumbnail(32, 32);

// Save image as tiff
image.Write("Snakeware.tiff");
```
