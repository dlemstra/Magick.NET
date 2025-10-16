@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "14.9.0"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "14.9.0"
powershell .\update.version.ps1 -library "Magick.NET.AvaloniaMediaImaging" -version "1.1.6"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "8.0.11"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "8.0.11"
powershell .\update.version.ps1

echo Also update the TheVersionProperty.ShouldContainTheCorrectVersion unit test
pause
