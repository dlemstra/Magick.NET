@echo off

call "%vs140comntools%vsvars32.bat"
powershell -ExecutionPolicy Unrestricted ..\..\..\Tools\Scripts\BuildLibraries.ps1 -dev Q8.x64 -configuration Debug

pause
