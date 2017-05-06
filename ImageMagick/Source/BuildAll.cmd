@echo off

call "%vs140comntools%vsvars32.bat"
powershell -ExecutionPolicy Unrestricted ..\..\Tools\Scripts\BuildLibraries.ps1
powershell -ExecutionPolicy Unrestricted ..\..\Tools\Scripts\GenerateLibrariesDoc.ps1

pause
