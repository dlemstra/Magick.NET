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
    [string]$pfxUri = ''
)

[System.Net.ServicePointManager]::SecurityProtocol = [System.Net.SecurityProtocolType]'Ssl3,Tls,Tls11,Tls12'

Write-Host "Installing NuGet"
$sourceNugetExe = "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"
$targetNugetExe = "$PSScriptRoot\..\..\tools\windows\nuget.exe"
Invoke-WebRequest $sourceNugetExe -OutFile $targetNugetExe

Write-Host "Installing Ghostscript 9.26"
$sourceGhostscriptExe = "https://github.com/ArtifexSoftware/ghostpdl-downloads/releases/download/gs926/gs926aw32.exe"
$targetGhostscriptExe = "$PSScriptRoot\..\..\tools\windows\gs926aw32.exe"
Invoke-WebRequest $sourceGhostscriptExe -OutFile $targetGhostscriptExe
& $targetGhostscriptExe /S

if ($pfxUri.Length -gt 0) {
    Write-Host "Downloading code signing certificate"
    Invoke-WebRequest $pfxUri -OutFile "$PSScriptRoot\ImageMagick.pfx"
}
