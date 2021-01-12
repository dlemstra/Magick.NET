#!/bin/sh
set -e

buildAndTest() {
    local quantum=$1

    dotnet build tests/Magick.NET.Core.Tests/Magick.NET.Core.Tests.csproj -f netcoreapp3.1 -c Test$quantum-OpenMP
    dotnet test tests/Magick.NET.Core.Tests/Magick.NET.Core.Tests.csproj -f netcoreapp3.1 -c Test$quantum-OpenMP
    dotnet build tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp3.1 -c Test$quantum-OpenMP
    dotnet test tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp3.1 -c Test$quantum-OpenMP
}

filename=tests/Magick.NET.Tests/Images/Coders/sample.pdf
gs -o fixed.pdf -sDEVICE=pdfwrite -dPDFSETTINGS=/prepress $filename
mv fixed.pdf $filename

buildAndTest "Q8"
buildAndTest "Q16"
buildAndTest "Q16-HDRI"
