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
    local runtime=$1
    local platform=$2
    local quantum=$3
    local openmp=$4

    folder=../../tests/Magick.NET.Tests/bin/Test$quantum$openmp/AnyCPU/net8
    mkdir -p $folder
    cp temp/$runtime/Release$quantum$openmp/$platform/Magick.Native-$quantum$openmp-$platform.dll* $folder | true

    folder=resources/Release$quantum
    mkdir -p $folder
    cp temp/resources/Release$quantum$openmp/$platform/*.xml $folder | true
}

copyToTestProjects() {
    local runtime=$1
    local platform=$2

    copyToTestProject $runtime $platform "Q8" ""
    copyToTestProject $runtime $platform "Q16" ""
    copyToTestProject $runtime $platform "Q16-HDRI" ""

    copyToTestProject $runtime $platform "Q8" "-OpenMP"
    copyToTestProject $runtime $platform "Q16" "-OpenMP"
    copyToTestProject $runtime $platform "Q16-HDRI" "-OpenMP"
}

cleanupPackage
installPackage
copyToTestProjects $1 $2
cleanupPackage
