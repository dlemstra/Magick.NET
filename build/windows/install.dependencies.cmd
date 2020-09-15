@echo off

powershell .\install.dependencies.ps1
if %errorlevel% neq 0 exit /b %errorlevel%

..\..\tools\windows\gs9531w32.exe /S
if %errorlevel% neq 0 exit /b %errorlevel%