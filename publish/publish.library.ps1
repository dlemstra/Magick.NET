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
    [string]$library,
    [string]$pfxPassword = '',
    [string]$version = $env:NuGetVersion,
    [string]$commit = $env:GitCommitId,
    [parameter(mandatory=$true)][string]$destination
)

. $PSScriptRoot\..\tools\windows\utils.ps1
. $PSScriptRoot\publish.shared.ps1

function createMagickNetLibraryNuGetPackage($library, $version, $commit, $pfxPassword) {
    $xml = loadAndInitNuSpec $library $version $commit

    if ($library -ne "Magick.NET.SystemWindowsMedia") {
        addLibrary $xml $library "" "AnyCPU" "net20"
    }

    addLibrary $xml $library "" "AnyCPU" "net40"

    if ($library -eq "Magick.NET.Core") {
        addLibrary $xml $library "" "AnyCPU" "netstandard13"
    }

    if ($library -ne "Magick.NET.SystemWindowsMedia") {
        addLibrary $xml $library "" "AnyCPU" "netstandard20"
    }

    createAndSignNuGetPackage $xml $library $version $pfxPassword
}

createMagickNetLibraryNuGetPackage $library $version $commit $pfxPassword
copyNuGetPackages $destination