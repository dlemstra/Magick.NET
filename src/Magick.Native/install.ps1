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

function installPackage($version, $target) {
    Remove-Item $target -Recurse -ErrorAction Ignore
    [void](New-Item -ItemType directory -Path $target)

    $temp = "$PSScriptRoot\packages"
    Remove-Item $temp -Recurse -ErrorAction Ignore
    [void](New-Item -ItemType directory -Path $temp)

    # Temporary download from DropBox
    $url = "https://dl.dropboxusercontent.com/s/170ady5k3v7390y/Magick.Native.$version.nupkg"
    Invoke-WebRequest $url -Outfile "$temp\Magick.Native.$version.nupkg"
    ..\..\tools\windows\nuget.exe install Magick.Native -Version $version -OutputDirectory "$target" -Source $temp

    #..\..\tools\windows\nuget.exe install Magick.Native -Version $version -OutputDirectory $temp

    Remove-Item $temp -Recurse -ErrorAction Ignore
}

function copyToSamplesProject($source, $target, $quantum, $platform) {
    $fileName = "Magick.Native-$quantum-$platform.dll"
    [void](New-Item -ItemType directory -Force -Path "$target\Test$quantum\$platform\net40")
    Copy-Item "$source\$fileName" "$target\Test$quantum\$platform\net40\$fileName"
}

function copyToSamplesProjects($source, $target) {
    copyToTestProject $source $target "Q8" "x86"
    copyToTestProject $source $target "Q8" "x64"
    copyToTestProject $source $target "Q16" "x86"
    copyToTestProject $source $target "Q16" "x64"
    copyToTestProject $source $target "Q16-HDRI" "x86"
    copyToTestProject $source $target "Q16-HDRI" "x64"
}

function copyToTestProject($source, $target, $quantum, $platform) {
    $fileName = "Magick.Native-$quantum-$platform.dll"
    [void](New-Item -ItemType directory -Force -Path "$target\Test$quantum\$platform\net45")
    Copy-Item "$source\$fileName" "$target\Test$quantum\$platform\net45\$fileName"
    [void](New-Item -ItemType directory -Force -Path "$target\Test$quantum\$platform\netcoreapp2.0")
    Copy-Item "$source\$fileName" "$target\Test$quantum\$platform\netcoreapp2.0\$fileName"
}

function copyToTestProjects($source, $target) {
    copyToTestProject $source $target "Q8" "x86"
    copyToTestProject $source $target "Q8" "x64"
    copyToTestProject $source $target "Q8-OpenMP" "x64"
    copyToTestProject $source $target "Q16" "x86"
    copyToTestProject $source $target "Q16" "x64"
    copyToTestProject $source $target "Q16-OpenMP" "x64"
    copyToTestProject $source $target "Q16-HDRI" "x86"
    copyToTestProject $source $target "Q16-HDRI" "x64"
    copyToTestProject $source $target "Q16-HDRI-OpenMP" "x64"
}

function copyMetadata($source, $target) {
    Copy-Item "$source\**\content\*.md" -Force "$target"
}

function copyLibraries($source, $target) {
    Remove-Item $target -Recurse -ErrorAction Ignore
    [void](New-Item -ItemType directory -Path $target)

    Copy-Item "$source\**\content\**\**\*.dll" -Force "$target"
    Copy-Item "$source\**\content\**\**\*.so" -Force "$target"
    Copy-Item "$source\**\content\**\**\*.dylib" -Force "$target"
}

function copyResource($source, $target, $quantum) {
    [void](New-Item -ItemType directory -Path "$target\Release$quantum")
    Copy-Item "$source\**\content\Release$quantum\x64\*.xml" -Force "$target\Release$quantum"
}

function copyResources($source, $target) {
    Remove-Item $target -Recurse -ErrorAction Ignore
    [void](New-Item -ItemType directory -Path $target)

    copyResource $source $target "Q8"
    copyResource $source $target "Q16"
    copyResource $source $target "Q16-HDRI"
}

function createCompressedLibrary($folder, $quantum, $platform) {
    $output = New-Object System.IO.FileStream "$folder\Magick.Native-$quantum-$platform.gz", ([IO.FileMode]::Create), ([IO.FileAccess]::Write), ([IO.FileShare]::None)
    $gzipStream = New-Object System.IO.Compression.GzipStream $output, ([IO.Compression.CompressionMode]::Compress)
    $input = New-Object System.IO.FileStream "$folder\Magick.Native-$quantum-$platform.dll", ([IO.FileMode]::Open), ([IO.FileAccess]::Read), ([IO.FileShare]::Read)
    $buffer = New-Object byte[]($input.Length)
    $buffer = New-Object byte[]($input.Length)
    [void]($input.Read($buffer, 0, $input.Length))
    $gzipStream.Write($buffer, 0, $buffer.Length)
    $input.Close()
    $gzipStream.Close()
    $output.Close()
}

function createCompressedLibraries($folder) {
    createCompressedLibrary $folder "Q8" "x86"
    createCompressedLibrary $folder "Q8" "x64"
    createCompressedLibrary $folder "Q16" "x86"
    createCompressedLibrary $folder "Q16" "x64"
    createCompressedLibrary $folder "Q16-HDRI" "x86"
    createCompressedLibrary $folder "Q16-HDRI" "x64"
}

function createTrademarkAttribute($source, $target) {
    $fileName = "$target\TrademarkAttribute.cs"
    Remove-Item $target -Recurse -ErrorAction Ignore

    $imageMagickVersion = (Get-Item "$source\**\content\ReleaseQ8\x64\*.dll").VersionInfo.FileVersion
    ([IO.File]::WriteAllText($fileName, "// <auto-generated/>
[assembly: System.Reflection.AssemblyTrademark(""ImageMagick $imageMagickVersion"")]"))
}

$version = [IO.File]::ReadAllText("$PSScriptRoot\Magick.Native.version").Trim()
$folder = "$PSScriptRoot\temp"
$libraries = "$PSScriptRoot\libraries"
$resources = "$PSScriptRoot\resources"
$testFolder = "$PSScriptRoot\..\..\tests\Magick.NET.Tests\bin"
$samplesFolder = "$PSScriptRoot\..\..\samples\Magick.NET.Samples\bin"

installPackage $version $folder
copyMetadata $folder $PSScriptRoot
copyLibraries $folder $libraries
copyResources $folder $resources
copyToTestProjects $libraries $testFolder
copyToSamplesProjects $libraries $samplesFolder
createCompressedLibraries $libraries
createTrademarkAttribute $folder $PSScriptRoot
Remove-Item $folder -Recurse -ErrorAction Ignore
