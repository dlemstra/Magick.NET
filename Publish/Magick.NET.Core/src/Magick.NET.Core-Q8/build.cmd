@echo off
call "%vs140comntools%vsvars32.bat"
powershell -ExecutionPolicy Unrestricted ..\..\..\..\Tools\Scripts\BuildCore.ps1 %~p0
pause
