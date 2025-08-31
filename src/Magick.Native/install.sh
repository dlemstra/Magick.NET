#!/bin/sh
set -e

version=`cat Magick.Native.version`

installPackage() {
    mkdir foo
    cd foo

    dotnet new console --no-restore
    dotnet add package Magick.Native --version $version --package-directory nuget

    mkdir ../temp
    cp -R nuget/magick.native/$version/content/* ../temp
    cd ..
}

cleanupPackage() {
    rm -Rf foo
    rm -Rf temp
}

copyToTestProject() {
    local platform=$1
    local architecture=$2
    local quantum=$3
    local openmp=$4

    folder=../../tests/Magick.NET.Tests/bin/Test$quantum$openmp/AnyCPU/net8.0
    mkdir -p $folder
    cp temp/$platform/Release$quantum$openmp/$architecture/Magick.Native-$quantum$openmp-$architecture.dll* $folder | true

    folder=resources/Release$quantum/$architecture
    mkdir -p $folder
    cp temp/resources/Release$quantum$openmp/$architecture/*.xml $folder | true
}

copyToTestProjects() {
    local platform=$1
    local architecture=$2

    copyToTestProject $platform $architecture "Q8" ""
    copyToTestProject $platform $architecture "Q16" ""
    copyToTestProject $platform $architecture "Q16-HDRI" ""
    copyToTestProject $platform $architecture "Q8" "-OpenMP"
    copyToTestProject $platform $architecture "Q16" "-OpenMP"
    copyToTestProject $platform $architecture "Q16-HDRI" "-OpenMP"
}

cleanupPackage
installPackage
copyToTestProjects $1 $2
cleanupPackage
