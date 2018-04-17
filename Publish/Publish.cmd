@echo off
call "..\Tools\VsDevCmd.cmd"
powershell -ExecutionPolicy Unrestricted ..\Tools\Scripts\Publish.ps1 "7.4.4.0"
pause
