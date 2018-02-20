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
build context. Then `docker build` and start a fresh container from the result. This will create an Ubuntu 16.04 container with a fresh copy of ImageMagick 
and various image libraries, ready to support building Magick.NET.Native from Visual Studio or the BuildCrossPlatform script.

After adding a project to the ImageMagick build, you will also need to add it to the Magick.NET.CrossPlatform project. In Visual Studio, this is configured in 
Project Properties->Linker->Input, or look for these sections in `Magick.NET.CrossPlatform.vcxproj`:

```xml
    <Link>
      <LibraryDependencies>pthread</LibraryDependencies>
      <AdditionalDependencies Condition="'$(Configuration)|$(Platform)'=='ReleaseQ8|x64'">$(StlAdditionalDependencies);%(AdditionalDependencies);/usr/local/lib/libMagickWand-7.Q8.a;/usr/local/lib/libMagickCore-7.Q8.a;/usr/local/lib64/libjpeg.a;/usr/local/lib/libpng16.a;/usr/local/lib/libtiff.a;/usr/local/lib/libwebp.a;/usr/local/lib/libwebpmux.a;/usr/local/lib/libwebpdemux.a;/usr/local/lib/libz.a;/usr/local/lib/liblzma.a;/usr/local/lib/libbz2.a</AdditionalDependencies>
      <AdditionalDependencies Condition="'$(Configuration)|$(Platform)'=='ReleaseQ16|x64'">$(StlAdditionalDependencies);%(AdditionalDependencies);/usr/local/lib/libMagickWand-7.Q16.a;/usr/local/lib/libMagickCore-7.Q16.a;/usr/local/lib64/libjpeg.a;/usr/local/lib/libpng16.a;/usr/local/lib/libtiff.a;/usr/local/lib/libwebp.a;/usr/local/lib/libwebpmux.a;/usr/local/lib/libwebpdemux.a;/usr/local/lib/libz.a;/usr/local/lib/liblzma.a;/usr/local/lib/libbz2.a</AdditionalDependencies>
      <AdditionalDependencies Condition="'$(Configuration)|$(Platform)'=='ReleaseQ16-HDRI|x64'">$(StlAdditionalDependencies);%(AdditionalDependencies);/usr/local/lib/libMagickWand-7.Q16HDRI.a;/usr/local/lib/libMagickCore-7.Q16HDRI.a;/usr/local/lib64/libjpeg.a;/usr/local/lib/libpng16.a;/usr/local/lib/libtiff.a;/usr/local/lib/libwebp.a;/usr/local/lib/libwebpmux.a;/usr/local/lib/libwebpdemux.a;/usr/local/lib/libz.a;/usr/local/lib/liblzma.a;/usr/local/lib/libbz2.a</AdditionalDependencies>
    </Link>
```

## Adding support for other formats

Magick.NET uses a statically linked build of ImageMagick to allow for an extremely portable Linux binary. This means for most use cases, it will "just work" on
standard glibc-based Linux distributions like Ubuntu or CentOS. Unfortunately, it also means some libraries and formats are left unsupported where an incompatible
license or other issue prevents it.

If you wish to create a custom build of Magick.NET for those situations, there are a few steps to follow. If, for example, you want to build Magick.NET with
support for TIFF, JPEG, and SVG using shared objects, you could simple add an extra install step to `ImageMagick/Source/Dockerfile`:

```Dockerfile
RUN apt-get install -y librsvg2-dev libxml2-dev libfreetype6-dev libtiff5-dev libjpeg-turbo8-dev
```

Remove the steps to copy and build delegate libraries from source, and update the ImageMagick configure options.

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