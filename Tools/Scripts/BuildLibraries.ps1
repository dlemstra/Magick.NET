#==================================================================================================
# Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
#
# Licensed under the ImageMagick License (the "License"); you may not use this file except in 
# compliance with the License. You may obtain a copy of the License at
#
#   http://www.imagemagick.org/script/license.php
#
# Unless required by applicable law or agreed to in writing, software distributed under the
# License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
# express or implied. See the License for the specific language governing permissions and
# limitations under the License.
#==================================================================================================
 param (
  [string]$dev = "",
  [string]$configuration = "Release"
 )

$scriptPath = Split-Path -parent $MyInvocation.MyCommand.Path
. $scriptPath\Shared\Functions.ps1
SetFolder $scriptPath

$Q8Builds = @(
  @{Name = "Q8"; QuantumDepth = "8"; PlatformToolset = "v140"}
)
$Q16Builds = @(
  @{Name= "Q16"; QuantumDepth = "16"; PlatformToolset = "v140"}
)
$Q16HDRIBuilds = @(
  @{Name = "Q16-HDRI"; QuantumDepth = "16"; PlatformToolset = "v140"}
)
$configurations = @(
  @{Platform = "x86"; Options = "/opencl /noHdri";      Builds = $Q8Builds}
  @{Platform = "x64"; Options = "/opencl /x64 /noHdri"; Builds = $Q8Builds}
  @{Platform = "x86"; Options = "/opencl /noHdri";      Builds = $Q16Builds}
  @{Platform = "x64"; Options = "/opencl /x64 /noHdri"; Builds = $Q16Builds}
  @{Platform = "x86"; Options = "/opencl";              Builds = $Q16HDRIBuilds}
  @{Platform = "x64"; Options = "/opencl /x64";         Builds = $Q16HDRIBuilds}
)

