# Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
# Licensed under the Apache License, Version 2.0.

param (
    [string]$quantumName = $env:QuantumName,
    [string]$platformName = $env:PlatformName,
    [string]$pfxPassword = '',
    [string]$version = $env:NuGetVersion,
    [string]$commit = $env:GitCommitId,
    [parameter(mandatory=$true)][string]$destination
)

. $PSScriptRoot\..\tools\windows\utils.ps1
. $PSScriptRoot\publish.shared.ps1

function addMagickNetLibraries($xml, $quantumName, $platform) {
    if (($platform -ne "AnyCPU") -and ($platform -ne "arm64")) {
        addLibrary $xml "Magick.NET" $quantumName $platform "net20"
    }
    addLibrary $xml "Magick.NET" $quantumName $platform "netstandard20"
    addLibrary $xml "Magick.NET" $quantumName $platform "netstandard21"
}

function addOpenMPLibrary($xml) {
    $redistFolder = "$($env:VSINSTALLDIR)VC\Redist\MSVC"
    $redistVersion = (ls -Directory $redistFolder -Filter 14.* | sort -Descending | select -First 1 -Property Name).Name
    $source = "$redistFolder\$redistVersion\x64\Microsoft.VC143.OpenMP\vcomp140.dll"
    $target = "runtimes\win-x64\native\vcomp140.dll"
    addFile $xml $source $target
}

function addNotice($xml, $runtime) {
    $source = fullPath "src\Magick.Native\libraries\$runtime\Notice.txt"
    $target = "Notice.$runtime.txt"
    addFile $xml $source $target
}

function addNativeLibrary($xml, $platform, $runtime, $suffix) {
    $source = fullPath "src\Magick.Native\libraries\$runtime\Magick.Native-$suffix"
    $target = "runtimes\$runtime-$platform\native\Magick.Native-$suffix"
    addFile $xml $source $target

    addNotice $xml $runtime
}

function addNativeLibraries($xml, $quantumName, $platform) {
    if ($platform -eq "AnyCPU") {
        addNativeLibraries $xml $quantumName "x86"
        addNativeLibraries $xml $quantumName "x64"
        addNativeLibraries $xml $quantumName "arm64"
        return
    }

    addNativeLibrary $xml $platform "win" "$quantumName-$platform.dll"

    if ($quantumName.EndsWith("-OpenMP")) {
        if ($platform -eq "x64") {
            addOpenMPLibrary $xml
            addNativeLibrary $xml $platform "linux" "$quantumName-$platform.dll.so"
        }
    } else {
        if ($platform -ne "x86") {
            addNativeLibrary $xml $platform "linux" "$quantumName-$platform.dll.so"
            if ($platform -eq "x64") {
                addNativeLibrary $xml $platform "linux-musl" "$quantumName-$platform.dll.so"
                addNativeLibrary $xml $platform "osx" "$quantumName-$platform.dll.dylib"
            }
        }
    }
}

function createMagickNetNuGetPackage($quantumName, $platform, $version, $commit, $pfxPassword) {
    $xml = loadAndInitNuSpec "Magick.NET" $version $commit

    $name = "Magick.NET-$quantumName-$platform"
    $xml.package.metadata.id = $name
    $xml.package.metadata.title = $name

    if (($platform -eq "arm64") -or ($platform -eq "AnyCPU")) {
        removetargetFramework $xml ".NETFramework2.0"
    }

    addMagickNetLibraries $xml $quantumName $platform
    addNativeLibraries $xml $quantumName $platform

    if ($platform -ne "arm64") {
        if ($platform -eq "AnyCPU") {
            addFile $xml "Magick.NET.targets" "build\netstandard20\$name.targets"
        } else {
            addFile $xml "Magick.NET.targets" "build\net20\$name.targets"
        }
    }

    createAndSignNuGetPackage $xml $name $version $pfxPassword
}

$platform = $platformName

if ($platform -eq "Any CPU") {
    $platform = "AnyCPU"
}

createMagickNetNuGetPackage $quantumName $platform $version $commit $pfxPassword
copyNuGetPackages $destination
