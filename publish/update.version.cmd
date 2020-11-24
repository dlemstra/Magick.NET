@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "7.22.2.2"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "5.2.2.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "2.0.8.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "3.0.2.0"
powershell .\update.version.ps1
