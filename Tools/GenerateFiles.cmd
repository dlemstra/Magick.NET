@echo off
call "%vs110comntools%vsvars32.bat"
powershell -ExecutionPolicy Unrestricted .\Scripts\GenerateFiles.ps1
pause