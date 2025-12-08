@echo off

powershell .\install.nuget.ps1
if %errorlevel% == 0 goto done

echo Failed to install nuget
exit /b %errorlevel%

:done
