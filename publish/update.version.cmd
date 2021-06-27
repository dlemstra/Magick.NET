@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "8.0.0"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "8.0.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "3.0.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "4.0.0"
powershell .\update.version.ps1
