#==================================================================================================
$scriptPath = Split-Path -parent $MyInvocation.MyCommand.Path
$scriptPath = "$scriptPath\.."
. $scriptPath\Shared\Functions.ps1
SetFolder $scriptPath
#==================================================================================================
. Tools\Scripts\Shared\Config.ps1
. Tools\Scripts\Shared\Publish.ps1
#==================================================================================================
function Getversion()
{
  $utcNow = [System.DateTime]::Now.ToUniversalTime()
  $build = [int]($utcNow.Date - (New-Object System.DateTime –ArgumentList 2000, 1, 1).Date).TotalDays
  $revision = [int]($utcNow.TimeOfDay.TotalSeconds / 2)
  return "1.0.$build.$revision"
}
#==================================================================================================
function Publish($builds, $version)
{
  UpdateAssemblyInfos 
  CheckStrongNames $builds
  UpdateResourceFiles $builds
  $hasNet20 = HasNet20($builds)

  $build = $builds[0];

  $id = "Magick.NET-dev-$($build.Quantum)-$($build.Platform)"
  CreateNuGetPackage $id $version $build $hasNet20

  $fileName = FullPath "Publish\NuGet\$id.$version.nupkg"
  appveyor PushArtifact $fileName
}
#==================================================================================================

$quantum = $args[0]
$platform = $args[1]

$version = Getversion
$builds = GetBuilds $quantum $platform
Publish $builds $version