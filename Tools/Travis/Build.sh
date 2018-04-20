#!/bin/bash -e

# Install fonts
sudo sh -c "echo ttf-mscorefonts-installer msttcorefonts/accepted-mscorefonts-eula select true | debconf-set-selections"
sudo apt-get install ttf-mscorefonts-installer
sudo fc-cache

# Build Q8
dotnet build Tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp2.0 --runtime linux-x64 -c TestQ8

wget -O Q8.zip https://www.dropbox.com/sh/tycq7qh50zssgr8/AACt2zhBn8GdVlWVxsKzw71Ka?dl=1
unzip Q8.zip -x / -d Tests/Magick.NET.Tests/bin/TestQ8/AnyCPU/netcoreapp2.0/

dotnet test Tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp2.0 -c TestQ8

# Build Q16
dotnet build Tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp2.0 --runtime linux-x64 -c TestQ16

wget -O Q16.zip https://www.dropbox.com/sh/5xmw2tl3q7a930u/AADN7QlNsvzhFXazc3FrleTYa?dl=1
unzip Q16.zip -x / -d Tests/Magick.NET.Tests/bin/TestQ16/AnyCPU/netcoreapp2.0/

dotnet test Tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp2.0 -c TestQ16

# Build Q16-HDRI
dotnet build Tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp2.0 --runtime linux-x64 -c TestQ16-HDRI

wget -O Q16-HDRI.zip https://www.dropbox.com/sh/tbsisu1byt8csbr/AABv32VeYzU6IBE5EyWL3v9za?dl=1
unzip Q16-HDRI.zip -x / -d Tests/Magick.NET.Tests/bin/TestQ16-HDRI/AnyCPU/netcoreapp2.0/

dotnet test Tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp2.0 -c TestQ16-HDRI