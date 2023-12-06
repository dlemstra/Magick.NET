@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "13.5.0"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "13.5.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "7.2.1"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "7.2.1"
powershell .\update.version.ps1
