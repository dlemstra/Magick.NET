@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "8.3.1"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "8.3.1"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "4.0.6"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "4.0.6"
powershell .\update.version.ps1
