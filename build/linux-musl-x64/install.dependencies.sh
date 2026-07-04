#!/bin/sh
set -e

apk update -q

apk add -q bash curl ffmpeg fontconfig git

git clone -q https://github.com/ImageMagick/msttcorefonts msttcorefonts
cd msttcorefonts
. ./install.sh

fc-cache
