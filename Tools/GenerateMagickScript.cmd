@echo off
call "%vs140comntools%vsvars32.bat"
powershell -ExecutionPolicy Unrestricted .\Scripts\GenerateMagickScript.ps1
pause
