#!/bin/sh
set -e

version=`cat Magick.Native.version`

installPackage() {
    mkdir temp

    echo "Downloading Magick.Native.$version.nupkg"
    # Temporary download from DropBox
    nuget_url="https://dl.dropboxusercontent.com/s/6gqi06zrj6q479b/Magick.Native.$version.nupkg"
    curl -s -o Magick.Native.$version.nupkg $nuget_url

    unzip Magick.Native.$version.nupkg -d temp
}

copyToTestProject() {
    local runtime=$1
    local quantum=$2

    folder=../../tests/Magick.NET.Tests/bin/Test$quantum/AnyCPU/netcoreapp3.0
    mkdir -p $folder
    cp temp/content/$runtime/Release$quantum/x64/Magick.Native-$quantum-x64.dll* $folder

    folder=resources/Release$quantum
    mkdir -p $folder
    cp temp/content/resources/Release$quantum/x64/*.xml $folder
}

copyToTestProjects() {
    local runtime=$1

    copyToTestProject $runtime "Q8"
    copyToTestProject $runtime "Q16"
    copyToTestProject $runtime "Q16-HDRI"
}

if [ ! -d "temp" ]; then
    installPackage
    copyToTestProjects $1
fi
