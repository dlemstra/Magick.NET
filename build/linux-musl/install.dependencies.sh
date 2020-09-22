#!/bin/sh
set -e

apk update

apk add curl fontconfig git

git clone https://github.com/ImageMagick/msttcorefonts msttcorefonts
cd msttcorefonts
. ./install.sh

fc-cache
