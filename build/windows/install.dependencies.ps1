# Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
# Licensed under the Apache License, Version 2.0.

param (
    [string]$pfxUri = ''
)

Write-Host "Downloading NuGet"
$sourceNugetExe = "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"
$targetNugetExe = "$PSScriptRoot\..\..\tools\windows\nuget.exe"
Invoke-WebRequest $sourceNugetExe -OutFile $targetNugetExe

Write-Host "Downloading Ghostscript 9.55.0"
$sourceGhostscriptExe = "https://github.com/ArtifexSoftware/ghostpdl-downloads/releases/download/gs9550/gs9550w32.exe"
$targetGhostscriptExe = "$PSScriptRoot\..\..\tools\windows\gs9550w32.exe"
Invoke-WebRequest $sourceGhostscriptExe -OutFile $targetGhostscriptExe

if ($pfxUri.Length -gt 0) {
    Write-Host "Downloading code signing certificate"
    Invoke-WebRequest $pfxUri -OutFile "$PSScriptRoot\ImageMagick.pfx"
}
