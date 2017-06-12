# Read raw image from camera

## Installation
You need to put the executable `dcraw.exe` into the directory that contains the Magick.NET dll. The zip file `ImageMagick-7.X.X-X-Q16-x86-windows.zip`
that you can download from http://www.imagemagick.org/script/binary-releases.php#windows contains this file.

## Convert CR2 to JPG

#### C#
```C#
using (MagickImage image = new MagickImage("StillLife.CR2"))
{
    image.Write("StillLife.jpg");
}
```

#### VB.NET
```VB.NET
Using image As New MagickImage("StillLife.CR2")
    image.Write("StillLife.jpg")
End Using
```