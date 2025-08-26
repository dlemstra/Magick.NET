@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "14.8.1"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "14.8.1"
powershell .\update.version.ps1 -library "Magick.NET.AvaloniaMediaImaging" -version "1.1.4"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "8.0.9"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "8.0.9"
powershell .\update.version.ps1
