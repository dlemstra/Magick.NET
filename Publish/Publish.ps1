function AddFileElement($xml, $src, $target)
{
	$files = $xml.package.files

	if (!($files))
	{
		$files = $xml.CreateElement("files", $xml.DocumentElement.NamespaceURI)
		[void]$xml.package.AppendChild($files)
	}

	$file = $xml.CreateElement("file", $xml.DocumentElement.NamespaceURI)
	
	$srcAtt = $xml.CreateAttribute("src")
	$srcAtt.Value = $src
	[void]$file.Attributes.Append($srcAtt)

	$targetAtt = $xml.CreateAttribute("target")
	$targetAtt.Value = $target
	[void]$file.Attributes.Append($targetAtt)

	[void]$files.AppendChild($file)
}

function Build($builds)
{
	$location = $(get-location)
	set-location "$location\.."

	foreach ($build in $builds)
	{
		$config = "Release"

		if ($build.RunTests -eq $true)
		{
			$config = "Tests"
		}

		msbuild /m $build.Solution /t:Rebuild ("/p:Configuration=$config" + $build.Quantum + ",RunCodeAnalysis=false,Platform=" + $build.Platform)
		CheckExitCode ("Build failed for Magick.NET-" + $build.Quantum + "-" + $build.PlatformName + " (" + $build.FrameworkName + ")")

		if ($build.RunTests -eq $true)
		{
			$dll = "Magick.NET.Tests\bin\Release" + $build.Quantum + "\" + $build.Framework + "\" + $build.PlatformName + "\Magick.NET.Tests.dll"
			VSTest.Console.exe $dll /Settings:Magick.NET.Tests\Magick.NET.Tests.testsettings
			CheckExitCode ("Test failed for Magick.NET-" + $build.Quantum + "-" + $build.PlatformName + " (" + $build.FrameworkName + ")")
		}
	}

	set-location $location
}

function CheckDependancies()
{
	if (!(Test-Path "NuGet.exe"))
	{
		Write-Error "Unable to find NuGet.exe"
		Exit
	}
}

function CheckExitCode($msg)
{
	if ($LastExitCode -ne 0)
	{
		Write-Error $msg
		Exit
	}
}

