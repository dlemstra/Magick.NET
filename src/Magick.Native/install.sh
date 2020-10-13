#!/bin/sh
set -e

version=`cat Magick.Native.version`

installPackage() {
    mkdir foo
    cd foo

    dotnet new console
    dotnet add package Magick.Native --version $version --package-directory nuget

    mkdir ../temp
    cp -R nuget/magick.native/$version/* ../temp
    cd ..
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
