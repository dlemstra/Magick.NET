@echo off

call "%vs110comntools%vsvars32.bat"
powershell -ExecutionPolicy Unrestricted ..\..\Tools\Scripts\BuildLibraries.ps1 -development

pause
