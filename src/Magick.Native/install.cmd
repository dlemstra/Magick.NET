@echo off

powershell .\install.ps1
if %errorlevel% neq 0 exit /b %errorlevel%