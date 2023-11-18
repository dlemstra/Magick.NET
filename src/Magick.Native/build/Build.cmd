@echo off
call "..\..\..\tools\windows\init.visualstudio.cmd"

set config=%1
set quantumName=%2
set platformName=%3

if "%config%"=="" goto invalid
if "%quantumName%"=="" goto invalid
if "%platformName%"=="" goto invalid

cd ..\..\..\..\Magick.Native\build\windows\

powershell .\build.ImageMagick.ps1 -config %config% -quantumName %quantumName% -platformName %platformName%
if %errorlevel% neq 0 goto done

powershell .\build.Native.ps1 -config  %config% -quantumName %quantumName% -platformName %platformName%
if %errorlevel% neq 0 goto done

cd ..\..\src\Magick.Native\bin\Debug%quantumName%\%platformName%

set testfolder=..\..\..\..\..\..\Magick.NET\tests

if not exist %testfolder%\Magick.NET.Tests\bin\%config%%quantumName%\%platformName%\net462 mkdir %testfolder%\Magick.NET.Tests\bin\%config%%quantumName%\%platformName%\net462
if not exist %testfolder%\Magick.NET.Tests\bin\%config%%quantumName%\AnyCPU\net462 mkdir %testfolder%\Magick.NET.Tests\bin\%config%%quantumName%\AnyCPU\net462
if not exist %testfolder%\Magick.NET.Tests\bin\%config%%quantumName%\%platformName%\net8 mkdir %testfolder%\Magick.NET.Tests\bin\%config%%quantumName%\%platformName%\net8
copy /y Magick.Native-%quantumName%-%platformName%.dll %testfolder%\Magick.NET.Tests\bin\%config%%quantumName%\%platformName%\net462
copy /y Magick.Native-%quantumName%-%platformName%.pdb %testfolder%\Magick.NET.Tests\bin\%config%%quantumName%\%platformName%\net462
copy /y Magick.Native-%quantumName%-%platformName%.dll %testfolder%\Magick.NET.Tests\bin\%config%%quantumName%\AnyCPU\net462
copy /y Magick.Native-%quantumName%-%platformName%.pdb %testfolder%\Magick.NET.Tests\bin\%config%%quantumName%\AnyCPU\net462
copy /y Magick.Native-%quantumName%-%platformName%.dll %testfolder%\Magick.NET.Tests\bin\%config%%quantumName%\%platformName%\net8
copy /y Magick.Native-%quantumName%-%platformName%.pdb %testfolder%\Magick.NET.Tests\bin\%config%%quantumName%\%platformName%\net8

if not exist %testfolder%\Magick.NET.SystemDrawing.Tests\bin\%config%%quantumName%\%platformName%\net462 mkdir %testfolder%\Magick.NET.SystemDrawing.Tests\bin\%config%%quantumName%\%platformName%\net462
if not exist %testfolder%\Magick.NET.SystemDrawing.Tests\bin\%config%%quantumName%\AnyCPU\net462 mkdir %testfolder%\Magick.NET.SystemDrawing.Tests\bin\%config%%quantumName%\AnyCPU\net462
if not exist %testfolder%\Magick.NET.SystemDrawing.Tests\bin\%config%%quantumName%\%platformName%\net8-windows mkdir %testfolder%\Magick.NET.SystemDrawing.Tests\bin\%config%%quantumName%\%platformName%\net8-windows
copy /y Magick.Native-%quantumName%-%platformName%.dll %testfolder%\Magick.NET.SystemDrawing.Tests\bin\%config%%quantumName%\%platformName%\net462
copy /y Magick.Native-%quantumName%-%platformName%.pdb %testfolder%\Magick.NET.SystemDrawing.Tests\bin\%config%%quantumName%\%platformName%\net462
copy /y Magick.Native-%quantumName%-%platformName%.dll %testfolder%\Magick.NET.SystemDrawing.Tests\bin\%config%%quantumName%\AnyCPU\net462
copy /y Magick.Native-%quantumName%-%platformName%.pdb %testfolder%\Magick.NET.SystemDrawing.Tests\bin\%config%%quantumName%\AnyCPU\net462
copy /y Magick.Native-%quantumName%-%platformName%.dll %testfolder%\Magick.NET.SystemDrawing.Tests\bin\%config%%quantumName%\%platformName%\net8-windows
copy /y Magick.Native-%quantumName%-%platformName%.pdb %testfolder%\Magick.NET.SystemDrawing.Tests\bin\%config%%quantumName%\%platformName%\net8-windows

if not exist %testfolder%\Magick.NET.SystemWindowsMedia.Tests\bin\%config%%quantumName%\%platformName%\net462 mkdir %testfolder%\Magick.NET.SystemWindowsMedia.Tests\bin\%config%%quantumName%\%platformName%\net462
if not exist %testfolder%\Magick.NET.SystemWindowsMedia.Tests\bin\%config%%quantumName%\AnyCPU\net462 mkdir %testfolder%\Magick.NET.SystemWindowsMedia.Tests\bin\%config%%quantumName%\AnyCPU\net462
if not exist %testfolder%\Magick.NET.SystemWindowsMedia.Tests\bin\%config%%quantumName%\%platformName%\net8-windows mkdir %testfolder%\Magick.NET.SystemWindowsMedia.Tests\bin\%config%%quantumName%\%platformName%\net8-windows
copy /y Magick.Native-%quantumName%-%platformName%.dll %testfolder%\Magick.NET.SystemWindowsMedia.Tests\bin\%config%%quantumName%\%platformName%\net462
copy /y Magick.Native-%quantumName%-%platformName%.pdb %testfolder%\Magick.NET.SystemWindowsMedia.Tests\bin\%config%%quantumName%\%platformName%\net462
copy /y Magick.Native-%quantumName%-%platformName%.dll %testfolder%\Magick.NET.SystemWindowsMedia.Tests\bin\%config%%quantumName%\AnyCPU\net462
copy /y Magick.Native-%quantumName%-%platformName%.pdb %testfolder%\Magick.NET.SystemWindowsMedia.Tests\bin\%config%%quantumName%\AnyCPU\net462
copy /y Magick.Native-%quantumName%-%platformName%.dll %testfolder%\Magick.NET.SystemWindowsMedia.Tests\bin\%config%%quantumName%\%platformName%\net8-windows
copy /y Magick.Native-%quantumName%-%platformName%.pdb %testfolder%\Magick.NET.SystemWindowsMedia.Tests\bin\%config%%quantumName%\%platformName%\net8-windows

goto done

:invalid

echo.
echo Do not use this script directly.
echo.

:done

pause
