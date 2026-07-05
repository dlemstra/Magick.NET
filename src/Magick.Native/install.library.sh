#!/bin/sh
set -e

version=`cat Magick.Native.version`

downloadAsset() {
    local pattern=$1
    local targetFolder=$2

    echo "Downloading $1 ($version)"
    gh release download "$version" --repo "dlemstra/Magick.Native" --pattern "$pattern" --dir "$targetFolder" --clobber

    local zipFile=$targetFolder/$1
    unzip -oq "$zipFile" -d $targetFolder
    rm -f "$zipFile"
}

copyLibrary() {
    local sourceFolder=$1
    local architecture=$2
    local quantumName=$3
    local target_architecture=$4
    local test_project="$5.Tests"
    local tfm=$6

    local fileName=$sourceFolder/Magick.Native-$quantumName-$architecture.dll*
    local folder=../../tests/$test_project/bin/Test$quantumName/$target_architecture/$tfm

    mkdir -p $folder
    cp $fileName $folder
}

copyToTestProject() {
    local sourceFolder=$1
    local platform=$2
    local architecture=$3
    local quantum=$4
    local openmp=$5
    local target_architecture=${6:-$architecture}

    local quantumName=$quantum$openmp

    copyLibrary $sourceFolder $architecture $quantumName $target_architecture Magick.NET net8.0

    if [ "$platform" = "windows" ]; then
        copyLibrary $sourceFolder $architecture $quantumName $target_architecture Magick.NET net472
        copyLibrary $sourceFolder $architecture $quantumName $target_architecture Magick.NET.AvaloniaMediaImaging net8.0
        copyLibrary $sourceFolder $architecture $quantumName $target_architecture Magick.NET.SystemDrawing net8.0
        copyLibrary $sourceFolder $architecture $quantumName $target_architecture Magick.NET.SystemDrawing net472
        copyLibrary $sourceFolder $architecture $quantumName $target_architecture Magick.NET.SystemWindowsMedia net8.0
        copyLibrary $sourceFolder $architecture $quantumName $target_architecture Magick.NET.SystemWindowsMedia net472
    fi
}

platform=$1
architecture=$2
quantum=$3
target_architecture=${4:-}

case "$quantumName" in
    *-OpenMP)
        quantum=${quantum%-OpenMP}
        openmp="-OpenMP"
        ;;
    *)
        openmp=""
        ;;
esac

case "$platform" in
    windows)
        folder=libraries/win
        ;;
    macos)
        folder=libraries/osx
        ;;
    *)
        folder=libraries/$platform
        ;;
esac

mkdir -p $folder
downloadAsset "$platform-$quantum-$architecture$openmp.zip" $folder
copyToTestProject $folder $platform $architecture $quantum "$openmp" $target_architecture
