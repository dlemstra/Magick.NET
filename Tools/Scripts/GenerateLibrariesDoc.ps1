#==================================================================================================
# Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
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
$scriptPath = Split-Path -parent $MyInvocation.MyCommand.Path
. $scriptPath\Shared\Functions.ps1
SetFolder $scriptPath

function GetVersion($file, $start, $padding)
{
  $lines = [System.IO.File]::ReadAllLines($file)
  foreach ($line in $lines)
  {
    if (!$line.StartsWith($start))
    {
      continue
    }

    return $line.SubString($start.Length, $line.Length - $start.Length - $padding).Replace(",", ".")
  }

  Write-Error "Unable to get version from: $file"

  return $null
}

function WriteDocStart($fileName)
{
  Add-Content $fileName "# Libraries"
  Add-Content $fileName "Magick.NET is build with the following libraries:"
  Add-Content $fileName ""
}

function WriteVersionFromResource($library, $resourceFile)
{
  $version = GetVersion $resourceFile "#define THIS_PROJECT_VERSION_STRING		""" 1
  if ($version -ne $null)
  {
    Add-Content $fileName "- $library $version"
    Return
  }
}

function WriteVersionForCroco()
{
  $version = GetVersion "ImageMagick\croco\src\libcroco-config.h" "#define LIBCROCO_VERSION """ 1
  Add-Content $fileName "- croco $version"
}

function WriteVersionForFfi()
{
  $version = GetVersion "ImageMagick\ffi\configure.ac" "AC_INIT([libffi], [" 45
  Add-Content $fileName "- ffi $version"
}

function WriteVersionForImageMagick()
{
  $version = GetVersion "ImageMagick\ImageMagick\MagickCore\version.h" "#define MagickLibVersionNumber  " 0
  Add-Content $fileName "- ImageMagick $version"
}

function WriteVersionForPixman()
{
  $version = GetVersion "ImageMagick\pixman\pixman\pixman-version.h" "#define PIXMAN_VERSION_STRING """ 1
  Add-Content $fileName "- pixman $version"
}

function WriteLibraryVersions($folders)
{
  $sourceDir = "ImageMagick\Source\ImageMagick"
  $libraries = Get-ChildItem $sourceDir

  foreach ($library in $libraries)
  {
    $resourceFile = Get-ChildItem -Path "$($sourceDir)\$($library)" -Filter "Resource.rc" -Recurse | Where-Object { $_.Directory.Name -eq "ImageMagick" }
    if ($resourceFile -ne $null)
    {
      WriteVersionFromResource $library $resourceFile.FullName
    }
    else
    {
      switch($library)
      {
        "croco" { WriteVersionForCroco }
        "ffi" { WriteVersionForFfi }
        "ImageMagick" { WriteVersionForImageMagick }
        "pixman" { WriteVersionForPixman }
        "VisualMagick" { }
        default { Write-Error "Unable to get version for: $library" }
      }
    }
  }
}

$fileName = "ImageMagick\Source\Libraries.md"
[void](New-Item -force $fileName)

WriteDocStart $fileName
WriteLibraryVersions