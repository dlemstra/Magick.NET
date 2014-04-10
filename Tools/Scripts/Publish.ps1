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
		@{Name = "Magick.NET.net20"; Quantum = "Q8"; Platform = "Win32"; PlatformName = "x86"; Framework = "v2.0"; FrameworkName = "net20"; RunTests = $true}
		@{Name = "Magick.NET.net20"; Quantum = "Q8"; Platform = "x64"; PlatformName = "x64"; Framework = "v2.0"; FrameworkName = "net20"; RunTests = $false}
		@{Name = "Magick.NET.net20"; Quantum = "Q16"; Platform = "Win32"; PlatformName = "x86"; Framework = "v2.0"; FrameworkName = "net20"; RunTests = $true}
		@{Name = "Magick.NET.net20"; Quantum = "Q16"; Platform = "x64"; PlatformName = "x64"; Framework = "v2.0"; FrameworkName = "net20"; RunTests = $false}
		@{Name = "Magick.NET"; Quantum = "Q8"; Platform = "Win32"; PlatformName = "x86"; Framework = "v4.0"; FrameworkName = "net40-client"; RunTests = $true}
		@{Name = "Magick.NET"; Quantum = "Q8"; Platform = "x64"; PlatformName = "x64"; Framework = "v4.0"; FrameworkName = "net40-client"; RunTests = $false}
		@{Name = "Magick.NET"; Quantum = "Q16"; Platform = "Win32"; PlatformName = "x86"; Framework = "v4.0"; FrameworkName = "net40-client"; RunTests = $true}
		@{Name = "Magick.NET"; Quantum = "Q16"; Platform = "x64"; PlatformName = "x64"; Framework = "v4.0"; FrameworkName = "net40-client"; RunTests = $false}
	)
