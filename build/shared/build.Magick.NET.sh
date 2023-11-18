#!/bin/bash
set -e

openmp=$1

buildMagickNET() {
    local config=Test$1
    if [ "$openmp" == "OpenMP" ]; then
        config=$config-OpenMP
    fi

    dotnet build tests/Magick.NET.Core.Tests/Magick.NET.Core.Tests.csproj -f net8 -c $config
    dotnet build tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f net8 -c $config
}

buildMagickNET "Q8"
buildMagickNET "Q16"
buildMagickNET "Q16-HDRI"
