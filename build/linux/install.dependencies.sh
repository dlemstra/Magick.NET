#!/bin/bash
set -e

apt-get update

apt-get install fontconfig unzip -y

git clone https://github.com/ImageMagick/msttcorefonts msttcorefonts
cd msttcorefonts
. ./install.sh

fc-cache

wget https://github.com/ArtifexSoftware/ghostpdl-downloads/releases/download/gs9533/ghostscript-9.53.3-linux-x86_64.tgz
tar zxvf ghostscript-9.53.3-linux-x86_64.tgz
cp ghostscript-9.53.3-linux-x86_64/gs-9533-linux-x86_64 /usr/bin/gs