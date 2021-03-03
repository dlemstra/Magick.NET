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
  executeFile "tools\FileGenerators\$name\bin\Release\FileGenerator.$name.exe"
}

function buildMagickNET()
{
  buildSolution "Magick.NET.sln" "Configuration=ReleaseQ8,RunCodeAnalysis=false,TargetFramework=net40,Platform=x86"
  buildSolution "Magick.NET.sln" "Configuration=ReleaseQ16,RunCodeAnalysis=false,TargetFramework=net40,Platform=x86"
  buildSolution "Magick.NET.sln" "Configuration=ReleaseQ16-HDRI,RunCodeAnalysis=false,TargetFramework=net40,Platform=x86"
}

if ($buildMagickNET -eq $true)
{
  buildMagickNET
}

generateFiles $name