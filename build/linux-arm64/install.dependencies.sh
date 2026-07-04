#!/bin/bash
set -e

sudo apt-get -qq update

sudo apt-get -qq install ffmpeg fontconfig unzip -y

git clone -q https://github.com/ImageMagick/msttcorefonts msttcorefonts
cd msttcorefonts
sudo ./install.sh

fc-cache
