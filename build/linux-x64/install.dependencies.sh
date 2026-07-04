#!/bin/bash
set -e

apt-get -qq update

apt-get -qq install ffmpeg fontconfig libgomp1 unzip -y > /dev/null

git clone -q https://github.com/ImageMagick/msttcorefonts msttcorefonts
cd msttcorefonts
. ./install.sh

fc-cache

wget -q https://github.com/dlemstra/Magick.NET.BuildDependencies/releases/download/build-binaries-2025-08-30/ghostscript-10.0.0-linux-x86_64.tgz
tar xzf ghostscript-10.0.0-linux-x86_64.tgz
cp ghostscript-10.0.0-linux-x86_64/gs-1000-linux-x86_64 /usr/bin/gs
chmod 755 /usr/bin/gs
