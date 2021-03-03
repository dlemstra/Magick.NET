# Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
# Licensed under the Apache License, Version 2.0.

param (
    [string]$config = "Release",
    [string]$quantumName = $env:QuantumName,
    [string]$platformName = $env:PlatformName
)

. $PSScriptRoot\..\..\tools\windows\utils.ps1

function buildMagickNET($config, $quantumName, $platformName) {
    buildSolution "Magick.NET.sln" "Configuration=$config$quantumName,Platform=$platformName"
}

buildMagickNET $config $quantumName $platformName