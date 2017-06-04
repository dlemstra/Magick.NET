@echo off

call "..\..\..\Tools\VsDevCmd.cmd"
powershell -ExecutionPolicy Unrestricted ..\..\..\Tools\Scripts\BuildLibraries.ps1 -dev Q16.x64

pause
