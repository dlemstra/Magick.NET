@echo off

echo Are you sure?
pause

set /p ApiKey=<api.key.txt
if not "%ApiKey%"=="" goto push

echo Unable to find api.key.txt
goto done

:push
for /r %%i in (*.nupkg) do ..\tools\windows\nuget.exe push %%i %ApiKey% -Source nuget.org

:done
pause
