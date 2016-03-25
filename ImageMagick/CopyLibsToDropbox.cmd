@echo off

set DROPBOX=%USERPROFILE%\Dropbox\Magick.NET.libs
if exist %DROPBOX% goto copy

echo Unable to find Dropbox folder
goto done

:copy

echo Are you sure?
pause

xcopy lib\Release %DROPBOX%\lib /Y /S /I
xcopy Q8\lib\Release %DROPBOX%\Q8\lib /Y /S /I
xcopy Q16\lib\Release %DROPBOX%\Q16\lib /Y /S /I
xcopy Q16-HDRI\lib\Release %DROPBOX%\Q16-HDRI\lib /Y /S /I

:done
pause
