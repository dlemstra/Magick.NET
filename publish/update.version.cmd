@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "12.2.1"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "12.2.1"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "6.1.2"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "6.1.2"
powershell .\update.version.ps1
