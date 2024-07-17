# Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
# Licensed under the Apache License, Version 2.0.

param (
    [string]$library = "",
    [string]$version = ""
)

. $PSScriptRoot\..\tools\windows\utils.ps1

function setValue($content, $startMatch, $endMatch, $value) {
    $start = $content.IndexOf($startMatch)
    if ($start -eq -1) {
        Write-Error "Unable to find startMatch"
        Exit 1
    }

    $start += $startMatch.Length

    $newContent = $content.Substring(0, $start)
    $newContent += $value

    $start = $content.IndexOf($endMatch, $start)
    if ($start -eq -1) {
        Write-Error "Unable to find endMatch"
        Exit 1
    }

    $newContent += $content.Substring($start)
    return $newContent
}

function getVersion($library) {
    $path = fullPath "publish\$library.nuspec"
    $xml = [xml](Get-Content $path)

    return $xml.package.metadata.version
}

function updateNugetVersion($library, $version) {
    $fileName = fullPath "publish\$library.nuspec"
    $xml = [xml](Get-Content $fileName)

    $xml.package.metadata.version = $version

    $xml.Save($fileName)
}

function updateAssemblyVersion($library, $version, $checkAssemblyVersion) {
    $fileName = fullPath "src\$library\$library.csproj"
    $content = [IO.File]::ReadAllText($fileName, [System.Text.Encoding]::Default)

    $assemblyVersion = $version.Substring(0, $version.LastIndexOf(".") + 1) + "0"

    $content = setValue $content "`<AssemblyVersion>" "`<" $assemblyVersion
    $content = setValue $content "`<Version`>" "`<" $version
    $content = setValue $content "`<FileVersion`>" "`<" $version
    [IO.File]::WriteAllText($fileName, $content, [System.Text.Encoding]::Default)
}

function updateMagickNETProps() {
    $fileName = fullPath "Magick.NET.props"
    $content = [IO.File]::ReadAllText($fileName, [System.Text.Encoding]::Default)

    $content = setValue $content "`<Copyright`>" "`<" "Copyright 2013-$((Get-Date).year) Dirk Lemstra"
    [IO.File]::WriteAllText($fileName, $content, [System.Text.Encoding]::Default)
}

function updateCoreVersion($xml) {
    $version = getVersion "Magick.NET.Core"

    $namespaceManager = New-Object -TypeName "Xml.XmlNamespaceManager" -ArgumentList $xml.NameTable
    $namespaceManager.AddNamespace("nuspec", $xml.DocumentElement.NamespaceURI)
    $nodes = $xml.SelectNodes("//nuspec:dependency[@id='Magick.NET.Core']", $namespaceManager)
    foreach ($node in $nodes) {
        $node.SetAttribute("version", $version)
    }
}

function updateNuspecFile($library, $version) {
    $fileName = fullPath "publish\$library.nuspec"
    $xml = [xml](Get-Content $fileName)

    $xml.package.metadata.copyright = "Copyright 2013-$((Get-Date).year) Dirk Lemstra"
    $xml.package.metadata.releaseNotes = "https://github.com/dlemstra/Magick.NET/releases/tag/$version"

    updateCoreVersion $xml

    $xml.Save($fileName)
}

function updateNuspecFiles() {
    $version = getVersion "Magick.NET"
    updateNuspecFile "Magick.NET" $version
    updateNuspecFile "Magick.NET.Core" $version
    updateNuspecFile "Magick.NET.SystemDrawing" $version
    updateNuspecFile "Magick.NET.SystemWindowsMedia" $version
}

if (($library -ne "") -and ($version -ne "")) {
    updateAssemblyVersion $library $version
    updateNugetVersion $library $version
}
else {
    updateMagickNETProps
    updateNuspecFiles
}
