@echo off

set REPOS=http://git.imagemagick.org/repos
set DATE=2017-01-14 19:43

set BASH="%PROGRAMFILES%\Git\bin\bash.exe"
if exist %BASH% goto EXECUTE

set bash="%PROGRAMFILES(x86)%\Git\bin\bash.exe"
if exist %BASH% goto EXECUTE

echo Failed to find bash.exe
echo %BASH%
exit /b 1

:EXECUTE
%BASH% --login -i -c "./Checkout.sh %REPOS% \"%DATE%\""

call "%vs140comntools%vsvars32.bat"
powershell -ExecutionPolicy Unrestricted ..\..\Tools\Scripts\GenerateLibrariesDoc.ps1

pause
