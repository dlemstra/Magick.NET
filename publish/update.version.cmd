@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "7.23.4.0"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "6.2.0.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "2.0.15.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "3.0.9.0"
powershell .\update.version.ps1
