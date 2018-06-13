#!/bin/bash

rm -Rf verify
mkdir verify

wget -O verify/Magick.NET-Q8-x64.Native.dll.so https://www.dropbox.com/s/j2bf7pjg78un8p9/Magick.NET-Q8-x64.Native.dll.so?dl=1
wget -O verify/Magick.NET-Q16-x64.Native.dll.so https://www.dropbox.com/s/04i9mjj7f43en9x/Magick.NET-Q16-x64.Native.dll.so?dl=1
wget -O verify/Magick.NET-Q16-HDRI-x64.Native.dll.so https://www.dropbox.com/s/hhhcjoumoeoejun/Magick.NET-Q16-HDRI-x64.Native.dll.so?dl=1

docker build -f Linux.Verify.Dockerfile .