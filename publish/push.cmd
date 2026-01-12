@echo off

if "%NUGET_API_KEY%"=="" (
    echo Error: NUGET_API_KEY environment variable is not set
    exit /b 1
)

echo Waiting 5 minutes before pushing packages...
ping -n 301 127.0.0.1 > nul

for /r %%i in (*.nupkg) do ..\tools\windows\nuget.exe push %%i -ApiKey %NUGET_API_KEY% -Source "https://api.nuget.org/v3/index.json"
