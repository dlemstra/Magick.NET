@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "12.0.1"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "12.0.1"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "6.0.1"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "6.0.1"
powershell .\update.version.ps1
