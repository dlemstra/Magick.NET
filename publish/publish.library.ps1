# Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    [string]$packageName = $env:PackageName,
    [string]$pfxPassword = '',
    [string]$version = $env:NuGetVersion,
    [string]$commit = $env:GitCommitId,
    [parameter(mandatory=$true)][string]$destination
)

. $PSScriptRoot\..\tools\windows\utils.ps1
. $PSScriptRoot\publish.shared.ps1

function createMagickNetLibraryNuGetPackage($packageName, $version, $commit, $pfxPassword) {
    $xml = loadAndInitNuSpec $packageName $version $commit

    if ($packageName -ne "Magick.NET.SystemWindowsMedia") {
        addLibrary $xml $packageName "" "AnyCPU" "net20"
    }

    addLibrary $xml $packageName "" "AnyCPU" "net40"

    if ($packageName -eq "Magick.NET.Core") {
        addLibrary $xml $packageName "" "AnyCPU" "netstandard13"
    }

    if ($packageName -ne "Magick.NET.SystemWindowsMedia") {
        addLibrary $xml $packageName "" "AnyCPU" "netstandard20"
    }

    createAndSignNuGetPackage $xml $packageName $version $pfxPassword
}

createMagickNetLibraryNuGetPackage $packageName $version $commit $pfxPassword
copyNuGetPackages $destination