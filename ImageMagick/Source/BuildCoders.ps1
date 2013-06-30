function Build($folder, $framework, $platform)
{
	BuildWebP $folder $framework $platform
}

function BuildWebP($folder, $framework, $platform)
{
	$libwebp = "$folder\VisualMagick\libwebp"
	CheckFolder($libwebp)

	$location = $(get-location)
	set-location "$location\$libwebp"
	
	nmake /f Makefile.vc CFG=release-static RTLIBCFG=dynamic OBJDIR=output ARCH=$platform clean
	nmake /f Makefile.vc CFG=release-static RTLIBCFG=dynamic OBJDIR=output ARCH=$platform

	CheckExitCode "Build failed."

	set-location $location

	$output = $libwebp + "\output\release-static\" +  $platform + "\lib\libwebp.lib"
	Copy-Item $output ("..\lib\" +  $framework + "\" + $platform)
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

$version = $args[0]
$framework = $args[1]
$platform = $args[2]

$folder = "ImageMagick-$version"

CheckFolder $folder
Build $folder $framework $platform