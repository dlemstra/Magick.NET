@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "14.12.0"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "14.12.0"
powershell .\update.version.ps1 -library "Magick.NET.AvaloniaMediaImaging" -version "1.1.7"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "8.0.20"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "8.0.20"
powershell .\update.version.ps1

echo Also update the TheVersionProperty.ShouldContainTheCorrectVersion unit test
pause
