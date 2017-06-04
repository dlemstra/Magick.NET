@echo off

call "..\..\..\Tools\VsDevCmd.cmd"
powershell -ExecutionPolicy Unrestricted ..\..\..\Tools\Scripts\BuildLibraries.ps1 -dev Q16-HDRI.x64 -configuration Debug

pause
