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
  Set-Location "Publish\Magick.NET.Core\src\$directory"

  dotnet restore

  dotnet build --configuration Release
  CheckExitCode "Failed to build $directory"

  dotnet pack --configuration Release

  TestCoreLibrary $directory

  Set-Location $location
}

function BuildCoreNative($directory)
{
  CopyNativeLibrary $directory x86 Win32
  CopyNativeLibrary $directory x64 x64

  $location = $(Get-Location)
  Set-Location $directory

  dotnet restore

  dotnet pack --configuration Release
  CheckExitCode "Failed to pack $directory"

  Set-Location $location
}

function BuildCore($name)
{
  $directory = $name.Split('\', [System.StringSplitOptions]::RemoveEmptyEntries) | Select-Object -Last 1

  if (Test-Path "Publish\Magick.NET.Core\test\$directory.Tests")
  {
    BuildCoreLibrary $directory
  }
  else
  {
    BuildCoreNative "Publish\Magick.NET.Core\src\$directory"
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

  Copy-Item "Source\Magick.NET.Native\bin\Release$quantum\$binDir\*.Native.dll" "$directory\runtimes\win7-$platform\native"
}

function TestCoreLibrary($directory)
{
  Set-Location "..\..\test\$directory.Tests"

  dotnet restore

  dotnet build

  # restore does not copy this file
  Copy-Item "..\..\src\$directory.Native\runtimes\win7-x64\native\*.Native.dll" "bin\Debug\netcoreapp1.3"

  dotnet test
  CheckExitCode "Failed to test $directory"
}