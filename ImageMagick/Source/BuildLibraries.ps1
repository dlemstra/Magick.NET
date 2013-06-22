function Build($folder, $platform, $builds)
{
	$configFile = "$folder\magick\magick-baseconfig.h"
	$config = [IO.File]::ReadAllText($configFile, [System.Text.Encoding]::Default)
	$config = $config.Replace("#define ProvideDllMain", "#undef ProvideDllMain")
	$config = $config.Replace("#define MAGICKCORE_X11_DELEGATE", "#undef MAGICKCORE_X11_DELEGATE")
	$config = $config.Replace("//#undef MAGICKCORE_EXCLUDE_DEPRECATED", "#define MAGICKCORE_EXCLUDE_DEPRECATED")
	$config = $config.Replace("// #undef MAGICKCORE_EMBEDDABLE_SUPPORT", "#define MAGICK_NET `"Magick.NET-" + $platform + ".dll`"")

	foreach ($build in $builds)
	{
		$newConfig = $config.Replace("#define MAGICKCORE_QUANTUM_DEPTH 16", "#define MAGICKCORE_QUANTUM_DEPTH " + $build.QuantumDepth)
		[IO.File]::WriteAllText($configFile, $newConfig, [System.Text.Encoding]::Default)

		ModifyPlatformToolset $folder $build

		$location = $(get-location)
		set-location "$location\$folder\VisualMagick"
		
		msbuild /m VisualStaticMTDLL.sln /t:Rebuild /p:Configuration=Release
		CheckExitCode "Build failed."
		
		set-location $location
		
		$newConfig = $newConfig.Replace("#define MAGICK_NET `"Magick.NET-" + $platform + ".dll`"", "// #undef MAGICKCORE_EMBEDDABLE_SUPPORT")
		[IO.File]::WriteAllText($configFile, $newConfig, [System.Text.Encoding]::Default)

		Copy-Item $configFile ("..\Q" + $build.QuantumDepth + "\include\magick")
		Copy-Item $folder\VisualMagick\lib\CORE_RL_*.lib ("..\Q" + $build.QuantumDepth + "\lib\" + $build.Framework + "\$platform")
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

function CheckFolder($folder)
{
	if (Test-Path $folder)
	{
		return;
	}

	Write-Error ("Unable to find folder: " + $folder + ".")
	Exit
}

function CopyFiles($folder)
{
	Copy-Item $folder\magick\*.h ..\include\magick
	Remove-Item ..\include\magick\magick-baseconfig.h
	Copy-Item $folder\Magick++\lib\Magick++.h ..\include
	Copy-Item $folder\Magick++\lib\Magick++\*.h ..\include\Magick++
	Copy-Item $folder\wand\*.h ..\include\wand
	Copy-Item $folder\VisualMagick\bin\*.xml ..\..\Magick.NET\Resources\xml
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

function FixX64($folder)
{
	$solutionFile = "$folder\VisualMagick\VisualStaticMTDLL.sln"
	$solution = [IO.File]::ReadAllText($solutionFile, [System.Text.Encoding]::Default)
	$solution = $solution.Replace("|Win32", "|x64")
	[IO.File]::WriteAllText($solutionFile, $solution, [System.Text.Encoding]::Default)

	foreach ($projectFile in [IO.Directory]::GetFiles($folder, "CORE_*.vcxproj", [IO.SearchOption]::AllDirectories))
	{
		$xml = [xml](get-content $projectFile)
		SelectNodes $xml "//msb:ProjectConfiguration" | Foreach {$_.SetAttribute("Include", $_.GetAttribute("Include").Replace("|Win32", "|x64"))}
		SelectNodes $xml "//msb:ProjectConfiguration/msb:Platform" | Foreach {$_.InnerText = "x64"}
		SelectNodes $xml "//msb:PropertyGroup[msb:ProjectName]/msb:Keyword" | Foreach {$_.InnerText = "x64Proj"}
		SelectNodes $xml "//msb:*[@Condition]" | Foreach {$_.SetAttribute("Condition", $_.GetAttribute("Condition").Replace("|Win32", "|x64"))}
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
	# Hack so we can include the xml files as resources files.
	$ntBaseFile = "$folder\magick\nt-base.c"
	$ntBase = [IO.File]::ReadAllText($ntBaseFile, [System.Text.Encoding]::Default)
	$ntBase = $ntBase.Replace("if (IsPathAccessible(path) != MagickFalse)
    handle=GetModuleHandle(path);
  else
    handle=GetModuleHandle(0);", "handle=GetModuleHandle(MAGICK_NET);");
	[IO.File]::WriteAllText($ntBaseFile, $ntBase, [System.Text.Encoding]::Default)

	# 'Fix' code analysis false positive.
	$stlHeaderFile = "$folder\Magick++\lib\Magick++\stl.h"
	$stlHeader = [IO.File]::ReadAllText($stlHeaderFile, [System.Text.Encoding]::Default)
	$stlHeader = $stlHeader.Replace("current->next     = 0;

  if ( previous != 0)
    previous->next = current;

  current->scene=scene;
  ++scene;", "current->next     = 0;
  current->scene    = scene++;

  if ( previous != 0)
    previous->next = current;")
	[IO.File]::WriteAllText($stlHeaderFile, $stlHeader, [System.Text.Encoding]::Default)

	# Fix static linking of libxml
	$xmlversionFile = "$folder\libxml\include\libxml\xmlversion.h"
	$xmlversion = [IO.File]::ReadAllText($xmlversionFile, [System.Text.Encoding]::Default)
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

$version = "6.8.5"
$folder = "ImageMagick-$version"

CheckFolder $folder
CopyFiles $folder
PatchFiles $folder

$platform = "x86"
CreateSolution $folder $platform
Build $folder $platform $builds

$platform = "x64"
CreateSolution $folder $platform
FixX64 $folder
Build $folder $platform $builds