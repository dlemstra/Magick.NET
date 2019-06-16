@echo off
call "windows\init.visualstudio.cmd"

powershell .\FileGenerators\FileGenerator.ps1 -name MagickColors -buildMagickNET $false
pause