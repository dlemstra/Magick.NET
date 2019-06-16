@echo off
call "windows\init.visualstudio.cmd"

powershell .\FileGenerators\FileGenerator.ps1 -name Drawables
pause