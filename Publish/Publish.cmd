@echo off
call "..\Tools\VsDevCmd.cmd"
powershell -ExecutionPolicy Unrestricted ..\Tools\Scripts\Publish.ps1 "7.4.0.0"
pause
