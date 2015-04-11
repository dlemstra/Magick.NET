#==================================================================================================
$scriptPath = Split-Path -parent $MyInvocation.MyCommand.Path
. $scriptPath\Shared\Functions.ps1
SetFolder $scriptPath
#==================================================================================================
. Tools\Scripts\Shared\FileGenerator.ps1
. Tools\Scripts\Shared\GzipAssembly.ps1
. Tools\Scripts\Shared\ProjectFiles.ps1
#==================================================================================================
[void][Reflection.Assembly]::LoadWithPartialName("System.IO.Compression.FileSystem")
$compressionLevel = [System.IO.Compression.CompressionLevel]::Optimal
#==================================================================================================
if ($args.count -ne 2)
{
	Write-Error "Invalid arguments"
	Exit 1
}
$imVersion = $args[0]
$version = $args[1]
#==================================================================================================
$builds = @(
		@{Name = "Magick.NET.net20"; Suffix = ".net20"; Quantum = "Q8"; Platform = "x86"; Framework = "v2.0"; FrameworkName = "net20"; RunTests = $true}
		@{Name = "Magick.NET.net20"; Suffix = ".net20"; Quantum = "Q8"; Platform = "x64"; Framework = "v2.0"; FrameworkName = "net20"; RunTests = $false}
		@{Name = "Magick.NET.net20"; Suffix = ".net20"; Quantum = "Q16"; Platform = "x86"; Framework = "v2.0"; FrameworkName = "net20"; RunTests = $true}
		@{Name = "Magick.NET.net20"; Suffix = ".net20"; Quantum = "Q16"; Platform = "x64"; Framework = "v2.0"; FrameworkName = "net20"; RunTests = $false}
		@{Name = "Magick.NET.net20"; Suffix = ".net20"; Quantum = "Q16-HDRI"; Platform = "x86"; Framework = "v2.0"; FrameworkName = "net20"; RunTests = $true}
		@{Name = "Magick.NET.net20"; Suffix = ".net20"; Quantum = "Q16-HDRI"; Platform = "x64"; Framework = "v2.0"; FrameworkName = "net20"; RunTests = $false}
		@{Name = "Magick.NET"; Suffix=""; Quantum = "Q8"; Platform = "x86"; Framework = "v4.0"; FrameworkName = "net40-client"; RunTests = $true}
		@{Name = "Magick.NET"; Suffix=""; Quantum = "Q8"; Platform = "x64"; Framework = "v4.0"; FrameworkName = "net40-client"; RunTests = $false}
		@{Name = "Magick.NET"; Suffix=""; Quantum = "Q16"; Platform = "x86"; Framework = "v4.0"; FrameworkName = "net40-client"; RunTests = $true}
		@{Name = "Magick.NET"; Suffix=""; Quantum = "Q16"; Platform = "x64"; Framework = "v4.0"; FrameworkName = "net40-client"; RunTests = $false}
		@{Name = "Magick.NET"; Suffix=""; Quantum = "Q16-HDRI"; Platform = "x86"; Framework = "v4.0"; FrameworkName = "net40-client"; RunTests = $true}
		@{Name = "Magick.NET"; Suffix=""; Quantum = "Q16-HDRI"; Platform = "x64"; Framework = "v4.0"; FrameworkName = "net40-client"; RunTests = $false}
	)
$anyCPUbuilds = @(
		@{Name = "Magick.NET.AnyCPU"; Suffix=""; Quantum = "Q8"; Platform = "AnyCPU"; Framework = "v4.0"; FrameworkName = "net40-client"; RunTests = $true}
		@{Name = "Magick.NET.AnyCPU"; Suffix=""; Quantum = "Q16"; Platform = "AnyCPU"; Framework = "v4.0"; FrameworkName = "net40-client"; RunTests = $true}
		@{Name = "Magick.NET.AnyCPU"; Suffix=""; Quantum = "Q16-HDRI"; Platform = "AnyCPU"; Framework = "v4.0"; FrameworkName = "net40-client"; RunTests = $true}
	)
