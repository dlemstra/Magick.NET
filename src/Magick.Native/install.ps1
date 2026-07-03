# Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
# Licensed under the Apache License, Version 2.0.

. $PSScriptRoot\..\..\tools\windows\utils.ps1

function installPackage($version, $target) {
    Remove-Item $target -Recurse -ErrorAction Ignore
    [void](New-Item -ItemType directory -Path $target)

    $release = Invoke-RestMethod -Uri "https://api.github.com/repos/dlemstra/Magick.Native/releases/tags/$version"

    Write-Host "Installing Magick.Native $version"

    foreach ($asset in $release.assets) {
        if ($asset.name -match '^(metadata|windows|linux|macos)') {
            $zipFile = "$target\$($asset.name)"
            Write-Host "Downloading $($asset.name)"
            Invoke-WebRequest -Uri $asset.browser_download_url -OutFile $zipFile
            Expand-Archive -Path $zipFile -DestinationPath $target -Force
            Remove-Item $zipFile
        }
    }
}

function copyToSamplesProject($source, $target, $quantum, $platform) {
    $fileName = "Magick.Native-$quantum-$platform.dll"
    [void](New-Item -ItemType directory -Force -Path "$target\Test$quantum\$platform\netstandard20")
    Copy-Item "$source\$fileName" "$target\Test$quantum\$platform\netstandard20\$fileName"
}

function copyToSamplesProjects($source, $target) {
    copyToSamplesProject $source $target "Q8" "x86"
    copyToSamplesProject $source $target "Q8" "x64"
    copyToSamplesProject $source $target "Q8" "arm64"
    copyToSamplesProject $source $target "Q16" "x86"
    copyToSamplesProject $source $target "Q16" "x64"
    copyToSamplesProject $source $target "Q16" "arm64"
    copyToSamplesProject $source $target "Q16-HDRI" "x86"
    copyToSamplesProject $source $target "Q16-HDRI" "x64"
    copyToSamplesProject $source $target "Q16-HDRI" "arm64"
}

function copyToTestProjectFolder($source, $target, $quantum, $platform, $configuration, $filename) {
    [void](New-Item -ItemType directory -Force -Path "$target\$configuration$quantum\$platform\net472")
    Copy-Item "$source\$fileName" "$target\$configuration$quantum\$platform\net472\$fileName"

    if ($platform -ne "AnyCPU") {
        [void](New-Item -ItemType directory -Force -Path "$target\$configuration$quantum\$platform\net8.0")
        Copy-Item "$source\$fileName" "$target\$configuration$quantum\$platform\net8.0\$fileName"
    }
}

function copyToTestProject($source, $target, $quantum, $platform, $libraryPlatform = "") {
    if ($libraryPlatform -eq "") {
        $libraryPlatform = $platform
    }

    $fileName = "Magick.Native-$quantum-$libraryPlatform.dll"

    copyToTestProjectFolder $source $target $quantum $platform "Test" $fileName
    copyToTestProjectFolder $source $target $quantum $platform "Debug" $fileName
}

function copyToTestProjects($source, $target) {
    copyToTestProject $source $target "Q8" "x86"
    copyToTestProject $source $target "Q8" "x64"
    copyToTestProject $source $target "Q8" "arm64"
    copyToTestProject $source $target "Q8" "AnyCPU" "x86"
    copyToTestProject $source $target "Q8" "AnyCPU" "x64"
    copyToTestProject $source $target "Q8-OpenMP" "x64"
    copyToTestProject $source $target "Q8-OpenMP" "arm64"
    copyToTestProject $source $target "Q16" "x86"
    copyToTestProject $source $target "Q16" "x64"
    copyToTestProject $source $target "Q16" "arm64"
    copyToTestProject $source $target "Q16" "AnyCPU" "x86"
    copyToTestProject $source $target "Q16" "AnyCPU" "x64"
    copyToTestProject $source $target "Q16-OpenMP" "x64"
    copyToTestProject $source $target "Q16-OpenMP" "arm64"
    copyToTestProject $source $target "Q16-HDRI" "x86"
    copyToTestProject $source $target "Q16-HDRI" "x64"
    copyToTestProject $source $target "Q16-HDRI" "arm64"
    copyToTestProject $source $target "Q16-HDRI" "AnyCPU" "x86"
    copyToTestProject $source $target "Q16-HDRI" "AnyCPU" "x64"
    copyToTestProject $source $target "Q16-HDRI-OpenMP" "x64"
    copyToTestProject $source $target "Q16-HDRI-OpenMP" "arm64"
}

