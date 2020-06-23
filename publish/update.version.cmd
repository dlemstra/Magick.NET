@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "7.20.0.1"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "3.0.0.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "2.0.1.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "1.0.2.0"
powershell .\update.version.ps1
