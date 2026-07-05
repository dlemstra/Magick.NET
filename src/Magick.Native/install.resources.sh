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

architecture=$1
quantumName=$2

case "$quantumName" in
    *-OpenMP)
        quantum=${quantumName%-OpenMP}
        ;;
    *)
        quantum=$quantumName
        ;;
esac

mkdir -p resources
downloadAsset "resources-$quantum-$architecture.zip" resources

folder=resources/Release$quantum/$architecture
mkdir -p $folder
mv resources/*.xml $folder
