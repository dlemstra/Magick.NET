#==================================================================================================
function AddFileElement($xml, $src, $target)
{
  $files = $xml.package.files

  if (!($files))
  {
    $files = $xml.CreateElement("files", $xml.DocumentElement.NamespaceURI)
    [void]$xml.package.AppendChild($files)
  }

  $file = $xml.CreateElement("file", $xml.DocumentElement.NamespaceURI)

  $srcAtt = $xml.CreateAttribute("src")
  $srcAtt.Value = $src
  [void]$file.Attributes.Append($srcAtt)

  $targetAtt = $xml.CreateAttribute("target")
  $targetAtt.Value = $target
  [void]$file.Attributes.Append($targetAtt)

  [void]$files.AppendChild($file)
}
#==================================================================================================
function CheckStrongNames($builds)
{
  foreach ($build in $builds)
  {
    $path = FullPath "Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET-$($build.Platform).dll"
    sn -Tp $path
    CheckExitCode "$path does not represent a strongly named assembly"

    if ($build.Quantum -ne "Q16" -or $build.Framework -ne "v4.0")
    {
      continue
    }

    $path = FullPath "Magick.NET.Web\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET.Web-$($build.Platform).dll"
    sn -Tp $path
    CheckExitCode "$path does not represent a strongly named assembly"
  }
}
#==================================================================================================
function CreateNuGetPackage($id, $version, $build, $hasNet20)
{
  $path = FullPath "Publish\NuGet\Magick.NET.nuspec"
  $xml = [xml](Get-Content $path)

  $versionPath = FullPath "ImageMagick\Source\Version.txt"
  $imVersion = [IO.File]::ReadAllText($versionPath, [System.Text.Encoding]::Unicode)
  $xml.package.metadata.releaseNotes = "Magick.NET linked with ImageMagick " + $imVersion

  if ($hasNet20 -eq $true)
  {
    AddFileElement $xml "..\..\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform).net20\Magick.NET.Core.dll" "lib\net20"
    AddFileElement $xml "..\..\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform).net20\Magick.NET.Core.xml" "lib\net20"
    AddFileElement $xml "..\..\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform).net20\Magick.NET.Wrapper-$($build.Platform).dll" "lib\net20"
    AddFileElement $xml "..\..\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform).net20\Magick.NET.Wrapper-$($build.Platform).xml" "lib\net20"
    AddFileElement $xml "..\..\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform).net20\Magick.NET-$($build.Platform).dll" "lib\net20"
    AddFileElement $xml "..\..\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform).net20\Magick.NET-$($build.Platform).xml" "lib\net20"
  }

  AddFileElement $xml "..\..\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET.Core.dll" "lib\$($build.FrameworkName)"
  AddFileElement $xml "..\..\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET.Core.xml" "lib\$($build.FrameworkName)"
  AddFileElement $xml "..\..\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET-$($build.Platform).dll" "lib\$($build.FrameworkName)"
  AddFileElement $xml "..\..\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET-$($build.Platform).xml" "lib\$($build.FrameworkName)"

  if ($build.Platform -ne "AnyCPU")
  {
    AddFileElement $xml "..\..\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET.Wrapper-$($build.Platform).dll" "lib\$($build.FrameworkName)"
    AddFileElement $xml "..\..\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)$($build.Suffix)\Magick.NET.Wrapper-$($build.Platform).xml" "lib\$($build.FrameworkName)"
  }

  AddFileElement $xml ("Readme.txt") "Readme.txt"

  WriteNuGetPackage $id $version $xml
}
#==================================================================================================
function HasNet20($builds)
{
  foreach ($build in $builds)
  {
    if ($build.Framework -eq "v2.0")
    {
      return $true
    }
  }

  return $false
}
#==================================================================================================
function SetVersion($content, $startMatch, $endMatch, $version)
{
  $start = $content.IndexOf($startMatch)
  if ($start -eq -1)
  {
    Write-Error "Unable to find startMatch"
    Exit
  }

  $start += $startMatch.Length

  $newContent = $content.Substring(0, $start)
  $newContent += $version

  $start = $content.IndexOf($endMatch, $start)
  if ($start -eq -1)
  {
    Write-Error "Unable to find endMatch"
    Exit
  }

  $newContent += $content.Substring($start)
  return $newContent
}
#==================================================================================================
function UpdateAssemblyInfo($fileName)
{
  $path = FullPath $fileName
  $content = [IO.File]::ReadAllText($path, [System.Text.Encoding]::Default)
  $content = SetVersion $content "AssemblyFileVersion(`"" "`"" $version
  [IO.File]::WriteAllText($path, $content, [System.Text.Encoding]::Default)
}
#==================================================================================================
function UpdateAssemblyInfos
{
  UpdateAssemblyInfo "Magick.NET.Wrapper\AssemblyInfo.cpp"
  UpdateAssemblyInfo "Magick.NET\Properties\AssemblyInfo.cs"
  UpdateAssemblyInfo "Magick.NET.Core\Properties\AssemblyInfo.cs"
  UpdateAssemblyInfo "Magick.NET.Web\Properties\AssemblyInfo.cs"
}
#==================================================================================================
function UpdateResourceFiles($builds)
{
  foreach ($build in $builds)
  {
    $platform = $($build.Platform)
    if ($platform -eq "x86")
    {
      $platform = "Win32"
    }

    $fileName = FullPath "Magick.NET.Wrapper$($build.Suffix)\Resources\Release$($build.Quantum)\$platform\Magick.NET.rc"

    $content = [IO.File]::ReadAllText($fileName, [System.Text.Encoding]::Unicode)
    $content = SetVersion $content "FILEVERSION " `r $version.Replace('.', ',')
    $content = SetVersion $content "PRODUCTVERSION " `r $version.Replace('.', ',')
    $content = SetVersion $content "`"FileVersion`", `""  "`"" $version
    $content = SetVersion $content "`"ProductVersion`", `"" "`"" $version

    [IO.File]::WriteAllText($fileName, $content, [System.Text.Encoding]::Unicode)
  }
}
#==================================================================================================
function WriteNuGetPackage($id, $version, $xml)
{
  $xml.package.metadata.id = $id
  $xml.package.metadata.title = $id
  $xml.package.metadata.version = $version

  $dir = FullPath "Publish\NuGet"
  $nuspecFile = "$dir\$id.nuspec"
  if (Test-Path $nuspecFile)
  {
    Remove-Item $nuspecFile
  }

  $xml.Save($nuspecFile)

  .\Tools\Programs\NuGet.exe pack $nuspecFile -NoPackageAnalysis -OutputDirectory $dir
  CheckExitCode "Failed to create NuGet package"

  Remove-Item $nuspecFile
}
#==================================================================================================