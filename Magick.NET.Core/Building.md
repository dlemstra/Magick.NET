#Building Magick.NET.Core

This document describes the requirements and instructions to build Magick.NET.Core on your own machine.

### Requirements

- Same as Magick.NET (see Building.md in root)
- .NET Core ([https://dotnet.github.io/getting-started/](https://dotnet.github.io/getting-started/))

### Build Magick.NET

Magick.NET.Core uses the native library from the Magick.NET project.
You can find the building instructions in `Building.md` in the root.
Both x86 and x64 need to be compiled for the Quantum you prefer (Q8/Q16/Q16-HDRI).
The rest of the document assumes you chose the Q8 version.

### Install CoreCLR

To install CoreCLR run `Magick.NET.Core\Install.cmd`.

### Build Magick.NET.Core.Native

Now you can build the native library that currently only supports .NET Core on Windows.
To build the project run `Magick.NET.Core\Magick.NET.Core-Q8.Native\Build.cmd`.

### Build Magick.NET.Core

The last step is building and testing the Magick.NET.Core library.
To build and test the project run `Magick.NET.Core\Magick.NET.Core-Q8\Build.cmd`.