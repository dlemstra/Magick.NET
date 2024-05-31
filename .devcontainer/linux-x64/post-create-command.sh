#!/bin/bash
set -e

config=$1
arch=$2

if [ -z "$GITHUB_USER" ]; then
    export GITHUB_USER='dlemstra'
fi

if [ -z "$GITHUB_TOKEN" ]; then
    export GITHUB_TOKEN=$(cat /keys/github.txt)
fi

build/shared/install.Magick.Native.sh ${GITHUB_USER} ${GITHUB_TOKEN} ${config} ${arch}
