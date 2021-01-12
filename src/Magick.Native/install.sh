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
    local openmp=$3

    folder=../../tests/Magick.NET.Tests/bin/Test$quantum$openmp/AnyCPU/netcoreapp3.1
    mkdir -p $folder
    cp temp/content/$runtime/Release$quantum$openmp/x64/Magick.Native-$quantum$openmp-x64.dll* $folder | true

    folder=resources/Release$quantum
    mkdir -p $folder
    cp temp/content/resources/Release$quantum$openmp/x64/*.xml $folder | true
}

copyToTestProjects() {
    local runtime=$1

    copyToTestProject $runtime "Q8" ""
    copyToTestProject $runtime "Q16" ""
    copyToTestProject $runtime "Q16-HDRI" ""

    copyToTestProject $runtime "Q8" "-OpenMP"
    copyToTestProject $runtime "Q16" "-OpenMP"
    copyToTestProject $runtime "Q16-HDRI" "-OpenMP"
}

if [ ! -d "temp" ]; then
    installPackage
    copyToTestProjects $1
fi
