# Cross-platform

## Compiling Magick.NET.Native on Linux

This step is not required but documented to explain how the Magick.NET.Native is compiled on Linux. To build Magick.NET.Native it is required to have
a Linux build machine running. A docker file for this can be found in `Source/Magick.NET.CrossPlatform/ubuntu.16.04`. After changing the SSH-key
and starting the container the `Magick.NET.CrossPlatform.sln` solution should be opened. It is possible that this requires installation of some
extra components, instructions for that can be found [here](https://blogs.msdn.microsoft.com/vcblog/2016/03/30/visual-c-for-linux-development/).
After opening the solution the `Remote Build Machine` should be configured. The project could be build from the solution but it is easier to use
`Tools\BuildCrossPlatform.cmd` for this. This script will also copy the `.so` files to the `ImageMagick\(Q8/Q16/Q16-HDRI)\lib\Release\CrossPlatform`
folders.

## Compiling ImageMagick

Because Magick.NET is linked against the code of the ImageMagick github repository it is very likely that the Linux machine where Magick.NET should
run on is not using the correct version of ImageMagick. The steps below are required to build ImageMagick. This is an example for Ubuntu 16.04 so it
might be a bit different on another Linux distribution.

- Clone the Magick.NET repository from `https://github.com/dlemstra/Magick.NET.git`.
- Switch to the tag that has the same version as the version of Magick.NET (for example `git checkout tags/1.2.3.4`).
- Run `ImageMagick/Source/Checkout.sh` that will clone the ImageMagick repository.
- An optional step is `sudo apt-get build-dep imagemagick` that will install all the dependencies.
- Go to the folder `ImageMagick/Source/ImageMagick/ImageMagick` and run one of the following commands (depening on the quantum)
    - Q8: `./configure --with-magick-plus-plus=no --with-quantum-depth=8 --enable-hdri=no`
    - Q16: `./configure --with-magick-plus-plus=no --with-quantum-depth=16 --enable-hdri=no`
    - Q16-HDRI: `./configure --with-magick-plus-plus=no --with-quantum-depth=16`
- The next step is `make install` or `sudo make install`.

## Using Magick.NET on Linux

### .NET Core

Running Magick.NET with .NET Core on Linux requires adding the library as a package reference to the project:

```xml
  <ItemGroup>
    <PackageReference Include="Magick.NET-Q8-x64" Version="7.x.x.x" />
  </ItemGroup>
```

When running the project it is possible that the following error occurs:

```
Unhandled Exception: System.TypeInitializationException: The type initializer for 'NativeMagickSettings' threw an exception. --->
  System.DllNotFoundException: Unable to load DLL 'Magick.NET-Q8-x64.Native.dll': The specified module could not be found.
```

This is most likely because the `Magick.NET-Q8-x64.Native.dll.so` file cannot find the ImageMagick libraries. This can be checked with the following
command: `ldd Magick.NET-Q8-x64.Native.dll.so`:

```
linux-vdso.so.1 =>  (0x00007ffe95fea000)
libMagickCore-7.Q8.so.3 => not found
libMagickWand-7.Q8.so.3 => not found
libjpeg.so.8 => /usr/lib/x86_64-linux-gnu/libjpeg.so.8 (0x00007fc00888b000)
libm.so.6 => /lib/x86_64-linux-gnu/libm.so.6 (0x00007fc008582000)
libc.so.6 => /lib/x86_64-linux-gnu/libc.so.6 (0x00007fc0081b8000)
/lib64/ld-linux-x86-64.so.2 (0x00005568c223f000)
```

This can be resolved by setting `LD_LIBRARY_PATH`: `export LD_LIBRARY_PATH=$LD_LIBRARY_PATH:/path/to/ImageMagick/libraries`

### Mono

Getting Magick.NET working on Mono sometimes requires an extra step. The Magick.NET.Native library is not always automatically copied to the output
directory when the NuGet reference is added to the project. The copy does not happens when the project is build on Windows instead of a Linux machine.
The native library located in the folder `runtimes/linux-x64/native` of the NuGet package should then be copied to the output folder of the project.