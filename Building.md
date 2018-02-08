# Building Magick.NET

This document describes the requirements and instructions to build Magick.NET on your own machine.

### Requirements

- Visual Studio 2017 (Community)
- Powershell (>= 3.0)
- Git for Windows (>= 2.7.2)
- Windows SDK (>= 10.0.10586)
- AMD APP SDK (Optional for OpenCL support)

### Build ImageMagick 7

Magick.NET uses the ImageMagick 7 library and that needs to be build first. The easiest way is to copy the precompiled .lib files from
[DropBox](https://www.dropbox.com/sh/5m3zllq81n4eyhm/AACQFGl4PKi9xnd15EbU5S1Ia?dl=0) and copy them to the folder `ImageMagick\lib`.
Another option is building ImageMagick yourself. Below are the steps for that.

1. This step is optional but you can decide to use the latest GIT revision. To do this you need to edit the file `ImageMagick\Source\Checkout.sh`
   and change the `date` variable to the current date and time.
2. Then you need to run `ImageMagick\Source\Checkout.cmd` that will clone the master branch of the GIT repository of ImageMagick.
3. Now run `ImageMagick\Source\BuildAll.cmd` or one of the files in `ImageMagick\Source\Development` to target a specific platform and Quantum. If you see
   errors related to stdafx.h, check that "MFC and ATL support (x86 and x64)" is installed as part of Visual Studio 2017. If you see errors related to 
   new.h, you may need to install Windows SDK 10.0.10240.0 (or another pre-10586 version)

And with the files in the `ImageMagick\Source\Debug` folder you can create a debug build.

### Build Magick.NET (.NET 4.0/2.0 x86/x64)

You are now ready to build Magick.NET. You can do this by opening the `Magick.NET.sln` solution.
This solution contains various Release configurations (Q8/Q16/Q16-HDRI) to build Magick.NET with the Quantum that you prefer.
The unit tests can be build by switching to one of the Test configurations.

### Build Magick.NET (.NET 4.0/2.0 AnyCPU)

If you want to build the AnyCPU version of Magick.NET you will need to execute `Tools\GenerateAnyCPU.cmd`.
This will build the x86 and x64 version of Magick.NET and creates the embedded resources that are required for the AnyCPU build.
You will need to run this command every time you make changes to the Magick.NET code.
When the build is done you can open the file `Magick.NET..sln` and change the platform to `AnyCPU`.
