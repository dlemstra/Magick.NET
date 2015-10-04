@echo off
call "%vs120comntools%vsvars32.bat"

for /r ..\..\Magick.NET.Tests\bin %%a in (*.dll) do (
  if "%%~nxa"=="Magick.NET.Tests.dll" (
    echo "Running tests from: %%~dpnxa"
    vstest.console /logger:AppVeyor %%~dpnxa
    if %ERRORLEVEL% neq 0 goto done
  )
)

:done
