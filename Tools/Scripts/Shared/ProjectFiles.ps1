#==================================================================================================
function CreateAnyCPUProjectFiles()
{
	$path = FullPath "Magick.NET.Wrapper\Magick.NET.Wrapper.vcxproj"
	$xml = [xml](get-content $path)
	SelectNodes $xml "//msb:ClCompile[@Include='GlobalSuppressions.cpp']" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
	SelectNodes $xml "//msb:ClCompile[@Include]" | Foreach {$_.SetAttribute("Include", "..\Magick.NET.Wrapper\" + $_.GetAttribute("Include"))}
	SelectNodes $xml "//msb:ClInclude[@Include]" | Foreach {$_.SetAttribute("Include", "..\Magick.NET.Wrapper\" + $_.GetAttribute("Include"))}
	SelectNodes $xml "//msb:CodeAnalysisRuleSet" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
	SelectNodes $xml "//msb:EmbedManagedResourceFile" | Foreach {$_.InnerText = $_.InnerText.Replace("Resources\", "..\Magick.NET.Wrapper\Resources\")}
	SelectNodes $xml "//msb:ForcedIncludeFiles" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
	SelectNodes $xml "//msb:LinkKeyFile" | Foreach {$_.InnerText = $_.InnerText.Replace("Magick.NET.snk", "\..\Magick.NET.Wrapper\Magick.NET.snk")}
	SelectNodes $xml "//msb:None[@Include]" | Foreach {$_.SetAttribute("Include", "..\Magick.NET.Wrapper\" + $_.GetAttribute("Include"))}
	SelectNodes $xml "//msb:ResourceCompile[@Include]" | Foreach {$_.SetAttribute("Include", "..\Magick.NET.Wrapper\" + $_.GetAttribute("Include"))}
	SelectNodes $xml "//msb:RunCodeAnalysis" | Foreach {[void]$_.ParentNode.RemoveChild($_)}

	$vcxproj = FullPath "Magick.NET.AnyCPU\Magick.NET.Wrapper.AnyCPU.vcxproj"
	Write-Host "Creating file: $vcxproj"
	$xml.Save($vcxproj)

	$path = FullPath "Magick.NET\Magick.NET.csproj"
	$xml = [xml](get-content $path)
	SelectNodes $xml "//msb:DefineConstants"  | Foreach {$_.InnerText = "ANYCPU;" + $_.InnerText}
	SelectNodes $xml "//msb:PropertyGroup[contains(@Condition, '|x64')]" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
	SelectNodes $xml "//msb:PropertyGroup[contains(@Condition, '|x86')]" | Foreach {$_.SetAttribute("Condition", $_.GetAttribute("Condition").Replace("x86", "AnyCPU"))}
	SelectNodes $xml "//msb:OutputPath" | Foreach {$_.InnerText = $_.InnerText.Replace("x86", "AnyCPU")}
	SelectNodes $xml "//msb:DocumentationFile" | Foreach {$_.InnerText = $_.InnerText.Replace("x86", "AnyCPU") }
	SelectNodes $xml "//msb:AssemblyName" | Foreach {$_.InnerText = $_.InnerText.Replace("x86", "AnyCPU")}
	SelectNodes $xml "//msb:PlatformTarget" | Foreach {$_.InnerText = "AnyCPU"}
	SelectNodes $xml "//msb:ProjectReference[@Include = '..\Magick.NET.Wrapper\Magick.NET.Wrapper.vcxproj']" | Foreach {$_.SetAttribute("Include", "..\Magick.NET.AnyCPU\Magick.NET.Wrapper.AnyCPU.vcxproj")}
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
	SelectNodes $xml "//msb:ProjectReference[@Include = '..\Magick.NET\Magick.NET.csproj']" | Foreach {$_.SetAttribute("Include", "..\Magick.NET\Magick.NET.AnyCPU.csproj")}
	
	$csproj = FullPath "Magick.NET.Web\Magick.NET.Web.AnyCPU.csproj"
	Write-Host "Creating file: $csproj"
	$xml.Save($csproj)
}
#==================================================================================================
function CreateNet20ProjectFiles()
{
	$path = FullPath "Magick.NET.Core\Magick.NET.Core.csproj"
	$xml = [xml](get-content $path)
	SelectNodes $xml "//msb:DefineConstants"  | Foreach {$_.InnerText = "NET20;" + $_.InnerText}
	SelectNodes $xml "//msb:OutputPath" | Foreach {$_.InnerText = $_.InnerText.Replace("AnyCPU", "AnyCPU.net20")}
	SelectNodes $xml "//msb:DocumentationFile" | Foreach {$_.InnerText = $_.InnerText.Replace("AnyCPU\", "AnyCPU.net20\")}
	SelectNodes $xml "//msb:TargetFrameworkProfile" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
	SelectNodes $xml "//msb:TargetFrameworkVersion" | Foreach {$_.InnerText = "v2.0"}
	
	$csproj = FullPath "Magick.NET.Core\Magick.NET.Core.net20.csproj"
	Write-Host "Creating file: $csproj"
	$xml.Save($csproj)

	$path = FullPath "Magick.NET.Wrapper\Magick.NET.Wrapper.vcxproj"
	$xml = [xml](get-content $path)
	SelectNodes $xml "//msb:ClCompile[@Include='GlobalSuppressions.cpp']" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
	SelectNodes $xml "//msb:ClCompile[@Include]" | Foreach {$_.SetAttribute("Include", "..\Magick.NET.Wrapper\" + $_.GetAttribute("Include"))}
	SelectNodes $xml "//msb:ClInclude[@Include]" | Foreach {$_.SetAttribute("Include", "..\Magick.NET.Wrapper\" + $_.GetAttribute("Include"))}
	SelectNodes $xml "//msb:CodeAnalysisRuleSet" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
	SelectNodes $xml "//msb:EmbedManagedResourceFile" | Foreach {$_.InnerText = $_.InnerText.Replace("Resources\", "..\Magick.NET.Wrapper\Resources\")}
	SelectNodes $xml "//msb:ForcedIncludeFiles" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
	SelectNodes $xml "//msb:LinkKeyFile" | Foreach {$_.InnerText = $_.InnerText.Replace("Magick.NET.snk", "\..\Magick.NET.Wrapper\Magick.NET.snk")}
	SelectNodes $xml "//msb:None[@Include]" | Foreach {$_.SetAttribute("Include", "..\Magick.NET.Wrapper\" + $_.GetAttribute("Include"))}
	SelectNodes $xml "//msb:PlatformToolset" | Foreach {$_.InnerText = "v90"}
	SelectNodes $xml "//msb:PreprocessorDefinitions" | Foreach {$_.InnerText = "NET20;" + $_.InnerText}
	SelectNodes $xml "//msb:ProjectReference[@Include = '..\Magick.NET.Core\Magick.NET.Core.csproj']" | Foreach {$_.SetAttribute("Include", "..\Magick.NET.Core\Magick.NET.Core.net20.csproj")}
	SelectNodes $xml "//msb:Reference[@Include='PresentationCore' or @Include='WindowsBase' or @Include='System.Xml.Linq']" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
	SelectNodes $xml "//msb:ResourceCompile[@Include]" | Foreach {$_.SetAttribute("Include", "..\Magick.NET.Wrapper\" + $_.GetAttribute("Include"))}
	SelectNodes $xml "//msb:RunCodeAnalysis" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
	SelectNodes $xml "//msb:TargetFrameworkProfile" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
	SelectNodes $xml "//msb:TargetFrameworkVersion" | Foreach {$_.InnerText = "v2.0"}

	$vcxproj = FullPath "Magick.NET.Wrapper.net20\Magick.NET.Wrapper.net20.vcxproj"
	Write-Host "Creating file: $vcxproj"
	$xml.Save($vcxproj)
	
	$path = FullPath "Magick.NET\Magick.NET.csproj"
	$xml = [xml](get-content $path)
	SelectNodes $xml "//msb:DefineConstants"  | Foreach {$_.InnerText = "NET20;" + $_.InnerText}
	SelectNodes $xml "//msb:DocumentationFile" | Foreach {$_.InnerText = $_.InnerText.Replace("x86\", "x86.net20\")}
	SelectNodes $xml "//msb:DocumentationFile" | Foreach {$_.InnerText = $_.InnerText.Replace("x64\", "x64.net20\")}
	SelectNodes $xml "//msb:OutputPath" | Foreach {$_.InnerText = $_.InnerText.Replace("x86", "x86.net20")}
	SelectNodes $xml "//msb:OutputPath" | Foreach {$_.InnerText = $_.InnerText.Replace("x64", "x64.net20")}
	SelectNodes $xml "//msb:ProjectReference[@Include = '..\Magick.NET.Core\Magick.NET.Core.csproj']" | Foreach {$_.SetAttribute("Include", "..\Magick.NET.Core\Magick.NET.Core.net20.csproj")}
	SelectNodes $xml "//msb:ProjectReference[@Include = '..\Magick.NET.Wrapper\Magick.NET.Wrapper.vcxproj']" | Foreach {$_.SetAttribute("Include", "..\Magick.NET.Wrapper.net20\Magick.NET.Wrapper.net20.vcxproj")}
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
	SelectNodes $xml "//msb:ProjectReference[@Include = '..\Magick.NET.Core\Magick.NET.Core.csproj']" | Foreach {$_.SetAttribute("Include", "..\Magick.NET.Core\Magick.NET.Core.net20.csproj")}
	SelectNodes $xml "//msb:ProjectReference[@Include = '..\Magick.NET.Wrapper\Magick.NET.Wrapper.vcxproj']" | Foreach {$_.SetAttribute("Include", "..\Magick.NET.Wrapper.net20\Magick.NET.Wrapper.net20.vcxproj")}
	SelectNodes $xml "//msb:ProjectReference[@Include = '..\Magick.NET\Magick.NET.csproj']" | Foreach {$_.SetAttribute("Include", "..\Magick.NET\Magick.NET.net20.csproj")}
	SelectNodes $xml "//msb:Reference[@Include='PresentationCore' or @Include='WindowsBase']" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
	SelectNodes $xml "//msb:TargetFrameworkVersion" | Foreach {$_.InnerText = "v3.5"}
	
	$csproj = FullPath "Magick.NET.Tests\Magick.NET.Tests.net20.csproj"
	Write-Host "Creating file: $csproj"
	$xml.Save($csproj)
}
#==================================================================================================
