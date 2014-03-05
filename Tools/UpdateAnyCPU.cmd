@echo off
call "%vs110comntools%vsvars32.bat"
powershell -ExecutionPolicy Unrestricted .\Scripts\UpdateAnyCPU.ps1
pause