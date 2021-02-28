@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "7.23.2.1"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "6.1.2.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "2.0.13.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "3.0.7.0"
powershell .\update.version.ps1
