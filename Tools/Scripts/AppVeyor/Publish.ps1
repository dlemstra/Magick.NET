$scriptPath = Split-Path -parent $MyInvocation.MyCommand.Path
$scriptPath = "$scriptPath\.."
. $scriptPath\Shared\Functions.ps1
SetFolder $scriptPath

. Tools\Scripts\Shared\Config.ps1
. Tools\Scripts\Shared\Publish.ps1

function Publish($builds, $version)
{
  CheckStrongNames $builds
  $hasNet20 = HasNet20($builds)

  $build = $builds[0];

  $id = "Magick.NET-dev-$($build.Quantum)-$($build.Platform)"
  CreateNuGetPackage $id $version $build $hasNet20

  $fileName = FullPath "Publish\NuGet\$id.$version.nupkg"
  appveyor PushArtifact $fileName
}


$quantum = $args[0]
$platform = $args[1]

$version = GetDevVersion
$builds = GetBuilds $quantum $platform
Publish $builds $version