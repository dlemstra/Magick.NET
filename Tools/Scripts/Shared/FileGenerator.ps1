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
function BuildFileGenerator()
{
  BuildSolution "Tools\Magick.NET.FileGenerator.sln" "Configuration=Release"
}

function GenerateFiles()
{
  BuildFileGenerator
  ExecuteFile "Tools\Magick.NET.FileGenerator\bin\Release\Magick.NET.FileGenerator.exe"
}
