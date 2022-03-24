@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "11.0.0"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "11.0.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "4.0.19"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "4.0.19"
powershell .\update.version.ps1
