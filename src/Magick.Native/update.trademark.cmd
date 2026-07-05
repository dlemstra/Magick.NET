@echo off
call ..\..\tools\windows\find-bash.cmd

%BASH% update.trademark.sh
pause
