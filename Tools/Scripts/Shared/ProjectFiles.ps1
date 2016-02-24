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
function AddProjectFile($xml, $xpath, $name, $attribute, $file)
{
  $element = CreateChild $xml "/msb:Project$xpath" $name
  $element.SetAttribute($attribute, $file)
}

function CreateAnyCPUProjectFiles()
{
  $path = FullPath "Magick.NET\Magick.NET.csproj"
  $xml = [xml](get-content $path)
  SelectNodes $xml "//msb:DefineConstants"  | Foreach {$_.InnerText = "ANYCPU;" + $_.InnerText}
  PatchAnyCPUProjectFile $xml "AnyCPU"

  $csproj = FullPath "Magick.NET\Magick.NET.AnyCPU.csproj"
  Write-Host "Creating file: $csproj"
  $xml.Save($csproj)

  $path = FullPath "Magick.NET.Tests\Magick.NET.Tests.csproj"
  $xml = [xml](get-content $path)
  SelectNodes $xml "//msb:DefineConstants"  | Foreach {$_.InnerText = "ANYCPU;" + $_.InnerText}
  PatchAnyCPUTestProjectFile $xml "AnyCPU" "Magick.NET.AnyCPU.csproj"

  $csproj = FullPath "Magick.NET.Tests\Magick.NET.Tests.AnyCPU.csproj"
  Write-Host "Creating file: $csproj"
  $xml.Save($csproj)

  $path = FullPath "Magick.NET.Web\Magick.NET.Web.csproj"
  $xml = [xml](get-content $path)
  SelectNodes $xml "//msb:DefineConstants"  | Foreach {$_.InnerText = "ANYCPU;" + $_.InnerText}
  SelectNodes $xml "//msb:PropertyGroup[contains(@Condition, '|x64')]" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
  SelectNodes $xml "//msb:PropertyGroup[contains(@Condition, '|x86')]" | Foreach {$_.SetAttribute("Condition", $_.GetAttribute("Condition").Replace("x86", "AnyCPU"))}
  SelectNodes $xml "//msb:OutputPath" | Foreach {$_.InnerText = $_.InnerText.Replace("x86", "AnyCPU")}
  SelectNodes $xml "//msb:DocumentationFile" | Foreach {$_.InnerText = $_.InnerText.Replace("x86", "AnyCPU")}
  SelectNodes $xml "//msb:AssemblyName" | Foreach {$_.InnerText = $_.InnerText.Replace("x86", "AnyCPU")}
  SelectNodes $xml "//msb:PlatformTarget" | Foreach {$_.InnerText = "AnyCPU"}
  SelectNodes $xml "//msb:ProjectReference[@Include = '..\Magick.NET\Magick.NET.csproj']" | Foreach {$_.SetAttribute("Include", "..\Magick.NET\Magick.NET.AnyCPU.csproj")}

  $csproj = FullPath "Magick.NET.Web\Magick.NET.Web.AnyCPU.csproj"
  Write-Host "Creating file: $csproj"
  $xml.Save($csproj)
}

