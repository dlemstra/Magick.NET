@echo off
call "..\..\tools\windows\init.visualstudio.cmd"

set PLATFORM_NAME=%2
powershell .\build.Magick.NET.ps1 -quantumName %1 -platformName %PLATFORM_NAME:"='% -config %3
if %errorlevel% neq 0 exit /b %errorlevel%