function CheckStrongName($builds)
{
	foreach ($build in $builds)
	{
		sn -Tp ("..\Magick.NET\bin\Release" + $build.Quantum + "\" + $build.Framework + "\" + $build.Platform + "\Magick.NET-" + $build.PlatformName + ".dll")
		CheckExitCode ("Magick.NET-" + $build.Quantum + "-" + $build.PlatformName + " (" + $build.FrameworkName + ") does not represent a strongly named assembly")

		if ($build.Quantum -ne "Q16" -or $build.Framework -ne "v4.0")
		{
			continue
		}

		sn -Tp ("..\Magick.NET.Web\bin\Release" + $build.Quantum + "\" + $build.PlatformName + "\Magick.NET.Web-" + $build.PlatformName + ".dll")
		CheckExitCode ("Magick.NET.Web-" + $build.Quantum + "-" + $build.PlatformName + " does not represent a strongly named assembly")
	}
}

function CreateNet20ProjectFiles()
{
	$xml = [xml](get-content "..\Magick.NET\Magick.NET.vcxproj")
	
	SelectNodes $xml "//msb:TargetFrameworkVersion" | Foreach {$_.InnerText = "v2.0"}
	SelectNodes $xml "//msb:TargetFrameworkProfile" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
	SelectNodes $xml "//msb:PlatformToolset" | Foreach {$_.InnerText = "v90"}
	SelectNodes $xml "//msb:ForcedIncludeFiles" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
	SelectNodes $xml "//msb:RunCodeAnalysis" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
	SelectNodes $xml "//msb:CodeAnalysisRuleSet" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
	SelectNodes $xml "//msb:ClCompile[@Include='GlobalSuppressions.cpp']" | Foreach {[void]$_.ParentNode.RemoveChild($_)}
	SelectNodes $xml "//msb:PreprocessorDefinitions" | Foreach {$_.InnerText = "NET20;" + $_.InnerText}

	$net20vcxproj = "..\Magick.NET\Magick.NET.net20.vcxproj"
	Write-Host "Creating file: $net20vcxproj"
	$xml.Save($net20vcxproj)
	
	$xml = [xml](get-content "..\Magick.NET.Tests\Magick.NET.Tests.csproj")
	SelectNodes $xml "//msb:ProjectReference[@Include = '..\Magick.NET\Magick.NET.vcxproj']" | Foreach {$_.SetAttribute("Include", "..\Magick.NET\Magick.NET.net20.vcxproj")}
	SelectNodes $xml "//msb:OutputPath" | Foreach {$_.InnerText = $_.InnerText.Replace("v4.0", "v2.0")}
	SelectNodes $xml "//msb:DefineConstants"  | Foreach {$_.InnerText = "NET20;" + $_.InnerText}
	
	$net20csproj = "..\Magick.NET.Tests\Magick.NET.Tests.net20.csproj"
	Write-Host "Creating file: $net20csproj"
	$xml.Save($net20csproj)
}

function CreateNuGetPackages($builds, $imVersion, $version)
{
	foreach ($build in $builds)
	{
		if ($build.Framework -ne "v4.0")
		{
			continue
		}

		$xml = [xml](get-content "NuGet\Magick.NET.nuspec")

		$id = "Magick.NET-" + $build.Quantum + "-" + $build.PlatformName
		$xml.package.metadata.id = $id
		$xml.package.metadata.title = $id
		$xml.package.metadata.version = $version
		$xml.package.metadata.releaseNotes = "Magick.NET linked with ImageMagick " + $imVersion

		$nuspecFile = "NuGet\$id.nuspec"
		if (Test-Path $nuspecFile)
		{
			Remove-Item $nuspecFile
		}

		AddFileElement $xml ("..\..\Magick.NET\bin\Release" + $build.Quantum + "\v2.0\" + $build.Platform + "\Magick.NET-" + $build.PlatformName + ".dll") "lib\net20"
		AddFileElement $xml ("..\..\Magick.NET\bin\Release" + $build.Quantum + "\v2.0\" + $build.Platform + "\Magick.NET-" + $build.PlatformName + ".xml") "lib\net20"
		AddFileElement $xml ("..\..\Magick.NET\bin\Release" + $build.Quantum + "\v4.0\" + $build.Platform + "\Magick.NET-" + $build.PlatformName + ".dll") "lib\net40-client"
		AddFileElement $xml ("..\..\Magick.NET\bin\Release" + $build.Quantum + "\v4.0\" + $build.Platform + "\Magick.NET-" + $build.PlatformName + ".xml") "lib\net40-client"

		AddFileElement $xml ("Readme.txt") "Readme.txt"

		$xml.Save($nuspecFile)

		.\NuGet.exe pack $nuspecFile -NoPackageAnalysis -OutputDirectory NuGet

		Remove-Item $nuspecFile
	}
}

function CreateScriptZipFile($build, $version)
{
	if ($build.PlatformName -ne "x86" -or $build.Framework -ne "v4.0")
	{
		return
	}

	$dir = "Zip\" + $build.Quantum
	if (Test-Path $dir)
	{
		Remove-Item $dir -recurse
	}

	[void](New-Item $dir -type directory)
	Copy-Item ("..\Magick.NET\Resources\Release" + $build.Quantum + "\MagickScript.xsd") $dir

	$zipFile = "Zip\MagickScript-$version-" + $build.Quantum + ".zip"

	Write-Host "Creating file: $zipFile"

	[System.IO.Compression.ZipFile]::CreateFromDirectory($dir, $zipFile, $compressionLevel, $false)
	Remove-Item $dir -recurse
}

function CreateWebZipFile($build, $version)
{
	if ($build.Quantum -ne "Q16" -or $build.Framework -ne "v4.0")
	{
		return
	}

	$dir = "Zip\" + $build.PlatformName
	if (Test-Path $dir)
	{
		Remove-Item $dir -recurse
	}

	[void](New-Item $dir -type directory)
	Copy-Item ("..\Magick.NET.Web\bin\Release" + $build.Quantum + "\" + $build.PlatformName + "\Magick.NET.Web-" + $build.PlatformName + ".dll") $dir

	$zipFile = "Zip\Magick.NET.Web-$version-" + $build.PlatformName + "-net40.zip"

	Write-Host "Creating file: $zipFile"

	[System.IO.Compression.ZipFile]::CreateFromDirectory($dir, $zipFile, $compressionLevel, $false)
	Remove-Item $dir -recurse
}

function CreateZipFiles($builds, $version)
{
	[void][Reflection.Assembly]::LoadWithPartialName("System.IO.Compression.FileSystem")
	$compressionLevel = [System.IO.Compression.CompressionLevel]::Optimal

	foreach ($build in $builds)
	{
		$dir = "Zip\" + $build.Quantum + "-" + $build.PlatformName + "-" + $build.FrameworkName
		if (Test-Path $dir)
		{
			Remove-Item $dir -recurse
		}

		[void](New-Item $dir -type directory)
		Copy-Item ("..\Magick.NET\bin\Release" + $build.Quantum + "\" + $build.Framework + "\" + $build.Platform + "\Magick.NET-" + $build.PlatformName + ".dll") $dir
		Copy-Item ("..\Magick.NET\bin\Release" + $build.Quantum + "\" + $build.Framework + "\" + $build.Platform + "\Magick.NET-" + $build.PlatformName + ".xml") $dir
		
		$zipFile = "Zip\Magick.NET-$version-" + $build.Quantum + "-" + $build.PlatformName + "-" + $build.FrameworkName + ".zip"
		if (Test-Path $zipFile)
		{
			Remove-Item $zipFile
		}

		Write-Host "Creating file: $zipFile"

		[System.IO.Compression.ZipFile]::CreateFromDirectory($dir, $zipFile, $compressionLevel, $false)
		Remove-Item $dir -recurse

		CreateWebZipFile $build $version
		CreateScriptZipFile $build $version
	}
}

function SelectNodes($xml, $xpath)
{
	[System.Xml.XmlNamespaceManager] $nsmgr = $xml.NameTable;
	$nsmgr.AddNamespace("msb", "http://schemas.microsoft.com/developer/msbuild/2003");
	
	return $xml.SelectNodes($xpath, $nsmgr)
}

function SetVersion($content, $startMatch, $endMatch, $version)
{
	$start = $content.IndexOf($startMatch)
	if ($start -eq -1)
	{
		Write-Error "Unable to find startMatch"
		Exit
	}

	$start += $startMatch.Length

	$newContent = $content.Substring(0, $start)
	$newContent += $version

	$start = $content.IndexOf($endMatch, $start)
	if ($start -eq -1)
	{
		Write-Error "Unable to find endMatch"
		Exit
	}

	$newContent += $content.Substring($start)
	return $newContent
}

function UpdateAssemblyInfo($fileName, $version)
{
	$content = [IO.File]::ReadAllText($fileName, [System.Text.Encoding]::Default)
	$content = SetVersion $content "AssemblyFileVersion(`"" "`"" $version
	[IO.File]::WriteAllText($fileName, $content, [System.Text.Encoding]::Default)
}

function UpdateResourceFiles($builds, $version)
{
	foreach ($build in $builds)
	{
		$fileName = "..\Magick.NET\Resources\Release" + $build.Quantum + "\" + $build.Framework + "\" + $build.Platform + "\Magick.NET.rc"

		$content = [IO.File]::ReadAllText($fileName, [System.Text.Encoding]::Unicode)
		$content = SetVersion $content "FILEVERSION " `r $version.Replace('.', ',')
		$content = SetVersion $content "PRODUCTVERSION " `r $version.Replace('.', ',')
		$content = SetVersion $content "`"FileVersion`", `""  "`"" $version
		$content = SetVersion $content "`"ProductVersion`", `"" "`"" $version

		[IO.File]::WriteAllText($fileName, $content, [System.Text.Encoding]::Unicode)
	}
}

$builds = @(
		@{Solution = "Magick.NET.net20.sln"; Quantum = "Q8"; Platform = "Win32"; PlatformName = "x86"; Framework = "v2.0"; FrameworkName = "net20"; RunTests = $true}
		@{Solution = "Magick.NET.net20.sln"; Quantum = "Q8"; Platform = "x64"; PlatformName = "x64"; Framework = "v2.0"; FrameworkName = "net20"; RunTests = $false}
		@{Solution = "Magick.NET.net20.sln"; Quantum = "Q16"; Platform = "Win32"; PlatformName = "x86"; Framework = "v2.0"; FrameworkName = "net20"; RunTests = $true}
		@{Solution = "Magick.NET.net20.sln"; Quantum = "Q16"; Platform = "x64"; PlatformName = "x64"; Framework = "v2.0"; FrameworkName = "net20"; RunTests = $false}
		@{Solution = "Magick.NET.sln"; Quantum = "Q8"; Platform = "Win32"; PlatformName = "x86"; Framework = "v4.0"; FrameworkName = "net40-client"; RunTests = $true}
		@{Solution = "Magick.NET.sln"; Quantum = "Q8"; Platform = "x64"; PlatformName = "x64"; Framework = "v4.0"; FrameworkName = "net40-client"; RunTests = $false}
		@{Solution = "Magick.NET.sln"; Quantum = "Q16"; Platform = "Win32"; PlatformName = "x86"; Framework = "v4.0"; FrameworkName = "net40-client"; RunTests = $true}
		@{Solution = "Magick.NET.sln"; Quantum = "Q16"; Platform = "x64"; PlatformName = "x64"; Framework = "v4.0"; FrameworkName = "net40-client"; RunTests = $false}
	)

CheckDependancies

$imVersion = "6.8.7.5"
$version = "6.8.7.502"

UpdateAssemblyInfo "..\Magick.NET\AssemblyInfo.cpp" $version
UpdateAssemblyInfo "..\Magick.NET.Web\Properties\AssemblyInfo.cs" $version
UpdateResourceFiles $builds $version

CreateNet20ProjectFiles

Build $builds
CheckStrongName $builds
CreateZipFiles $builds $version
CreateNuGetPackages $builds $imVersion $version
