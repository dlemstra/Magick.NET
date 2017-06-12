# Reading images

## Read image

#### C#
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

### VB.NET
```VB.NET
' Read from file.
Using image As New MagickImage("Snakeware.jpg")
End Using

' Read from stream.
Using memStream As MemoryStream = LoadMemoryStreamImage()
    Using image As New MagickImage(memStream)
    End Using
End Using

' Read from byte array.
Dim data As Byte() = LoadImageBytes()
Using image As New MagickImage(data)
End Using

' Read image that has no predefined dimensions.
Dim settings As New MagickReadSettings()
settings.Width = 800
settings.Height = 600
Using image As New MagickImage("xc:yellow", settings)
End Using

Using image As New MagickImage()
    image.Read("Snakeware.jpg")
    image.Read(memStream)
    image.Read("xc:yellow", settings)

    Using memStream As MemoryStream = LoadMemoryStreamImage()
        image.Read(memStream)
    End Using
End Using
```

## Read basic image information:

#### C#
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

### VB.NET
```VB.NET
' Read from file
Dim info As New MagickImageInfo("Snakeware.jpg")

' Read from stream
Using memStream As MemoryStream = LoadMemoryStreamImage()
    info = New MagickImageInfo(memStream)
End Using

' Read from byte array
Dim data As Byte() = LoadImageBytes()
info = New MagickImageInfo(data)

info = New MagickImageInfo()
info.Read("Snakeware.jpg")
Using memStream As MemoryStream = LoadMemoryStreamImage()
    info.Read(memStream)
End Using
info.Read(data)

Console.WriteLine(info.Width)
Console.WriteLine(info.Height)
Console.WriteLine(info.ColorSpace)
Console.WriteLine(info.Format)
Console.WriteLine(info.Density.X);
Console.WriteLine(info.Density.Y);
Console.WriteLine(info.Density.Units);
```

## Read image with multiple layers/frames:

#### C#
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

### VB.NET
```VB.NET
' Read from file
Using collection As New MagickImageCollection("Snakeware.gif")
End Using

' Read from stream
Using memStream As MemoryStream = LoadMemoryStreamImage()
    Using collection As New MagickImageCollection(memStream)
    End Using
End Using

' Read from byte array
Dim data As Byte() = LoadImageBytes()
Using collection As New MagickImageCollection(data)
End Using

' Read pdf with custom density.
Dim settings As New MagickReadSettings()
settings.Density = New Density(144)

Using collection As New MagickImageCollection("Snakeware.pdf", settings)
End Using

Using collection As New MagickImageCollection()
    collection.Read("Snakeware.jpg")
    Using memStream As MemoryStream = LoadMemoryStreamImage()
      collection.Read(memStream)
    End Using
    collection.Read(data)
    collection.Read("Snakeware.pdf", settings)
End Using
```