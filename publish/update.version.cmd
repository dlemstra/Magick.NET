@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "7.22.0.0"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "5.0.0.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "2.0.4.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "2.0.2.0"
powershell .\update.version.ps1
