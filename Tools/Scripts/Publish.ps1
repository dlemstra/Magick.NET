#==================================================================================================
# Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
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
$scriptPath = Split-Path -parent $MyInvocation.MyCommand.Path
. $scriptPath\Shared\Functions.ps1
SetFolder $scriptPath

. Tools\Scripts\Shared\Build.ps1
. Tools\Scripts\Shared\Config.ps1
. Tools\Scripts\Shared\GzipAssembly.ps1
. Tools\Scripts\Shared\ProjectFiles.ps1
. Tools\Scripts\Shared\Publish.ps1

[void][Reflection.Assembly]::LoadWithPartialName("System.IO.Compression.FileSystem")
$compressionLevel = [System.IO.Compression.CompressionLevel]::Optimal

function BuildAll($builds)
{
  foreach ($build in $builds)
  {
    Build $build

    $platform = "/Platform:$($build.Platform)"
    $dll = "Tests\Magick.NET.Tests\bin\Release$($build.Quantum)\$($build.Platform)\net45\Magick.NET.Tests.dll"
    vstest.console.exe /inIsolation $platform $dll
    CheckExitCode ("Test failed for Magick.NET-" + $build.Quantum + "-" + $build.Platform)
  }
}

function CheckArchive()
{
  if ((Test-Path "..\Magick.NET.Archive\$version"))
  {
    Write-Error "$version has already been published"
    Exit
  }
}

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

function CleanupZipFolder()
{
  $folder = FullPath "Publish\Zip\Releases"
  if (Test-Path $folder)
  {
    Remove-Item $folder -recurse
  }
}

function CopyCorePackage($name)
{
  $source = "Publish\Magick.NET.Core\src\$name\bin\Release\$name.$version.nupkg"
  $destination = "Publish\NuGet"

  Copy-Item $source $destination
}

function CopyPdbFiles($builds)
{
  foreach ($build in $builds)
  {
    $source = "Source\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET-$($build.Quantum)-$($build.Platform).pdb"
    $destination = "Publish\Pdb\$($build.FrameworkName).Magick.NET-$($build.Quantum)-$($build.Platform).pdb"

    Copy-Item $source $destination

    if ($build.Platform -ne "AnyCPU")
    {
      $source = "Source\Magick.NET.Native$($build.Suffix)\bin\Release$($build.Quantum)\$($build.Platform)\Magick.NET-$($build.Quantum)-$($build.Platform).Native.pdb"
      if (Test-Path $source)
      {
        $destination = "Publish\Pdb\$($build.FrameworkName).Magick.NET-$($build.Quantum)-$($build.Platform).Native.pdb"

        Copy-Item $source $destination
      }
    }
  }
}

function CopyZipFiles($builds)
{
  foreach ($build in $builds)
  {
    $platform = $($build.Platform)
    if ($platform -eq "x86")
    {
      $platform = "Win32"
    }

    $rootDir = FullPath "Publish\Zip\Releases\Magick.NET-$($build.Quantum)-$($build.Platform)"
    if (!(Test-Path $rootDir))
    {
      [void](New-Item $rootDir -type directory)
    }

    Copy-Item "Copyright.txt" $rootDir
    Copy-Item "Publish\Readme.txt" $rootDir
    Copy-Item "Source\Magick.NET\Resources\Release$($build.Quantum)\MagickScript.xsd" $rootDir


    $dir = "$rootDir\$($build.FrameworkName)\Magick.NET"
    if (!(Test-Path $dir))
    {
      [void](New-Item $dir -type directory)
    }

    Copy-Item "Source\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET-$($build.Quantum)-$($build.Platform).dll" $dir
    Copy-Item "Source\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET-$($build.Quantum)-$($build.Platform).xml" $dir

    if ($build.Platform -ne "AnyCPU")
    {
      Copy-Item "Source\Magick.NET.Native\bin\Release$($build.Quantum)\$($platform)\Magick.NET-$($build.Quantum)-$($build.Platform).Native.dll" $dir
    }

    if ($build.Framework -ne "v4.0")
    {
      continue
    }

    $dir = "$rootDir\$($build.FrameworkName)\Magick.NET.Web"
    if (!(Test-Path $dir))
    {
      [void](New-Item $dir -type directory)
    }

    Copy-Item "Source\Magick.NET.Web\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET.Web-$($build.Quantum)-$($build.Platform).dll" $dir
    Copy-Item "Source\Magick.NET.Web\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET.Web-$($build.Quantum)-$($build.Platform).xml" $dir
  }
}

function CreateNuGetPackages($builds)
{
  foreach ($build in $builds)
  {
    $id = "Magick.NET-$($build.Quantum)-$($build.Platform)"
    CreateNuGetPackage $id $version $build

    if ($build.Quantum -eq "Q16")
    {
      CreateNuGetPackageWithSamples $build $id
    }
  }
}

function CreateNuGetPackageWithSamples($build, $id)
{
  $path = FullPath "Publish\NuGet\Magick.NET.Sample.nuspec"
  $xml = [xml](Get-Content $path)

  $xml.package.metadata.dependencies.dependency.id = $id
  $xml.package.metadata.dependencies.dependency.version = $version

  $id = "Magick.NET-$($build.Quantum)-$($build.Platform).Sample"
  $samples = FullPath "Samples\Magick.NET.Samples\Samples\Magick.NET"
  $files = Get-ChildItem -File -Path $samples\* -Exclude *.cs,*.msl,*.vb -Recurse
  $offset = $files[0].FullName.LastIndexOf("\Magick.NET.Samples\") + 20
  foreach($file in $files)
  {
    AddFileElement $xml $file "Content\$($file.FullName.SubString($offset))"
  }

  $file = FullPath "Publish\NuGet\Sample.Install.ps1"
  AddFileElement $xml $file "Tools\Install.ps1"

  WriteNuGetPackage $id $version $xml
}

function CreatePreProcessedFiles()
{
  $samples = FullPath "Samples\Magick.NET.Samples\Samples\Magick.NET"
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
  }
}

function PreparePublish($builds)
{
  BuildAll $builds
  CheckStrongNames $builds
  CopyPdbFiles $builds
  CopyZipFiles $builds
}

function Publish($builds)
{
  CreateNuGetPackages $builds
  CreateZipFiles $builds
}

function PublishCore()
{
  PublishCoreDepth "Q8"
  PublishCoreDepth "Q16"
  PublishCoreDepth "Q16-HDRI"
}

function PublishCoreDepth($quantum)
{
  BuildCore "Magick.NET.Core-$quantum.Native"
  BuildCore "Magick.NET.Core-$quantum"
  CopyCorePackage "Magick.NET.Core-$quantum.Native"
  CopyCorePackage "Magick.NET.Core-$quantum"

  $source = "Publish\Magick.NET.Core\src\Magick.NET.Core-$quantum\bin\Debug\netstandard1.3\Magick.NET.Core-$quantum.pdb"
  $destination = "Publish\Pdb\dotnet.Magick.NET.Core-$quantum.pdb"
  Copy-Item $source $destination
}

if ($args.count -ne 1)
{
  Write-Error "Invalid arguments"
  Exit 1
}
$version = $args[0]

CheckArchive
Cleanup
UpdateAssemblyInfos $version
UpdateCoreProjects $version
CreateNet20ProjectFiles
UpdateResourceFiles $builds $version
CreatePreProcessedFiles
PreparePublish $builds
GzipAssemblies
CreateAnyCPUProjectFiles
PreparePublish $anyCPUbuilds
Publish $builds
Publish $anyCPUbuilds
PublishCore
CleanupZipFolder