# Building Magick.NET

This document describes the requirements and instructions to build Magick.NET on your own machine.

### Requirements

- Visual Studio 2019 (community)
- Powershell (>= 3.0)
- Git for Windows (>= 2.7.2)
- Windows SDK (>= 10.0.10586)

### Install Magick.Native

Magick.NET uses the [Magick.Native](https://github.com/dlemstra/Magick.Native) library and that needs to be installed first. This can be done
with the file `install.cmd` inside the [src/Magick.Native](src/Magick.Native) folder. The script will install the version that is specified in
the file [Magick.Native.version](src/Magick.Native/Magick.Native.version). Currently this file is downloaded from DropBox so it is not easy to
change the version inside the repository. Once GitHub Package Registory for NuGet packages is really public without requiring to login it will
be possible to change the version inside the repository.

### Debugging Magick.Native

This project also includes some files that make it easy to debug the [Magick.Native](https://github.com/dlemstra/Magick.Native) library.
For this to be possible the Magick.Native library needs to be cloned out at the same level as the Magick.NET library. If Magick.NET is cloned in
`C:\Projects\Magick.NET` the library needs to be cloned into `C:\Projects\Magick.Native`. Before executing one of the  build files inside this project
ImageMagick needs to be cloned in the Magick.Native project. This can be done with the file `Magick.Native\src\ImageMagick\checkout.cmd`.
After the library is cloned one of the scripts inside [src/Magick.Native/build](src/Magick.Native/build]) can be used to do create a debug build
that can be used to debug the native code through this project.