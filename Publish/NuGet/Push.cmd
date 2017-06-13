@echo off

echo Are you sure?
pause

set /p ApiKey=<ApiKey.txt
if not "%ApiKey%"=="" goto push

echo Unable to find ApiKey.txt
goto done

:push
for /r %%i in (*.nupkg) do ..\..\Tools\Programs\nuget.exe push %%i %ApiKey% -Source nuget.org

:done
pause
