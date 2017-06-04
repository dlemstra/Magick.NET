@echo off
call "VsDevCmd.cmd"
powershell -ExecutionPolicy Unrestricted .\Scripts\GenerateNative.ps1
pause
