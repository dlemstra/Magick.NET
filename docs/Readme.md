# Documentation

## Installation

You have two options to get the Magick.NET binaries in your project:
- Use NuGet:
  - Right click on the references of your project and choose 'Manage NuGet packages'.
  - Search for Magick.NET and choose the package that uses the platform of your choice: x64/arm64/x86 or AnyCPU.
- Build Magick.NET yourself:
  - Instructions on how to build Magick.NET can be found [here](../BUILDING.md).

For some formats additional software needs to be installed:
- AI/EPS/PDF/PS requires [Ghostscript](https://www.ghostscript.com/download/gsdnld.html).
- Video (AVI/MP4/etc..) requires [FFmpeg](https://ffmpeg.org/).

## Q8, Q16 or Q16-HDRI?

Versions with Q8 in the name are 8 bits-per-pixel component (e.g. 8-bit red, 8-bit green, etc.), whereas, Q16 are 16 bits-per-pixel component.
A Q16 version permits you to read or write 16-bit images without losing precision but requires twice as much resources as the Q8 version.
The Q16-HDRI version uses twice the amount of memory as the Q16. It is more precise because it uses a floating point (32 bits-per-pixel component)
and it allows out-of-bound pixels (less than 0 and more than 65535). The Q8 version is the recommended version. If you need to read/write images
with a better quality you should use the Q16 version instead.

## .NET Standard / .NET Core

Starting with version 7.0.0.0102 support for .NET Standard was added. This used to be a separate NuGet package but starting with 7.0.6.0
this became part of the normal NuGet package. On Windows this works without any extra steps but on Linux this will require some extra work.
Instructions for the cross-platform build can be found [here](CrossPlatform.md).

## ImageMagick 7

Magick.NET is linked with ImageMagick 7. Most examples on the Internet use ImageMagick 6 so there could be some minor differences in syntax.
The biggest difference between 6 and 7 is that the latter uses Alpha instead of Opacity. You can find some more information about ImageMagick 7
here: https://imagemagick.org/script/porting.php.

## Default memory limit

On Windows ImageMagick uses only 50% of the available memory by default for pixel cache. If the application where Magick.NET is being used is
the only application that uses memory it would be wise to make that limit higher. This can be done by calling `ResourceLimits.LimitMemory`
with the percentage of total memory that should be used. The maximum value that should be used would be around 80/90%. When ImageMagick reaches
the memory limit, it will not stop functioning. Instead it will use temporary page files to compensate for the memory limit.

## OpenMP

Magick.NET does not use OpenMP because the C++ Redistributable is statically linked and the OpenMP library cannot be statically linked. OpenMP is
used to perform multithreaded operation on an image to increase the performance. The best use case for this would be a standalone desktop application.
Starting with Magick.NET 7.0.7.700 there are extra packages for the x64 build of Magick.NET that have OpenMP support.

## Ghostscript

You only need to install Ghostscript if you want to convert EPS/PDF/PS files. Make sure you only install the version of GhostScript with the same
platform. If you use the 64-bit version of Magick.NET you should also install the 64-bit version of Ghostscript. You can use the 32-bit version
together with the 64-version but you will get a better performance if you keep the platforms the same. Ghostscript can be downloaded here:
https://ghostscript.com/releases/gsdnld.html. If you don't want to install Ghostscript on your machine you can copy `gsdll32.dll/gsdl64.dll` and
`gswin32c.exe/gswin64c.exe` to your server and tell Magick.NET where the file is located with the code below.

```C#
MagickNET.SetGhostscriptDirectory(@"C:\MyProgram\Ghostscript");
```

_Be aware that you need a [license](https://ghostscript.com/licensing/) if you want to use Ghostscript commercially._

## Initialization

Magick.NET uses ImageMagick and this will be initialized the first time you using something that uses ImageMagick. You can however decide to initialize ImageMagick on startup of your application and for example use your own xml configuration files. Documentation about the initialization can be found [here](Initialization.md).

## Temporary directory

ImageMagick sometimes needs to write temporary files to the hard drive. The default folder for this is `%TEMP%` but the folder can be changed with
the following code:

```C#
MagickNET.SetTempDirectory(@"C:\MyProgram\MyTempFiles");
```

## Examples

Below here you can find some examples on how to use Magick.NET. Because Magick.NET comes with a xml documentation file IntelliSense will also
provide you with some help on how to use Magick.NET.

- [Reading images](ReadingImages.md)
  - Read image
  - Read basic image information
  - Read image (with multiple layers/frames)
- [Resize image](ResizeImage.md)
  - Resize animated gif
  - Resize to a fixed size
- [Exception handling](ExceptionHandling.md)
  - Exception handling
  - Obtain warning that occurred during reading
- [Convert image](ConvertImage.md)
  - Convert image from one format to another
  - Convert CMYK to RGB
- [Combining images](CombiningImages.md)
  - Merge multiple images
  - Create animated gif
- [Convert PDF](ConvertPDF.md)
  - Convert PDF to multiple images
  - Convert PDF to one image
  - Create a PDF file from two images
  - Create a PDF file from a single image
  - Read a single page from a PDF file
- [Command line option -define](Defines.md)
  - Command line option -define
  - Defines that need to be set before reading an image
- [Using colors](UsingColors.md)
- [Watermark](Watermark.md)
- [Exif data](ExifData.md)
  - Read exif data
  - Create thumbnail from exif data
- [Read raw thumbnail](ReadRawThumbnail.md)
  - Read thumbnail from raw image
- [Lossless compression](LosslessCompression.md)
  - Lossless compress JPEG logo
- [Detailed debug information](DetailedDebugInformation.md)
  - Get detailed debug information from ImageMagick
- [Drawing](Drawing.md)
  - Draw text

## More documentation
For some great ImageMagick examples please visit the following page: https://imagemagick.org/Usage. Create a [new issue](https://github.com/dlemstra/Magick.NET/issues)
if you need help to change one of these examples into code.
