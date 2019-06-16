@echo off
call "windows\init.visualstudio.cmd"

powershell .\FileGenerators\FileGenerator.ps1 -name Native -buildMagickNET $false
pause