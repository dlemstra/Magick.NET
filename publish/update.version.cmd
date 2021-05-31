@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "7.24.1.0"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "7.0.1.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "2.0.17.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "3.0.11.0"
powershell .\update.version.ps1
