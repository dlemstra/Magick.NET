@echo off
call "%vs110comntools%vsvars32.bat"
powershell -ExecutionPolicy Unrestricted ..\Tools\Scripts\Publish.ps1 "6.8.9.5" "6.8.9.501"
pause
