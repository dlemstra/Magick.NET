@echo off

if not exist "C:\Program Files (x86)\gs" choco install ghostscript.app -y -x86 -version 9.18

set LIBDIR=C:\Magick.NET.libs
if exist %LIBDIR% goto done

echo Downloading .lib files
appveyor DownloadFile https://www.dropbox.com/sh/5m3zllq81n4eyhm/AACQFGl4PKi9xnd15EbU5S1Ia?dl=1
echo Extracting .lib files
7z x -o%LIBDIR% AACQFGl4PKi9xnd15EbU5S1Ia

:done