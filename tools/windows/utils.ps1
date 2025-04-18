# Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
# Licensed under the Apache License, Version 2.0.

function checkExitCode($msg) {
    if ($LastExitCode -ne 0) {
        Write-Error $msg
        Exit 1
    }
}

function fullPath($path) {
    return "$PSScriptRoot\..\..\$path"
}

function buildSolution($solution, $properties) {
    $path = fullPath $solution
    $directory = Split-Path -parent $path
    $filename = Split-Path -leaf $path

    $location = $(Get-Location)
    Set-Location $directory

    msbuild $filename /restore ("/p:$($properties)")
    checkExitCode "Failed to build: $($path)"

    msbuild $filename /m /t:Rebuild ("/p:$($properties)")
    checkExitCode "Failed to build: $($path)"

    Set-Location $location
}
