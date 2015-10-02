@echo off
call "%vs140comntools%vsvars32.bat"
powershell -ExecutionPolicy Unrestricted .\Scripts\GenerateFiles.ps1
pause
