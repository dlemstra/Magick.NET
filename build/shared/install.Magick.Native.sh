#!/bin/bash
set -e

github_username=$1
github_token=$2
config=$3
arch=$4

cd src/Magick.Native
./create-nuget-config.sh $github_username $github_token
./install.sh $config $arch
