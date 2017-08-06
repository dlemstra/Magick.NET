@echo off
call "..\Tools\VsDevCmd.cmd"
powershell -ExecutionPolicy Unrestricted ..\Tools\Scripts\Publish.ps1 "7.0.6.600"
pause
