@echo off
set "SCRIPT_DIR=%~dp0"

set quantum=%1
set architecture=%2
set hdri=%3
set openMP=%4

if "%quantum%"=="" goto invalid
if "%architecture%"=="" goto invalid
if "%hdri%"=="" goto invalid
if "%openMP%"=="" goto invalid

cd ..\..\..\..\Magick.Native\build\windows

call "build.cmd" Debug %quantum% %architecture% %hdri% %openMP%

set quantumName=%quantum%
if not "%hdri%"=="noHdri" set quantumName=%quantumName%-HDRI

call "copy-resources.cmd" ..\..\..\Magick.NET\src\Magick.Native\resources\Release%quantumName%

goto done

if not "%openMP%"=="noOpenMP" set quantumName=%quantumName%-OpenMP

cd ..\..\src\Magick.Native\bin\Debug%quantumName%\%architecture%

set testfolder=..\..\..\..\..\..\Magick.NET\tests

if not exist %testfolder%\Magick.NET.Tests\bin\%config%%quantumName%\%architecture%\net472 mkdir %testfolder%\Magick.NET.Tests\bin\%config%%quantumName%\%architecture%\net472
if not exist %testfolder%\Magick.NET.Tests\bin\%config%%quantumName%\AnyCPU\net472 mkdir %testfolder%\Magick.NET.Tests\bin\%config%%quantumName%\AnyCPU\net472
if not exist %testfolder%\Magick.NET.Tests\bin\%config%%quantumName%\%architecture%\net8.0 mkdir %testfolder%\Magick.NET.Tests\bin\%config%%quantumName%\%architecture%\net8.0
copy /y Magick.Native-%quantumName%-%architecture%.dll %testfolder%\Magick.NET.Tests\bin\%config%%quantumName%\%architecture%\net472
copy /y Magick.Native-%quantumName%-%architecture%.pdb %testfolder%\Magick.NET.Tests\bin\%config%%quantumName%\%architecture%\net472
copy /y Magick.Native-%quantumName%-%architecture%.dll %testfolder%\Magick.NET.Tests\bin\%config%%quantumName%\AnyCPU\net472
copy /y Magick.Native-%quantumName%-%architecture%.pdb %testfolder%\Magick.NET.Tests\bin\%config%%quantumName%\AnyCPU\net472
copy /y Magick.Native-%quantumName%-%architecture%.dll %testfolder%\Magick.NET.Tests\bin\%config%%quantumName%\%architecture%\net8.0
copy /y Magick.Native-%quantumName%-%architecture%.pdb %testfolder%\Magick.NET.Tests\bin\%config%%quantumName%\%architecture%\net8.0

if not exist %testfolder%\Magick.NET.AvaloniaMediaImaging.Tests\bin\%config%%quantumName%\%architecture%\net8.0 mkdir %testfolder%\Magick.NET.AvaloniaMediaImaging.Tests\bin\%config%%quantumName%\%architecture%\net8.0
copy /y Magick.Native-%quantumName%-%architecture%.dll %testfolder%\Magick.NET.AvaloniaMediaImaging.Tests\bin\%config%%quantumName%\%architecture%\net8.0
copy /y Magick.Native-%quantumName%-%architecture%.pdb %testfolder%\Magick.NET.AvaloniaMediaImaging.Tests\bin\%config%%quantumName%\%architecture%\net8.0

if not exist %testfolder%\Magick.NET.SystemDrawing.Tests\bin\%config%%quantumName%\%architecture%\net472 mkdir %testfolder%\Magick.NET.SystemDrawing.Tests\bin\%config%%quantumName%\%architecture%\net472
if not exist %testfolder%\Magick.NET.SystemDrawing.Tests\bin\%config%%quantumName%\AnyCPU\net472 mkdir %testfolder%\Magick.NET.SystemDrawing.Tests\bin\%config%%quantumName%\AnyCPU\net472
if not exist %testfolder%\Magick.NET.SystemDrawing.Tests\bin\%config%%quantumName%\%architecture%\net8.0 mkdir %testfolder%\Magick.NET.SystemDrawing.Tests\bin\%config%%quantumName%\%architecture%\net8.0
copy /y Magick.Native-%quantumName%-%architecture%.dll %testfolder%\Magick.NET.SystemDrawing.Tests\bin\%config%%quantumName%\%architecture%\net472
copy /y Magick.Native-%quantumName%-%architecture%.pdb %testfolder%\Magick.NET.SystemDrawing.Tests\bin\%config%%quantumName%\%architecture%\net472
copy /y Magick.Native-%quantumName%-%architecture%.dll %testfolder%\Magick.NET.SystemDrawing.Tests\bin\%config%%quantumName%\AnyCPU\net472
copy /y Magick.Native-%quantumName%-%architecture%.pdb %testfolder%\Magick.NET.SystemDrawing.Tests\bin\%config%%quantumName%\AnyCPU\net472
copy /y Magick.Native-%quantumName%-%architecture%.dll %testfolder%\Magick.NET.SystemDrawing.Tests\bin\%config%%quantumName%\%architecture%\net8.0
copy /y Magick.Native-%quantumName%-%architecture%.pdb %testfolder%\Magick.NET.SystemDrawing.Tests\bin\%config%%quantumName%\%architecture%\net8.0

if not exist %testfolder%\Magick.NET.SystemWindowsMedia.Tests\bin\%config%%quantumName%\%architecture%\net472 mkdir %testfolder%\Magick.NET.SystemWindowsMedia.Tests\bin\%config%%quantumName%\%architecture%\net472
if not exist %testfolder%\Magick.NET.SystemWindowsMedia.Tests\bin\%config%%quantumName%\AnyCPU\net472 mkdir %testfolder%\Magick.NET.SystemWindowsMedia.Tests\bin\%config%%quantumName%\AnyCPU\net472
if not exist %testfolder%\Magick.NET.SystemWindowsMedia.Tests\bin\%config%%quantumName%\%architecture%\net8.0 mkdir %testfolder%\Magick.NET.SystemWindowsMedia.Tests\bin\%config%%quantumName%\%architecture%\net8.0
copy /y Magick.Native-%quantumName%-%architecture%.dll %testfolder%\Magick.NET.SystemWindowsMedia.Tests\bin\%config%%quantumName%\%architecture%\net472
copy /y Magick.Native-%quantumName%-%architecture%.pdb %testfolder%\Magick.NET.SystemWindowsMedia.Tests\bin\%config%%quantumName%\%architecture%\net472
copy /y Magick.Native-%quantumName%-%architecture%.dll %testfolder%\Magick.NET.SystemWindowsMedia.Tests\bin\%config%%quantumName%\AnyCPU\net472
copy /y Magick.Native-%quantumName%-%architecture%.pdb %testfolder%\Magick.NET.SystemWindowsMedia.Tests\bin\%config%%quantumName%\AnyCPU\net472
copy /y Magick.Native-%quantumName%-%architecture%.dll %testfolder%\Magick.NET.SystemWindowsMedia.Tests\bin\%config%%quantumName%\%architecture%\net8.0
copy /y Magick.Native-%quantumName%-%architecture%.pdb %testfolder%\Magick.NET.SystemWindowsMedia.Tests\bin\%config%%quantumName%\%architecture%\net8.0

goto done

:invalid

echo.
echo Do not use this script directly.
echo.

:done

cd %SCRIPT_DIR%

pause
