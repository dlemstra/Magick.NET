#!/bin/bash
set -e

testMagickNET() {
    local quantum=$1

    dotnet test tests/Magick.NET.Core.Tests/Magick.NET.Core.Tests.csproj -f net60 -c Test$quantum
    dotnet test tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f net60 -c Test$quantum
}

testMagickNET "Q8"
testMagickNET "Q16"
testMagickNET "Q16-HDRI"
