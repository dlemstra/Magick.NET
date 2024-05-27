# Cross-platform

## Compiling Magick.NET.Native on Linux and macOS

The binaries for Linux and macOS are build on VSTS (https://dlemstra.visualstudio.com/Magick.NET/_build) with the help of scripts in
the `Source/Magick.NET.CrossPlatform` folder. The `Build.Linux.sh` and `Build.macOS.sh` scripts in that folder create a statically linked
 library. Cloning the Magick.NET repository and running those scripts on a Linux or macOS machine can be done to create a custom build of
 the native library.

## Adding support for other formats

Magick.NET uses a statically linked build of ImageMagick to allow for an extremely portable Linux binary. This means for most use cases,
it will "just work" on standard Linux distributions like Ubuntu or CentOS. Unfortunately, it also means some libraries and formats are
left unsupported where an incompatible license or other issue prevents it.

To create a custom build of the native library Magick.NET the `Build.Linux.sh` or the `Build.macOS.sh` file will need to be tweaked.

## Fonts

On Linux and macOS the `fontconfig` library is used to read fonts. This needs to be installed on the system or container that is running 
Magick.NET. It might also be required to run `fc-cache` to update the font cache.

### .NET Core

Running Magick.NET with .NET Core on Linux requires adding the library as a package reference to the project:

```xml
<ItemGroup>
  <PackageReference Include="Magick.NET-Q8-x64" Version="7.x.x.x" />
</ItemGroup>
```

### Native library is not copied to the bin folder

On some of the platforms it appears that the binaries are not copied to the output folder. This can be forced
by adding one of the following properties your `.csproj` file:

```xml
<PropertyGroup>
  <MagickCopyNativeWindows>true</MagickCopyNativeWindows>
  <MagickCopyNativeLinux>true</MagickCopyNativeLinux>
  <MagickCopyNativeLinuxMusl>true</MagickCopyNativeLinuxMusl>
  <MagickCopyNativeMacOS>true</MagickCopyNativeMacOS>
</PropertyGroup>
```

### Mono

With Mono there are sometimes some extra steps that need to be taken. When targeting `net4*` (.NET Framework)
the native library needs to have a `lib` prefix. This will be done automatically when for example the
`MagickCopyNativeLinux` property is set to `true`. But this also means that the Windows library will be copied.
So for Mono it is recommended to set the `MagickCopyNativeWindows` property to `false`.

```xml
<PropertyGroup>
  <MagickCopyNativeWindows>false</MagickCopyNativeWindows>
  <MagickCopyNativeLinux>true</MagickCopyNativeLinux>
</PropertyGroup>
```
