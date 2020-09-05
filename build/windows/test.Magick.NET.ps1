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
    [string]$quantumName = $env:QuantumName,
    [string]$platformName = $env:PlatformName
)

. $PSScriptRoot\..\..\tools\windows\utils.ps1

function runTests($quantumName, $platformName, $targetFramework, $project) {
    $platform = $platformName
    $testPlatform = $platformName

    if ($platform -eq "Any CPU") {
      $platform = "AnyCPU"
      $testPlatform = "x64"
    }

    $folder = fullPath "tests\$project.Tests\bin\Test$quantumName\$platform\$targetFramework"
    $fileName = "$folder\$project.Tests.dll"

    $vstest = "$($env:VSINSTALLDIR)\Common7\IDE\Extensions\TestPlatform\vstest.console.exe"
    & $vstest $fileName /platform:$testPlatform /TestAdapterPath:$folder

    CheckExitCode("Failed to test Magick.NET")
}

function testMagickNET($quantumName, $platformName) {
    runTests $quantumName $platformName "net45" "Magick.NET"

    if ($platformName -ne "Any CPU") {
        runTests $quantumName $platformName "netcoreapp3.0" "Magick.NET"

        if ($quantumName -like "*OpenMP*") {
            return
        }

        runTests $quantumName $platformName "netcoreapp3.0" "Magick.NET.SystemDrawing"
        runTests $quantumName $platformName "netcoreapp3.0" "Magick.NET.SystemWindowsMedia"
    } else {
        runTests "" $platformName "net45" "Magick.NET.Core"
        runTests "" $platformName "netcoreapp3.0" "Magick.NET.Core"
    }

    if ($quantumName -like "*OpenMP*") {
        return
    }

    runTests $quantumName $platformName "net45" "Magick.NET.SystemDrawing"
    runTests $quantumName $platformName "net45" "Magick.NET.SystemWindowsMedia"
}

testMagickNET $quantumName $platformName