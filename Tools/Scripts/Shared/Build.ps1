#==================================================================================================
# Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
#
# Licensed under the ImageMagick License (the "License"); you may not use this file except in 
# compliance with the License. You may obtain a copy of the License at
#
#   http://www.imagemagick.org/script/license.php
#
# Unless required by applicable law or agreed to in writing, software distributed under the
# License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
# express or implied. See the License for the specific language governing permissions and
# limitations under the License.
#==================================================================================================
function Build($build)
{
  BuildSolution "$($build.Name).sln" "Configuration=Tests$($build.Quantum),RunCodeAnalysis=false,Platform=$($build.Platform)"
}

function BuildCoreLibrary($directory)
{
  $location = $(Get-Location)
  Set-Location "Magick.NET.Core\src\$directory"

  dotnet restore

  dotnet build
  CheckExitCode "Failed to build $directory"

  dotnet pack

  TestCoreLibrary $directory x64
  TestCoreLibrary $directory x86

  Set-Location $location
}

function BuildCoreNative($directory)
{
  CopyNativeLibrary $directory x86 Win32
  CopyNativeLibrary $directory x64 x64

  $location = $(Get-Location)
  Set-Location $directory

  dotnet restore

  dotnet pack
  CheckExitCode "Failed to pack $directory"

  Set-Location $location
}

function BuildCore($name)
{
  $directory = $name.Split('\', [System.StringSplitOptions]::RemoveEmptyEntries) | Select-Object -Last 1

  if (Test-Path "Magick.NET.Core\test\$directory.Tests")
  {
    BuildCoreLibrary $directory
  }
  else
  {
    BuildCoreNative "Magick.NET.Core\src\$directory"
  }
}

function CopyNativeLibrary($directory, $platform, $binDir)
{
  $quantum = ($directory.Replace(".Native", "").Split('-') | Select-Object -Skip 1) -join '-'

  $target = "$directory\runtimes\win7-$platform\native"
  if (!(Test-Path $target))
  {
    [Void](New-Item $target -itemtype directory)
  }

  Copy-Item "Magick.NET.Native\bin\Release$quantum\$binDir\*.Native.dll" "$directory\runtimes\win7-$platform\native"
}

function TestCoreLibrary($directory, $platform)
{
  Set-Location "..\..\test\$directory.Tests"

  dnvm use 1.0.0-rc1-update1 -r coreclr -a $platform
  dnu restore
  dnu build

  dnx test
  CheckExitCode "Failed to test $directory-$platform"
}