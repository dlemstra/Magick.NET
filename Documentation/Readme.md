# Documentation

## Installation

You have two options to get the Magick.NET binaries in your project:
- Use the zip files: 
  - Download the latest [release](https://github.com/dlemstra/Magick.NET/releases) and unpack the files for your .NET version and platform into
    the bin directory of your project.
  - Add a reference to Magick.NET-(Q8/Q16/Q16-HDRI).dll.
 - Use NuGet:
   - Right click on the references of your project and choose 'Manage NuGet packages'. 
   - Search for Magick.NET and choose the package that uses the platform of your choice: x86/x64 or AnyCPU.

If you want to be able to read an AI/EPS/PDF/PS file you need to install [Ghostscript](https://www.ghostscript.com/download/gsdnld.html).

## Q8, Q16 or Q16-HDRI?

Versions with Q8 in the name are 8 bits-per-pixel component (e.g. 8-bit red, 8-bit green, etc.), whereas, Q16 are 16 bits-per-pixel component.
A Q16 version permits you to read or write 16-bit images without losing precision but requires twice as much resources as the Q8 version.
The Q16-HDRI version uses twice the amount of memory as the Q16. It is more precise because it uses a floating point (32 bits-per-pixel component)
and it allows out-of-bound pixels (less than 0 and more than 65535). The Q8 version is the recommended version. If you need to read/write images
with a better quality you should use the Q16 version instead.

## AnyCPU

The AnyCPU version of Magick.NET is designed to allow your application to be used in a 32 bit or a 64 bit environment. When one of the classes
in the library is used it will detect if the application pool is 32 or 64 bit. It will then read the x86 or the x64 version of the dll from an
embedded resource. This resource is written to a temporary directory to improve the start up time the next it is used. You can change the
directory that is used with the `CacheDirectory` property of the `MagickAnyCPU` class when the default directory is causing issues in your
production environment.

```C#
MagickAnyCPU.CacheDirectory = @"C:\MyProgram\MyTempDir";
```

If you are planning to read RAW files you should configure the folder and copy the dcraw executable to that folder. If you don't want to do
this you will need to add the folder that contains the executable to your %PATH%. More information about reading RAW files can be found here:
[Read raw image from camera](ReadRawImageFromCamera.md).

## .NET Standard

Starting with version 7.0.0.0102 support for .NET Standard 1.3 was added. This currently only works on Windows. This used to be a separate
NuGet package but starting with 7.X.X.X this became part of the normal NuGet package.

## ImageMagick 7

Magick.NET is linked with ImageMagick 7. Most examples on the Internet use ImageMagick 6 so there could be some minor differences in syntax.
The biggest difference between 6 and 7 is that the latter uses Alpha instead of Opacity. You can find some more information about ImageMagick 7
here: https://www.imagemagick.org/script/porting.php.

## Ghostscript

You only need to install Ghostscript if you want to convert EPS/PDF/PS files. Make sure you only install the version of GhostScript with the same
platform. If you use the 64-bit version of Magick.NET you should also install the 64-bit version of Ghostscript. You can use the 32-bit version
together with the 64-version but you will get a better performance if you keep the platforms the same. Ghostscript can be downloaded here:
http://www.ghostscript.com/download/gsdnld.html. If you don't want to install Ghostscript on your machine you can copy `gsdll32.dll/gsdl64.dll` and
`gswin32c.exe/gswin64c.exe` to your server and tell Magick.NET where the file is located with the code below.

```C#
MagickNET.SetGhostscriptDirectory(@"C:\MyProgram\Ghostscript");
```

_Be aware that you need a [license](https://www.ghostscript.com/doc/current/Commprod.htm) if you want to use Ghostscript commercially._

## Initialization

Because Magick.NET embeds all the ImageMagick files __you don't need to initialize__ the library. You can however decide to use your own xml
configuration files. See below for an example:

```C#
using System;
using ImageMagick;

namespace MagickExample
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            MagickNET.Initialize(@"C:\MyProgram\MyImageMagickXmlFiles");
        }
    }
}
```

## Temporary directory

ImageMagick sometimes needs to write temporary files to the hard drive. The default folder for this is `%TEMP%` but the folder can be changed with
the following code:

```C#
MagickNET.SetTempDirectory(@"C:\MyProgram\MyTempFiles");
```