$scriptPath = Split-Path -parent $MyInvocation.MyCommand.Path
$scriptPath = "$scriptPath\.."
. $scriptPath\Shared\Functions.ps1
SetFolder $scriptPath

. Tools\Scripts\Shared\Build.ps1
. Tools\Scripts\Shared\Config.ps1
. Tools\Scripts\Shared\GzipAssembly.ps1

function FixStrongNameForNet20()
{
  $fileName = "C:\Program Files (x86)\MSBuild\Microsoft.Cpp\v4.0\Platforms\Win32\Microsoft.Cpp.Win32.targets"
  $content = [IO.File]::ReadAllText($fileName)
  $content = $content.Replace('="%(Link.DelaySign)"', '="$(LinkDelaySign)"').Replace('="%(Link.KeyFile)"', '="$(LinkKeyFile)"')

  [IO.File]::WriteAllText($fileName, $content)
}

function AppVeyorBuild($quantum, $platform, $version)
{
  if ($platform -eq "AnyCPU")
  {
    AppVeyorBuild $quantum "x86" $version
    AppVeyorBuild $quantum "x64" $version

    if ($quantum -eq "Q8")
    {
      GzipAssembliesQ8
    }
    elseif ($quantum -eq "Q16")
    {
      GzipAssembliesQ16
    }
    else
    {
      GzipAssembliesQ16HDRI
    }
  }

  $builds = GetBuilds $quantum $platform

  UpdateResourceFiles $builds $version

  foreach ($build in $builds)
  {
    Build $build
  }
}

$quantum = $args[0]
$platform = $args[1]

$version = GetDevVersion
FixStrongNameForNet20
UpdateAssemblyInfos $version
AppVeyorBuild $quantum $platform $version