# Cross-platform

## Compiling Magick.NET.Native on Linux

This step is not required but documented to explain how the Magick.NET.Native is compiled on Linux. To build Magick.NET.Native it is required to have
a Linux build machine running. A docker file for this can be found in `ImageMagick/Source`. The docker build requires that ImageMagick sources be checked out
on your machine, as described in `Building.md`. After adding your public ssh key to `ImageMagick/Source/authorized_keys`, running `docker build`,
and starting the container, the `Magick.NET.CrossPlatform.sln` solution should be opened. It is possible that this requires installation 
of some extra components, instructions for that can be found [here](https://blogs.msdn.microsoft.com/vcblog/2016/03/30/visual-c-for-linux-development/).
After opening the solution the `Remote Build Machine` should be configured. The project could be built from the solution but it is easier to use
`Tools\BuildCrossPlatform.cmd` for this. This script will also copy the `.so` files to the `ImageMagick\(Q8/Q16/Q16-HDRI)\lib\Release\CrossPlatform`
folders.

## Compiling ImageMagick

Building the dockerfile referenced above is the simplest way to get a fresh build of ImageMagick itself. The dockerfile includes configuration and compilation 
steps for ImageMagick and it's dependencies, and the Magick.NET.CrossPlatform project will link against that build. To build against a different version of 
ImageMagick or a particular image library, simply update the relevant project under `ImageMagick/Source` or update build options in `ImageMagick/Source/Dockerfile`.
If you are adding additional projects to the build, make sure you also add lines to `ImageMagick/Source/.dockerignore` to ensure they are included in the docker
build context.

Then rerun `docker build` and start a fresh container from the result. This will create an Ubuntu 16.04 container with a fresh copy of ImageMagick and various 
image libraries, ready to support building Magick.NET.Native from Visual Studio or the BuildCrossPlatform script.

## Extra requirements

By default, Magick.NET has support built in for JPEG, TIFF, PNG, and WebP file formats. Lossless compression support includes zlib, lzma, and bzip2. There
are no external dependencies beyond basic system libraries and glibc6/libm, which should be present by default on most Linux distros. 

Distributions based on musl libc such as Alpine will not have a glibc available, and will not work with Magick.NET out of the box. Compatibility layers for 
glibc on musl-libc systems exist, but have not been tested successfully with Magick.NET. YMMV.

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
