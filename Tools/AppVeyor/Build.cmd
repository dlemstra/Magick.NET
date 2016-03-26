@echo off
call "%vs140comntools%vsvars32.bat"

set LIBDIR=C:\Magick.NET.libs
set TARGET=C:\Magick.NET\ImageMagick
xcopy %LIBDIR%\lib %TARGET%\lib\Release /Y /S /I
xcopy %LIBDIR%\Q8\lib %TARGET%\Q8\lib\Release /Y /S /I
xcopy %LIBDIR%\Q16\lib %TARGET%\Q16\lib\Release /Y /S /I
xcopy %LIBDIR%\Q16-HDRI\lib %TARGET%\Q16-HDRI\lib\Release /Y /S /I

powershell -ExecutionPolicy Unrestricted ..\Scripts\AppVeyor\Build.ps1 %1 %2