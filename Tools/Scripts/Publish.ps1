#==================================================================================================
# Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
. Tools\Scripts\Shared\Publish.ps1

[void][Reflection.Assembly]::LoadWithPartialName("System.IO.Compression.FileSystem")
$compressionLevel = [System.IO.Compression.CompressionLevel]::Optimal

function BuildAll($builds)
{
  foreach ($build in $builds)
  {
    Build $build
    TestBuild $build
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

function CopyPdbFile($build, $framework)
{
  $source = "Source\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)\$($framework)\Magick.NET-$($build.Quantum)-$($build.Platform).pdb"
  $destination = "Publish\Pdb\$($framework).Magick.NET-$($build.Quantum)-$($build.Platform).pdb"

  Copy-Item $source $destination
}

function CopyPdbFiles($builds)
{
  foreach ($build in $builds)
  {
    CopyPdbFile $build "net20"
    CopyPdbFile $build "net40"
    CopyPdbFile $build "netstandard13"

    if ($build.Platform -ne "AnyCPU")
    {
      $source = "Source\Magick.NET.Native$($build.Suffix)\bin\Release$($build.Quantum)\$($build.Platform)\Magick.NET-$($build.Quantum)-$($build.Platform).Native.pdb"
      if (Test-Path $source)
      {
        $destination = "Publish\Pdb\Magick.NET-$($build.Quantum)-$($build.Platform).Native.pdb"

        Copy-Item $source $destination
      }
    }
  }
}

function CopyFrameworkToZipFile($build, $rootDir, $framework)
{
  $dir = "$rootDir\$framework\Magick.NET"
  if (!(Test-Path $dir))
  {
    [void](New-Item $dir -type directory)
  }

  Copy-Item "Source\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)\$($framework)\Magick.NET-$($build.Quantum)-$($build.Platform).dll" $dir
  Copy-Item "Source\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)\$($framework)\Magick.NET-$($build.Quantum)-$($build.Platform).xml" $dir

  if ($build.Platform -ne "AnyCPU")
  {
    Copy-Item "Source\Magick.NET.Native\bin\Release$($build.Quantum)\$($platform)\Magick.NET-$($build.Quantum)-$($build.Platform).Native.dll" $dir
  }

  if ($framework -ne "net40")
  {
    return
  }

  $dir = "$rootDir\$framework\Magick.NET.Web"
  if (!(Test-Path $dir))
  {
    [void](New-Item $dir -type directory)
  }

  Copy-Item "Source\Magick.NET.Web\bin\Release$($build.Quantum)\$($build.Platform)\$($framework)\Magick.NET.Web-$($build.Quantum)-$($build.Platform).dll" $dir
  Copy-Item "Source\Magick.NET.Web\bin\Release$($build.Quantum)\$($build.Platform)\$($framework)\Magick.NET.Web-$($build.Quantum)-$($build.Platform).xml" $dir
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

    CopyFrameworkToZipFile $build $rootDir "net20"
    CopyFrameworkToZipFile $build $rootDir "net40"
  }
}

function CreateAllNuGetPackages($builds)
{
  foreach ($build in $builds)
  {
    $id = "Magick.NET-$($build.Quantum)-$($build.Platform)"
    CreateNuGetPackages $id $version $build

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
  CreateAllNuGetPackages $builds
  CreateZipFiles $builds
}

function TestBuild($build)
{
  $platform=$($build.Platform).Replace("AnyCPU", "x64")
  $platform = "/Platform:$($platform)"
  $dll = "Tests\Magick.NET.Tests\bin\Release$($build.Quantum)\$($build.Platform)\net45\Magick.NET.Tests.dll"
  vstest.console.exe /inIsolation $platform $dll
  CheckExitCode ("Test failed for Magick.NET-" + $build.Quantum + "-" + $build.Platform)
}

if ($args.count -ne 1)
{
  Write-Error "Invalid arguments"
  Exit 1
}
$version = $args[0]

CheckArchive
Cleanup
UpdateVersions $version
UpdateResourceFiles $builds $version
CreatePreProcessedFiles
PreparePublish $builds
GzipAssemblies
CreateAnyCPUProjectFiles
PreparePublish $anyCPUbuilds
Publish $builds
Publish $anyCPUbuilds
CleanupZipFolder