@echo off

echo Are you sure?
pause

set /p ApiKey=<../keys/nuget.txt
if not "%ApiKey%"=="" goto push

echo Unable to find nuget.txt in the keys folder.
goto done

:push
for /r %%i in (*.nupkg) do ..\tools\windows\nuget.exe push %%i %ApiKey% -Source nuget.org

:done
pause
