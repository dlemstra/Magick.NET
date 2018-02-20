#!/bin/bash

cd ../../ImageMagick/Source
./Checkout.sh Linux

cd ../../
docker build -t dlemstra/magick.net-linux -f Source/Magick.NET.CrossPlatform/Linux.Dockerfile
docker push dlemstra/magick.net-linux