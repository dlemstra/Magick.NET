$scriptPath = Split-Path -parent $MyInvocation.MyCommand.Path
. $scriptPath\Shared\Functions.ps1
SetFolder $scriptPath

. Tools\Scripts\Shared\Build.ps1
. Tools\Scripts\Shared\Config.ps1
. Tools\Scripts\Shared\FileGenerator.ps1
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

    if ($build.RunTests -eq $true)
    {
      $dll = "Magick.NET.Tests\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET.Tests.dll"
      VSTest.Console.exe $dll
      CheckExitCode ("Test failed for Magick.NET-" + $build.Quantum + "-" + $build.Platform + " (" + $build.FrameworkName + ")")
    }
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

function CopyPdbFiles($builds)
{
  foreach ($build in $builds)
  {
    $source = "Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET.Core.pdb"
    $destination = "Publish\Pdb\$($build.Quantum)-$($build.FrameworkName).Magick.NET.Core.pdb"

    Copy-Item $source $destination

    $source = "Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET-$($build.Platform).pdb"
    $destination = "Publish\Pdb\$($build.Quantum)-$($build.FrameworkName).Magick.NET-$($build.Platform).pdb"

    Copy-Item $source $destination

    if ($build.Platform -ne "AnyCPU")
    {
      $source = "Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET.Wrapper-$($build.Platform).pdb"
      $destination = "Publish\Pdb\$($build.Quantum)-$($build.FrameworkName).Magick.NET.Wrapper-$($build.Platform).pdb"

      Copy-Item $source $destination
    }
  }
}

function CopyZipFiles($builds)
{
  foreach ($build in $builds)
  {
    $dir = FullPath "Publish\Zip\Releases\Magick.NET-$($build.Quantum)-$($build.Platform)"
    if (!(Test-Path $dir))
    {
      [void](New-Item $dir -type directory)
    }

    Copy-Item "Magick.NET\Resources\Release$($build.Quantum)\MagickScript.xsd" $dir

    $dir = "$dir\$($build.FrameworkName)"
    if (!(Test-Path $dir))
    {
      [void](New-Item $dir -type directory)
    }

    Copy-Item "Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET.Core.dll" $dir
    Copy-Item "Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET.Core.xml" $dir
    Copy-Item "Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET-$($build.Platform).dll" $dir
    Copy-Item "Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET-$($build.Platform).xml" $dir

    if ($build.Platform -ne "AnyCPU")
    {
      Copy-Item "Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET.Wrapper-$($build.Platform).dll" $dir
      Copy-Item "Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET.Wrapper-$($build.Platform).xml" $dir
    }

    if ($build.Framework -ne "v4.0")
    {
      continue
    }

    $dir = FullPath "Publish\Zip\Releases\Magick.NET.Web-$($build.Quantum)-$($build.Platform)\$($build.FrameworkName)"
    if (!(Test-Path $dir))
    {
      [void](New-Item $dir -type directory)
    }

    Copy-Item "Magick.NET.Web\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET.Web-$($build.Platform).dll" $dir
    Copy-Item "Magick.NET.Web\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET.Web-$($build.Platform).xml" $dir
  }
}

function CreateNuGetPackages($builds)
{
  $hasNet20 = HasNet20($builds)

  foreach ($build in $builds)
  {
    if ($build.Framework -ne "v4.0")
    {
      continue
    }

    $id = "Magick.NET-$($build.Quantum)-$($build.Platform)"
    CreateNuGetPackage $id $version $build $hasNet20

    if ($build.Quantum -ne "Q16")
    {
      continue
    }

    $path = FullPath "Publish\NuGet\Magick.NET.Sample.nuspec"
    $xml = [xml](Get-Content $path)

    $xml.package.metadata.dependencies.dependency.id = $id
    $xml.package.metadata.dependencies.dependency.version = $version

    $id = "Magick.NET-$($build.Quantum)-$($build.Platform).Sample"
    $samples = FullPath "Magick.NET.Samples\Samples\Magick.NET"
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
}

function CreatePreProcessedFiles()
{
  $samples = FullPath "Magick.NET.Samples\Samples\Magick.NET"
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

    $dir = FullPath "Publish\Zip\Releases\Magick.NET.Web-$($build.Quantum)-$($build.Platform)"
    if (!(Test-Path $dir))
    {
      continue
    }

    $zipFile = FullPath "Publish\Zip\Magick.NET.Web-$version-$($build.Quantum)-$($build.Platform).zip"
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

if ($args.count -ne 2)
{
  Write-Error "Invalid arguments"
  Exit 1
}
$imVersion = $args[0]
$version = $args[1]

CheckArchive
Cleanup
UpdateAssemblyInfos $version
CreateNet20ProjectFiles
UpdateResourceFiles $builds $version
CreatePreProcessedFiles
PreparePublish $builds
GzipAssemblies
CreateAnyCPUProjectFiles
PreparePublish $anyCPUbuilds
Publish $builds
Publish $anyCPUbuilds
CleanupZipFolder

