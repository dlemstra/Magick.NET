#==================================================================================================
function CreateAnyCPUProjectFiles()
{
	CreateTestProjectForAnyCPU
	CreateWebProjectForAnyCPU
}
#==================================================================================================
function CreateNet20ProjectFiles()
{
	$path = FullPath "Magick.NET\Magick.NET.vcxproj"
	$xml = [xml](get-content $path)
	
	SelectNodes $xml "//msb:ClCompile[@Include]" | Foreach {$_.SetAttribute("Include", "..\Magick.NET\" + $_.GetAttribute("Include"))}
	SelectNodes $xml "//msb:ClCompile[@Include='GlobalSuppressions.cpp']" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
	SelectNodes $xml "//msb:ClInclude[@Include]" | Foreach {$_.SetAttribute("Include", "..\Magick.NET\" + $_.GetAttribute("Include"))}
	SelectNodes $xml "//msb:CodeAnalysisRuleSet" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
	SelectNodes $xml "//msb:EmbedManagedResourceFile" | Foreach {$_.InnerText = $_.InnerText.Replace("Resources\", "..\Magick.NET\Resources\")}
	SelectNodes $xml "//msb:ForcedIncludeFiles" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
	SelectNodes $xml "//msb:LinkKeyFile" | Foreach {$_.InnerText = $_.InnerText.Replace("Magick.NET.snk", "\..\Magick.NET\Magick.NET.snk")}
	SelectNodes $xml "//msb:None[@Include]" | Foreach {$_.SetAttribute("Include", "..\Magick.NET\" + $_.GetAttribute("Include"))}
	SelectNodes $xml "//msb:PlatformToolset" | Foreach {$_.InnerText = "v90"}
	SelectNodes $xml "//msb:PreprocessorDefinitions" | Foreach {$_.InnerText = "NET20;" + $_.InnerText}
   SelectNodes $xml "//msb:Reference[@Include='PresentationCore' or @Include='WindowsBase' or @Include='System.Xml.Linq']" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
	SelectNodes $xml "//msb:ResourceCompile[@Include]" | Foreach {$_.SetAttribute("Include", "..\Magick.NET\" + $_.GetAttribute("Include"))}
	SelectNodes $xml "//msb:RunCodeAnalysis" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
	SelectNodes $xml "//msb:TargetFrameworkProfile" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
	SelectNodes $xml "//msb:TargetFrameworkVersion" | Foreach {$_.InnerText = "v2.0"}

	$net20vcxproj = FullPath "Magick.NET.net20\Magick.NET.net20.vcxproj"
	Write-Host "Creating file: $net20vcxproj"
	$xml.Save($net20vcxproj)
	
	$path = FullPath "Magick.NET.Tests\Magick.NET.Tests.csproj"
	$xml = [xml](get-content $path)
	SelectNodes $xml "//msb:DefineConstants"  | Foreach {$_.InnerText = "NET20;" + $_.InnerText}
	SelectNodes $xml "//msb:OutputPath" | Foreach {$_.InnerText = $_.InnerText.Replace("Magick.NET", "Magick.NET.net20")}
	SelectNodes $xml "//msb:ProjectReference[@Include = '..\Magick.NET\Magick.NET.vcxproj']" | Foreach {$_.SetAttribute("Include", "..\Magick.NET.net20\Magick.NET.net20.vcxproj")}
	
	$net20csproj = FullPath "Magick.NET.Tests\Magick.NET.Tests.net20.csproj"
	Write-Host "Creating file: $net20csproj"
	$xml.Save($net20csproj)
}
#==================================================================================================
function CreateTestProjectForAnyCPU()
{
	$xml = [xml](get-content "Magick.NET.Tests\Magick.NET.Tests.csproj")
	SelectNodes $xml "//msb:OutputPath" | Foreach {$_.InnerText = $_.InnerText.Replace("Magick.NET", "Magick.NET.AnyCPU")}
	SelectNodes $xml "//msb:ProjectReference[@Include = '..\Magick.NET\Magick.NET.vcxproj']" | Foreach {$_.SetAttribute("Include", "..\Magick.NET.AnyCPU\Magick.NET.AnyCPU.csproj")}
	
	$AnyCPUcsproj = FullPath "Magick.NET.Tests\Magick.NET.Tests.AnyCPU.csproj"
	Write-Host "Creating file: $AnyCPUcsproj"
	$xml.Save($AnyCPUcsproj)
}
#==================================================================================================
function CreateWebProjectForAnyCPU()
{
	$xml = [xml](get-content "Magick.NET.Web\Magick.NET.Web.csproj")
	SelectNodes $xml "//msb:AssemblyName" | Foreach {$_.InnerText = $_.InnerText.Replace("x86", "AnyCPU")}
	SelectNodes $xml "//msb:DocumentationFile" | Foreach {$_.InnerText = $_.InnerText.Replace("x86", "AnyCPU")}
	SelectNodes $xml "//msb:OutputPath" | Foreach {$_.InnerText = $_.InnerText.Replace("x86", "AnyCPU")}
	SelectNodes $xml "//msb:PlatformTarget" | Foreach {$_.InnerText = "AnyCPU"}
	SelectNodes $xml "//msb:ProjectReference[@Include = '..\Magick.NET\Magick.NET.vcxproj']" | Foreach {$_.SetAttribute("Include", "..\Magick.NET.AnyCPU\Magick.NET.AnyCPU.csproj")}
	SelectNodes $xml "//msb:PropertyGroup[@Condition]" | Foreach {if ($_.GetAttribute("Condition") -match 'x64') {[void]$_.ParentNode.RemoveChild($_)}}
	SelectNodes $xml "//msb:PropertyGroup[@Condition]" | Foreach {$_.SetAttribute("Condition", $_.GetAttribute("Condition").Replace("Win32", "AnyCPU"))}

	$AnyCPUcsproj = FullPath "Magick.NET.Web\Magick.NET.Web.AnyCPU.csproj"
	Write-Host "Creating file: $AnyCPUcsproj"
	$xml.Save($AnyCPUcsproj)
}
#==================================================================================================