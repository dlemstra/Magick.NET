# Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
#
# Licensed under the ImageMagick License (the "License"); you may not use this file except in
# compliance with the License. You may obtain a copy of the License at
#
#   https://www.imagemagick.org/script/license.php
#
# Unless required by applicable law or agreed to in writing, software distributed under the
# License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
# either express or implied. See the License for the specific language governing permissions
# and limitations under the License.

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
    addLibrary $xml "Magick.NET" $quantumName $platform "net20"
    addLibrary $xml "Magick.NET" $quantumName $platform "net40"
    addLibrary $xml "Magick.NET" $quantumName $platform "netstandard20"
}

function addOpenMPLibrary($xml) {
    $redistFolder = "$($env:VSINSTALLDIR)VC\Redist\MSVC"
    $redistVersion = (ls -Directory $redistFolder -Filter 14.* | sort -Descending | select -First 1 -Property Name).Name
    $source = "$redistFolder\$redistVersion\x64\Microsoft.VC142.OpenMP\vcomp140.dll"
    $target = "runtimes\win-x64\native\vcomp140.dll"
    addFile $xml $source $target
}

function addNativeLibrary($xml, $quantumName, $platform, $runtime, $extension) {
    $source = fullPath "src\Magick.Native\libraries\$runtime\Magick.Native-$quantumName-$platform$extension"
    $target = "runtimes\$runtime-$platform\native\Magick.Native-$quantumName-$platform$extension"
    addFile $xml $source $target
}

function addNativeLibraries($xml, $quantumName, $platform) {
    if ($platform -eq "AnyCPU")
    {
        addNativeLibraries $xml $quantumName "x86"
        addNativeLibraries $xml $quantumName "x64"
        return
    }

    addNativeLibrary $xml $quantumName $platform "win" ".dll"

    if ($platform -eq "x64") {
        if ($quantumName.EndsWith("-OpenMP")) {
            addOpenMPLibrary $xml
            addNativeLibrary $xml $quantumName $platform "linux" "-$quantumName-$platform.dll.so"
        } else {
            addNativeLibrary $xml $quantumName $platform "linux" "-$quantumName-$platform.dll.so"
            addNativeLibrary $xml $quantumName $platform "linux-musl" "-$quantumName-$platform.dll.so"
            addNativeLibrary $xml $quantumName $platform "osx" "-$quantumName-$platform.dll.dylib"
        }
    }
}

function createMagickNetNuGetPackage($quantumName, $platform, $version, $commit, $pfxPassword) {
    $xml = loadAndInitNuSpec "Magick.NET" $version $commit

    $name = "Magick.NET-$quantumName-$platform"
    $xml.package.metadata.id = $name
    $xml.package.metadata.title = $name

    $platform = $platformName

    if ($platform -eq "Any CPU") {
        $platform = "AnyCPU"
    }

    addMagickNetLibraries $xml $quantumName $platform
    addNativeLibraries $xml $quantumName $platform

    if ($platform -ne "AnyCPU") {
        addFile $xml "Magick.NET.targets" "build\net20\$name.targets"
        addFile $xml "Magick.NET.targets" "build\net40\$name.targets"
    }

    createAndSignNuGetPackage $xml $name $version $pfxPassword
}

$platform = $platformName

if ($platform -eq "Any CPU") {
    $platform = "AnyCPU"
}

createMagickNetNuGetPackage $quantumName $platform $version $commit $pfxPassword
copyNuGetPackages $destination