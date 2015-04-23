@echo off

set DROPBOX=%USERPROFILE%\Dropbox\Magick.NET.libs

xcopy %DROPBOX%\lib lib /Y /S /I
xcopy %DROPBOX%\Q8\lib Q8\lib /Y /S /I
xcopy %DROPBOX%\Q16\lib Q16\lib /Y /S /I
xcopy %DROPBOX%\Q16-HDRI\lib Q16-HDRI\lib /Y /S /I

pause
