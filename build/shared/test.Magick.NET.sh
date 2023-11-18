#!/bin/bash
set -e

openmp=$1

testMagickNET() {
    local config=Test$1
    if [ "$openmp" == "OpenMP" ]; then
        config=$config-OpenMP
    fi

    dotnet test tests/Magick.NET.Core.Tests/Magick.NET.Core.Tests.csproj -f net8 --no-build -c $config
    dotnet test tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f net8 --no-build -c $config
}

testMagickNET "Q8"
testMagickNET "Q16"
testMagickNET "Q16-HDRI"
