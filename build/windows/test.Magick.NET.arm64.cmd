@echo off
call "..\..\tools\windows\init.visualstudio.cmd"

echo "src\Magick.Native\Install.cmd should be executed before running the tests"
pause

"c:\Program Files\dotnet\dotnet.exe" test -c TestQ8 /p:Platform=arm64 --framework net60 ..\..\tests\Magick.NET.Tests\Magick.NET.Tests.csproj
if %errorlevel% neq 0 exit /b %errorlevel%
"c:\Program Files\dotnet\dotnet.exe" test -c TestQ16 /p:Platform=arm64 --framework net60 ..\..\tests\Magick.NET.Tests\Magick.NET.Tests.csproj
if %errorlevel% neq 0 exit /b %errorlevel%
"c:\Program Files\dotnet\dotnet.exe" test -c TestQ16-HDRI /p:Platform=arm64 --framework net60 ..\..\tests\Magick.NET.Tests\Magick.NET.Tests.csproj
if %errorlevel% neq 0 exit /b %errorlevel%
pause
