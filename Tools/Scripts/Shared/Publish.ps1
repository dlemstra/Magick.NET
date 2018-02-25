# Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
#
# Licensed under the ImageMagick License (the "License"); you may not use this file except in
# compliance with the License. You may obtain a copy of the License at
#
#   https://www.imagemagick.org/script/license.php
#
# Unless required by applicable law or agreed to in writing, software distributed under the
# License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
# either express or implied. See the License for the specific language governing permissions
# and limitations under the License.

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

  if ($src -match ".dll$")
  {
    SignFile $src.replace("..\..\","")
  }
}

function CheckStrongName($build, $framework)
{
  $path = FullPath "Source\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)\$($framework)\Magick.NET-$($build.Quantum)-$($build.Platform).dll"
  sn -Tp $path
  CheckExitCode "$path does not represent a strongly named assembly"

  if ($framework -eq "net40")
  {
    $path = FullPath "Source\Magick.NET.Web\bin\Release$($build.Quantum)\$($build.Platform)\$($framework)\Magick.NET.Web-$($build.Quantum)-$($build.Platform).dll"
    sn -Tp $path
    CheckExitCode "$path does not represent a strongly named assembly"
  }
}

function CheckStrongNames($builds)
{
  foreach ($build in $builds)
  {
    CheckStrongName $build "net20"
    CheckStrongName $build "net40"
    CheckStrongName $build "netstandard13"
  }
}

function AddLibraryToPackage($xml,$build,$platform)
{
  AddFileElement $xml "..\..\Source\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)\$($platform)\Magick.NET-$($build.Quantum)-$($build.Platform).dll" "lib\$($platform)"
  AddFileElement $xml "..\..\Source\Magick.NET\bin\Release$($build.Quantum)\$($build.Platform)\$($platform)\Magick.NET-$($build.Quantum)-$($build.Platform).xml" "lib\$($platform)"
}

function AddDocumentsToPackage($xml)
{
  AddFileElement $xml "..\Readme.txt" "Readme.txt"
  AddFileElement $xml "..\..\Copyright.txt" "Copyright.txt"
}

function CreateOpenMPNuGetPackage($id, $version, $build)
{
  $path = FullPath "Publish\NuGet\Magick.NET.nuspec"
  $xml = [xml](Get-Content $path)

  AddLibraryToPackage $xml $build "net40"

  AddFileElement $xml "..\..\Source\Magick.NET.Native\bin\OpenMP-Release$($build.Quantum)\$($build.Platform)\Magick.NET-$($build.Quantum)-$($build.Platform).Native.dll" "runtimes\win7-$($build.Platform)\native"
  AddFileElement $xml "Magick.NET.targets" "build\net40\$id.targets"

  AddDocumentsToPackage $xml

  WriteNuGetPackage $id $version $xml
}

function CreateNuGetPackages($id, $version, $build)
{
  $path = FullPath "Publish\NuGet\Magick.NET.nuspec"
  $xml = [xml](Get-Content $path)

  $platform = $($build.Platform)
  if ($platform -eq "x86")
  {
    $platform = "Win32"
  }

  AddLibraryToPackage $xml $build "net20"
  AddLibraryToPackage $xml $build "net40"
  AddLibraryToPackage $xml $build "netstandard13"

  if ($build.Platform -ne "AnyCPU")
  {
    AddFileElement $xml "..\..\Source\Magick.NET.Native\bin\Release$($build.Quantum)\$($platform)\Magick.NET-$($build.Quantum)-$($build.Platform).Native.dll" "runtimes\win7-$($build.Platform)\native"
    AddFileElement $xml "Magick.NET.targets" "build\net20\$id.targets"
    AddFileElement $xml "Magick.NET.targets" "build\net40\$id.targets"
  }
  else
  {
    AddFileElement $xml "..\..\Source\Magick.NET.Native\bin\Release$($build.Quantum)\Win32\Magick.NET-$($build.Quantum)-x86.Native.dll" "runtimes\win7-x86\native"
    AddFileElement $xml "..\..\Source\Magick.NET.Native\bin\Release$($build.Quantum)\x64\Magick.NET-$($build.Quantum)-x64.Native.dll" "runtimes\win7-x64\native"
  }

  if ($build.Platform -ne "x86")
  {
    AddFileElement $xml "..\..\ImageMagick\$($build.Quantum)\lib\Release\CrossPlatform\linux\Magick.NET-$($build.Quantum)-x64.Native.dll.so" "runtimes\linux-x64\native"
  }

  AddDocumentsToPackage $xml

  WriteNuGetPackage $id $version $xml

  $webId = $id.Replace("Magick.NET", "Magick.NET.Web")
  $path = FullPath "Publish\NuGet\Magick.NET.Web.nuspec"
  $xml = [xml](Get-Content $path)

  AddFileElement $xml "..\..\Source\Magick.NET.Web\bin\Release$($build.Quantum)\$($build.Platform)\net40\Magick.NET.Web-$($build.Quantum)-$($build.Platform).dll" "lib\net40"
  AddFileElement $xml "..\..\Source\Magick.NET.Web\bin\Release$($build.Quantum)\$($build.Platform)\net40\Magick.NET.Web-$($build.Quantum)-$($build.Platform).xml" "lib\net40"

  AddDocumentsToPackage $xml

  WriteNuGetPackage $webId $version $xml
}

