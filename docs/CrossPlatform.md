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

Getting Magick.NET working for legacy Framework targets under Mono requires an extra step.
As part of the NuGet restore for the relevant Magick.NET package, native libraries from are automatically copied to the project output,
but when you have a `net4*` target the SDK assumes that Windows native libraries are desired. On a Windows host, this isn't a problem.
The solution is to copy the correct library from the `runtimes` dir of the NuGet package, and optionally delete the incorrect versions,
by adding something like this to the `.csproj`:
```xml
<ItemGroup>
  <PackageReference Include="Magick.NET-Q8-AnyCPU" GeneratePathProperty="true" /> <!-- add `GeneratePathProperty` so `$(PkgMagick_NET-Q8-AnyCPU)` is usable -->
</ItemGroup>
<Target Name="PostBuild" AfterTargets="PostBuildEvent" Condition=" '$(OS)' != 'WINDOWS_NT' ">
  <ItemGroup>
    <ToDelete Include="$(OutputPath)Magick.Native-*.dll" />
    <ToCopy Include="$(PkgMagick_NET-Q8-AnyCPU)/runtimes/linux-x64/native/*.dll.so" /> <!-- change `linux-x64` to match host -->
  </ItemGroup>
  <Delete Files="@(ToDelete)" />
  <Copy SourceFiles="@(ToCopy)" DestinationFiles="@(ToCopy->'$(OutputPath)lib%(Filename)%(Extension)')" /> <!-- without a DllMap, Mono looks for e.g. `libmagick.dll.so` when a `[DllImport("magick.dll")]` is used, so this transforms the filename to the expected format -->
</Target>
```
