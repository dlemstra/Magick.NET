# Cross-platform

## Compiling Magick.NET.Native on Linux and macOS

The binaries for Linux and macOS are build on VSTS (https://dlemstra.visualstudio.com/Magick.NET/_build) with the help of scripts in the `Source/Magick.NET.CrossPlatform`
folder. The `Build.Linux.sh` and `Build.macOS.sh` scripts in that folder create a statically linked library. Cloning the Magick.NET repository and running those scripts
on a Linux or macOS machine can be done to create a custom build of the native library.

## Adding support for other formats

Magick.NET uses a statically linked build of ImageMagick to allow for an extremely portable Linux binary. This means for most use cases, it will "just work" on
standard Linux distributions like Ubuntu or CentOS. Unfortunately, it also means some libraries and formats are left unsupported where an incompatible license
or other issue prevents it.

To create a custom build of the native library Magick.NET the `Build.Linux.sh` or the `Build.macOS.sh` file will need to be tweaked.

## Fonts

On Linux and macOS the `fontconfig` library is used to read fonts. This needs to be installed on the system or container that is running Magick.NET. It might also be
required to run `fc-cache` to update the font cache.

### .NET Core

Running Magick.NET with .NET Core on Linux requires adding the library as a package reference to the project:

```xml
  <ItemGroup>
    <PackageReference Include="Magick.NET-Q8-x64" Version="7.x.x.x" />
  </ItemGroup>
```

### Mono

Getting Magick.NET working on Mono sometimes requires an extra step. The Magick.NET.Native library is not always automatically copied to the output
directory when the NuGet reference is added to the project. The copy does not happens when the project is build on Windows instead of a Linux machine.
The native library located in the folder `runtimes/linux-x64/native` of the NuGet package should then be copied to the output folder of the project.
And it is possible that you will need to rename the native library and prefix it with `lib` (e.g `libMagick.NET-Q8-x64.Native.dll.so`).
