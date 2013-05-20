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

function CheckSignature($builds)
{
	foreach ($build in $builds)
	{
		sn -Tp ("Magick.NET\bin\Release" + $build.Quantum + "\" + $build.Framework + "\" + $build.Platform + "\Magick.NET.dll")
		CheckExitCode ("Magick.NET-" + $build.Quantum + "-" + $build.Platform + " (" + $build.FrameworkName + ") does not represent a strongly named assembly")

		if ($build.Quantum -ne "Q16" -or $build.Framework -ne "v4.0")
		{
			continue;
		}

		sn -Tp ("Magick.NET.Web\bin\Release" + $build.Quantum + "\" + $build.PlatformName + "\Magick.NET.Web.dll")
		CheckExitCode ("Magick.NET.Web-" + $build.Quantum + "-" + $build.PlatformName + " does not represent a strongly named assembly")
	}
}

function CreateNuGetPackages($builds, $imVersion, $version)
{
	$content = [IO.File]::ReadAllText("Magick.NET.targets", [System.Text.Encoding]::Default)
	[IO.File]::WriteAllText("NuGet\Magick.NET.net20.targets", $content.Replace("NET_VERSION","net20"), [System.Text.Encoding]::Default)
	[IO.File]::WriteAllText("NuGet\Magick.NET.net40-client.targets", $content.Replace("NET_VERSION","net40-client"), [System.Text.Encoding]::Default)

	foreach ($build in $builds)
	{
		if ($build.Framework -ne "v4.0")
		{
			continue;
		}

		$xml = [xml](get-content "Publish\Magick.NET.nuspec")

		$id = "Magick.NET-" + $build.Quantum + "-" + $build.PlatformName
		$xml.package.metadata.id = $id
		$xml.package.metadata.title = $id
		$xml.package.metadata.version = $version
		$xml.package.metadata.releaseNotes = "Magick.NET compiled against ImageMagick " + $imVersion

		$nuspecFile = "NuGet\$id.nuspec"
		if (Test-Path $nuspecFile)
		{
			Remove-Item $nuspecFile
		}

		AddFileElement $xml ("..\..\Magick.NET\bin\Release" + $build.Quantum + "\v2.0\" + $build.Platform + "\Magick.NET.dll") "lib\net20"
		AddFileElement $xml ("..\..\Magick.NET\bin\Release" + $build.Quantum + "\v2.0\" + $build.Platform + "\Magick.NET.xml") "lib\net20"
		AddFileElement $xml ("..\..\Magick.NET\bin\Release" + $build.Quantum + "\v4.0\" + $build.Platform + "\Magick.NET.dll") "lib\net40-client"
		AddFileElement $xml ("..\..\Magick.NET\bin\Release" + $build.Quantum + "\v4.0\" + $build.Platform + "\Magick.NET.xml") "lib\net40-client"

		AddFileElement $xml ("..\..\ImageMagick\" + $build.Quantum + "\bin\v2.0\" + $build.PlatformName + "\*.dll") "ImageMagick\net20"
		AddFileElement $xml ("..\..\ImageMagick\" + $build.Quantum + "\bin\v4.0\" + $build.PlatformName + "\*.dll") "ImageMagick\net40-client"
		AddFileElement $xml ("..\..\ImageMagick\xml\*.xml") "ImageMagick\xml"

		AddFileElement $xml ("..\..\Publish\NuGet\Magick.NET.net20.targets") "build\net20\$id.targets"
		AddFileElement $xml ("..\..\Publish\NuGet\Magick.NET.net40-client.targets") "build\net40-client\$id.targets"

		$xml.Save($nuspecFile)

		Publish\NuGet.exe pack Publish\$nuspecFile -NoPackageAnalysis -OutputDirectory Publish\NuGet

		Remove-Item Publish\$nuspecFile
	}

	Remove-Item Publish\NuGet\Magick.NET.net20.targets
	Remove-Item Publish\NuGet\Magick.NET.net40-client.targets
}

function CreateZipFiles($builds, $version)
{
	$location = $(get-location)

	[void][Reflection.Assembly]::LoadWithPartialName("System.IO.Compression.FileSystem")
	$compressionLevel = [System.IO.Compression.CompressionLevel]::Optimal

	foreach ($build in $builds)
	{
		$dir = "$location\Publish\Zip\" + $build.Quantum + "-" + $build.PlatformName + "-" + $build.FrameworkName
		if (Test-Path $dir)
		{
			Remove-Item $dir -recurse
		}

		[void](New-Item $dir -type directory)
		Copy-Item ("Magick.NET\bin\Release" + $build.Quantum + "\" + $build.Framework + "\" + $build.Platform + "\Magick.NET.dll") $dir
		Copy-Item ("Magick.NET\bin\Release" + $build.Quantum + "\" + $build.Framework + "\" + $build.Platform + "\Magick.NET.xml") $dir
		
		[void](New-Item $dir\ImageMagick -type directory)
		Copy-Item ("ImageMagick\" + $build.Quantum + "\bin\" + $build.Framework + "\" + $build.PlatformName + "\*.dll") $dir\ImageMagick
		Copy-Item ImageMagick\xml\*xml $dir\ImageMagick

		$fileName = "Magick.NET-$version-" + $build.Quantum + "-" + $build.PlatformName + "-" + $build.FrameworkName + ".zip"
		$zipFile = "$dir\..\$fileName"
		if (Test-Path $zipFile)
		{
			Remove-Item $zipFile
		}

		Write-Host "Creating file: Zip\$fileName"

		[System.IO.Compression.ZipFile]::CreateFromDirectory($dir, $zipFile, $compressionLevel, $false)
		Remove-Item $dir -recurse

		if ($build.Quantum -ne "Q16" -or $build.Framework -ne "v4.0")
		{
			continue;
		}

		$dir = "$location\Publish\Zip\" + $build.PlatformName
		if (Test-Path $dir)
		{
			Remove-Item $dir -recurse
		}

		[void](New-Item $dir -type directory)
		Copy-Item ("Magick.NET.Web\bin\Release" + $build.Quantum + "\" + $build.PlatformName + "\Magick.NET.Web.dll") $dir

		$fileName = "Magick.NET.Web-$version-" + $build.PlatformName + "-net40.zip"
		$zipFile = "$dir\..\$fileName"

		Write-Host "Creating file: Zip\$fileName"

		[System.IO.Compression.ZipFile]::CreateFromDirectory($dir, $zipFile, $compressionLevel, $false)
		Remove-Item $dir -recurse
	}
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

$location = $(get-location)
set-location "$location\.."

$imVersion = "6.8.5.4"
$version = "6.8.5.401"

UpdateAssemblyInfo "..\Magick.NET\AssemblyInfo.cpp" $version
UpdateAssemblyInfo "..\Magick.NET.Web\Properties\AssemblyInfo.cs" $version
UpdateResourceFiles $builds $version

Build $builds
CheckSignature $builds
CreateZipFiles $builds $version
CreateNuGetPackages $builds $imVersion $version