function SetValue($content, $startMatch, $endMatch, $value)
{
  $start = $content.IndexOf($startMatch)
  if ($start -eq -1)
  {
    Write-Error "Unable to find startMatch"
    Exit 1
  }

  $start += $startMatch.Length

  $newContent = $content.Substring(0, $start)
  $newContent += $value

  $start = $content.IndexOf($endMatch, $start)
  if ($start -eq -1)
  {
    Write-Error "Unable to find endMatch"
    Exit 1
  }

  $newContent += $content.Substring($start)
  return $newContent
}

function GetValue($content, $startMatch, $endMatch)
{
  $start = $content.IndexOf($startMatch)
  if ($start -eq -1)
  {
    Write-Error "Unable to find startMatch"
    Exit 1
  }

  $start += $startMatch.Length

  $end = $content.IndexOf($endMatch, $start)
  if ($end -eq -1)
  {
    Write-Error "Unable to find endMatch"
    Exit 1
  }

  return $content.Substring($start, $end - $start)
}

function SignFile($fileName)
{
  & signtool verify /q /pa $fileName
  if ($LastExitCode -ne 0)
  {
    & signtool sign /n "ImageMagick Studio LLC" /tr http://sha256timestamp.ws.symantec.com/sha256/timestamp /td sha256 /fd sha256 $fileName
  }
}

function UpdateVersion($fileName, $version, $checkAssemblyVersion)
{
  $path = FullPath $fileName
  $content = [IO.File]::ReadAllText($path, [System.Text.Encoding]::Default)
  if ($checkAssemblyVersion -eq $true)
  {
    $assemblyVersion = GetValue $content "`<AssemblyVersion>" "`<"
    $semVer = $assemblyVersion.Substring(0, $assemblyVersion.LastIndexOf(".") + 1)
    if (!($version.StartsWith($semVer)))
    {
      Write-Error "Invalid assembly file version $assemblyVersion"
      Exit 1
    }
  }

  $content = SetValue $content "`<Version`>" "`<" $version
  $content = SetValue $content "`<FileVersion`>" "`<" $version
  [IO.File]::WriteAllText($path, $content, [System.Text.Encoding]::Default)
}

function UpdateVersions($version, $checkAssemblyVersion)
{
  UpdateVersion "Source\Magick.NET\Magick.NET.csproj" $version $checkAssemblyVersion
  UpdateVersion "Source\Magick.NET.Web\Magick.NET.Web.csproj" $version $checkAssemblyVersion
}

function UpdateResourceFile($fileName, $version)
{
  $content = [IO.File]::ReadAllText($fileName, [System.Text.Encoding]::Unicode)
  $content = SetValue $content "FILEVERSION " `r $version.Replace('.', ',')
  $content = SetValue $content "PRODUCTVERSION " `r $version.Replace('.', ',')
  $content = SetValue $content "`"FileVersion`", `""  "`"" $version
  $content = SetValue $content "`"ProductVersion`", `"" "`"" $version
  $content = SetValue $content "`"LegalCopyright`", `"" "`"" "Copyright © Dirk Lemstra $((Get-Date).year)"

  [IO.File]::WriteAllText($fileName, $content, [System.Text.Encoding]::Unicode)
}

function UpdateResourceFiles($builds, $version)
{
  foreach ($build in $builds)
  {
    $platform = $($build.Platform)
    if ($platform -eq "AnyCPU")
    {
      continue
    }

    if ($platform -eq "x86")
    {
      $platform = "Win32"
    }

    $fileName = FullPath "Source\Magick.NET.Native\Resources\Release$($build.Quantum)\$platform\Magick.NET.rc"
    UpdateResourceFile $filename $version

    $fileName = FullPath "Source\Magick.NET.Native\Resources\Debug$($build.Quantum)\$platform\Magick.NET.rc"
    UpdateResourceFile $filename $version
  }
}

function WriteNuGetPackage($id, $version, $xml)
{
  $xml.package.metadata.id = $id
  $xml.package.metadata.title = $id
  $xml.package.metadata.version = $version
  $xml.package.metadata.releaseNotes = "https://github.com/dlemstra/Magick.NET/releases/tag/$version"

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
