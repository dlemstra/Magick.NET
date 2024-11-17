@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "14.2.0"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "14.2.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "8.0.2"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "8.0.2"
powershell .\update.version.ps1
