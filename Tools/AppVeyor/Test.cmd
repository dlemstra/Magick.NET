@echo off
setlocal EnableDelayedExpansion

call "..\VsDevCmd.cmd"

set platform="%1"
if "%1" == "AnyCPU" (
  set platform="x86"
)

set vstest="%VSINSTALLDIR%\Common7\IDE\Extensions\TestPlatform\vstest.console.exe"

for /d %%d in (..\..\Tests\Magick.NET.Tests\bin\Test*) do (
    set folder=%%~d\%1\net45
    set filename=!folder!\Magick.NET.Tests.dll
    echo "Running tests from: !filename!"
    %vstest% !filename! /platform:%platform% /TestAdapterPath:!folder!
    if !errorlevel! neq 0 exit /b !errorlevel!
)
