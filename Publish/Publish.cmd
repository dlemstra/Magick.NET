@echo off
call "..\Tools\VsDevCmd.cmd"
powershell -ExecutionPolicy Unrestricted ..\Tools\Scripts\Publish.ps1 "7.10.1.0"
pause
