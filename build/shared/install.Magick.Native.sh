#!/bin/bash
set -e

platform=$1
architecture=$2
quantum=$3

cd src/Magick.Native

if [ "$platform" != "windows" ]; then
    ./install.resources.sh $architecture $quantum
    ./install.library.sh $platform $architecture $quantum
    exit 0
fi

target_architecture=$architecture

if [ "$architecture" = "Any CPU" ]; then
    target_architecture="AnyCPU"
    ./install.resources.sh x64 $quantum
    ./install.library.sh windows x86 $quantum $target_architecture
    ./install.library.sh windows x64 $quantum $target_architecture
    ./install.library.sh windows arm64 $quantum $target_architecture
else
    ./install.resources.sh $architecture $quantum
    ./install.library.sh windows $architecture $quantum
fi

case "$architecture" in
    x64|"Any CPU")
        ./install.library.sh linux x64 $quantum $target_architecture
        ./install.library.sh linux-musl x64 $quantum $target_architecture

        case "$quantum" in
            *-OpenMP) ;;
            *) ./install.library.sh macos x64 $quantum $target_architecture ;;
        esac
        ;;
esac

case "$architecture" in
    arm64|"Any CPU")
        ./install.library.sh linux arm64 $quantum $target_architecture

        case "$quantum" in
            *-OpenMP) ;;
            *) ./install.library.sh macos arm64 $quantum $target_architecture ;;
        esac
        ;;
esac
