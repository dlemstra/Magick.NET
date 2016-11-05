#Building Magick.NET

This document describes the requirements and instructions to build Magick.NET on your own machine.

### Requirements

- Visual Studio 2015 (Community)
- Powershell (>= 3.0)
- Git for Windows (>= 2.7.2)
- Windows SDK (>= 10.0.10586)
- AMD APP SDK (Optional for OpenCL support)

### Edit Microsoft.Cpp.Common.props

Edit `%ProgramFiles(x86)%\MSBuild\Microsoft.Cpp\v4.0\V140\Microsoft.Cpp.Common.props` and change TargetUniversalCRTVersion from 10.0.10240.0 to 10.0.10586.0. This fixes a compatibility issue with Windows 2003/XP.

### Build ImageMagick 7

Magick.NET uses the ImageMagick 7 library and you need to build that first. Below are the steps for that.

1. This step is optional but you can decide to use the latest GIT revision. To do this you need to edit the file `ImageMagick\Source\Checkout.cmd` and change the `DATE` variable to the current date and time.
2. Then you need to run `ImageMagick\Source\Checkout.cmd` that will clone the master branch of the GIT repository of ImageMagick.
3. Now run `ImageMagick\Source\BuildAll.cmd`. There are also a couple `BuildDevelopment[Quantum].cmd` files that you can use to quickly build the x86 version of that Quantum.

You can also download the precompiled .lib files from [DropBox](https://www.dropbox.com/sh/5m3zllq81n4eyhm/AACQFGl4PKi9xnd15EbU5S1Ia?dl=0) and copy them to `ImageMagick\lib`.

### Build Magick.NET (.NET 4.0 x86/x64)

You are now ready to build ImageMagick. You can do this by opening the `Magick.NET.sln` solution.
This solution contains various Release configurations (Q8/Q16/Q16-HDRI) to build Magick.NET with the Quantum that you prefer.
The unit tests can be build by switching to one of the Test configurations.

### Build Magick.NET (.NET 2.0 x86/x64)

For the .NET 2.0 build you need to open the solution `Magick.NET.net20.sln`.
If you added new files to the .NET 4.0 project make sure you first run `Tools\GenerateNet20.cmd` to update the project files.

### Build Magick.NET (.NET 4.0/2.0 AnyCPU)

If you want to build the AnyCPU version of Magick.NET you will need to execute `Tools\GenerateAnyCPU.cmd`.
This will build the x86 and x64 version of Magick.NET and creates the embedded resources that are required for the AnyCPU build.
You will need to run this command every time you make changes to the Magick.NET code.
When the build is done you can open the file `Magick.NET.AnyCPU.sln` for the .NET 4.0 build or `Magick.NET.AnyCPU.net20.sln` for the .NET 2.0 one.
