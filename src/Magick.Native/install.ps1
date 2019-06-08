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

function installPackage($version, $target) {
    Remove-Item $target -Recurse -ErrorAction Ignore
    [void](New-Item -ItemType directory -Path $target)

    $temp = "$PSScriptRoot\temp"
    Remove-Item $temp -Recurse -ErrorAction Ignore
    [void](New-Item -ItemType directory -Path $temp)
    ..\..\tools\nuget.exe install Magick.Native -Version $version -OutputDirectory $temp

    Copy-Item "$temp\**\content\*.*" -Destination $target
    Remove-Item $temp -Recurse -ErrorAction Ignore
}

function copyToTestProject($source, $target, $quantum, $platform) {
    $fileName = "Magick.Native-$quantum-$platform.dll"
    [void](New-Item -ItemType directory -Force -Path "$target\Test$quantum\$platform\net45")
    Copy-Item "$source\$fileName" "$target\Test$quantum\$platform\net45\$fileName"
    [void](New-Item -ItemType directory -Force -Path "$target\Test$quantum\$platform\netcoreapp2.0")
    Copy-Item "$source\$fileName" "$target\Test$quantum\$platform\netcoreapp2.0\$fileName"
}

$version = [IO.File]::ReadAllText("$PSScriptRoot\version").Trim()
$folder = "$PSScriptRoot\libraries"
$testFolder = "$PSScriptRoot\..\..\tests\Magick.NET.Tests\bin"

installPackage $version $folder
copyToTestProject $folder $testFolder "Q8" "x86"
copyToTestProject $folder $testFolder "Q8" "x64"
copyToTestProject $folder $testFolder "Q8-OpenMP" "x64"
copyToTestProject $folder $testFolder "Q16" "x86"
copyToTestProject $folder $testFolder "Q16" "x64"
copyToTestProject $folder $testFolder "Q16-OpenMP" "x64"
copyToTestProject $folder $testFolder "Q16-HDRI" "x86"
copyToTestProject $folder $testFolder "Q16-HDRI" "x64"
copyToTestProject $folder $testFolder "Q16-HDRI-OpenMP" "x64"
