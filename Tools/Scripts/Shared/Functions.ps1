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
function CheckExitCode($msg)
{
  if ($LastExitCode -ne 0)
  {
    Write-Error $msg
    Exit 1
  }
}

function CheckFolder($folder)
{
  if (Test-Path $folder)
  {
    return;
  }

  Write-Error "Unable to find folder: $($folder)"
  Exit 1
}

function CreateChild($xml, $xpath, $name)
{
  [System.Xml.XmlNamespaceManager] $nsmgr = $xml.NameTable;
  $nsmgr.AddNamespace("msb", "http://schemas.microsoft.com/developer/msbuild/2003");

  $parent = $xml.SelectSingleNode($xpath, $nsmgr)
  $element = $xml.CreateElement($name, "http://schemas.microsoft.com/developer/msbuild/2003")
  $parent.AppendChild($element)

  return $element
}

function ExecuteFile($path)
{
  Invoke-Expression $path
  CheckExitCode "Failed to execute: $($path)"
}

function FullPath($path)
{
  $location = $(Get-Location)
  return "$($location)\$($path)"
}

function SelectNodes($xml, $xpath)
{
  [System.Xml.XmlNamespaceManager] $nsmgr = $xml.NameTable;
  $nsmgr.AddNamespace("msb", "http://schemas.microsoft.com/developer/msbuild/2003");

  return $xml.SelectNodes($xpath, $nsmgr)
}

function SetFolder($scriptPath)
{
  Set-Location "${scriptPath}\..\.."
}
