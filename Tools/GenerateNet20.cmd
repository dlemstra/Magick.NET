@echo off
call "VsDevCmd.cmd"
powershell -ExecutionPolicy Unrestricted .\Scripts\Net20.ps1
pause
