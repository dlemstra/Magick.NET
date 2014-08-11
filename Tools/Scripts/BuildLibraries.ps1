#==================================================================================================
$scriptPath = Split-Path -parent $MyInvocation.MyCommand.Path
. $scriptPath\Shared\Functions.ps1
SetFolder $scriptPath
#==================================================================================================
$Q8Builds = @(
	@{
		Name					= "Q8";
		QuantumDepth		= "8";
		Framework			= "v2.0"
		PlatformToolset	= "v90"
	}
	@{
		Name					= "Q8";
		QuantumDepth		= "8";
		Framework			= "v4.0";
		PlatformToolset	= "v110"
	}
)
$Q16Builds = @(
	@{
		Name					= "Q16";
		QuantumDepth		= "16";
		Framework			= "v2.0"
		PlatformToolset	= "v90"
	}
	@{
		Name					= "Q16";
		QuantumDepth		= "16";
		Framework			= "v4.0";
		PlatformToolset	= "v110"
	}
)
$Q16HDRIBuilds = @(
	@{
		Name					= "Q16-HDRI";
		QuantumDepth		= "16";
		Framework			= "v2.0"
		PlatformToolset	= "v90"
	}
	@{
		Name					= "Q16-HDRI";
		QuantumDepth		= "16";
		Framework			= "v4.0";
		PlatformToolset	= "v110"
	}
)
$configurations = @(
	@{
		Platform = "x86";
		Options  = "/opencl /noHdri";
		Builds   = $Q8Builds;
	}
	@{
		Platform = "x86";
		Options  = "/opencl /noHdri";
		Builds   = $Q16Builds;
	}
	@{
		Platform = "x86";
		Options  = "/opencl";
		Builds   = $Q16HDRIBuilds;
	}
	@{
		Platform = "x64";
		Options  = "/opencl /x64 /noHdri";
		Builds   = $Q8Builds;
	}
	@{
		Platform = "x64";
		Options  = "/opencl /x64 /noHdri";
		Builds   = $Q16Builds;
	}
	@{
		Platform = "x64";
		Options  = "/opencl /x64";
		Builds   = $Q16HDRIBuilds;
	}
)
#==================================================================================================
function AddCoders()
{
	$projectFile = FullPath "ImageMagick\Source\ImageMagick\VisualMagick\coders\CORE_coders_mtdll_lib.vcxproj"
	$xml = [xml](get-content $projectFile)
	SelectNodes $xml "//msb:AdditionalIncludeDirectories" | Foreach {$_.InnerText = "..\webp\src;" + $_.InnerText}
	$xml.Save($projectFile)
}
#==================================================================================================
function Build($platform, $builds)
{
	$configFile = FullPath "ImageMagick\Source\ImageMagick\MagickCore\magick-baseconfig.h"
	$config = [IO.File]::ReadAllText($configFile, [System.Text.Encoding]::Default)
	$config = $config.Replace("#define ProvideDllMain", "#undef ProvideDllMain")
	$config = $config.Replace("#define MAGICKCORE_JBIG_DELEGATE", "#undef MAGICKCORE_JBIG_DELEGATE")
	$config = $config.Replace("// #define MAGICKCORE_LIBRARY_NAME `"MyImageMagick.dll`"", "#define MAGICKCORE_LIBRARY_NAME `"Magick.NET-" + $platform + ".dll`"")

	ModifyDebugInformationFormat
	AddCoders

	foreach ($build in $builds)
	{
		$newConfig = $config.Replace("#define MAGICKCORE_QUANTUM_DEPTH 16", "#define MAGICKCORE_QUANTUM_DEPTH " + $build.QuantumDepth)
		[IO.File]::WriteAllText($configFile, $newConfig, [System.Text.Encoding]::Default)

		ModifyPlatformToolset $build

		$options = "Configuration=Release,Platform="
		if ($platform -eq "x64")
		{
			$options = "$($options)x64";
		}
		else
		{
			$options = "$($options)Win32";
		}

		if ($build.Framework -eq "v2.0")
		{
			$options = "$($options),VCBuildAdditionalOptions=/arch:SSE";
		}

		BuildSolution "ImageMagick\Source\ImageMagick\VisualMagick\VisualStaticMTDLL.sln" $options

		$newConfig = $newConfig.Replace("#define MAGICKCORE_LIBRARY_NAME `"Magick.NET-" + $platform + ".dll`"", "// #define MAGICKCORE_LIBRARY_NAME `"MyImageMagick.dll`"")
		[IO.File]::WriteAllText($configFile, $newConfig, [System.Text.Encoding]::Default)

		Copy-Item $configFile "ImageMagick\$($build.Name)\include\MagickCore"
		Copy-Item ImageMagick\Source\ImageMagick\VisualMagick\lib\CORE_RL_*.lib "ImageMagick\lib\$($build.Framework)\$platform"

		Move-Item "ImageMagick\lib\$($build.Framework)\$($platform)\CORE_RL_coders_.lib"   "ImageMagick\$($build.Name)\lib\$($build.Framework)\$platform" -force
		Move-Item "ImageMagick\lib\$($build.Framework)\$($platform)\CORE_RL_Magick++_.lib" "ImageMagick\$($build.Name)\lib\$($build.Framework)\$platform" -force
		Move-Item "ImageMagick\lib\$($build.Framework)\$($platform)\CORE_RL_MagickCore_.lib"   "ImageMagick\$($build.Name)\lib\$($build.Framework)\$platform" -force
		Move-Item "ImageMagick\lib\$($build.Framework)\$($platform)\CORE_RL_MagickWand_.lib"     "ImageMagick\$($build.Name)\lib\$($build.Framework)\$platform" -force
	}
}
#==================================================================================================
function BuildAll()
{
	foreach ($config in $configurations)
	{
		CreateSolution $config.Platform $config.Options
		Build $config.Platform $config.Builds
	}
}
#==================================================================================================
function BuildDevelopment()
{
	$config = $configurations[1]
	$build = @($config.Builds[1]);

	CreateSolution $config.Platform $config.Options
	Build $config.Platform $build
}
#==================================================================================================
function CopyFiles($folder)
{
	Remove-Item ImageMagick\include -recurse
	[void](New-Item -ItemType directory -Path ImageMagick\include\Magick++)
	Copy-Item ImageMagick\Source\ImageMagick\Magick++\lib\Magick++.h ImageMagick\include
	Copy-Item ImageMagick\Source\ImageMagick\Magick++\lib\Magick++\*.h ImageMagick\include\Magick++
	[void](New-Item -ItemType directory -Path ImageMagick\include\MagickCore)
	Copy-Item ImageMagick\Source\ImageMagick\MagickCore\*.h ImageMagick\include\MagickCore
	Remove-Item ImageMagick\include\MagickCore\magick-baseconfig.h
	[void](New-Item -ItemType directory -Path ImageMagick\include\MagickWand)
	Copy-Item ImageMagick\Source\ImageMagick\MagickWand\*.h ImageMagick\include\MagickWand

	$xmlDirectory = FullPath "ImageMagick\Source\ImageMagick\VisualMagick\bin"
	foreach ($xmlFile in [IO.Directory]::GetFiles($xmlDirectory, "*.xml"))
	{
		if ([IO.Path]::GetFileName($xmlFile) -eq "log.xml")
		{
			continue
		}

		Copy-Item $xmlFile Magick.NET\Resources\xml
	}
}
#==================================================================================================
function CreateSolution($platform, $options)
{
	$solutionFile = FullPath "ImageMagick\Source\ImageMagick\VisualMagick\VisualStaticMTDLL.sln"

	if (Test-Path $solutionFile)
	{
		Remove-Item $solutionFile
	}

	$location = $(get-location)
	set-location "ImageMagick\Source\ImageMagick\VisualMagick\configure"

	Write-Host ""
	Write-Host "Static Multi-Threaded DLL runtimes ($platform)."
	if ($options -ne "")
	{
		Write-Host "Options: $options."
	}

	Start-Process .\configure.exe -ArgumentList "/mtsd /noWizard $options" -wait

	set-location $location

	RemoveProjects $solutionFile
	UpgradeSolution $solutionFile
}
#==================================================================================================
function ModifyDebugInformationFormat($folder)
{
	$folder = FullPath "ImageMagick\Source\ImageMagick"
	foreach ($projectFile in [IO.Directory]::GetFiles($folder, "CORE_*.vcxproj", [IO.SearchOption]::AllDirectories))
	{
		$xml = [xml](get-content $projectFile)
		SelectNodes $xml "//msb:DebugInformationFormat" | Foreach {$_.InnerText = ""}
		$xml.Save($projectFile)
	}
}
#==================================================================================================
function ModifyPlatformToolset($build)
{
	$folder = FullPath "ImageMagick\Source\ImageMagick"
	foreach ($projectFile in [IO.Directory]::GetFiles($folder, "CORE_*.vcxproj", [IO.SearchOption]::AllDirectories))
	{
		$xml = [xml](get-content $projectFile)
		SelectNodes $xml "//msb:PlatformToolset" | Foreach {$_.InnerText = $build.PlatformToolset}
		$xml.Save($projectFile)
	}
}
#==================================================================================================
function PatchFiles()
{
	# Fix static linking of libxml
	$xmlversionFile = FullPath "ImageMagick\Source\ImageMagick\libxml\include\libxml\xmlversion.h"
	$xmlversion = [IO.File]::ReadAllText($xmlversionFile, [System.Text.Encoding]::Default)
	$xmlversion = [regex]::Replace($xmlversion, "([^`r])`n", '$1' + "`r`n")
	$xmlversion = $xmlversion.Replace("#if !defined(_DLL)
#  if !defined(LIBXML_STATIC)
#    define LIBXML_STATIC 1
#  endif
#endif", "#define LIBXML_STATIC")
	[IO.File]::WriteAllText($xmlversionFile, $xmlversion, [System.Text.Encoding]::Default)
}
#==================================================================================================
function RemoveProjects($solutionFile)
{
	Write-Host "Removing projects from solution."
	$lines = [IO.File]::ReadAllLines($solutionFile, [System.Text.Encoding]::Default)
	
	for ($i=0; $i -le $lines.Length - 1; $i++)
	{
		if ($lines[$i].Contains("All") -eq $true)
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
#==================================================================================================
function UpgradeSolution($solutionFile)
{
	$folder = FullPath "ImageMagick\Source\ImageMagick\VisualMagick"
	foreach ($projectFile in [IO.Directory]::GetFiles("$folder", "CORE_*.vcxproj", [IO.SearchOption]::AllDirectories))
	{
		Remove-Item "$projectFile"
		Remove-Item "$projectFile.filters"
	}

	Write-Host "Upgrading solution."
	devenv /upgrade $solutionFile
	CheckExitCode "Upgrade failed."

	Remove-Item "$folder\Backup" -recurse -force
	Remove-Item "$folder\UpgradeLog.htm"
}
#==================================================================================================
CheckFolder "ImageMagick\Source"
PatchFiles
CopyFiles
if ($args[0] -eq "-development")
{
	BuildDevelopment
}
else
{
	BuildAll
}
#==================================================================================================
