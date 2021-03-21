@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "7.23.3.0"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "6.1.3.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "2.0.14.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "3.0.8.0"
powershell .\update.version.ps1
