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

copy /y Magick.Native-%quantumName%-%platformName%.dll ..\..\..\..\..\..\Magick.NET\tests\Magick.NET.Tests\bin\%config%%quantumName%\%platformName%\net452
copy /y Magick.Native-%quantumName%-%platformName%.pdb ..\..\..\..\..\..\Magick.NET\tests\Magick.NET.Tests\bin\%config%%quantumName%\%platformName%\net452
copy /y Magick.Native-%quantumName%-%platformName%.dll ..\..\..\..\..\..\Magick.NET\tests\Magick.NET.Tests\bin\%config%%quantumName%\%platformName%\netcoreapp3.0
copy /y Magick.Native-%quantumName%-%platformName%.pdb ..\..\..\..\..\..\Magick.NET\tests\Magick.NET.Tests\bin\%config%%quantumName%\%platformName%\netcoreapp3.0

copy /y Magick.Native-%quantumName%-%platformName%.dll ..\..\..\..\..\..\Magick.NET\tests\Magick.NET.SystemDrawing.Tests\bin\%config%%quantumName%\%platformName%\net452
copy /y Magick.Native-%quantumName%-%platformName%.pdb ..\..\..\..\..\..\Magick.NET\tests\Magick.NET.SystemDrawing.Tests\bin\%config%%quantumName%\%platformName%\net452
copy /y Magick.Native-%quantumName%-%platformName%.dll ..\..\..\..\..\..\Magick.NET\tests\Magick.NET.SystemDrawing.Tests\bin\%config%%quantumName%\%platformName%\netcoreapp3.0
copy /y Magick.Native-%quantumName%-%platformName%.pdb ..\..\..\..\..\..\Magick.NET\tests\Magick.NET.SystemDrawing.Tests\bin\%config%%quantumName%\%platformName%\netcoreapp3.0

copy /y Magick.Native-%quantumName%-%platformName%.dll ..\..\..\..\..\..\Magick.NET\tests\Magick.NET.SystemWindowsMedia.Tests\bin\%config%%quantumName%\%platformName%\net452
copy /y Magick.Native-%quantumName%-%platformName%.pdb ..\..\..\..\..\..\Magick.NET\tests\Magick.NET.SystemWindowsMedia.Tests\bin\%config%%quantumName%\%platformName%\net452
copy /y Magick.Native-%quantumName%-%platformName%.dll ..\..\..\..\..\..\Magick.NET\tests\Magick.NET.SystemWindowsMedia.Tests\bin\%config%%quantumName%\%platformName%\netcoreapp3.0
copy /y Magick.Native-%quantumName%-%platformName%.pdb ..\..\..\..\..\..\Magick.NET\tests\Magick.NET.SystemWindowsMedia.Tests\bin\%config%%quantumName%\%platformName%\netcoreapp3.0

goto done

:invalid

echo.
echo Do not use this script directly.
echo.

:done

pause