#!/bin/bash

cd ../../ImageMagick/Source
./Checkout.sh Linux

rm -Rf ImageMagick/freetype
git clone git://git.sv.nongnu.org/freetype/freetype2.git ImageMagick/freetype
cd ImageMagick/freetype
git checkout VER-2-9
cd ../../

rm -Rf ImageMagick/fontconfig
git clone git://anongit.freedesktop.org/fontconfig ImageMagick/fontconfig
cd ImageMagick/fontconfig
git checkout 2.12.6
cd ../../

cd ../../
docker build -t dlemstra/magick.net-linux -f Source/Magick.NET.CrossPlatform/Linux.Dockerfile .
docker push dlemstra/magick.net-linux