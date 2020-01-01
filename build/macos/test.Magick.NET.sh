#!/bin/bash
set -e

buildAndTest() {
    local quantum=$1

    dotnet build tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp2.0 -c Test$quantum
    dotnet test tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp2.0 -c Test$quantum
}

./imagick_type_gen.pl > type.xml
cat type.xml
cwd=$(pwd)
export MAGICK_FONT_PATH=$cwd

cd ../../src/Magick.Native
./install.sh

cd ../../
buildAndTest "Q8"
buildAndTest "Q16"
buildAndTest "Q16-HDRI"
