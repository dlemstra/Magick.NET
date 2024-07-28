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
    addLibrary $xml "Magick.NET" $quantumName $platform "netstandard20"
    addLibrary $xml "Magick.NET" $quantumName $platform "net8.0"
}

function addOpenMPLibrary($xml, $platform) {
    $redistFolder = "$($env:VSINSTALLDIR)VC\Redist\MSVC"
    $redistVersion = (ls -Directory $redistFolder -Filter 14.* | sort -Descending | select -First 1 -Property Name).Name
    $source = "$redistFolder\$redistVersion\$platform\Microsoft.VC143.OpenMP\vcomp140.dll"
    $target = "runtimes\win-$platform\native\vcomp140.dll"
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

    if ($platform -eq "x86") {
        return
    }

    if ($quantumName.EndsWith("-OpenMP")) {
        addOpenMPLibrary $xml $platform
    } else {
        addNativeLibrary $xml $platform "osx" "$quantumName-$platform.dll.dylib"
    }

    addNativeLibrary $xml $platform "linux" "$quantumName-$platform.dll.so"

    if ($platform -eq "x64") {
        addNativeLibrary $xml $platform "linux-musl" "$quantumName-$platform.dll.so"
    }
}

function createMagickNetNuGetPackage($quantumName, $platform, $version, $commit, $pfxPassword) {
    $xml = loadAndInitNuSpec "Magick.NET" $version $commit

    $name = "Magick.NET-$quantumName-$platform"
    $xml.package.metadata.id = $name
    $xml.package.metadata.title = $name

    addMagickNetLibraries $xml $quantumName $platform
    addNativeLibraries $xml $quantumName $platform
    addFile $xml "Magick.NET.targets" "build\netstandard20\$name.targets"
    addFile $xml "Magick.NET.targets" "buildTransitive\netstandard20\$name.targets"

    createAndSignNuGetPackage $xml $name $version $pfxPassword
}

$platform = $platformName

if ($platform -eq "Any CPU") {
    $platform = "AnyCPU"
}

createMagickNetNuGetPackage $quantumName $platform $version $commit $pfxPassword
copyNuGetPackages $destination
