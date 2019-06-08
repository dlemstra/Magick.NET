# Reading images

## Read image

```C#
// Read from file.
using (MagickImage image = new MagickImage("Snakeware.jpg"))
{
}

// Read from stream.
using (MemoryStream memStream = LoadMemoryStreamImage())
{
    using (MagickImage image = new MagickImage(memStream))
    {
    }
}

// Read from byte array.
byte[] data = LoadImageBytes();
using (MagickImage image = new MagickImage(data))
{
}

// Read image that has no predefined dimensions.
MagickReadSettings settings = new MagickReadSettings();
settings.Width = 800;
settings.Height = 600;
using (MagickImage image = new MagickImage("xc:yellow", settings))
{
}

using (MagickImage image = new MagickImage())
{
    image.Read("Snakeware.jpg");
    image.Read(memStream);
    image.Read("xc:yellow", settings);

    using (MemoryStream memStream = LoadMemoryStreamImage())
    {
        image.Read(memStream);
    }
}
```

## Read basic image information:

```C#
// Read from file
MagickImageInfo info = new MagickImageInfo("Snakeware.jpg");

// Read from stream
using (MemoryStream memStream = LoadMemoryStreamImage())
{
    info = new MagickImageInfo(memStream);
}

// Read from byte array
byte[] data = LoadImageBytes();
info = new MagickImageInfo(data);

info = new MagickImageInfo();
info.Read("Snakeware.jpg");
using (MemoryStream memStream = LoadMemoryStreamImage())
{
    info.Read(memStream);
}
info.Read(data);

Console.WriteLine(info.Width);
Console.WriteLine(info.Height);
Console.WriteLine(info.ColorSpace);
Console.WriteLine(info.Format);
Console.WriteLine(info.Density.X);
Console.WriteLine(info.Density.Y);
Console.WriteLine(info.Density.Units);
```

## Read image with multiple layers/frames:

```C#
// Read from file
using (MagickImageCollection collection = new MagickImageCollection("Snakeware.gif"))
{
}

// Read from stream
using (MemoryStream memStream = LoadMemoryStreamImage())
{
    using (MagickImageCollection collection = new MagickImageCollection(memStream))
    {
    }
}

// Read from byte array
byte[] data = LoadImageBytes();
using (MagickImageCollection collection = new MagickImageCollection(data))
{
}

// Read pdf with custom density.
MagickReadSettings settings = new MagickReadSettings();
settings.Density = new Density(144);

using (MagickImageCollection collection = new MagickImageCollection("Snakeware.pdf", settings))
{
}

using (MagickImageCollection collection = new MagickImageCollection())
{
    collection.Read("Snakeware.jpg");
    using (MemoryStream memStream = LoadMemoryStreamImage())
    {
        collection.Read(memStream);
    }
    collection.Read(data);
    collection.Read("Snakeware.pdf", settings);
}
```