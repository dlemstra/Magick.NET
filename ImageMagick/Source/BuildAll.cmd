@echo off

call "..\..\Tools\VsDevCmd.cmd"
powershell -ExecutionPolicy Unrestricted ..\..\Tools\Scripts\BuildLibraries.ps1
powershell -ExecutionPolicy Unrestricted ..\..\Tools\Scripts\GenerateLibrariesDoc.ps1

pause
