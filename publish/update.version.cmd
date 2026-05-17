@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "14.13.1"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "14.13.1"
powershell .\update.version.ps1 -library "Magick.NET.AvaloniaMediaImaging" -version "1.1.11"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "8.0.22"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "8.0.22"
powershell .\update.version.ps1

echo Also update the TheVersionProperty.ShouldContainTheCorrectVersion unit test
pause
