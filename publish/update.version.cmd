@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "7.23.2.0"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "6.1.1.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "2.0.12.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "3.0.6.0"
powershell .\update.version.ps1
