@echo off
call "..\VsDevCmd.cmd"

set platform="%1"
if "%1" == "AnyCPU" (
  set platform="x86"
)

for /r ..\..\Tests\Magick.NET.Tests\bin %%a in (*.dll) do (
  if "%%~nxa"=="Magick.NET.Tests.dll" (
    echo "Running tests from: %%~dpnxa"
    vstest.console /inIsolation /platform:%platform% /logger:AppVeyor %%~dpnxa
    if %errorlevel% neq 0 exit /b %errorlevel%
  )
)
