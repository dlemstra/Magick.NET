@echo off

powershell .\install.dependencies.ps1
if %errorlevel% == 0 goto installGhostscript

echo Failed to install dependencies.
exit /b %errorlevel%

:installGhostscript
..\..\tools\windows\gs1000w32.exe /S
if %errorlevel% == 0 goto done

echo Failed to install Ghostscript.
exit /b %errorlevel%

:done
