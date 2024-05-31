@echo off
call "..\tools\windows\init.visualstudio.cmd"

set PLATFORM_NAME=%2
powershell .\publish.ps1 -destination output -quantumName %1 -platformName %PLATFORM_NAME:"='%
if %errorlevel% neq 0 exit /b %errorlevel%
