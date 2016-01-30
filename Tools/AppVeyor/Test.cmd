@echo off
call "%vs140comntools%vsvars32.bat"

set platform="%1"
if "%1" == "AnyCPU" (
  set platform="x86"
)

for /r ..\..\Magick.NET.Tests\bin %%a in (*.dll) do (
  if "%%~nxa"=="Magick.NET.Tests.dll" (
    echo "Running tests from: %%~dpnxa"
    vstest.console /inIsolation /platform:%platform% /logger:AppVeyor %%~dpnxa
    if %ERRORLEVEL% neq 0 goto done
  )
)

:done
