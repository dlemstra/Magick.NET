@echo off

call "..\..\..\Tools\VsDevCmd.cmd"
powershell -ExecutionPolicy Unrestricted ..\..\..\Tools\Scripts\BuildLibraries.ps1 -dev Q8.x86

pause
