#!/bin/bash -e

# Install fonts
if [[ "$TRAVIS_OS_NAME" == "linux" ]]; then
  sudo sh -c "echo ttf-mscorefonts-installer msttcorefonts/accepted-mscorefonts-eula select true | debconf-set-selections"
  sudo apt-get install ttf-mscorefonts-installer
  sudo fc-cache
else
  brew install fontconfig
fi

# Build Q8
dotnet build Tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp2.0 -c TestQ8
if [[ "$TRAVIS_OS_NAME" == "linux" ]]; then  
  link=https://www.dropbox.com/s/j2bf7pjg78un8p9/Magick.NET-Q8-x64.Native.dll.so?dl=1
  filename=Magick.NET-Q8-x64.Native.dll.so
else
  link=https://www.dropbox.com/s/w9569iz6cryg1a0/Magick.NET-Q8-x64.Native.dll.dylib?dl=1
  filename=Magick.NET-Q8-x64.Native.dll.dylib
fi
wget -O Tests/Magick.NET.Tests/bin/TestQ8/AnyCPU/netcoreapp2.0/$filename "$link"

dotnet test Tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp2.0 -c TestQ8

# Build Q16
dotnet build Tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp2.0 -c TestQ16
if [[ "$TRAVIS_OS_NAME" == "linux" ]]; then
  link=https://www.dropbox.com/s/04i9mjj7f43en9x/Magick.NET-Q16-x64.Native.dll.so?dl=1
  filename=Magick.NET-Q16-x64.Native.dll.so
else
  link=https://www.dropbox.com/s/84vbyl4bms1eg9g/Magick.NET-Q16-x64.Native.dll.dylib?dl=1
  filename=Magick.NET-Q16-x64.Native.dll.dylib
fi
wget -O Tests/Magick.NET.Tests/bin/TestQ16/AnyCPU/netcoreapp2.0/$filename "$link"

dotnet test Tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp2.0 -c TestQ16

# Build Q16-HDRI
dotnet build Tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp2.0 -c TestQ16-HDRI
if [[ "$TRAVIS_OS_NAME" == "linux" ]]; then  
  link=https://www.dropbox.com/s/hhhcjoumoeoejun/Magick.NET-Q16-HDRI-x64.Native.dll.so?dl=1
  filename=Magick.NET-Q16-HDRI-x64.Native.dll.so
else
  link=https://www.dropbox.com/s/zqd8d1jjxswt1sp/Magick.NET-Q16-HDRI-x64.Native.dll.dylib?dl=1
  filename=Magick.NET-Q16-HDRI-x64.Native.dll.dylib
fi
wget -O Tests/Magick.NET.Tests/bin/TestQ16-HDRI/AnyCPU/netcoreapp2.0/$filename "$link"

dotnet test Tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp2.0 -c TestQ16-HDRI