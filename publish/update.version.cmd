@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "13.1.1"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "13.1.1"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "7.0.3"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "7.0.3"
powershell .\update.version.ps1
