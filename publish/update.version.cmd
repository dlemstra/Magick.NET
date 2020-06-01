@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "7.18.0.0"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "1.0.0.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "1.0.0.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "1.0.0.0"
powershell .\update.version.ps1
