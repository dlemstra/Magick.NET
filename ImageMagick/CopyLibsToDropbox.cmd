@echo off

set DROPBOX=%USERPROFILE%\Dropbox\Magick.NET.libs
if exist %DROPBOX% goto copy

echo Unable to find Dropbox folder
goto done

echo Are you sure?
pause

:copy
xcopy lib %DROPBOX%\lib /Y /S /I
xcopy Q8\lib %DROPBOX%\Q8\lib /Y /S /I
xcopy Q16\lib %DROPBOX%\Q16\lib /Y /S /I
xcopy Q16-HDRI\lib %DROPBOX%\Q16-HDRI\lib /Y /S /I

:done
pause
