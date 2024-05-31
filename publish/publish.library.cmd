@echo off
call "..\tools\windows\init.visualstudio.cmd"

powershell .\publish.library.ps1 -destination output -library %1
if %errorlevel% neq 0 exit /b %errorlevel%
