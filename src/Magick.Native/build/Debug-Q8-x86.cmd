@echo off
call "..\..\..\tools\windows\init.visualstudio.cmd"

set quantumName=Q8
set platformName=x86

cd ..\..\..\..\Magick.Native\build\dotnet\windows\

powershell .\build.ImageMagick.ps1 -config Debug -quantumName %quantumName% -platformName %platformName%
if %errorlevel% neq 0 exit /b %errorlevel%

powershell .\build.Native.ps1 -config Debug -quantumName %quantumName% -platformName %platformName%
if %errorlevel% neq 0 exit /b %errorlevel%

cd ..\..\..\src\Magick.Native\bin\Debug%quantumName%\%platformName%

copy /y Magick.Native-%quantumName%-%platformName%.dll ..\..\..\..\..\..\Magick.NET\tests\Magick.NET.Tests\bin\Debug%quantumName%\%platformName%\net45
copy /y Magick.Native-%quantumName%-%platformName%.pdb ..\..\..\..\..\..\Magick.NET\tests\Magick.NET.Tests\bin\Debug%quantumName%\%platformName%\net45
copy /y Magick.Native-%quantumName%-%platformName%.dll ..\..\..\..\..\..\Magick.NET\tests\Magick.NET.Tests\bin\Debug%quantumName%\%platformName%\netcoreapp2.0
copy /y Magick.Native-%quantumName%-%platformName%.pdb ..\..\..\..\..\..\Magick.NET\tests\Magick.NET.Tests\bin\Debug%quantumName%\%platformName%\netcoreapp2.0

pause