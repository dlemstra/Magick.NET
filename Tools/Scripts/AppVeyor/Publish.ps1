# Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

$scriptPath = Split-Path -parent $MyInvocation.MyCommand.Path
$scriptPath = "$scriptPath\.."
. $scriptPath\Shared\Functions.ps1
SetFolder $scriptPath

. Tools\Scripts\Shared\Build.ps1
. Tools\Scripts\Shared\Config.ps1
. Tools\Scripts\Shared\Publish.ps1

function Publish($builds, $version)
{
    foreach($build in $builds)
    {
        BuildRelease $build
    }

    CheckStrongNames $builds

    foreach($build in $builds)
    {
        $id = "Magick.NET-dev-$($build.Quantum)-$($build.Platform)"
        CreateNuGetPackages $id $version $build

        $fileName = FullPath "Publish\NuGet\$id.$version.nupkg"
        appveyor PushArtifact $fileName

        $webId = $id.Replace("Magick.NET", "Magick.NET.Web")
        $fileName = FullPath "Publish\NuGet\$webId.$version.nupkg"
        appveyor PushArtifact $fileName
    }
}

$quantum = $args[0]
$platform = $args[1]

$version = GetDevVersion
$builds = GetBuilds $quantum $platform
Publish $builds $version