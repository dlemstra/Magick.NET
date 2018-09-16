#!/bin/bash
set -e

echo -n `git rev-parse HEAD` > Magick.NET/ImageMagick/Source/ImageMagick.commit
cd Magick.NET/Build/Linux
sudo ./Build.sh