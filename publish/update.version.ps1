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
    [parameter(mandatory=$true)][string]$version
)

. $PSScriptRoot\..\tools\windows\utils.ps1

function setValue($content, $startMatch, $endMatch, $value) {
  $start = $content.IndexOf($startMatch)
  if ($start -eq -1) {
    Write-Error "Unable to find startMatch"
    Exit 1
  }

  $start += $startMatch.Length

  $newContent = $content.Substring(0, $start)
  $newContent += $value

  $start = $content.IndexOf($endMatch, $start)
  if ($start -eq -1) {
    Write-Error "Unable to find endMatch"
    Exit 1
  }

  $newContent += $content.Substring($start)
  return $newContent
}

function updateAssemblyVersion($library, $version, $checkAssemblyVersion) {
    $fileName = fullPath "src\$library\$library.csproj"
    $content = [IO.File]::ReadAllText($fileName, [System.Text.Encoding]::Default)

    $assemblyVersion = $version.Substring(0, $version.LastIndexOf(".") + 1) + "0"

    $content = setValue $content "`<AssemblyVersion>" "`<" $assemblyVersion
    $content = setValue $content "`<Version`>" "`<" $version
    $content = setValue $content "`<FileVersion`>" "`<" $version
    $content = setValue $content "`<Copyright`>" "`<" "Copyright 2013-$((Get-Date).year) Dirk Lemstra"
    [IO.File]::WriteAllText($fileName, $content, [System.Text.Encoding]::Default)
}

updateAssemblyVersion "Magick.NET" $version
updateAssemblyVersion "Magick.NET.Web" $version