function Build($platform, $builds)
{
  $configFile = FullPath "ImageMagick\Source\ImageMagick\ImageMagick\MagickCore\magick-baseconfig.h"
  $config = [IO.File]::ReadAllText($configFile, [System.Text.Encoding]::Default)

  if ($configuration -eq "Release")
  {
    ModifyDebugInformationFormat
  }

  foreach ($build in $builds)
  {
    $newConfig = $config.Replace("#define MAGICKCORE_QUANTUM_DEPTH 16", "#define MAGICKCORE_QUANTUM_DEPTH " + $build.QuantumDepth)
    $newConfig = $newConfig.Replace("//#define MAGICKCORE_LIBRARY_NAME `"MyImageMagick.dll`"", "#define MAGICKCORE_LIBRARY_NAME `"Magick.NET-" + $build.Name + "-" + $platform + ".Native.dll`"")
    [IO.File]::WriteAllText($configFile, $newConfig, [System.Text.Encoding]::Default)

    ModifyPlatformToolset $build

    $platformName = "Win32"
    if ($platform -eq "x64")
    {
      $platformName = "x64";
    }

    $options = "Configuration=$configuration,Platform=$($platformName),PlatformToolset=v140,VCBuildAdditionalOptions=/arch:SSE"

    BuildSolution "ImageMagick\Source\ImageMagick\VisualMagick\VisualStaticMT.sln" $options

    Copy-Item $configFile "ImageMagick\$($build.Name)\include\MagickCore"
    $newConfig = $newConfig.Replace("#define MAGICKCORE_LIBRARY_NAME `"Magick.NET-" + $platform + ".dll`"", "//#define MAGICKCORE_LIBRARY_NAME `"MyImageMagick.dll`"")
    [IO.File]::WriteAllText($configFile, $newConfig, [System.Text.Encoding]::Default)

    $type = "RL"
    if ($configuration -eq "Debug")
    {
      $type = "DB"
    }

    if (!(Test-Path "ImageMagick\lib\$($configuration)\$($platform)"))
    {
      [void](New-Item -ItemType directory -Path "ImageMagick\lib\$($configuration)\$($platform)")
    }
    Copy-Item "ImageMagick\Source\ImageMagick\VisualMagick\lib\CORE_$($type)_*.lib" "ImageMagick\lib\$($configuration)\$($platform)"
    
    if ($configuration -eq "Debug")
    {
      Copy-Item "ImageMagick\Source\ImageMagick\VisualMagick\lib\CORE_$($type)_*.pdb" "ImageMagick\lib\$($configuration)\$($platform)"
    }

    if (!(Test-Path "ImageMagick\$($build.Name)\lib\$($configuration)\$($platform)"))
    {
      [void](New-Item -ItemType directory -Path "ImageMagick\$($build.Name)\lib\$($configuration)\$($platform)")
    }
    Move-Item "ImageMagick\lib\$($configuration)\$($platform)\CORE_$($type)_coders_.*"     "ImageMagick\$($build.Name)\lib\$($configuration)\$($platform)" -force
    Move-Item "ImageMagick\lib\$($configuration)\$($platform)\CORE_$($type)_MagickCore_.*" "ImageMagick\$($build.Name)\lib\$($configuration)\$($platform)" -force
    Move-Item "ImageMagick\lib\$($configuration)\$($platform)\CORE_$($type)_MagickWand_.*" "ImageMagick\$($build.Name)\lib\$($configuration)\$($platform)" -force
  }
}

function BuildAll()
{
  foreach ($config in $configurations)
  {
    CreateSolution $config.Platform $config.Options
    Build $config.Platform $config.Builds
  }
}

function BuildDevelopment($dev)
{
  $info = $dev.Split('.')
  $quantum = $info[0]
  $platform = $info[1]

  $config = $null
  if ($quantum -eq "Q8")
  {
    if ($platform -eq "x86")
    {
      $config = $configurations[0]
    }
    elseif ($platform -eq "x64")
    {
      $config = $configurations[1]
    }
  }
  elseif ($quantum -eq "Q16")
  {
    if ($platform -eq "x86")
    {
      $config = $configurations[2]
    }
    elseif ($platform -eq "x64")
    {
      $config = $configurations[3]
    }
  }
  elseif ($quantum -eq "Q16-HDRI")
  {
    if ($platform -eq "x86")
    {
      $config = $configurations[4]
    }
    elseif ($platform -eq "x64")
    {
      $config = $configurations[5]
    }
  }

  if ($config -eq $null)
  {
    return
  }

  $build = @($config.Builds[0]);

  CreateSolution $config.Platform $config.Options
  Build $config.Platform $build
}

function CopyFiles($folder)
{
  Remove-Item ImageMagick\include -recurse
  [void](New-Item -ItemType directory -Path ImageMagick\include\MagickCore)
  Copy-Item ImageMagick\Source\ImageMagick\ImageMagick\MagickCore\*.h ImageMagick\include\MagickCore
  Remove-Item ImageMagick\include\MagickCore\magick-baseconfig.h
  [void](New-Item -ItemType directory -Path ImageMagick\include\MagickWand)
  Copy-Item ImageMagick\Source\ImageMagick\ImageMagick\MagickWand\*.h ImageMagick\include\MagickWand
  [void](New-Item -ItemType directory -Path ImageMagick\include\jpeg)
  Copy-Item ImageMagick\Source\ImageMagick\jpeg\*.h ImageMagick\include\jpeg

  $xmlDirectory = FullPath "ImageMagick\Source\ImageMagick\VisualMagick\bin"
  foreach ($xmlFile in [IO.Directory]::GetFiles($xmlDirectory, "*.xml"))
  {
    if (([IO.Path]::GetFileName($xmlFile) -eq "log.xml") -or
        ([IO.Path]::GetFileName($xmlFile) -eq "policy.xml"))
    {
      continue
    }

    Copy-Item $xmlFile Source\Magick.NET.Native\Resources\xml
  }
}

function CreateSolution($platform, $options)
{
  $solutionFile = FullPath "ImageMagick\Source\ImageMagick\VisualMagick\VisualStaticMT.sln"

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

  Start-Process .\configure.exe -ArgumentList "/smt /noWizard /noOpenMP /VS2015 $options" -wait

  set-location $location
}

function ModifyDebugInformationFormat()
{
  $folder = FullPath "ImageMagick\Source\ImageMagick"
  foreach ($projectFile in [IO.Directory]::GetFiles($folder, "CORE_*.vcxproj", [IO.SearchOption]::AllDirectories))
  {
    $xml = [xml](get-content $projectFile)
    SelectNodes $xml "//msb:DebugInformationFormat" | Foreach {$_.InnerText = ""}
    $xml.Save($projectFile)
  }
}

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

function RecompileConfigure()
{
  BuildSolution "ImageMagick\Source\ImageMagick\VisualMagick\configure\configure.sln" "Configuration=Release,Platform=Win32,PlatformToolset=v140"
}

CheckFolder "ImageMagick\Source"
PatchFiles
RecompileConfigure

if ($dev -ne "")
{
  BuildDevelopment $dev
}
else
{
  BuildAll
}

CopyFiles