function copyMetadata($source, $target) {
    Copy-Item "$source\metadata\*.md" -Force "$target"
}

function copyNotice($source, $target) {
    $filename = fullPath "src\Magick.NET\Copyright.txt"
    $copyright = Get-Content $filename
    $notice = Get-Content $source

    Set-Content -Path $target -Value "* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *`r`n"
    Add-Content -Path $target "[ Magick.NET ] copyright:`r`n"
    Add-Content -Path $target $copyright
    Add-Content -Path $target ""
    Add-Content -Path $target $notice
}

function copyLibraries($source, $target) {
    Remove-Item $target -Recurse -ErrorAction Ignore
    [void](New-Item -ItemType directory -Path $target)

    copyNotice "$source\metadata\NOTICE.txt" "$target\Notice.txt"

    [void](New-Item -ItemType directory -Path "$target\win")
    Copy-Item "$source\windows\*\*\*.dll" -Force "$target\win"

    if (Test-Path "$source\linux") {
        [void](New-Item -ItemType directory -Path "$target\linux")
        Copy-Item "$source\linux\*\*\*.so" -Force "$target\linux"
    }

    if (Test-Path "$source\linux-musl") {
        [void](New-Item -ItemType directory -Path "$target\linux-musl")
        Copy-Item "$source\linux-musl\*\*\*.so" -Force "$target\linux-musl"
    }

    if (Test-Path "$source\macos") {
        [void](New-Item -ItemType directory -Path "$target\osx")
        Copy-Item "$source\macos\*\*\*.dylib" -Force "$target\osx"
    }
}

function copyResource($source, $target, $quantum) {
    [void](New-Item -ItemType directory -Path "$target\Release$quantum")
    Copy-Item "$source\resources\Release$quantum\*" "$target\Release$quantum" -Recurse -Force
}

function copyResources($source, $target) {
    Remove-Item $target -Recurse -ErrorAction Ignore
    [void](New-Item -ItemType directory -Path $target)

    copyResource $source $target "Q8"
    copyResource $source $target "Q16"
    copyResource $source $target "Q16-HDRI"
}

function createTrademarkAttribute($source, $target) {
    $fileName = "$target\TrademarkAttribute.cs"
    Remove-Item $target -Recurse -ErrorAction Ignore

    $imageMagickVersion = (Get-Item "$source\Magick.Native-Q8-x64.dll").VersionInfo.ProductVersion
    ([IO.File]::WriteAllText($fileName, "// <auto-generated/>
[assembly: System.Reflection.AssemblyTrademark(""ImageMagick $imageMagickVersion"")]"))
}

$version = [IO.File]::ReadAllText("$PSScriptRoot\Magick.Native.version")
if ($version -ne $version.Trim()) {
    Write-Error "Version contains whitespace"
    Exit 1
}

$folder = "$PSScriptRoot\temp"
$libraries = "$PSScriptRoot\libraries"
$windowsLibraries = "$libraries\win"
$resources = "$PSScriptRoot\resources"
$samplesFolder = fullPath "samples\Magick.NET.Samples\bin"
$testsFolder = fullPath "tests"

installPackage $version $folder
copyMetadata $folder $PSScriptRoot
copyLibraries $folder $libraries
copyResources $folder $resources
copyToTestProjects $windowsLibraries "$testsFolder\Magick.NET.Tests\bin"
copyToTestProjects $windowsLibraries "$testsFolder\Magick.NET.AvaloniaMediaImaging.Tests\bin"
copyToTestProjects $windowsLibraries "$testsFolder\Magick.NET.SystemDrawing.Tests\bin"
copyToTestProjects $windowsLibraries "$testsFolder\Magick.NET.SystemWindowsMedia.Tests\bin"
copyToSamplesProjects $windowsLibraries $samplesFolder
createTrademarkAttribute $windowsLibraries $PSScriptRoot
Remove-Item $folder -Recurse -ErrorAction Ignore
