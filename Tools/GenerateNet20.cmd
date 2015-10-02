@echo off
call "%vs140comntools%vsvars32.bat"
powershell -ExecutionPolicy Unrestricted .\Scripts\Net20.ps1
pause
