@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "9.0.0"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "9.0.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "4.0.13"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "4.0.13"
powershell .\update.version.ps1
