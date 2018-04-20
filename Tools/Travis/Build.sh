#!/bin/bash -e

# Build Q8
dotnet build Tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp2.0 --runtime linux-x64 -c TestQ8

wget -O Q8.zip https://www.dropbox.com/sh/tycq7qh50zssgr8/AACt2zhBn8GdVlWVxsKzw71Ka?dl=1
unzip Q8.zip -x / -d Tests/Magick.NET.Tests/bin/TestQ8/AnyCPU/netcoreapp2.0/

dotnet test Tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp2.0 -c TestQ8