# The .NET library for ImageMagick: Magick.NET

[![Build Status](https://github.com/dlemstra/Magick.NET/workflows/master/badge.svg)](https://github.com/dlemstra/Magick.NET/actions)
[![codecov](https://codecov.io/gh/dlemstra/Magick.NET/branch/master/graph/badge.svg)](https://codecov.io/gh/dlemstra/Magick.NET)
[![GitHub license](https://img.shields.io/badge/license-Apache%202-green.svg)](https://raw.githubusercontent.com/dlemstra/Magick.NET/master/License.txt)
[![Gitter](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/Magick-NET/Lobby?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
[![Twitter URL](https://img.shields.io/badge/twitter-follow-1da1f2.svg)](https://twitter.com/MagickNET)
[![Donate](https://img.shields.io/badge/%24-donate-ff00ff.svg)](https://github.com/sponsors/dlemstra)

ImageMagick is a powerful image manipulation library that supports over [100 major file formats](https://www.imagemagick.org/script/formats.php) (not including sub-formats).
With Magick.NET you can use ImageMagick in your C#/VB.NET/.NET Core application without having to install ImageMagick on your server or desktop.

## Supported Platforms

- .NET Framework (2.0 and higher)
- .NET Core (.NETStandard 2.0 and higher on Windows, [Linux](docs/CrossPlatform.md) and [macOS](docs/CrossPlatform.md))

## Documentation

For examples on how to install and use Magick.NET visit the [documentation](docs/Readme.md) page.
For more information about ImageMagick go to: [http://www.imagemagick.org/](http://www.imagemagick.org/).

## Download

You can add Magick.NET to your project with one of the following NuGet packages:

| Package | Linux | macOS | Windows | Downloads
|-|-|-|-|-|
| [Magick.NET-Q8-x64](https://www.nuget.org/packages/Magick.NET-Q8-x64/) | [![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q8-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q8-x64/) | ![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q8-x64.svg) | [![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q8-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q8-x64/) | [![NuGet](https://img.shields.io/nuget/dt/Magick.NET-Q8-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q8-x64/)
| [Magick.NET-Q8-x86](https://www.nuget.org/packages/Magick.NET-Q8-x86/) | | | [![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q8-x86.svg)](https://www.nuget.org/packages/Magick.NET-Q8-x86/) | [![NuGet](https://img.shields.io/nuget/dt/Magick.NET-Q8-x86.svg)](https://www.nuget.org/packages/Magick.NET-Q8-x86/)
| [Magick.NET-Q8-AnyCPU](https://www.nuget.org/packages/Magick.NET-Q8-AnyCPU/) | [![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q8-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q8-AnyCPU/) | [![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q8-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q8-AnyCPU/) | [![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q8-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q8-AnyCPU/) | [![NuGet](https://img.shields.io/nuget/dt/Magick.NET-Q8-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q8-AnyCPU/)
| [Magick.NET-Q8-OpenMP-x64](https://www.nuget.org/packages/Magick.NET-Q8-OpenMP-x64/) | | | [![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q8-OpenMP-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q8-OpenMP-x64/) | [![NuGet](https://img.shields.io/nuget/dt/Magick.NET-Q8-OpenMP-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q8-OpenMP-x64/)
| [Magick.NET-Q16-x64](https://www.nuget.org/packages/Magick.NET-Q16-x64/) | [![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-x64/) | [![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-x64/) | [![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-x64/) | [![NuGet](https://img.shields.io/nuget/dt/Magick.NET-Q16-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-x64/)
| [Magick.NET-Q16-x86](https://www.nuget.org/packages/Magick.NET-Q16-x86/) | | | [![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-x86.svg)](https://www.nuget.org/packages/Magick.NET-Q16-x86/) | [![NuGet](https://img.shields.io/nuget/dt/Magick.NET-Q16-x86.svg)](https://www.nuget.org/packages/Magick.NET-Q16-x86/)
| [Magick.NET-Q16-AnyCPU](https://www.nuget.org/packages/Magick.NET-Q16-AnyCPU/) | [![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q16-AnyCPU/) | [![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q16-AnyCPU/) | [![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q16-AnyCPU/) | [![NuGet](https://img.shields.io/nuget/dt/Magick.NET-Q16-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q16-AnyCPU/)
| [Magick.NET-Q16-OpenMP-x64](https://www.nuget.org/packages/Magick.NET-Q16-OpenMP-x64/) | | | [![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-OpenMP-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-OpenMP-x64/) | [![NuGet](https://img.shields.io/nuget/dt/Magick.NET-Q16-OpenMP-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-OpenMP-x64/)
| [Magick.NET-Q16-HDRI-x64](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-x64/) | [![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-HDRI-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-x64/) | [![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-HDRI-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-x64/) | [![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-HDRI-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-x64/) | [![NuGet](https://img.shields.io/nuget/dt/Magick.NET-Q16-HDRI-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-x64/)
| [Magick.NET-Q16-HDRI-x86](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-x86/) | | | [![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-HDRI-x86.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-x86/) | [![NuGet](https://img.shields.io/nuget/dt/Magick.NET-Q16-HDRI-x86.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-x86/)
| [Magick.NET-Q16-HDRI-AnyCPU](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-AnyCPU/) | [![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-HDRI-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-AnyCPU/) | [![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-HDRI-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-AnyCPU/) | [![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-HDRI-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-AnyCPU/) | [![NuGet](https://img.shields.io/nuget/dt/Magick.NET-Q16-HDRI-AnyCPU.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-AnyCPU/)
| [Magick.NET-Q16-HDRI-OpenMP-x64](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-OpenMP-x64/) | | | [![NuGet](https://img.shields.io/nuget/v/Magick.NET-Q16-HDRI-OpenMP-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-OpenMP-x64/) | [![NuGet](https://img.shields.io/nuget/dt/Magick.NET-Q16-HDRI-OpenMP-x64.svg)](https://www.nuget.org/packages/Magick.NET-Q16-HDRI-OpenMP-x64/)


Follow me on twitter([@MagickNET](https://twitter.com/MagickNET)) to receive information about new downloads and changes to Magick.NET and ImageMagick.

## Development build

Every commit to Magick.NET is automatically build and tested with the help of [AppVeyor](http://www.appveyor.com). This build also includes the creation of a NuGet package.
These packages can be downloaded from the following NuGet feed: [https://ci.appveyor.com/nuget/Magick.NET](https://ci.appveyor.com/nuget/Magick.NET). It is not recommended to use
this build in a production environment.

## Versioning

Magick.NET uses the following: [versioning strategy](docs/Versioning.md).

## Donate

If you have an uncontrollable urge to give me something for the time and effort I am putting into this project then please buy me something from my
[amazon wish list](http://www.amazon.de/registry/wishlist/2XFZAC3J04WAY) or send me an [amazon gift card](https://www.amazon.de/Amazon-Gutschein-per-E-Mail-Amazon/dp/B0054PDOV8).
If you prefer to use PayPal then [click here](https://www.paypal.me/DirkLemstra). You can also sponsor me through [GitHub Sponsors](https://github.com/sponsors/dlemstra).

----
_A special thanks goes out to [Snakeware](https://www.snakeware.nl) who let me spend company time on this project._
