@echo off
call "VsDevCmd.cmd"
powershell -ExecutionPolicy Unrestricted .\Scripts\BuildCrossPlatform.ps1
pause
cd "..\ImageMagick"
call "CopyLibsToDropbox.cmd
