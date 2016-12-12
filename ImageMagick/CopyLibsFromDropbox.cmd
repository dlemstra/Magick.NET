@echo off

set DROPBOX=%USERPROFILE%\Dropbox\Magick.NET.libs
if exist %DROPBOX% goto copy

echo You can download the library files here: https://www.dropbox.com/sh/5m3zllq81n4eyhm/AACQFGl4PKi9xnd15EbU5S1Ia?dl=0
goto done

:copy
xcopy %DROPBOX%\lib\Release lib\Release /Y /S /I
xcopy %DROPBOX%\Q8\lib\Release Q8\lib\Release /Y /S /I
xcopy %DROPBOX%\Q16\lib\Release Q16\lib\Release /Y /S /I
xcopy %DROPBOX%\Q16-HDRI\lib\Release Q16-HDRI\lib\Release /Y /S /I

:done
pause
