#!/bin/bash -e

# Install fonts
if [[ "$TRAVIS_OS_NAME" == "linux" ]]; then
  sudo sh -c "echo ttf-mscorefonts-installer msttcorefonts/accepted-mscorefonts-eula select true | debconf-set-selections"
  sudo apt-get install ttf-mscorefonts-installer
  sudo fc-cache
fi

# Build Q8
if [[ "$TRAVIS_OS_NAME" == "linux" ]]; then
  dotnet build Tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp2.0 --runtime linux-x64 -c TestQ8
  wget -O Q8.zip https://www.dropbox.com/sh/tycq7qh50zssgr8/AACt2zhBn8GdVlWVxsKzw71Ka?dl=1
else
  dotnet build Tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp2.0 --runtime osx-x64 -c TestQ8
  wget -O Q8.zip https://www.dropbox.com/sh/hsumjx0e99xqk5d/AACovle8G5SJgc2H7XqhhNU1a?dl=1
fi
unzip Q8.zip -x / -d Tests/Magick.NET.Tests/bin/TestQ8/AnyCPU/netcoreapp2.0/

dotnet test Tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp2.0 -c TestQ8

# Build Q16
if [[ "$TRAVIS_OS_NAME" == "linux" ]]; then
  dotnet build Tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp2.0 --runtime linux-x64 -c TestQ16
  wget -O Q16.zip https://www.dropbox.com/sh/5xmw2tl3q7a930u/AADN7QlNsvzhFXazc3FrleTYa?dl=1
else
  dotnet build Tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp2.0 --runtime osx-x64 -c TestQ16
  wget -O Q16.zip https://www.dropbox.com/sh/2bh81m9djp4kgxu/AABNBxdtHS5SYIxPEkmhMnwfa?dl=1
fi
unzip Q16.zip -x / -d Tests/Magick.NET.Tests/bin/TestQ16/AnyCPU/netcoreapp2.0/

dotnet test Tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp2.0 -c TestQ16

# Build Q16-HDRI
if [[ "$TRAVIS_OS_NAME" == "linux" ]]; then
  dotnet build Tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp2.0 --runtime linux-x64 -c TestQ16-HDRI
  wget -O Q16-HDRI.zip https://www.dropbox.com/sh/tbsisu1byt8csbr/AABv32VeYzU6IBE5EyWL3v9za?dl=1
else
  dotnet build Tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp2.0 --runtime osx-x64 -c TestQ16-HDRI
  wget -O Q16-HDRI.zip https://www.dropbox.com/sh/5mirllz36axixfh/AAC-odW0R4E9WbIAn5Ix-QIPa?dl=1
fi
unzip Q16-HDRI.zip -x / -d Tests/Magick.NET.Tests/bin/TestQ16-HDRI/AnyCPU/netcoreapp2.0/

dotnet test Tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp2.0 -c TestQ16-HDRI