@echo off

echo Are you sure?
pause

for /r %%i in (*.nupkg) do ..\..\Tools\Programs\nuget.exe push %%i

pause
