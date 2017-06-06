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
. $scriptPath\Shared\Build.ps1
. $scriptPath\Shared\Functions.ps1
SetFolder $scriptPath

. Tools\Scripts\Shared\FileGenerator.ps1

function BuildMagickNET()
{
  BuildSolution "Magick.NET.sln" "Configuration=ReleaseQ8,RunCodeAnalysis=false,TargetFramework=net40,Platform=x86"
  BuildSolution "Magick.NET.sln" "Configuration=ReleaseQ16,RunCodeAnalysis=false,TargetFramework=net40,Platform=x86"
  BuildSolution "Magick.NET.sln" "Configuration=ReleaseQ16-HDRI,RunCodeAnalysis=false,TargetFramework=net40,Platform=x86"
}

BuildMagickNET
GenerateFiles("MagickScript")
BuildMagickNET
