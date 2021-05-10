@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "7.24.0.0"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "7.0.0.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "2.0.16.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "3.0.10.0"
powershell .\update.version.ps1
