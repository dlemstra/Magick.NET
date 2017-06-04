@echo off
call "..\VsDevCmd.cmd"

set LIBDIR=C:\Magick.NET.libs
set TARGET=C:\Magick.NET\ImageMagick

if "%2"=="AnyCPU" goto anycpu

xcopy %LIBDIR%\lib\Release\%2 %TARGET%\lib\Release\%2 /Y /S /I
xcopy %LIBDIR%\%1\lib\Release\%2 %TARGET%\%1\lib\Release\%2 /Y /S /I

goto build

:anycpu
xcopy %LIBDIR%\lib\Release %TARGET%\lib\Release /Y /S /I
xcopy %LIBDIR%\%1\lib\Release %TARGET%\%1\lib\Release /Y /S /I

:build

rmdir /S /Q %LIBDIR%
xcopy %TARGET% %LIBDIR% /Y /S /I

powershell -ExecutionPolicy Unrestricted ..\Scripts\AppVeyor\Build.ps1 %1 %2