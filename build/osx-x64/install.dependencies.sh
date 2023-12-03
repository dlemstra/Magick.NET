#!/bin/bash
set -e

export HOMEBREW_NO_AUTO_UPDATE=1

brew install fontconfig
fc-list

downloadUrl=$(curl 'https://evermeet.cx/ffmpeg/info/ffmpeg/6.0' -fsS| jq -rc '.download.zip.url') 
curl -f -L -# --compressed -A 'https://github.com/eugeneware/ffmpeg-static binaries download script' -o "ffmpeg-darwin-x64.zip" "$downloadUrl"
unzip -o -d /usr/local/bin -j ffmpeg-darwin-x64.zip ffmpeg
