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
. $scriptPath\Shared\Functions.ps1
SetFolder $scriptPath

. Tools\Scripts\Shared\Build.ps1

function BuildCrossPlatform()
{
    $configurations = @(
        @{OperatingSystem = "linux"; Quantum = "Q8"}
        @{OperatingSystem = "linux"; Quantum = "Q16"}
        @{OperatingSystem = "linux"; Quantum = "Q16-HDRI"}
    )

    foreach ($configuration in $configurations)
    {
        BuildSolution "Magick.NET.CrossPlatform.sln" "Configuration=Release$($configuration.Quantum),Platform=x64"

        $dir = "ImageMagick\$($configuration.Quantum)\lib\Release\CrossPlatform\$($configuration.OperatingSystem)"
        CreateFolder $dir
        Copy-Item "Source\Magick.NET.CrossPlatform\bin\$($configuration.OperatingSystem)-x64\Release$($configuration.Quantum)\*.Native.dll.so" $dir
    }
}

BuildCrossPlatform
