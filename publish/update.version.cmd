@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "12.1.0"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "12.1.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "6.1.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "6.1.0"
powershell .\update.version.ps1
