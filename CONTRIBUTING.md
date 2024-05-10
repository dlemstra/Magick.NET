# Building Magick.NET

This document describes the requirements and instructions to build Magick.NET on your own machine.

### Requirements

- Visual Studio 2019 (community)
- Powershell (>= 3.0)
- Git for Windows (>= 2.7.2)
- Windows SDK (>= 10.0.10586)

### Download the code

The first step is to either clone this repository with `git` or download the latest [release](https://github.com/dlemstra/Magick.NET/releases) and
unzip the file.

### Install Magick.Native

Magick.NET uses the [Magick.Native](https://github.com/dlemstra/Magick.Native) library and that needs to be installed first. To install this the
following steps need to be done:

- Log into your GitHub account and go to the **Settings**.
- Now click on **Generate new token** on the top right.
- Write the name of the token under **Note** and then select **read:packages** option, finally click **Generate token**.
- Copy and store the token safely as it won't be shown again!
- Open a command prompt and navigate to the *src\Magick.Native* folder.
- Type in `install.cmd` there will be two prompts, one for the username and other for the previously obtained read token.

### Building the project

After installing the Magick.Native library the `.sln` file of this project can be used to build the Magick.NET library. Inside VisualStudio select
the desired version (Q8, Q16, Q16-HDRI) and build the solution.

### Submitting a pull request

Before opening a pull request the unit tests of the project should be run. The tests will only run in the `Test*` or `Debug*` configuration. If new
functionality was added or if a bug was fixed with this pull request make sure that a new unit test is added to avoid a regression. Also make sure
that there are no compiler warnings anymore because the GitHub actions build will treat warnings as errors.

### Debugging Magick.Native

This project also includes some files that make it easy to debug the [Magick.Native](https://github.com/dlemstra/Magick.Native) library.
For this to be possible the Magick.Native library needs to be cloned out at the same level as the Magick.NET library. If Magick.NET is cloned in
`C:\Projects\Magick.NET` the library needs to be cloned into `C:\Projects\Magick.Native`. Before executing one of the build files inside this project
ImageMagick needs to be cloned in the Magick.Native project. This can be done with the file `Magick.Native\src\ImageMagick\checkout.cmd`.
After the library is cloned one of the scripts inside [src/Magick.Native/build](src/Magick.Native/build) can be used to do create a debug build
that can be used to debug the native code through this project.
