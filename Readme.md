# The .NET library for ImageMagick: Magick.NET

[![Build Status](https://github.com/dlemstra/Magick.NET/workflows/main/badge.svg)](https://github.com/dlemstra/Magick.NET/actions)
[![GitHub license](https://img.shields.io/badge/license-Apache%202-green.svg)](https://raw.githubusercontent.com/dlemstra/Magick.NET/main/License.txt)
[![Twitter URL](https://img.shields.io/badge/twitter-follow-1da1f2.svg)](https://twitter.com/MagickNET)
[![Donate](https://img.shields.io/badge/%24-donate-ff00ff.svg)](https://github.com/sponsors/dlemstra)

ImageMagick is a powerful image manipulation library that supports over [100 major file formats](https://imagemagick.org/script/formats.php) (not including sub-formats).
With Magick.NET you can use ImageMagick in your C#/VB.NET/.NET Core application without having to install ImageMagick on your server or desktop.

## Documentation

For examples on how to install and use Magick.NET visit the [documentation](docs/Readme.md) page.
For more information about ImageMagick go to: [http://www.imagemagick.org/](http://www.imagemagick.org/).

## Supported platforms

|.NET Version|Platform|Platform specific|AnyCPU|OpenMP|
|-|-|:-:|:-:|:-:|
|**.NET Standard 2.1**|windows (x64)|✅|✅|✅|
||windows (arm64)|✅|✅|✅|
||linux (x64)|✅|✅|✅|
||linux-arm64 (arm64)|✅|✅|✅|
||linux-musl (x64)|✅|✅|✅|
||macOS (x64)|✅|✅|❌|
||macOS (arm64)|✅|✅|❌|
||windows (x86)|✅|✅|❌|
|**.NET Standard 2.0**|windows (x64)|✅|✅|✅|
||windows (arm64)|✅|✅|✅|
||linux (x64)|✅|✅|✅|
||linux-arm64 (arm64)|✅|✅|✅|
||linux-musl (x64)|✅|✅|✅|
||macOS (x64)|✅|✅|❌|
||macOS (arm64)|✅|✅|❌|
||windows (x86)|✅|✅|❌|

More information about Linux and macOS can be found [here](docs/CrossPlatform.md).

## Download

Magick.NET can be added to a project with one of the following NuGet packages:

|Platform|Quantum|Platform specific|AnyCPU|OpenMP|
|-|-|:-:|:-:|:-:|
|**windows (x64)**|Q8|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q8-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q8-x64/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q8-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q8-AnyCPU/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q8-OpenMP-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q8-OpenMP-x64/)|
||Q16|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-x64/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q16-AnyCPU/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-OpenMP-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-OpenMP-x64/)|
||Q16-HDRI|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-HDRI-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-x64/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-HDRI-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-AnyCPU/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-HDRI-OpenMP-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-OpenMP-x64/)|
|**windows (arm64)**|Q8|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q8-arm64.svg)](https://www.nuget.org/packages/Magick.NET-Q8-arm64/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q8-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q8-AnyCPU/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q8-OpenMP-arm64.svg)](https://www.nuget.org/packages/Magick.NET-Q8-OpenMP-arm64/)|
||Q16|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-arm64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-arm64/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q16-AnyCPU/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-OpenMP-arm64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-OpenMP-arm64/)|
||Q16-HDRI|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-HDRI-arm64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-arm64/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-HDRI-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-AnyCPU/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-HDRI-OpenMP-arm64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-OpenMP-arm64/)|
|**linux (x64)**|Q8|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q8-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q8-x64/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q8-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q8-AnyCPU/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q8-OpenMP-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q8-OpenMP-x64/)|
||Q16|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-x64/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q16-AnyCPU/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-OpenMP-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-OpenMP-x64/)|
||Q16-HDRI|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-HDRI-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-x64/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-HDRI-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-AnyCPU/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-HDRI-OpenMP-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-OpenMP-x64/)|
|**linux-arm64 (arm64)**|Q8|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q8-arm64.svg)](https://www.nuget.org/packages/Magick.NET-Q8-arm64/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q8-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q8-AnyCPU/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q8-OpenMP-arm64.svg)](https://www.nuget.org/packages/Magick.NET-Q8-OpenMP-arm64/)|
||Q16|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-arm64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-arm64/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q16-AnyCPU/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-OpenMP-arm64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-OpenMP-arm64/)|
||Q16-HDRI|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-HDRI-arm64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-arm64/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-HDRI-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-AnyCPU/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-HDRI-OpenMP-arm64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-OpenMP-arm64/)|
|**linux-musl (x64)**|Q8|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q8-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q8-x64/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q8-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q8-AnyCPU/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q8-OpenMP-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q8-OpenMP-x64/)|
||Q16|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-x64/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q16-AnyCPU/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-OpenMP-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-OpenMP-x64/)|
||Q16-HDRI|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-HDRI-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-x64/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-HDRI-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-AnyCPU/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-HDRI-OpenMP-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-OpenMP-x64/)|
|**macOS (x64)**|Q8|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q8-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q8-x64/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q8-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q8-AnyCPU/)|❌|
||Q16|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-x64/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q16-AnyCPU/)|❌|
||Q16-HDRI|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-HDRI-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-x64/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-HDRI-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-AnyCPU/)|❌|
|**macOS (arm64)**|Q8|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q8-arm64.svg)](https://www.nuget.org/packages/Magick.NET-Q8-arm64/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q8-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q8-AnyCPU/)|❌|
||Q16|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-arm64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-arm64/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q16-AnyCPU/)|❌|
||Q16-HDRI|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-HDRI-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-x64/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-HDRI-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-AnyCPU/)|❌|
|**windows (x86)**|Q8|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q8-x86.svg)](https://www.nuget.org/packages/Magick.NET-Q8-x86/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q8-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q8-AnyCPU/)|❌|
||Q16|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-x86.svg)](https://www.nuget.org/packages/Magick.NET-Q16-x86/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q16-AnyCPU/)|❌|
||Q16-HDRI|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-HDRI-x86.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-x86/)|[![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-HDRI-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-AnyCPU/)|❌|

Follow me on twitter ([@MagickNET](https://twitter.com/MagickNET)) to receive information about new downloads and changes to Magick.NET and ImageMagick.

## Extra libraries

Besides the quantum specific packages there are also some extra libraries in this project. One of these libraries is the [Magick.NET.Core](https://www.nuget.org/packages/Magick.NET.Core/) library that is a dependency of the quantum specific packages. This library can be used to add extra functionality and interact with the Magick.NET libraries. Two examples are [Magick.NET.SystemDrawing](https://www.nuget.org/packages/Magick.NET.SystemDrawing/) and [Magick.NET.SystemWindowsMedia](https://www.nuget.org/packages/Magick.NET.SystemWindowsMedia/) that can be used to interact with `System.Drawing` and `System.Windows.Media`.

## Development build

Every commit to Magick.NET is automatically build and tested with the help of [GitHub Actions](https://github.com/features/actions). This build also includes the creation of a NuGet package. These packages can be downloaded here: [https://github.com/dlemstra/Magick.NET/actions](https://github.com/dlemstra/Magick.NET/actions). It is not recommended to use this build in a production environment.

## Versioning

Magick.NET uses [semantic versioning](https://semver.org/#semantic-versioning-200).

## Donate

If you have an uncontrollable urge to give me something for the time and effort I am putting into this project then please sponsor me through [GitHub Sponsors](https://github.com/sponsors/dlemstra) or send me an [amazon gift card](https://www.amazon.de/Amazon-Gutschein-per-E-Mail-Amazon/dp/B0054PDOV8).
If you prefer to use PayPal then [click here](https://www.paypal.me/DirkLemstra).

----
_A special thanks goes out to [Snakeware](https://www.snakeware.nl) who let me spend company time on this project._

## Sponsors

<img src="https://github.com/dlemstra/dlemstra/raw/main/sponsors/microsoft.svg" width="128" height="128" alt="Microsoft" title="Microsoft" />

[Microsoft's Free and Open Source Software Fund (FOSS Fund #20 June, 2024)](https://aka.ms/microsoftfossfund)

<img src="https://github.com/dlemstra/dlemstra/raw/main/sponsors/aws.svg" width="128" height="128" alt="Amazon Web Services" title="Amazon Web Services" />

[.NET on AWS Open Source Software Fund (July 2024)](https://github.com/aws/dotnet-foss)
