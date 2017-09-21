@echo off
call "..\VsDevCmd.cmd"

set platform="%1"
if "%1" == "AnyCPU" (
  set platform="x86"
)

set vstest="%VSINSTALLDIR%Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe"

for /r ..\..\Tests\Magick.NET.Tests\bin %%a in (*.dll) do (
  if "%%~nxa"=="Magick.NET.Tests.dll" (
    echo "Running tests from: %%~dpnxa"
    %vstest% %%~dpnxa /inIsolation /platform:%platform% /TestAdapterPath:%%~dpa /logger:AppVeyor
    if %errorlevel% neq 0 exit /b %errorlevel%
  )
)
