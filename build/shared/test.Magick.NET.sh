#!/bin/bash
set -e

architecture=$1
quantumName=$2

config=Test$quantumName

export FONTCONFIG_PATH=/etc/fonts
export FONTCONFIG_FILE=/etc/fonts/fonts.conf

./tests/Magick.NET.Core.Tests/bin/$config/$architecture/net8.0/Magick.NET.Core.Tests
./tests/Magick.NET.Tests/bin/$config/$architecture/net8.0/Magick.NET.Tests
