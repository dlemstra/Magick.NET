# Convert image

## Convert image from one format to another

```C#
// Read first frame of gif image
using var image = new MagickImage(SampleFiles.SnakewareGif);

// Save frame as jpg
image.Write("Snakeware.jpg");

var settings = new MagickReadSettings();
// Tells the xc: reader the image to create should be 800x600
settings.Width = 800;
settings.Height = 600;

using var memStream = new MemoryStream();

// Create image that is completely purple and 800x600
using (var purple = new MagickImage("xc:purple", settings))
{
    // Sets the output format to png
    purple.Format = MagickFormat.Png;

    // Write the image to the memorystream
    purple.Write(memStream);
}

// Read image from file
using var snakeware = new MagickImage(SampleFiles.SnakewarePng);

// Sets the output format to jpeg
snakeware.Format = MagickFormat.Jpeg;

// Create byte array that contains a jpeg file
var data = snakeware.ToByteArray();
```

## Convert CMYK to RGB

```C#
// Uses sRGB.icm, eps/pdf produce better result when you set this before loading.
var settings = new MagickReadSettings
{
    ColorSpace = ColorSpace.sRGB
};

// Create empty image
using var eps = new MagickImage();

// Reads the eps image, the specified settings tell Ghostscript to create an sRGB image
eps.Read(SampleFiles.SnakewareEps, settings);

// Save image as tiff
eps.Write("Snakeware.tiff");

// Read image from file
using var png = new MagickImage(SampleFiles.SnakewareJpg);

// Will use the CMYK profile if the image does not contain a color profile.
// The second profile will transform the colorspace from CMYK to RGB
png.TransformColorSpace(ColorProfile.USWebCoatedSWOP, ColorProfile.SRGB);

// Save image as png
png.Write("Snakeware.png");

// Read image from file
using var tiff = new MagickImage(SampleFiles.SnakewareJpg);

// Will use the CMYK profile if your image does not contain a color profile.
// The second profile will transform the colorspace from your custom icc profile
tiff.TransformColorSpace(ColorProfile.USWebCoatedSWOP, new ColorProfile(SampleFiles.YourProfileIcc));

// Save image as tiff
tiff.Write("Snakeware.tiff");
```
