@echo off
call "%vs140comntools%vsvars32.bat"
powershell -ExecutionPolicy Unrestricted ..\Tools\Scripts\Publish.ps1 "7.0.2.1" "7.0.2.100"
pause