$anyCPUbuilds = @(
		@{Name = "Magick.NET.AnyCPU"; Quantum = "Q8"; Platform = "AnyCPU"; PlatformName = "AnyCPU"; Framework = "v4.0"; FrameworkName = "net40-client"; RunTests = $true}
		@{Name = "Magick.NET.AnyCPU"; Quantum = "Q16"; Platform = "AnyCPU"; PlatformName = "AnyCPU"; Framework = "v4.0"; FrameworkName = "net40-client"; RunTests = $true}
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
			$dll = "Magick.NET.Tests\bin\Release$($build.Quantum)\$($build.Name)\Magick.NET.Tests.dll"
			VSTest.Console.exe $dll /Settings:Magick.NET.Tests\Magick.NET.Tests.testsettings
			CheckExitCode ("Test failed for Magick.NET-" + $build.Quantum + "-" + $build.PlatformName + " (" + $build.FrameworkName + ")")
		}
	}
}
#==================================================================================================
function CheckArchive()
{
	if ((Test-Path "Publish\Archive\$version"))
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
		$path = FullPath "$($build.Name)\bin\Release$($build.Quantum)\$($build.Platform)\Magick.NET-$($build.PlatformName).dll"
		sn -Tp $path
		CheckExitCode "$path does not represent a strongly named assembly"

		if ($build.Quantum -ne "Q16" -or $build.Framework -ne "v4.0")
		{
			continue
		}

		$path = FullPath "Magick.NET.Web\bin\Release$($build.Quantum)\$($build.Platform)\Magick.NET.Web-$($build.PlatformName).dll"
		CheckExitCode "$path does not represent a strongly named assembly"
	}
}
#==================================================================================================
function CopyPdbFiles($builds)
{
	foreach ($build in $builds)
	{
		$source = "$($build.Name)\bin\Release$($build.Quantum)\$($build.Platform)\Magick.NET-$($build.PlatformName).pdb"
		$destination = "Publish\Pdb\$($build.Quantum)-$($build.FrameworkName).Magick.NET-$($build.PlatformName).pdb"

		Copy-Item $source $destination
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

		$id = "Magick.NET-$($build.Quantum)-$($build.PlatformName)"
		$xml.package.metadata.releaseNotes = "Magick.NET linked with ImageMagick " + $imVersion

		if ($hasNet20 -eq $true)
		{
			AddFileElement $xml "..\..\Magick.NET.net20\bin\Release$($build.Quantum)\$($build.Platform)\Magick.NET-$($build.PlatformName).dll" "lib\net20"
			AddFileElement $xml "..\..\Magick.NET.net20\bin\Release$($build.Quantum)\$($build.Platform)\Magick.NET-$($build.PlatformName).xml" "lib\net20"
		}
		AddFileElement $xml "..\..\$($build.Name)\bin\Release$($build.Quantum)\$($build.Platform)\Magick.NET-$($build.PlatformName).dll" "lib\net40-client"
		AddFileElement $xml "..\..\$($build.Name)\bin\Release$($build.Quantum)\$($build.Platform)\Magick.NET-$($build.PlatformName).xml" "lib\net40-client"

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
		
		$id = "Magick.NET-$($build.Quantum)-$($build.PlatformName).Sample"
		$samples = FullPath "Magick.NET.Samples\Samples\Magick.NET"
		$files = Get-ChildItem -File -Path $samples\* -Exclude *.cs,*.msl -Recurse
		$offset = $files[0].FullName.LastIndexOf("\Magick.NET.Samples\") + 20
		foreach($file in $files)
		{
			AddFileElement $xml $file "Content\$($file.FullName.SubString($offset))"
		}

		CreateNuGetPackage $id $xml
	}
}
#==================================================================================================
function CreatePreProcessedFiles()
{
	$samples = FullPath "Magick.NET.Samples\Samples\Magick.NET"
	$files = Get-ChildItem -Path $samples\* -Include *.cs,*.msl -Recurse
	foreach($file in $files)
	{
		$content = Get-Content $file
		$content = $content.Replace("namespace RootNamespace.","namespace `$rootnamespace`$.")
		Set-Content "$file.pp" $content
	}
}
#==================================================================================================
function CreateScriptZipFile($build)
{
	$dir = FullPath "Publish\Zip\$($build.Quantum)"
	if (Test-Path $dir)
	{
		Remove-Item $dir -recurse
	}

	[void](New-Item $dir -type directory)
	Copy-Item "Magick.NET\Resources\Release$($build.Quantum)\MagickScript.xsd" $dir

	$zipFile = FullPath "Publish\Zip\MagickScript-$version-$($build.Quantum).zip"

	Write-Host "Creating file: $zipFile"

	[System.IO.Compression.ZipFile]::CreateFromDirectory($dir, $zipFile, $compressionLevel, $false)
	Remove-Item $dir -recurse
}
#==================================================================================================
function CreateWebZipFile($build)
{
	if ($build.Framework -ne "v4.0")
	{
		return
	}

	$dir = FullPath "Publish\Zip\$($build.PlatformName)"
	if (Test-Path $dir)
	{
		Remove-Item $dir -recurse
	}

	[void](New-Item $dir -type directory)
	Copy-Item "Magick.NET.Web\bin\Release$($build.Quantum)\$($build.PlatformName)\Magick.NET.Web-$($build.PlatformName).dll" $dir

	$zipFile = FullPath "Publish\Zip\Magick.NET.Web-$version-$($build.Quantum)-$($build.PlatformName)-net40.zip"

	Write-Host "Creating file: $zipFile"

	[System.IO.Compression.ZipFile]::CreateFromDirectory($dir, $zipFile, $compressionLevel, $false)
	Remove-Item $dir -recurse
}
#==================================================================================================
function CreateZipFiles($builds)
{
	foreach ($build in $builds)
	{
		$dir = FullPath "Publish\Zip\$($build.Quantum)-$($build.PlatformName)-$($build.FrameworkName)"
		if (Test-Path $dir)
		{
			Remove-Item $dir -recurse
		}

		[void](New-Item $dir -type directory)
		Copy-Item "$($build.Name)\bin\Release$($build.Quantum)\$($build.Platform)\Magick.NET-$($build.PlatformName).dll" $dir
		Copy-Item "$($build.Name)\bin\Release$($build.Quantum)\$($build.Platform)\Magick.NET-$($build.PlatformName).xml" $dir
		
		$zipFile = FullPath "Publish\Zip\Magick.NET-$version-$($build.Quantum)-$($build.PlatformName)-$($build.FrameworkName).zip"
		if (Test-Path $zipFile)
		{
			Remove-Item $zipFile
		}

		Write-Host "Creating file: $zipFile"

		[System.IO.Compression.ZipFile]::CreateFromDirectory($dir, $zipFile, $compressionLevel, $false)
		Remove-Item $dir -recurse

		CreateWebZipFile $build
	}
}
#==================================================================================================
function Publish($builds)
{
	Build $builds
	CheckStrongName $builds
	CopyPdbFiles $builds
	CreateZipFiles $builds
	CreateNuGetPackages $builds
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
		$fileName = FullPath "$($build.Name)\Resources\Release$($build.Quantum)\$($build.Platform)\Magick.NET.rc"

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
UpdateAssemblyInfo "Magick.NET\AssemblyInfo.cpp"
UpdateAssemblyInfo "Magick.NET.AnyCPU\Properties\AssemblyInfo.cs"
UpdateAssemblyInfo "Magick.NET.Web\Properties\AssemblyInfo.cs"
CreateNet20ProjectFiles
UpdateResourceFiles $builds
CreatePreProcessedFiles
Publish $builds
CreateScriptZipFile $builds[0]
CreateScriptZipFile $builds[2]
GzipAssemblies
GenerateAnyCPUFiles
CreateAnyCPUProjectFiles
Publish $anyCPUbuilds
#==================================================================================================