@echo off
call "VsDevCmd.cmd"
powershell -ExecutionPolicy Unrestricted .\Scripts\ProjectFiles.ps1
pause
