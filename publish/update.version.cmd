@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "8.4.0"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "8.4.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "4.0.9"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "4.0.9"
powershell .\update.version.ps1
