#!/bin/bash
set -e

version=$(<Magick.Native.version)

installPackage() {
    mkdir temp

    # Temporary download from DropBox
    nuget_url="https://dl.dropboxusercontent.com/s/r0lvym0jrqyw0hz/Magick.Native.$version.nupkg"
    curl -s -o Magick.Native.$version.nupkg $nuget_url

    cwd=$(pwd)
    nuget install Magick.Native -Version $version -OutputDirectory temp -Source $cwd

    #nuget install Magick.Native -Version $version -OutputDirectory temp
}

copyToTestProject() {
    local quantum=$1

    folder=../../tests/Magick.NET.Tests/bin/Test$quantum/AnyCPU/netcoreapp2.0
    mkdir -p $folder

    cp temp/Magick.Native.$version/content/Release$quantum/x64/Magick.Native-$quantum-x64.dll.so $folder
    cp temp/Magick.Native.$version/content/Release$quantum/x64/Magick.Native-$quantum-x64.dll.dylib $folder

    folder=resources/Release$quantum
    mkdir -p $folder
    cp temp/Magick.Native.$version/content/Release$quantum/x64/*.xml $folder
}

copyToTestProjects() {
    copyToTestProject "Q8"
    copyToTestProject "Q16"
    copyToTestProject "Q16-HDRI"
}

installPackage
copyToTestProjects