@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "7.19.0.0"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "2.0.0.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "2.0.0.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "1.0.1.0"
powershell .\update.version.ps1
