@echo off

set REPOS=http://git.imagemagick.org/repos
set DATE=2016-19-10 07:29

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
