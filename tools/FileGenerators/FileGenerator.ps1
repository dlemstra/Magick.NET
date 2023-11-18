# Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
# Licensed under the Apache License, Version 2.0.

param (
    [string]$name,
    [string]$buildMagickNET = $true
)

. $PSScriptRoot\..\windows\utils.ps1

function executeFile($path)
{
  $executable = fullPath $path
  Invoke-Expression $executable
  checkExitCode "Failed to execute: $($path)"
}

function generateFiles($name)
{
  buildSolution "tools\FileGenerators\FileGenerator.$name.sln" "Configuration=Release"
  executeFile "tools\FileGenerators\$name\bin\x64\Release\net8\FileGenerator.$name.exe"
}

function buildMagickNET()
{
  buildSolution "Magick.NET.sln" "Configuration=ReleaseQ16,RunCodeAnalysis=false,Platform=x64"
}

if ($buildMagickNET -eq $true)
{
  buildMagickNET
}

generateFiles $name
