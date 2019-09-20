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
    [string]$quantumName = $env:QuantumName,
    [string]$platformName = $env:PlatformName,
    [string]$pfxPassword = ''
)

. $PSScriptRoot\..\..\tools\windows\utils.ps1

function signLibrary($library, $quantumName, $platformName, $pfxPassword, $targetFramework) {
    $pfxFile = fullPath "build\windows\ImageMagick.pfx"
    $signtool = "C:\Program Files (x86)\Windows Kits\10\bin\10.0.17763.0\x64\signtool.exe"

    $platform = $platformName

    if ($platform -eq "Any CPU") {
      $platform = "AnyCPU"
    }

    $fileName = fullPath "src\$library\bin\Release$quantumName\$platform\$targetFramework\$library-$quantumName-$platform.dll"

    for ($i=0; $i -le 10; $i++)
    {
      Start-Sleep -s $i
      & $signtool sign /f "$pfxFile" /p "$pfxPassword" /tr http://sha256timestamp.ws.symantec.com/sha256/timestamp /td sha256 /fd sha256 $fileName
      if ($LastExitCode -eq 0)
      {
        break
      }
    }
    if ($LastExitCode -ne 0)
    {
      throw "Failed to sign files."
    }
}

function signLibraries($quantumName, $platformName, $pfxPassword) {
    if ($pfxPassword.Length -eq 0) {
        return
    }

    signLibrary "Magick.NET" $quantumName $platformName $pfxPassword "net20"
    signLibrary "Magick.NET" $quantumName $platformName $pfxPassword "net40"
    signLibrary "Magick.NET" $quantumName $platformName $pfxPassword "netstandard13"
    signLibrary "Magick.NET" $quantumName $platformName $pfxPassword "netstandard20"

    if (!$quantumName.EndsWith("-OpenMP")) {
        signLibrary "Magick.NET.Web" $quantumName $platformName $pfxPassword "net40"
    }
}

signLibraries $quantumName $platformName $pfxPassword
