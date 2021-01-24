@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "7.23.1.0"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "6.1.0.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "2.0.11.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "3.0.5.0"
powershell .\update.version.ps1
