@echo off

set REPOS=http://git.imagemagick.org/repos
set DATE=2016-08-29 20:59

set BASH="%PROGRAMFILES%\Git\bin\bash.exe"
if exist %BASH% goto EXECUTE

set bash="%PROGRAMFILES(x86)%\Git\bin\bash.exe"
if exist %BASH% goto EXECUTE

echo Failed to find bash.exe
echo %BASH%
exit /b 1

:EXECUTE
%BASH% --login -i -c "./Checkout.sh %REPOS% \"%DATE%\""
pause
