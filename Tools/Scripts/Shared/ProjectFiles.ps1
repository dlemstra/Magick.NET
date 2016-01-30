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
function CreateAnyCPUProjectFiles()
{
  $path = FullPath "Magick.NET\Magick.NET.csproj"
  $xml = [xml](get-content $path)
  SelectNodes $xml "//msb:DefineConstants"  | Foreach {$_.InnerText = "ANYCPU;" + $_.InnerText}
  SelectNodes $xml "//msb:PropertyGroup[contains(@Condition, '|x64')]" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
  SelectNodes $xml "//msb:PropertyGroup[contains(@Condition, '|x86')]" | Foreach {$_.SetAttribute("Condition", $_.GetAttribute("Condition").Replace("x86", "AnyCPU"))}
  SelectNodes $xml "//msb:OutputPath" | Foreach {$_.InnerText = $_.InnerText.Replace("x86", "AnyCPU")}
  SelectNodes $xml "//msb:DocumentationFile" | Foreach {$_.InnerText = $_.InnerText.Replace("x86", "AnyCPU") }
  SelectNodes $xml "//msb:AssemblyName" | Foreach {$_.InnerText = $_.InnerText.Replace("x86", "AnyCPU")}
  SelectNodes $xml "//msb:PlatformTarget" | Foreach {$_.InnerText = "AnyCPU"}
  $element = CreateChild $xml "/msb:Project" "Import"
  $element.SetAttribute("Project", "Magick.NET.AnyCPU.targets")

  $csproj = FullPath "Magick.NET\Magick.NET.AnyCPU.csproj"
  Write-Host "Creating file: $csproj"
  $xml.Save($csproj)

  $path = FullPath "Magick.NET.Tests\Magick.NET.Tests.csproj"
  $xml = [xml](get-content $path)
  SelectNodes $xml "//msb:DefineConstants"  | Foreach {$_.InnerText = "ANYCPU;" + $_.InnerText}
  SelectNodes $xml "//msb:OutputPath" | Foreach {$_.InnerText = $_.InnerText.Replace("x86", "AnyCPU")}
  SelectNodes $xml "//msb:ProjectReference[@Include = '..\Magick.NET\Magick.NET.csproj']" | Foreach {$_.SetAttribute("Include", "..\Magick.NET\Magick.NET.AnyCPU.csproj")}
  $element = CreateChild $xml "/msb:Project" "Import"
  $element.SetAttribute("Project", "Magick.NET.Tests.AnyCPU.targets")

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
  $path = FullPath "Magick.NET.Native\Magick.NET.Native.vcxproj"
  $xml = [xml](get-content $path)
  SelectNodes $xml "//msb:AdditionalLibraryDirectories" | Foreach {$_.InnerText = $_.InnerText.Replace("v4.0", "v2.0")}
  SelectNodes $xml "//msb:ClCompile[@Include]" | Foreach {$_.SetAttribute("Include", "..\Magick.NET.Native\" + $_.GetAttribute("Include"))}
  SelectNodes $xml "//msb:ClInclude[@Include]" | Foreach {$_.SetAttribute("Include", "..\Magick.NET.Native\" + $_.GetAttribute("Include"))}
  SelectNodes $xml "//msb:EmbedManagedResourceFile" | Foreach {$_.InnerText = $_.InnerText.Replace("Resources\", "..\Magick.NET.Native\Resources\")}
  SelectNodes $xml "//msb:None[@Include]" | Foreach {$_.SetAttribute("Include", "..\Magick.NET.Native\" + $_.GetAttribute("Include"))}
  SelectNodes $xml "//msb:PlatformToolset" | Foreach {$_.InnerText = "v90"}
  SelectNodes $xml "//msb:PreprocessorDefinitions" | Foreach {$_.InnerText = "NET20;" + $_.InnerText}
  SelectNodes $xml "//msb:ResourceCompile[@Include]" | Foreach {$_.SetAttribute("Include", "..\Magick.NET.Native\" + $_.GetAttribute("Include"))}

  $vcxproj = FullPath "Magick.NET.Native.net20\Magick.NET.Native.net20.vcxproj"
  Write-Host "Creating file: $vcxproj"
  $xml.Save($vcxproj)

  $path = FullPath "Magick.NET\Magick.NET.csproj"
  $xml = [xml](get-content $path)
  SelectNodes $xml "//msb:DefineConstants"  | Foreach {$_.InnerText = "NET20;" + $_.InnerText}
  SelectNodes $xml "//msb:DocumentationFile" | Foreach {$_.InnerText = $_.InnerText.Replace("x86\", "x86.net20\")}
  SelectNodes $xml "//msb:DocumentationFile" | Foreach {$_.InnerText = $_.InnerText.Replace("x64\", "x64.net20\")}
  SelectNodes $xml "//msb:OutputPath" | Foreach {$_.InnerText = $_.InnerText.Replace("x86", "x86.net20")}
  SelectNodes $xml "//msb:OutputPath" | Foreach {$_.InnerText = $_.InnerText.Replace("x64", "x64.net20")}
  SelectNodes $xml "//msb:Reference[@Include='PresentationCore' or @Include='WindowsBase' or @Include='System.Xml.Linq']" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
  SelectNodes $xml "//msb:TargetFrameworkProfile" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
  SelectNodes $xml "//msb:TargetFrameworkVersion" | Foreach {$_.InnerText = "v2.0"}

  $csproj = FullPath "Magick.NET\Magick.NET.net20.csproj"
  Write-Host "Creating file: $csproj"
  $xml.Save($csproj)

  $path = FullPath "Magick.NET.Tests\Magick.NET.Tests.csproj"
  $xml = [xml](get-content $path)
  SelectNodes $xml "//msb:DefineConstants"  | Foreach {$_.InnerText = "NET20;" + $_.InnerText}
  SelectNodes $xml "//msb:OutputPath" | Foreach {$_.InnerText = $_.InnerText.Replace("x86", "x86.net20")}
  SelectNodes $xml "//msb:OutputPath" | Foreach {$_.InnerText = $_.InnerText.Replace("x64", "x64.net20")}
  SelectNodes $xml "//msb:ProjectReference[@Include = '..\Magick.NET\Magick.NET.csproj']" | Foreach {$_.SetAttribute("Include", "..\Magick.NET\Magick.NET.net20.csproj")}
  SelectNodes $xml "//msb:Reference[@Include='PresentationCore' or @Include='WindowsBase']" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
  SelectNodes $xml "//msb:TargetFrameworkVersion" | Foreach {$_.InnerText = "v3.5"}

  $csproj = FullPath "Magick.NET.Tests\Magick.NET.Tests.net20.csproj"
  Write-Host "Creating file: $csproj"
  $xml.Save($csproj)
}
