# Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
# Licensed under the Apache License, Version 2.0.

param (
    [string]$library,
    [string]$version = $env:NuGetVersion,
    [string]$commit = $env:GitCommitId,
    [parameter(mandatory=$true)][string]$destination
)

. $PSScriptRoot\..\tools\windows\utils.ps1
. $PSScriptRoot\publish.shared.ps1

function createMagickNetLibraryNuGetPackage($library, $version, $commit) {
    $xml = loadAndInitNuSpec $library $version $commit

    if ($library -eq "Magick.NET.SystemWindowsMedia" -or $library -eq "Magick.NET.AvaloniaMediaImaging") {
        addLibrary $xml $library "" "AnyCPU" "net462"
        addLibrary $xml $library "" "AnyCPU" "net8.0"
    } elseif ($library -eq "Magick.NET.SystemDrawing") {
        addLibrary $xml $library "" "AnyCPU" "net462"
        addLibrary $xml $library "" "AnyCPU" "netstandard20"
        addLibrary $xml $library "" "AnyCPU" "net8.0"
    } else {
        addLibrary $xml $library "" "AnyCPU" "netstandard20"
        addLibrary $xml $library "" "AnyCPU" "net8.0"
    }

    createNuGetPackage $xml $library $version
}

createMagickNetLibraryNuGetPackage $library $version $commit
copyNuGetPackages $destination
