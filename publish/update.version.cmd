@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "7.22.3.0"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "5.3.0.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "2.0.9.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "3.0.3.0"
powershell .\update.version.ps1
