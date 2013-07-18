function AddCoders($folder)
{
	$projectFile = "$folder\VisualMagick\coders\CORE_coders_mtdll_lib.vcxproj"
	$xml = [xml](get-content $projectFile)
	SelectNodes $xml "//msb:AdditionalIncludeDirectories" | Foreach {$_.InnerText = "..\webp\src;" + $_.InnerText}
	$xml.Save($projectFile)
}

function Build($folder, $platform, $builds)
{
	$configFile = "$folder\magick\magick-baseconfig.h"
	$config = [IO.File]::ReadAllText($configFile, [System.Text.Encoding]::Default)
	$config = $config.Replace("#define ProvideDllMain", "#undef ProvideDllMain")
	$config = $config.Replace("#define MAGICKCORE_X11_DELEGATE", "#undef MAGICKCORE_X11_DELEGATE")
	$config = $config.Replace("//#undef MAGICKCORE_EXCLUDE_DEPRECATED", "#define MAGICKCORE_EXCLUDE_DEPRECATED")
	$config = $config.Replace("// #undef MAGICKCORE_WEBP_DELEGATE", "#define MAGICKCORE_WEBP_DELEGATE")
	$config = $config.Replace("// #undef MAGICKCORE_WMF_DELEGATE", "#define MAGICKCORE_WMF_DELEGATE")
	$config = $config.Replace("// #define MAGICKCORE_LIBRARY_NAME `"MyImageMagick.dll`"", "#define MAGICKCORE_LIBRARY_NAME `"Magick.NET-" + $platform + ".dll`"")

	ModifyDebugInformationFormat $folder
	AddCoders $folder

	foreach ($build in $builds)
	{
		$newConfig = $config.Replace("#define MAGICKCORE_QUANTUM_DEPTH 16", "#define MAGICKCORE_QUANTUM_DEPTH " + $build.QuantumDepth)
		[IO.File]::WriteAllText($configFile, $newConfig, [System.Text.Encoding]::Default)

		ModifyPlatformToolset $folder $build

		$location = $(get-location)
		set-location "$location\$folder\VisualMagick"
		
		if ($platform -eq "x64")
		{
			msbuild /m VisualStaticMTDLL.sln /t:Rebuild ("/p:Configuration=Release,Platform=x64")
		}
		else
		{
			msbuild /m VisualStaticMTDLL.sln /t:Rebuild ("/p:Configuration=Release,Platform=Win32")
		}
		
		CheckExitCode "Build failed."
		
		set-location $location
		
		$newConfig = $newConfig.Replace("#define MAGICKCORE_LIBRARY_NAME `"Magick.NET-" + $platform + ".dll`"", "// #define MAGICKCORE_LIBRARY_NAME `"MyImageMagick.dll`"")
		[IO.File]::WriteAllText($configFile, $newConfig, [System.Text.Encoding]::Default)

		Copy-Item $configFile ("..\Q" + $build.QuantumDepth + "\include\magick")
		Copy-Item $folder\VisualMagick\lib\CORE_RL_*.lib ("..\lib\" + $build.Framework + "\$platform")

		Move-Item ("..\lib\" + $build.Framework + "\$platform\CORE_RL_coders_.lib") ("..\Q" + $build.QuantumDepth + "\lib\" + $build.Framework + "\$platform") -force
		Move-Item ("..\lib\" + $build.Framework + "\$platform\CORE_RL_magick_.lib") ("..\Q" + $build.QuantumDepth + "\lib\" + $build.Framework + "\$platform") -force
		Move-Item ("..\lib\" + $build.Framework + "\$platform\CORE_RL_Magick++_.lib") ("..\Q" + $build.QuantumDepth + "\lib\" + $build.Framework + "\$platform") -force
		Move-Item ("..\lib\" + $build.Framework + "\$platform\CORE_RL_wand_.lib") ("..\Q" + $build.QuantumDepth + "\lib\" + $build.Framework + "\$platform") -force
	}
}

function CheckExitCode($msg)
{
	if ($LastExitCode -ne 0)
	{
		Write-Error $msg
		Exit 1
	}
}

function CheckFolder($folder)
{
	if (Test-Path $folder)
	{
		return;
	}

	Write-Error ("Unable to find folder: " + $folder + ".")
	Exit 1
}

function CopyFiles($folder)
{
	Remove-Item ..\include -recurse
	[void](New-Item -ItemType directory -Path ..\include\magick)
	Copy-Item $folder\magick\*.h ..\include\magick
	Remove-Item ..\include\magick\magick-baseconfig.h
	Copy-Item $folder\Magick++\lib\Magick++.h ..\include
	[void](New-Item -ItemType directory -Path ..\include\Magick++)
	Copy-Item $folder\Magick++\lib\Magick++\*.h ..\include\Magick++
	[void](New-Item -ItemType directory -Path ..\include\wand)
	Copy-Item $folder\wand\*.h ..\include\wand

	foreach ($xmlFile in [IO.Directory]::GetFiles("$folder\VisualMagick\bin", "*.xml"))
	{
		if ([IO.Path]::GetFileName($xmlFile) -eq "log.xml")
		{
			continue
		}

		Copy-Item $xmlFile ..\..\Magick.NET\Resources\xml
	}
}

function CreateSolution($folder, $platform)
{
	$solutionFile = "$folder\VisualMagick\VisualStaticMTDLL.sln"

	if (Test-Path $solutionFile)
	{
		Remove-Item $solutionFile
	}

	$location = $(get-location)
	set-location "$location\$folder\VisualMagick\configure"

	Write-Host ""
	Write-Host "Static Multi-Threaded DLL runtimes ($platform)."
	Start-Process .\configure.exe -wait

	set-location $location

	RemoveProjects $solutionFile
	UpgradeSolution $folder $solutionFile
}

function ModifyDebugInformationFormat($folder)
{
	foreach ($projectFile in [IO.Directory]::GetFiles($folder, "CORE_*.vcxproj", [IO.SearchOption]::AllDirectories))
	{
		$xml = [xml](get-content $projectFile)
		SelectNodes $xml "//msb:DebugInformationFormat" | Foreach {$_.InnerText = ""}
		$xml.Save($projectFile)
	}
}

function ModifyPlatformToolset($folder, $build)
{
	foreach ($projectFile in [IO.Directory]::GetFiles($folder, "CORE_*.vcxproj", [IO.SearchOption]::AllDirectories))
	{
		$xml = [xml](get-content $projectFile)
		SelectNodes $xml "//msb:PlatformToolset" | Foreach {$_.InnerText = $build.PlatformToolset}
		$xml.Save($projectFile)
	}
}

function PatchFiles($folder)
{
	# Fix static linking of libxml
	$xmlversionFile = "$folder\libxml\include\libxml\xmlversion.h"
	$xmlversion = [IO.File]::ReadAllText($xmlversionFile, [System.Text.Encoding]::Default)
	$xmlversion = [regex]::Replace($xmlversion, "([^`r])`n", '$1' + "`r`n")
	$xmlversion = $xmlversion.Replace("#if !defined(_DLL)
#  if !defined(LIBXML_STATIC)
#    define LIBXML_STATIC 1
#  endif
#endif", "#define LIBXML_STATIC")
	[IO.File]::WriteAllText($xmlversionFile, $xmlversion, [System.Text.Encoding]::Default)
}

function RemoveProjects($solutionFile)
{
	Write-Host "Removing projects from solution."
	$lines = [IO.File]::ReadAllLines($solutionFile, [System.Text.Encoding]::Default)
	
	for ($i=0; $i -le $lines.Length - 1; $i++)
	{
		if ($lines[$i].Contains("CORE_xlib") -eq $true -OR
				$lines[$i].Contains("UTIL_") -eq $true -OR
				$lines[$i].Contains("All") -eq $true)
		{
			$lines[$i] = ""
			if ($lines[$i + 1].Contains("EndProject"))
			{
				$lines[$i + 1] = ""
			}
		}
	}

	[IO.File]::WriteAllText($solutionFile, [String]::Join([Environment]::NewLine, $lines), [System.Text.Encoding]::Default)
}

function SelectNodes($xml, $xpath)
{
	[System.Xml.XmlNamespaceManager] $nsmgr = $xml.NameTable;
	$nsmgr.AddNamespace("msb", "http://schemas.microsoft.com/developer/msbuild/2003");
	
	return $xml.SelectNodes($xpath, $nsmgr)
}

function UpgradeSolution($folder, $solutionFile)
{
	Write-Host "Upgrading solution."
	devenv /upgrade $solutionFile
	CheckExitCode "Upgrade failed."

	Remove-Item "$folder\VisualMagick\Backup" -recurse -force
	Remove-Item "$folder\VisualMagick\UpgradeLog.htm"
	Remove-Item "$folder\VisualMagick\UpgradeLog.xml"
}

$builds = @(
		@{QuantumDepth = "8"; Framework = "v2.0"; PlatformToolset="v90"}
		@{QuantumDepth = "8"; Framework = "v4.0"; PlatformToolset="v110"}
		@{QuantumDepth = "16"; Framework = "v2.0"; PlatformToolset="v90"}
		@{QuantumDepth = "16"; Framework = "v4.0"; PlatformToolset="v110"}
	)

$version = $args[0]
$folder = "ImageMagick-$version"

CheckFolder $folder
PatchFiles $folder
CopyFiles $folder

$platform = "x86"
CreateSolution $folder $platform
Build $folder $platform $builds

$platform = "x64"
CreateSolution $folder $platform
Build $folder $platform $builds