#!/bin/bash

cd ../../ImageMagick/Source
./Checkout.sh Linux

git clone git://git.sv.nongnu.org/freetype/freetype2.git ImageMagick/freetype
cd ImageMagick/freetype
git checkout VER-2-9

cd ../../../../
docker build -t dlemstra/magick.net-linux -f Source/Magick.NET.CrossPlatform/Linux.Dockerfile .
docker push dlemstra/magick.net-linux