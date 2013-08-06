@echo off
set version=6.8.6

call "%vs110comntools%vsvars32.bat"
powershell -ExecutionPolicy Unrestricted .\BuildLibraries.ps1 %version%

pause