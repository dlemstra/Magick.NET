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