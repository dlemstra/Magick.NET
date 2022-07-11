@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "11.3.0"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "11.3.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "5.1.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "5.1.0"
powershell .\update.version.ps1