function CreateNet20ProjectFiles()
{
  $path = FullPath "Magick.NET\Magick.NET.csproj"
  $xml = [xml](get-content $path)
  SelectNodes $xml "//msb:DefineConstants"  | Foreach {$_.InnerText = "NET20;" + $_.InnerText}
  SelectNodes $xml "//msb:DocumentationFile" | Foreach {$_.InnerText = $_.InnerText.Replace("x86\", "x86.net20\")}
  SelectNodes $xml "//msb:DocumentationFile" | Foreach {$_.InnerText = $_.InnerText.Replace("x64\", "x64.net20\")}
  SelectNodes $xml "//msb:OutputPath" | Foreach {$_.InnerText = $_.InnerText.Replace("x86", "x86.net20")}
  SelectNodes $xml "//msb:OutputPath" | Foreach {$_.InnerText = $_.InnerText.Replace("x64", "x64.net20")}
  PatchNet20ProjectFile $xml

  $csproj = FullPath "Magick.NET\Magick.NET.net20.csproj"
  Write-Host "Creating file: $csproj"
  $xml.Save($csproj)

  $path = FullPath "Magick.NET.Tests\Magick.NET.Tests.csproj"
  $xml = [xml](get-content $path)
  SelectNodes $xml "//msb:DefineConstants"  | Foreach {$_.InnerText = "NET20;" + $_.InnerText}
  SelectNodes $xml "//msb:OutputPath" | Foreach {$_.InnerText = $_.InnerText.Replace("x86", "x86.net20")}
  SelectNodes $xml "//msb:OutputPath" | Foreach {$_.InnerText = $_.InnerText.Replace("x64", "x64.net20")}
  SelectNodes $xml "//msb:BaseIntermediateOutputPath" | Foreach {$_.InnerText = $_.InnerText.Replace("net40-client", "net20")}
  PatchNet20TestProjectFile $xml

  $csproj = FullPath "Magick.NET.Tests\Magick.NET.Tests.net20.csproj"
  Write-Host "Creating file: $csproj"
  $xml.Save($csproj)

  $path = FullPath "Magick.NET\Magick.NET.csproj"
  $xml = [xml](get-content $path)
  SelectNodes $xml "//msb:DefineConstants"  | Foreach {$_.InnerText = "NET20;ANYCPU;" + $_.InnerText}
  PatchAnyCPUProjectFile $xml "AnyCPU.net20"
  PatchNet20ProjectFile $xml

  $csproj = FullPath "Magick.NET\Magick.NET.AnyCPU.net20.csproj"
  Write-Host "Creating file: $csproj"
  $xml.Save($csproj)

  $path = FullPath "Magick.NET.Tests\Magick.NET.Tests.csproj"
  $xml = [xml](get-content $path)
  SelectNodes $xml "//msb:DefineConstants"  | Foreach {$_.InnerText = "NET20;ANYCPU;" + $_.InnerText}
  PatchAnyCPUTestProjectFile $xml "AnyCPU.net20" "Magick.NET.AnyCPU.net20.csproj"
  PatchNet20TestProjectFile $xml

  $csproj = FullPath "Magick.NET.Tests\Magick.NET.Tests.AnyCPU.net20.csproj"
  Write-Host "Creating file: $csproj"
  $xml.Save($csproj)
}

function PatchAnyCPUProjectFile($xml, $binDir)
{
  SelectNodes $xml "//msb:PropertyGroup[contains(@Condition, '|x64')]" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
  SelectNodes $xml "//msb:PropertyGroup[contains(@Condition, '|x86')]" | Foreach {$_.SetAttribute("Condition", $_.GetAttribute("Condition").Replace("x86", $binDir))}
  SelectNodes $xml "//msb:OutputPath" | Foreach {$_.InnerText = $_.InnerText.Replace("x86", $binDir)}
  SelectNodes $xml "//msb:DocumentationFile" | Foreach {$_.InnerText = $_.InnerText.Replace("x86\", "$binDir\") }
  SelectNodes $xml "//msb:DocumentationFile" | Foreach {$_.InnerText = $_.InnerText.Replace("-x86", "-AnyCPU") }
  SelectNodes $xml "//msb:AssemblyName" | Foreach {$_.InnerText = $_.InnerText.Replace("x86", "AnyCPU")}
  SelectNodes $xml "//msb:PlatformTarget" | Foreach {$_.InnerText = "AnyCPU"}
  SelectNodes $xml "//msb:Compile[contains(@Include, 'NativeLibraryLoader.cs')]" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
  AddProjectFile $xml "/msb:ItemGroup" "Compile" "Include" "..\Magick.NET.AnyCPU\MagickAnyCPU.cs"
  AddProjectFile $xml "/msb:ItemGroup" "Compile" "Include" "..\Magick.NET.AnyCPU\NativeLibraryLoader.cs"
  AddProjectFile $xml "" "Import" "Project" "..\Magick.NET.AnyCPU\Magick.NET.$binDir.targets"
}

function PatchAnyCPUTestProjectFile($xml, $binDir, $projectFile)
{
  SelectNodes $xml "//msb:OutputPath" | Foreach {$_.InnerText = $_.InnerText.Replace("x86", $binDir)}
  SelectNodes $xml "//msb:ProjectReference[@Include = '..\Magick.NET\Magick.NET.csproj']" | Foreach {$_.SetAttribute("Include", "..\Magick.NET\$projectFile")}
}

function PatchNet20ProjectFile($xml)
{
  SelectNodes $xml "//msb:BaseIntermediateOutputPath" | Foreach {$_.InnerText = $_.InnerText.Replace("net40-client", "net20")}
  SelectNodes $xml "//msb:Reference[@Include='PresentationCore' or @Include='WindowsBase' or @Include='System.Xml.Linq']" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
  SelectNodes $xml "//msb:TargetFrameworkProfile" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
  SelectNodes $xml "//msb:TargetFrameworkVersion" | Foreach {$_.InnerText = "v2.0"}
}

function PatchNet20TestProjectFile($xml)
{
  SelectNodes $xml "//msb:Reference[@Include='PresentationCore' or @Include='WindowsBase']" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
  SelectNodes $xml "//msb:ProjectReference[@Include = '..\Magick.NET\Magick.NET.csproj']" | Foreach {$_.SetAttribute("Include", "..\Magick.NET\Magick.NET.net20.csproj")}
  SelectNodes $xml "//msb:TargetFrameworkVersion" | Foreach {$_.InnerText = "v3.5"}
}