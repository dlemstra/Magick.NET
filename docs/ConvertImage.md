# Convert image

## Convert image from one format to another

```C#
// Read first frame of gif image
using (var image = new MagickImage("Snakeware.gif"))
{
    // Save frame as jpg
    image.Write("Snakeware.jpg");
}

// Write to stream
var settings = new MagickReadSettings();
// Tells the xc: reader the image to create should be 800x600
settings.Width = 800;
settings.Height = 600;

using (var memStream = new MemoryStream())
{
    // Create image that is completely purple and 800x600
    using (var image = new MagickImage("xc:purple", settings))
    {
        // Sets the output format to png
        image.Format = MagickFormat.Png;
        // Write the image to the memorystream
        image.Write(memStream);
    }
}

// Read image from file
using (var image = new MagickImage("Snakeware.png"))
{
    // Sets the output format to jpeg
    image.Format = MagickFormat.Jpeg;
    // Create byte array that contains a jpeg file
    byte[] data = image.ToByteArray();
}
```

## Convert CMYK to RGB

```C#
// Uses sRGB.icm, eps/pdf produce better result when you set this before loading.
var settings = new MagickReadSettings();
settings.ColorSpace = ColorSpace.sRGB;

// Create empty image 
using (var image = new MagickImage())
{
    // Reads the eps image, the specified settings tell Ghostscript to create an sRGB image
    image.Read("Snakeware.eps", settings);
    // Save image as tiff
    image.Write("Snakeware.tiff");
}

// Read image from file
using (var image = new MagickImage("Snakeware.jpg"))
{
    // Add a CMYK profile if your image does not contain a color profile.
    image.AddProfile(ColorProfile.USWebCoatedSWOP);
    // Adding the second profile will transform the colorspace from CMYK to RGB
    image.AddProfile(ColorProfile.SRGB);
    // Save image as png
    image.Write("Snakeware.png");
}

// Use custom color profile
using (var image = new MagickImage("Snakeware.jpg"))
{
    // First add a CMYK profile if your image does not contain a color profile.
    image.AddProfile(ColorProfile.USWebCoatedSWOP);
    // Adding the second profile will transform the colorspace from your custom icc profile
    image.AddProfile(new ColorProfile("YourProfile.icc"));
    // Save image as tiff
    image.Write("Snakeware.tiff");
}
```