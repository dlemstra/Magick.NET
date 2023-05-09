# Convert PDF

## Installation

You need to install the latest version of [GhostScript](https://www.ghostscript.com/download/gsdnld.html) before you can
convert a pdf using Magick.NET.

Make sure you only install the version of GhostScript with the same platform. If you use the 64-bit version of Magick.NET
you should also install the 64-bit version of Ghostscript. You can use the 32-bit version together with the 64-version but
you will get a better performance if you keep the platforms the same.

## Convert PDF to multiple images

```C#
// Settings the density to 300 dpi will create an image with a better quality
var settings = new MagickReadSettings
{
    Density = new Density(300, 300)
};

using var images = new MagickImageCollection();

// Add all the pages of the pdf file to the collection
images.Read(SampleFiles.SnakewarePdf, settings);

var page = 1;
foreach (var image in images)
{
    // Write page to file that contains the page number
    image.Write("Snakeware.Page" + page + ".png");
    // Writing to a specific format works the same as for a single image
    image.Format = MagickFormat.Ptif;
    image.Write("Snakeware.Page" + page + ".tif");
    page++;
}
```

## Convert PDF to one image

```C#
// Settings the density to 300 dpi will create an image with a better quality
var settings = new MagickReadSettings
{
    Density = new Density(300)
};

using var images = new MagickImageCollection();

// Add all the pages of the pdf file to the collection
images.Read(SampleFiles.SnakewarePdf, settings);

// Create new image that appends all the pages horizontally
using var horizontal = images.AppendHorizontally();

// Save result as a png
horizontal.Write("Snakeware.horizontal.png");

// Create new image that appends all the pages vertically
using var vertical = images.AppendVertically();

// Save result as a png
vertical.Write("Snakeware.vertical.png");
```

## Create a PDF from two images

```C#
using var images = new MagickImageCollection();

// Add first page
images.Add(new MagickImage(SampleFiles.SnakewareJpg));
// Add second page
images.Add(new MagickImage(SampleFiles.SnakewareJpg));

// Create pdf file with two pages
images.Write("Snakeware.pdf");
```

## Create a PDF from a single image

```C#
// Read image from file
using var image = new MagickImage(SampleFiles.SnakewareJpg);

// Create pdf file with a single page
image.Write("Snakeware.pdf");
```

## Read a single page from a PDF

```C#
var settings = new MagickReadSettings
{
    FrameIndex = 0, // First page
    FrameCount = 1, // Number of pages
};

using var images = new MagickImageCollection();

// Read only the first page of the pdf file
images.Read(SampleFiles.SnakewarePdf, settings);

// Clear the collection
images.Clear();

settings.FrameCount = 2; // Number of pages

// Read the first two pages of the pdf file
images.Read(SampleFiles.SnakewarePdf, settings);
```
