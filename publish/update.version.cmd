@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "7.22.2.1"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "5.2.1.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "2.0.7.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "3.0.1.0"
powershell .\update.version.ps1
