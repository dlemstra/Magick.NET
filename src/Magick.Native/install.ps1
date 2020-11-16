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

    $nugetPath = "..\..\tools\windows\nuget.exe"

    if (!(Test-Path $nugetPath))
    {
        Write-Host "Downloading latest NuGet Package manager."

        Invoke-WebRequest -Uri "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe" -OutFile $nugetPath
    }

    if (!(Test-Path "nuget.config"))
    {
        $username = Read-Host "Enter your GitHub username"
        $token = Read-Host "Enter your package read token"

        $process = Start-Process -FilePath ".\create-nuget-config.cmd" -ArgumentList "$username $token" -PassThru -NoNewWindow
        $process.WaitForExit()
    }

    Write-Host "Installing Magick.Native.$version.nupkg"

    $process = Start-Process -FilePath $nugetPath -ArgumentList "install Magick.Native -Version $version -OutputDirectory $target" -PassThru -NoNewWindow
    $process.WaitForExit()

    Remove-Item $temp -Recurse -ErrorAction Ignore
}

function copyToSamplesProject($source, $target, $quantum, $platform) {
    $fileName = "Magick.Native-$quantum-$platform.dll"
    [void](New-Item -ItemType directory -Force -Path "$target\Test$quantum\$platform\net40")
    Copy-Item "$source\$fileName" "$target\Test$quantum\$platform\net40\$fileName"
}

function copyToSamplesProjects($source, $target) {
    copyToSamplesProject $source $target "Q8" "x86"
    copyToSamplesProject $source $target "Q8" "x64"
    copyToSamplesProject $source $target "Q16" "x86"
    copyToSamplesProject $source $target "Q16" "x64"
    copyToSamplesProject $source $target "Q16-HDRI" "x86"
    copyToSamplesProject $source $target "Q16-HDRI" "x64"
}

function copyToTestProject($source, $target, $quantum, $platform) {
    $fileName = "Magick.Native-$quantum-$platform.dll"
    [void](New-Item -ItemType directory -Force -Path "$target\Test$quantum\$platform\net452")
    Copy-Item "$source\$fileName" "$target\Test$quantum\$platform\net452\$fileName"
    [void](New-Item -ItemType directory -Force -Path "$target\Test$quantum\$platform\netcoreapp3.1")
    Copy-Item "$source\$fileName" "$target\Test$quantum\$platform\netcoreapp3.1\$fileName"
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

    [void](New-Item -ItemType directory -Path "$target\win")
    Copy-Item "$source\**\content\windows\**\**\*.dll" -Force "$target\win"

    [void](New-Item -ItemType directory -Path "$target\linux")
    Copy-Item "$source\**\content\linux\**\**\*.so" -Force "$target\linux"

    [void](New-Item -ItemType directory -Path "$target\linux-musl")
    Copy-Item "$source\**\content\linux-musl\**\**\*.so" -Force "$target\linux-musl"

    [void](New-Item -ItemType directory -Path "$target\osx")
    Copy-Item "$source\**\content\macos\**\**\*.dylib" -Force "$target\osx"
}

function copyResource($source, $target, $quantum) {
    [void](New-Item -ItemType directory -Path "$target\Release$quantum")
    Copy-Item "$source\**\content\resources\Release$quantum\x64\*.xml" -Force "$target\Release$quantum"
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

    $imageMagickVersion = (Get-Item "$source\Magick.Native-Q8-x64.dll").VersionInfo.FileVersion
    ([IO.File]::WriteAllText($fileName, "// <auto-generated/>
[assembly: System.Reflection.AssemblyTrademark(""ImageMagick $imageMagickVersion"")]"))
}

$version = [IO.File]::ReadAllText("$PSScriptRoot\Magick.Native.version").Trim()
$folder = "$PSScriptRoot\temp"
$libraries = "$PSScriptRoot\libraries"
$windowsLibraries = "$libraries\win"
$resources = "$PSScriptRoot\resources"
$samplesFolder = "$PSScriptRoot\..\..\samples\Magick.NET.Samples\bin"

installPackage $version $folder
copyMetadata $folder $PSScriptRoot
copyLibraries $folder $libraries
copyResources $folder $resources
copyToTestProjects $windowsLibraries "$PSScriptRoot\..\..\tests\Magick.NET.Tests\bin"
copyToTestProjects $windowsLibraries "$PSScriptRoot\..\..\tests\Magick.NET.SystemDrawing.Tests\bin"
copyToTestProjects $windowsLibraries "$PSScriptRoot\..\..\tests\Magick.NET.SystemWindowsMedia.Tests\bin"
copyToSamplesProjects $windowsLibraries $samplesFolder
createCompressedLibraries $windowsLibraries
createTrademarkAttribute $windowsLibraries $PSScriptRoot
Remove-Item $folder -Recurse -ErrorAction Ignore
