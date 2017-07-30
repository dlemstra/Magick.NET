@echo off

set DROPBOX=%USERPROFILE%\Dropbox\Magick.NET.libs
if exist %DROPBOX% goto copy

echo Unable to find Dropbox folder
goto done

:copy

echo Are you sure that you want to copy the libraries?
pause

xcopy lib\Release %DROPBOX%\lib\Release /Y /S /I
xcopy Q8\lib\Release %DROPBOX%\Q8\lib\Release /Y /S /I
xcopy Q16\lib\Release %DROPBOX%\Q16\lib\Release /Y /S /I
xcopy Q16-HDRI\lib\Release %DROPBOX%\Q16-HDRI\lib\Release /Y /S /I

:done
pause
