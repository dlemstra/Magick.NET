@echo off

echo "src\Magick.Native\Install.cmd should be executed before running the tests"
pause

"c:\Program Files\dotnet\dotnet.exe" test -c TestQ8 /p:Platform=arm64 --framework net8.0 ..\..\tests\Magick.NET.Tests\Magick.NET.Tests.csproj
if %errorlevel% neq 0 goto DONE
"c:\Program Files\dotnet\dotnet.exe" test -c TestQ16 /p:Platform=arm64 --framework net8.0 ..\..\tests\Magick.NET.Tests\Magick.NET.Tests.csproj
if %errorlevel% neq 0 goto DONE
"c:\Program Files\dotnet\dotnet.exe" test -c TestQ16-HDRI /p:Platform=arm64 --framework net8.0 ..\..\tests\Magick.NET.Tests\Magick.NET.Tests.csproj
if %errorlevel% neq 0 goto DONE
"c:\Program Files\dotnet\dotnet.exe" test -c TestQ8-OpenMP /p:Platform=arm64 --framework net8.0 ..\..\tests\Magick.NET.Tests\Magick.NET.Tests.csproj
if %errorlevel% neq 0 goto DONE
"c:\Program Files\dotnet\dotnet.exe" test -c TestQ16-OpenMP /p:Platform=arm64 --framework net8.0 ..\..\tests\Magick.NET.Tests\Magick.NET.Tests.csproj
if %errorlevel% neq 0 goto DONE
"c:\Program Files\dotnet\dotnet.exe" test -c TestQ16-HDRI-OpenMP /p:Platform=arm64 --framework net8.0 ..\..\tests\Magick.NET.Tests\Magick.NET.Tests.csproj
if %errorlevel% neq 0 goto DONE

:DONE
pause
