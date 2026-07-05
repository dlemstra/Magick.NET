#!/bin/bash
set -e

architecture=$1
quantumName=$2

config=Test$quantumName

dotnet build tests/Magick.NET.Core.Tests/Magick.NET.Core.Tests.csproj -f net8.0 -c $config -p:Platform=$architecture
dotnet build tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f net8.0 -c $config -p:Platform=$architecture
