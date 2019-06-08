# Convert image

## Convert image from one format to another

#### C#
```C#
// Read first frame of gif image
using (MagickImage image = new MagickImage("Snakeware.gif"))
{
    // Save frame as jpg
    image.Write("Snakeware.jpg");
}

// Write to stream
MagickReadSettings settings = new MagickReadSettings();
// Tells the xc: reader the image to create should be 800x600
settings.Width = 800;
settings.Height = 600;

using (MemoryStream memStream = new MemoryStream())
{
    // Create image that is completely purple and 800x600
    using (MagickImage image = new MagickImage("xc:purple", settings))
    {
        // Sets the output format to png
        image.Format = MagickFormat.Png;
        // Write the image to the memorystream
        image.Write(memStream);
    }
}

// Read image from file
using (MagickImage image = new MagickImage("Snakeware.png"))
{
    // Sets the output format to jpeg
    image.Format = MagickFormat.Jpeg;
    // Create byte array that contains a jpeg file
    byte[] data = image.ToByteArray();
}
```

#### VB.NET
```VB.NET
' Read first frame of gif image
Using image As New MagickImage("Snakeware.gif")
    ' Save frame as jpg
    image.Write("Snakeware.jpg")
End Using

Dim settings As New MagickReadSettings()
' Tells the xc: reader the image to create should be 800x600
settings.Width = 800
settings.Height = 600

Using memStream As New MemoryStream()
    ' Create image that is completely purple and 800x600
    Using image As New MagickImage("xc:purple", settings)
        ' Sets the output format to png
        image.Format = MagickFormat.Png
        ' Write the image to the memorystream
        image.Write(memStream)
    End Using
End Using

' Read image from file
Using image As New MagickImage("Snakeware.png")
    ' Sets the output format to jpeg
    image.Format = MagickFormat.Jpeg
    ' Create byte array that contains a jpeg file
    Dim data As Byte() = image.ToByteArray()
End Using
```

## Convert CMYK to RGB

#### C#
```C#
// Uses sRGB.icm, eps/pdf produce better result when you set this before loading.
MagickReadSettings settings = new MagickReadSettings();
settings.ColorSpace = ColorSpace.sRGB;

// Create empty image 
using (MagickImage image = new MagickImage())
{
    // Reads the eps image, the specified settings tell Ghostscript to create an sRGB image
    image.Read("Snakeware.eps", settings);
    // Save image as tiff
    image.Write("Snakeware.tiff");
}

// Read image from file
using (MagickImage image = new MagickImage("Snakeware.jpg"))
{
    // Add a CMYK profile if your image does not contain a color profile.
    image.AddProfile(ColorProfile.USWebCoatedSWOP);

    // Adding the second profile will transform the colorspace from CMYK to RGB
    image.AddProfile(ColorProfile.SRGB);
    // Save image as png
    image.Write("Snakeware.png");
}

// Use custom color profile
using (MagickImage image = new MagickImage("Snakeware.jpg"))
{
    // First add a CMYK profile if your image does not contain a color profile.
    image.AddProfile(ColorProfile.USWebCoatedSWOP);

    // Adding the second profile will transform the colorspace from your custom icc profile
    image.AddProfile(new ColorProfile("YourProfile.icc"));
    // Save image as tiff
    image.Write("Snakeware.tiff");
}
```

#### VB.NET
```VB.NET
' Uses sRGB.icm, eps/pdf produce better result when you set this before loading.
Dim settings As New MagickReadSettings()
settings.ColorSpace = ColorSpace.sRGB

' Create empty image
Using image As New MagickImage()
    ' Reads the eps image, the specified settings tell Ghostscript to create an sRGB image
    image.Read("Snakeware.eps", settings)
    ' Save image as tiff
    image.Write("Snakeware.tiff")
End Using

' Read image from file
Using image As New MagickImage("Snakeware.jpg")
    ' First add a CMYK profile if your image does not contain a color profile.
    image.AddProfile(ColorProfile.USWebCoatedSWOP)

    ' Adding the second profile will transform the colorspace from CMYK to RGB
    image.AddProfile(ColorProfile.SRGB)
    ' Save image as png
    image.Write("Snakeware.png")
End Using

' Read image from file
Using image As New MagickImage("Snakeware.jpg")
    ' First add a CMYK profile if your image does not contain a color profile.
    image.AddProfile(ColorProfile.USWebCoatedSWOP)

    ' Adding the second profile will transform the colorspace from your custom icc profile
    image.AddProfile(New ColorProfile("YourProfile.icc"))
    ' Save image as tiff
    image.Write("Snakeware.tiff")
End Using
```