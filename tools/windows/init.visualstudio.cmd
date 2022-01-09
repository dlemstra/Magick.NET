@echo off

set TOOLSDIR=C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools

if exist "%TOOLSDIR%" goto found

set TOOLSDIR=C:\Program Files\Microsoft Visual Studio\2022\Professional\Common7\Tools

if exist "%TOOLSDIR%" goto found

set TOOLSDIR=C:\Program Files\Microsoft Visual Studio\2022\Enterprise\Common7\Tools

if exist "%TOOLSDIR%" goto found

goto notfound

:found
set CURRENTDIR=%cd%
call "%TOOLSDIR%\VsDevCmd.bat"
cd %CURRENTDIR%
goto end

:notfound
echo "Unable to find Visual Studio folder."
pause

:end
