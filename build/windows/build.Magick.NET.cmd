@echo off
call "..\..\tools\windows\init.visualstudio.cmd"

powershell .\build.Magick.NET.ps1 -config %1
if %errorlevel% neq 0 exit /b %errorlevel%