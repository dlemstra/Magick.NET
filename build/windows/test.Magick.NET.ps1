# Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
# Licensed under the Apache License, Version 2.0.

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
    runTests $quantumName $platformName "net462" "Magick.NET"

    if ($platformName -ne "Any CPU") {
        runTests $quantumName $platformName "net8.0" "Magick.NET"

        if ($quantumName -like "*OpenMP*") {
            return
        }

        runTests $quantumName $platformName "net8.0" "Magick.NET.SystemDrawing"
        runTests $quantumName $platformName "net8.0" "Magick.NET.SystemWindowsMedia"
    } else {
        runTests "" $platformName "net462" "Magick.NET.Core"
        runTests "" $platformName "net8.0" "Magick.NET.Core"
    }

    if ($quantumName -like "*OpenMP*") {
        return
    }

    runTests $quantumName $platformName "net462" "Magick.NET.SystemDrawing"
    runTests $quantumName $platformName "net462" "Magick.NET.SystemWindowsMedia"
}

testMagickNET $quantumName $platformName
