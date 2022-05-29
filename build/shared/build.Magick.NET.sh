#!/bin/bash
set -e

buildMagickNET() {
    local quantum=$1

    dotnet build tests/Magick.NET.Core.Tests/Magick.NET.Core.Tests.csproj -f net60 -c Test$quantum
    dotnet build tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f net60 -c Test$quantum
}

buildMagickNET "Q8"
buildMagickNET "Q16"
buildMagickNET "Q16-HDRI"
