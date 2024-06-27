@echo off

powershell .\install.dependencies.ps1
if %errorlevel% neq 0 exit /b %errorlevel%

..\..\tools\windows\gs1000w32.exe /S
if %errorlevel% neq 0 exit /b %errorlevel%

dotnet tool install --global sign --version 0.9.1-beta.24325.5
if %errorlevel% neq 0 exit /b %errorlevel%
