#==================================================================================================
function CreateTestProjectForAnyCPU()
{
	$xml = [xml](get-content "Magick.NET.Tests\Magick.NET.Tests.csproj")
	SelectNodes $xml "//msb:ProjectReference[@Include = '..\Magick.NET\Magick.NET.vcxproj']" | Foreach {$_.SetAttribute("Include", "..\Magick.NET.AnyCPU\Magick.NET.AnyCPU.csproj")}
	SelectNodes $xml "//msb:OutputPath" | Foreach {$_.InnerText = $_.InnerText.Replace("v4.0", "AnyCPU")}
	
	$AnyCPUcsproj = FullPath "Magick.NET.Tests\Magick.NET.Tests.AnyCPU.csproj"
	Write-Host "Creating file: $AnyCPUcsproj"
	$xml.Save($AnyCPUcsproj)
}
#==================================================================================================