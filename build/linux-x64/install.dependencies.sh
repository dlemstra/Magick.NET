#!/bin/bash
set -e

apt-get -qq update

apt-get -qq install curl ffmpeg fontconfig libgomp1 unzip -y > /dev/null

curl -fsSL https://cli.github.com/packages/githubcli-archive-keyring.gpg -o /usr/share/keyrings/githubcli-archive-keyring.gpg
chmod go+r /usr/share/keyrings/githubcli-archive-keyring.gpg
echo "deb [arch=$(dpkg --print-architecture) signed-by=/usr/share/keyrings/githubcli-archive-keyring.gpg] https://cli.github.com/packages stable main" > /etc/apt/sources.list.d/github-cli.list
apt-get -qq update
apt-get -qq install gh -y > /dev/null

git clone https://github.com/ImageMagick/msttcorefonts msttcorefonts
cd msttcorefonts
. ./install.sh

fc-cache

wget -q https://github.com/dlemstra/Magick.NET.BuildDependencies/releases/download/build-binaries-2025-08-30/ghostscript-10.0.0-linux-x86_64.tgz
tar xzf ghostscript-10.0.0-linux-x86_64.tgz
cp ghostscript-10.0.0-linux-x86_64/gs-1000-linux-x86_64 /usr/bin/gs
chmod 755 /usr/bin/gs
