@echo off

call "%vs140comntools%vsvars32.bat"
powershell -ExecutionPolicy Unrestricted ..\..\..\Tools\Scripts\BuildLibraries.ps1 -dev Q16-HDRI.x64 -configuration Debug

pause
