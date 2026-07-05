@echo off
call ..\..\tools\windows\find-bash.cmd

%BASH% update.native.sh
pause
