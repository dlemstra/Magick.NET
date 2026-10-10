@echo off

powershell .\update.version.ps1 -library "Magick.NET" -version "15.0.0"
powershell .\update.version.ps1 -library "Magick.NET.Core" -version "15.0.0"
powershell .\update.version.ps1 -library "Magick.NET.AvaloniaMediaImaging" -version "2.0.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemDrawing" -version "9.0.0"
powershell .\update.version.ps1 -library "Magick.NET.SystemWindowsMedia" -version "9.0.0"
powershell .\update.version.ps1

echo Also update the TheVersionProperty.ShouldContainTheCorrectVersion unit test
pause
