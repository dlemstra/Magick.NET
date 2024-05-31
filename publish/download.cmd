@echo off

set /p ApiKey=<../keys/github.txt
if not "%ApiKey%"=="" goto download

echo Unable to find github.txt in the keys folder.
goto done

:download
powershell .\download.ps1 -token %ApiKey%

:done
pause
