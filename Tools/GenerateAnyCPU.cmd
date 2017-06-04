@echo off
call "VsDevCmd.cmd"
powershell -ExecutionPolicy Unrestricted .\Scripts\AnyCPU.ps1
pause
