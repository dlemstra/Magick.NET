@echo off
set version=6.8.6

setlocal
	call "%vs90comntools%vsvars32.bat"

	powershell -ExecutionPolicy Unrestricted .\BuildCoders.ps1 %version% v2.0 x86
	if not %errorlevel% == 0 goto DONE

	call "%vcinstalldir%\vcvarsall.bat" amd64
	powershell -ExecutionPolicy Unrestricted .\BuildCoders.ps1 %version% v2.0 x64
	if not %errorlevel% == 0 goto DONE
endlocal

call "%vs110comntools%vsvars32.bat"

powershell -ExecutionPolicy Unrestricted .\BuildCoders.ps1 %version% v4.0 x86
if not %errorlevel% == 0 goto DONE

setlocal
	call "%vcinstalldir%\vcvarsall.bat" amd64
	powershell -ExecutionPolicy Unrestricted .\BuildCoders.ps1 %version% v4.0 x64
	if not %errorlevel% == 0 goto DONE
endlocal

powershell -ExecutionPolicy Unrestricted .\BuildLibraries.ps1 %version%

:DONE
pause