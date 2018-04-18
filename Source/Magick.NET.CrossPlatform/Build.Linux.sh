#!/bin/bash

cd ../../ImageMagick/Source
./Checkout.sh Linux

if [ ! -d "ImageMagick/freetype" ]; then
  git clone git://git.sv.nongnu.org/freetype/freetype2.git ImageMagick/freetype
  if [ $? != 0 ]; then echo "Error during checkout"; exit; fi
fi
cd ImageMagick/freetype
git reset --hard
git pull origin master
git checkout VER-2-9
cd ../../

if [ ! -d "ImageMagick/fontconfig" ]; then
  git clone git://anongit.freedesktop.org/fontconfig ImageMagick/fontconfig
  if [ $? != 0 ]; then echo "Error during checkout"; exit; fi
fi
cd ImageMagick/fontconfig
git reset --hard
git pull origin master
git checkout 2.12.6
cd ../../

cd ../../
docker build -t dlemstra/magick.net-linux -f Source/Magick.NET.CrossPlatform/Linux.Dockerfile .
docker push dlemstra/magick.net-linux