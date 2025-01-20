#!/bin/bash
set -e

sudo apt-get update

sudo apt-get install ffmpeg fontconfig unzip -y

git clone https://github.com/ImageMagick/msttcorefonts msttcorefonts
cd msttcorefonts
sudo ./install.sh

fc-cache
