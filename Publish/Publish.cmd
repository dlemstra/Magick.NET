@echo off
call "%vs140comntools%vsvars32.bat"
powershell -ExecutionPolicy Unrestricted ..\Tools\Scripts\Publish.ps1 "7.0.0.0" "7.0.0.0020"
pause