#==================================================================================================
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
#==================================================================================================
function Build($builds)
{
	foreach ($build in $builds)
	{
		$config = "Release"

		if ($build.RunTests -eq $true)
		{
			$config = "Tests"
		}

		BuildSolution "$($build.Name).sln" "Configuration=$config$($build.Quantum),RunCodeAnalysis=false,Platform=$($build.Platform)"

		if ($build.RunTests -eq $true)
		{
			$dll = "Magick.NET.Tests\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET.Tests.dll"
			if ($build.Framework -eq "v2.0")
			{
				VSTest.Console.exe $dll 
			}
			else
			{
				VSTest.Console.exe $dll /Settings:Magick.NET.Tests\Magick.NET.Tests.testsettings
			}
			CheckExitCode ("Test failed for Magick.NET-" + $build.Quantum + "-" + $build.Platform + " (" + $build.FrameworkName + ")")
		}
	}
}
#==================================================================================================
function CheckArchive()
{
	if ((Test-Path "..\Magick.NET.Archive\$version"))
	{
		Write-Error "$version has already been published"
		Exit
	}
}
#==================================================================================================
function CheckStrongName($builds)
{
	foreach ($build in $builds)
	{
		$path = FullPath "Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET-$($build.Platform).dll"
		sn -Tp $path
		CheckExitCode "$path does not represent a strongly named assembly"

		if ($build.Quantum -ne "Q16" -or $build.Framework -ne "v4.0")
		{
			continue
		}

		$path = FullPath "Magick.NET.Web\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET.Web-$($build.Platform).dll"
		sn -Tp $path
		CheckExitCode "$path does not represent a strongly named assembly"
	}
}
#==================================================================================================
function Cleanup()
{
	CleanupZipFolder

	$folder = FullPath "Publish\Pdb"
	if (Test-Path $folder)
	{
		Remove-Item $folder -recurse
	}
	[void](New-Item -ItemType directory -Path $folder)
}
#==================================================================================================
function CleanupZipFolder()
{
	$folder = FullPath "Publish\Zip\Releases"
	if (Test-Path $folder)
	{
		Remove-Item $folder -recurse
	}
}
#==================================================================================================
function CopyPdbFiles($builds)
{
	foreach ($build in $builds)
	{
		$source = "Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET.Core.pdb"
		$destination = "Publish\Pdb\$($build.Quantum)-$($build.FrameworkName).Magick.NET.Core.pdb"

		Copy-Item $source $destination

		$source = "Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET-$($build.Platform).pdb"
		$destination = "Publish\Pdb\$($build.Quantum)-$($build.FrameworkName).Magick.NET-$($build.Platform).pdb"

		Copy-Item $source $destination

		if ($build.Platform -ne "AnyCPU")
		{
			$source = "Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET.Wrapper-$($build.Platform).pdb"
			$destination = "Publish\Pdb\$($build.Quantum)-$($build.FrameworkName).Magick.NET.Wrapper-$($build.Platform).pdb"

			Copy-Item $source $destination
		}
	}
}
#==================================================================================================
function CopyZipFiles($builds)
{
	foreach ($build in $builds)
	{
		$dir = FullPath "Publish\Zip\Releases\Magick.NET-$($build.Quantum)-$($build.Platform)"
		if (!(Test-Path $dir))
		{
			[void](New-Item $dir -type directory)
		}

		Copy-Item "Magick.NET\Resources\Release$($build.Quantum)\MagickScript.xsd" $dir

		$dir = "$dir\$($build.FrameworkName)"
		if (!(Test-Path $dir))
		{
			[void](New-Item $dir -type directory)
		}

		Copy-Item "Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET.Core.dll" $dir
		Copy-Item "Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET.Core.xml" $dir
		Copy-Item "Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET-$($build.Platform).dll" $dir
		Copy-Item "Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET-$($build.Platform).xml" $dir

		if ($build.Platform -ne "AnyCPU")
		{
			Copy-Item "Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET.Wrapper-$($build.Platform).dll" $dir
			Copy-Item "Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET.Wrapper-$($build.Platform).xml" $dir
		}

		if ($build.Framework -ne "v4.0")
		{
			continue
		}

		$dir = FullPath "Publish\Zip\Releases\Magick.NET.Web-$($build.Quantum)-$($build.Platform)\$($build.FrameworkName)"
		if (!(Test-Path $dir))
		{
			[void](New-Item $dir -type directory)
		}

		Copy-Item "Magick.NET.Web\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET.Web-$($build.Platform).dll" $dir
		Copy-Item "Magick.NET.Web\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET.Web-$($build.Platform).xml" $dir
	}
}
#==================================================================================================
function CreateNuGetPackage($id, $xml)
{
	$xml.package.metadata.id = $id
	$xml.package.metadata.title = $id
	$xml.package.metadata.version = $version

	$dir = FullPath "Publish\NuGet"
	$nuspecFile = "$dir\$id.nuspec"
	if (Test-Path $nuspecFile)
	{
		Remove-Item $nuspecFile
	}

	$xml.Save($nuspecFile)

	.\Tools\Programs\NuGet.exe pack $nuspecFile -NoPackageAnalysis -OutputDirectory $dir
	CheckExitCode "Failed to create NuGet package"

	Remove-Item $nuspecFile
}
#==================================================================================================
function CreateNuGetPackages($builds)
{
	$hasNet20 = $false
	foreach ($build in $builds)
	{
		if ($build.Framework -eq "v2.0")
		{
			$hasNet20 = $true
		}
	}

	foreach ($build in $builds)
	{
		if ($build.Framework -ne "v4.0")
		{
			continue
		}

		$path = FullPath "Publish\NuGet\Magick.NET.nuspec"
		$xml = [xml](Get-Content $path)
		
		$id = "Magick.NET-$($build.Quantum)-$($build.Platform)"
		$xml.package.metadata.releaseNotes = "Magick.NET linked with ImageMagick " + $imVersion

		if ($hasNet20 -eq $true)
		{
			AddFileElement $xml "..\..\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform).net20\Magick.NET.Core.dll" "lib\net20"
			AddFileElement $xml "..\..\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform).net20\Magick.NET.Core.xml" "lib\net20"
			AddFileElement $xml "..\..\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform).net20\Magick.NET.Wrapper-$($build.Platform).dll" "lib\net20"
			AddFileElement $xml "..\..\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform).net20\Magick.NET.Wrapper-$($build.Platform).xml" "lib\net20"
			AddFileElement $xml "..\..\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform).net20\Magick.NET-$($build.Platform).dll" "lib\net20"
			AddFileElement $xml "..\..\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform).net20\Magick.NET-$($build.Platform).xml" "lib\net20"
		}

		AddFileElement $xml "..\..\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET.Core.dll" "lib\$($build.FrameworkName)"
		AddFileElement $xml "..\..\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET.Core.xml" "lib\$($build.FrameworkName)"
		AddFileElement $xml "..\..\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET-$($build.Platform).dll" "lib\$($build.FrameworkName)"
		AddFileElement $xml "..\..\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET-$($build.Platform).xml" "lib\$($build.FrameworkName)"

		if ($build.Platform -ne "AnyCPU")
		{
			AddFileElement $xml "..\..\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET.Wrapper-$($build.Platform).dll" "lib\$($build.FrameworkName)"
			AddFileElement $xml "..\..\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET.Wrapper-$($build.Platform).xml" "lib\$($build.FrameworkName)"
		}

		AddFileElement $xml ("Readme.txt") "Readme.txt"
		
		CreateNuGetPackage $id $xml

		if ($build.Quantum -ne "Q16")
		{
			continue
		}

		$path = FullPath "Publish\NuGet\Magick.NET.Sample.nuspec"
		$xml = [xml](Get-Content $path)
		
		$xml.package.metadata.dependencies.dependency.id = $id
		$xml.package.metadata.dependencies.dependency.version = $version
		
		$id = "Magick.NET-$($build.Quantum)-$($build.Platform).Sample"
		$samples = FullPath "Magick.NET.Samples\Samples\Magick.NET"
		$files = Get-ChildItem -File -Path $samples\* -Exclude *.cs,*.msl,*.vb -Recurse
		$offset = $files[0].FullName.LastIndexOf("\Magick.NET.Samples\") + 20
		foreach($file in $files)
		{
			AddFileElement $xml $file "Content\$($file.FullName.SubString($offset))"
		}

		$file = FullPath "Publish\NuGet\Sample.Install.ps1"
		AddFileElement $xml $file "Tools\Install.ps1"

		CreateNuGetPackage $id $xml
	}
}
#==================================================================================================
function CreatePreProcessedFiles()
{
	$samples = FullPath "Magick.NET.Samples\Samples\Magick.NET"
	$files = Get-ChildItem -Path $samples\* -Include *.cs,*.msl,*.vb -Recurse
	foreach($file in $files)
	{
		$content = Get-Content $file
		if ($file.FullName.EndsWith(".cs"))
		{
			$content = $content.Replace("namespace RootNamespace.","namespace `$rootnamespace`$.")
		}
		if ($file.FullName.EndsWith(".vb"))
		{
			$content = $content.Replace("Namespace RootNamespace.","Namespace `$rootnamespace`$.")
		}
		Set-Content "$file.pp" $content
	}
}
#==================================================================================================
function CreateZipFiles($builds)
{
	foreach ($build in $builds)
	{
		$dir = FullPath "Publish\Zip\Releases\Magick.NET-$($build.Quantum)-$($build.Platform)"
		if (!(Test-Path $dir))
		{
			continue
		}

		$zipFile = FullPath "Publish\Zip\Magick.NET-$version-$($build.Quantum)-$($build.Platform).zip"
		if (Test-Path $zipFile)
		{
			Remove-Item $zipFile
		}

		Write-Host "Creating file: $zipFile"

		[System.IO.Compression.ZipFile]::CreateFromDirectory($dir, $zipFile, $compressionLevel, $false)
		Remove-Item $dir -recurse

		$dir = FullPath "Publish\Zip\Releases\Magick.NET.Web-$($build.Quantum)-$($build.Platform)"
		if (!(Test-Path $dir))
		{
			continue
		}

		$zipFile = FullPath "Publish\Zip\Magick.NET.Web-$version-$($build.Quantum)-$($build.Platform).zip"
		if (Test-Path $zipFile)
		{
			Remove-Item $zipFile
		}

		Write-Host "Creating file: $zipFile"

		[System.IO.Compression.ZipFile]::CreateFromDirectory($dir, $zipFile, $compressionLevel, $false)
		Remove-Item $dir -recurse
	}
}
#==================================================================================================
function PreparePublish($builds)
{
	Build $builds
	CheckStrongName $builds
	CopyPdbFiles $builds
	CopyZipFiles $builds
}
#==================================================================================================
function Publish($builds)
{
	CreateNuGetPackages $builds
	CreateZipFiles $builds
}
#==================================================================================================
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
#==================================================================================================
function UpdateAssemblyInfo($fileName)
{
   $path = FullPath $fileName
	$content = [IO.File]::ReadAllText($path, [System.Text.Encoding]::Default)
	$content = SetVersion $content "AssemblyFileVersion(`"" "`"" $version
	[IO.File]::WriteAllText($path, $content, [System.Text.Encoding]::Default)
}
#==================================================================================================
function UpdateResourceFiles($builds)
{
	foreach ($build in $builds)
	{
		$platform = $($build.Platform)
		if ($platform -eq "x86")
		{
			$platform = "Win32"
		}

		$fileName = FullPath "Magick.NET.Wrapper$($build.Suffix)\Resources\Release$($build.Quantum)\$platform\Magick.NET.rc"

		$content = [IO.File]::ReadAllText($fileName, [System.Text.Encoding]::Unicode)
		$content = SetVersion $content "FILEVERSION " `r $version.Replace('.', ',')
		$content = SetVersion $content "PRODUCTVERSION " `r $version.Replace('.', ',')
		$content = SetVersion $content "`"FileVersion`", `""  "`"" $version
		$content = SetVersion $content "`"ProductVersion`", `"" "`"" $version

		[IO.File]::WriteAllText($fileName, $content, [System.Text.Encoding]::Unicode)
	}
}
#==================================================================================================
CheckArchive
Cleanup
UpdateAssemblyInfo "Magick.NET.Wrapper\AssemblyInfo.cpp"
UpdateAssemblyInfo "Magick.NET\Properties\AssemblyInfo.cs"
UpdateAssemblyInfo "Magick.NET.Core\Properties\AssemblyInfo.cs"
UpdateAssemblyInfo "Magick.NET.Web\Properties\AssemblyInfo.cs"
CreateNet20ProjectFiles
UpdateResourceFiles $builds
CreatePreProcessedFiles
PreparePublish $builds
GzipAssemblies
CreateAnyCPUProjectFiles
PreparePublish $anyCPUbuilds
Publish $builds
Publish $anyCPUbuilds
CleanupZipFolder
#==================================================================================================
