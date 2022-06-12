@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "11.2.0"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "11.2.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "5.0.3"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "5.0.3"
powershell .\update.version.ps1
