#!/bin/sh
set -e

apk update

apk add bash curl ffmpeg fontconfig git

git clone https://github.com/ImageMagick/msttcorefonts msttcorefonts
cd msttcorefonts
. ./install.sh

fc-cache
