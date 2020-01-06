@echo off
call "..\..\tools\windows\init.visualstudio.cmd"

powershell .\build.Magick.NET.ps1 -quantumName %1 -platformName %2 -config %3
if %errorlevel% neq 0 exit /b %errorlevel%