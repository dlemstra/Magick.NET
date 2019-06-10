# Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    [parameter(mandatory=$true)][string]$destination
)

. $PSScriptRoot\..\..\tools\windows\utils.ps1

function copyLibrary($library, $quantumName, $platformName, $targetFramework, $destination) {
    $platform = $platformName

    if ($platform -eq "Any CPU") {
      $platform = "AnyCPU"
    }

    $source = fullPath "src\$library\bin\Release$quantumName\$platform\$targetFramework\$library-$quantumName-$platform.dll"
    $target = "$destination\$library\$targetFramework\$library-$quantumName-$platform.dll"

    [void](New-Item -ItemType directory -Force -Path "$destination\$library\$targetFramework")
    Copy-Item $source $target
}

function copyLibraries($quantumName, $platformName, $destination) {
    copyLibrary "Magick.NET" $quantumName $platformName "net20" $destination
    copyLibrary "Magick.NET" $quantumName $platformName "net40" $destination
    copyLibrary "Magick.NET" $quantumName $platformName "netstandard13" $destination
    copyLibrary "Magick.NET" $quantumName $platformName "netstandard20" $destination

    if (!$quantumName.EndsWith("-OpenMP")) {
        copyLibrary "Magick.NET.Web" $quantumName $platformName "net40" $destination
    }
}

function copyNativeLibrary($quantumName, $platformName, $extension, $destination) {
    if ($platformName -eq "Any CPU") {
      return
    }

    $source = fullPath "src\Magick.Native\libraries\Magick.Native-$quantumName-$platformName$extension"
    $target = "$destination\Magick.NET\Magick.Native-$quantumName-$platformName$extension"
    Copy-Item $source $target
}

function copyNativeLibraries($quantumName, $platformName, $destination) {
    copyNativeLibrary $quantumName $platformName ".dll" $destination

    if ($platformName -eq "x64") {
        copyNativeLibrary $quantumName $platformName ".dll.so" $destination
        copyNativeLibrary $quantumName $platformName ".dll.dylib" $destination
    }
}

copyLibraries $quantumName $platformName $destination
copyNativeLibraries $quantumName $platformName $destination
