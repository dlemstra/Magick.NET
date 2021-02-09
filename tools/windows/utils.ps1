# Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
#
# Licensed under the ImageMagick License (the "License"); you may not use this file except in
# compliance with the License. You may obtain a copy of the License at
#
#   https://imagemagick.org/script/license.php
#
# Unless required by applicable law or agreed to in writing, software distributed under the
# License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
# either express or implied. See the License for the specific language governing permissions
# and limitations under the License.

function checkExitCode($msg)
{
  if ($LastExitCode -ne 0)
  {
    Write-Error $msg
    Exit 1
  }
}

function fullPath($path)
{
  return "$PSScriptRoot\..\..\$path"
}

function buildSolution($solution, $properties)
{
    $path = fullPath $solution
    $directory = Split-Path -parent $path
    $filename = Split-Path -leaf $path
    $nuget = fullPath "tools\windows\nuget.exe"

    & $nuget restore $path
    & $nuget restore $path

    $location = $(Get-Location)
    Set-Location $directory

    msbuild $filename /m /t:Rebuild ("/p:$($properties)")
    checkExitCode "Failed to build: $($path)"

    Set-Location $location
}