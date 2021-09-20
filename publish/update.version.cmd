@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "8.3.2"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "8.3.2"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "4.0.7"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "4.0.7"
powershell .\update.version.ps1
