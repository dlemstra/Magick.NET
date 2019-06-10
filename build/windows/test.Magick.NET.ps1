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
    [string]$platformName = $env:PlatformName
)

. $PSScriptRoot\..\..\tools\windows\utils.ps1

function runTests($quantumName, $platformName, $targetFramework) {
    $vstest = "$($env:VSINSTALLDIR)\Common7\IDE\Extensions\TestPlatform\vstest.console.exe"

    $platform = $platformName
    $testPlatform = $platformName

    if ($platform -eq "Any CPU") {
      $platform = "AnyCPU"
      $testPlatform = "x64"
    }

    $folder = fullPath "tests\Magick.NET.Tests\bin\Test$quantumName\$platform\$targetFramework"
    $fileName = "$folder\Magick.NET.Tests.dll"

    & $vstest $fileName /platform:$testPlatform /TestAdapterPath:$folder

    CheckExitCode("Failed to test Magick.NET")
}

function testMagickNET($quantumName, $platformName) {
    runTests $quantumName $platformName "net45"

    if ($platformName -ne "Any CPU") {
        runTests $quantumName $platformName "netcoreapp2.0"
    }
}

testMagickNET $quantumName $platformName