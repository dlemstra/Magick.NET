# Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
# Licensed under the Apache License, Version 2.0.

param (
    [parameter(mandatory=$true)][string]$token
)

$ErrorActionPreference = "Stop"

function downloadNuGetPackages() {
    $runId = Read-Host "Please enter the run id"

    if (Test-Path .\temp) {
        Remove-Item .\temp\* -Recurse -Force | Out-Null
    } else {
        New-Item -ItemType Directory -Path .\temp | Out-Null
    }

    gh run download $runId --dir .\temp --repo dlemstra/Magick.NET

    $packages = Get-ChildItem -Path .\temp -Recurse -Filter "*.nupkg"

    foreach ($package in $packages) {
        Move-Item -Path $package.FullName -Destination ".\$($package.Name)"
    }

    Remove-Item .\temp -Recurse -Force | Out-Null
}

$env:GITHUB_TOKEN = $token
downloadNuGetPackages
