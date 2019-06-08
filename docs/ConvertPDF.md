# Convert PDF

## Installation

You need to install the latest version of [GhostScript](https://www.ghostscript.com/download/gsdnld.html) before you can
convert a pdf using Magick.NET.

Make sure you only install the version of GhostScript with the same platform. If you use the 64-bit version of Magick.NET
you should also install the 64-bit version of Ghostscript. You can use the 32-bit version together with the 64-version but
you will get a better performance if you keep the platforms the same.

## Convert PDF to multiple images

```C#
MagickReadSettings settings = new MagickReadSettings();
// Settings the density to 300 dpi will create an image with a better quality
settings.Density = new Density(300, 300);

using (MagickImageCollection images = new MagickImageCollection())
{
    // Add all the pages of the pdf file to the collection
    images.Read("Snakeware.pdf", settings);

    int page = 1;
    foreach (MagickImage image in images)
    {
        // Write page to file that contains the page number
        image.Write("Snakeware.Page" + page + ".png");
        // Writing to a specific format works the same as for a single image
        image.Format = MagickFormat.Ptif;
        image.Write("Snakeware.Page" + page + ".tif");    
        page++;
    }
}
```

## Convert PDF to one image

```C#
MagickReadSettings settings = new MagickReadSettings();
// Settings the density to 300 dpi will create an image with a better quality
settings.Density = new Density(300);

using (MagickImageCollection images = new MagickImageCollection())
{
    // Add all the pages of the pdf file to the collection
    images.Read("Snakeware.pdf", settings);

    // Create new image that appends all the pages horizontally
    using (IMagickImage horizontal = images.AppendHorizontally())
    {
        // Save result as a png
        horizontal.Write("Snakeware.horizontal.png");
    }

    // Create new image that appends all the pages vertically
    using (IMagickImage vertical = images.AppendVertically())
    {
        // Save result as a png
        vertical.Write("Snakeware.vertical.png");
    }
}
```

## Create a PDF from two images

```C#
using (MagickImageCollection collection = new MagickImageCollection())
{
    // Add first page
    collection.Add(new MagickImage("SnakewarePage1.jpg"));
    // Add second page
    collection.Add(new MagickImage("SnakewarePage2.jpg"));

    // Create pdf file with two pages
    collection.Write("Snakeware.pdf");
}
```

## Create a PDF from a single image

```C#
// Read image from file
using (MagickImage image = new MagickImage("Snakeware.jpg"))
{
    // Create pdf file with a single page
    image.Write("Snakeware.pdf");
}
```

## Read a single page from a PDF

```C#
using (MagickImageCollection collection = new MagickImageCollection())
{
    MagickReadSettings settings = new MagickReadSettings();
    settings.FrameIndex = 0; // First page
    settings.FrameCount = 1; // Number of pages

    // Read only the first page of the pdf file
    collection.Read("Snakeware.pdf", settings);

    // Clear the collection
    collection.Clear();

    settings.FrameCount = 2; // Number of pages

    // Read the first two pages of the pdf file
    collection.Read("Snakeware.pdf", settings);
}
```
