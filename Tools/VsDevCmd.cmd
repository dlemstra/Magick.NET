@echo off

set TOOLSDIR=C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\Tools

if exist "%TOOLSDIR%" goto found

set TOOLSDIR=C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\Common7\Tools

if exist "%TOOLSDIR%" goto found

set TOOLSDIR=C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\Tools

if exist "%TOOLSDIR%" goto found

goto notfound

:found
call "%TOOLSDIR%\VsDevCmd.bat"
goto end

:notfound
echo "Unable to find Visual Studio folder."
pause

:end