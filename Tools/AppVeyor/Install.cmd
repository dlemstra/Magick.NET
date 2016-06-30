@echo off

if not exist "C:\Program Files (x86)\gs" powershell -ExecutionPolicy Unrestricted ..\Scripts\AppVeyor\InstallGhostscript.ps1
if %errorlevel% neq 0 exit /b %errorlevel%

set LIBDIR=C:\Magick.NET.libs
if exist %LIBDIR% goto done

echo Downloading .lib files
appveyor DownloadFile https://www.dropbox.com/sh/5m3zllq81n4eyhm/AACQFGl4PKi9xnd15EbU5S1Ia?dl=1
echo Extracting .lib files
7z x -o%LIBDIR% AACQFGl4PKi9xnd15EbU5S1Ia
if %errorlevel% neq 0 exit /b %errorlevel%

:done