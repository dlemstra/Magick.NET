#!/bin/bash
set -e

openmp=$1

testMagickNET() {
    local config=Test$1
    if [ "$openmp" == "OpenMP" ]; then
        config=$config-OpenMP
    fi

    ./tests/Magick.NET.Core.Tests/bin/$config/AnyCPU/net8.0/Magick.NET.Core.Tests
    ./tests/Magick.NET.Tests/bin/$config/AnyCPU/net8.0/Magick.NET.Tests
}

testMagickNET "Q8"
testMagickNET "Q16"
testMagickNET "Q16-HDRI"
