@echo off

call "..\..\..\Tools\VsDevCmd.cmd"
powershell -ExecutionPolicy Unrestricted ..\..\..\Tools\Scripts\BuildLibraries.ps1 -dev Q8.x64 -configuration Debug

pause
