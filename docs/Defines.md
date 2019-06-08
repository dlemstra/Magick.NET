# Defines

## Command line option -define

```C#
// Read image from file
using (MagickImage image = new MagickImage("Snakeware.png"))
{
    // Tells the dds coder to use dxt1 compression when writing the image
    image.Settings.SetDefine(MagickFormat.Dds, "compression", "dxt1");
    // Write the image
    image.Write("Snakeware.dds");
}
```

## Defines that need to be set before reading an image

```C#
MagickReadSettings settings = new MagickReadSettings();
// Set define that tells the jpeg coder that the output image will be 32x32
settings.SetDefine(MagickFormat.Jpeg, "size", "32x32");

// Read image from file
using (MagickImage image = new MagickImage("Snakeware.jpg"))
{
    // Create thumnail that is 32 pixels wide and 32 pixels high
    image.Thumbnail(32,32);
    // Save image as tiff
    image.Write("Snakeware.tiff");
}
```