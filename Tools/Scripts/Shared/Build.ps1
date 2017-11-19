# Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

function BuildRelease($build,$codecov)
{
    Build $build $codecov "Release"
}

function BuildTest($build,$codecov)
{
    Build $build $codecov "Test"
}

function Build($build,$codecov,$configuration)
{
    $platform=$($build.Platform).Replace("AnyCPU", "Any CPU")
    $properties="Configuration=$($configuration)$($build.Quantum),RunCodeAnalysis=false,Platform=$platform"
    if ($codecov -eq $true)
    {
        $properties+=",CodeCov=true"
    }
    BuildSolution "$($build.Name).sln" $properties
}

function BuildSolution($solution, $properties)
{
    $path = FullPath $solution
    $directory = Split-Path -parent $path
    $filename = Split-Path -leaf $path

    .\Tools\Programs\nuget.exe restore $solution

    $location = $(Get-Location)
    Set-Location $directory

    msbuild $filename /t:Restore ("/p:$($properties)")
    CheckExitCode "Failed to restore: $($path)"

    msbuild $filename /t:Rebuild ("/p:$($properties)")
    CheckExitCode "Failed to build: $($path)"

    Set-Location $location
}

function CopyNativeLibrary($directory, $platform, $binDir)
{
    $quantum = ($directory.Replace(".Native", "").Split('-') | Select-Object -Skip 1) -join '-'

    $target = "$directory\runtimes\win7-$platform\native"
    CreateFolder $target

    Copy-Item "Source\Magick.NET.Native\bin\Release$quantum\$binDir\*.Native.dll" "$directory\runtimes\win7-$platform\native"
}