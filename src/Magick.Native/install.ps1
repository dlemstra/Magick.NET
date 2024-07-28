# Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
# Licensed under the Apache License, Version 2.0.

. $PSScriptRoot\..\..\tools\windows\utils.ps1

function installPackage($version, $target) {
    Remove-Item $target -Recurse -ErrorAction Ignore
    [void](New-Item -ItemType directory -Path $target)

    $temp = "$PSScriptRoot\packages"
    Remove-Item $temp -Recurse -ErrorAction Ignore
    [void](New-Item -ItemType directory -Path $temp)

    $nugetPath = fullPath "tools\windows\nuget.exe"

    if (!(Test-Path $nugetPath))
    {
        Write-Host "Downloading latest NuGet Package manager."

        Invoke-WebRequest -Uri "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe" -OutFile $nugetPath
    }

    if (!(Test-Path "nuget.config"))
    {
        $username = Read-Host "Enter your GitHub username"
        $token = Read-Host "Enter your package read token"

        $process = Start-Process -FilePath ".\create-nuget-config.cmd" -ArgumentList "$username $token" -PassThru -NoNewWindow
        $process.WaitForExit()
    }

    Write-Host "Installing Magick.Native.$version.nupkg"

    $process = Start-Process -FilePath "$nugetPath" -ArgumentList "install Magick.Native -Version $version -OutputDirectory ""$target""" -PassThru -NoNewWindow
    $process.WaitForExit()

    Remove-Item $temp -Recurse -ErrorAction Ignore
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
    [void](New-Item -ItemType directory -Force -Path "$target\$configuration$quantum\$platform\net462")
    Copy-Item "$source\$fileName" "$target\$configuration$quantum\$platform\net462\$fileName"

    if ($platform -ne "AnyCPU") {
        [void](New-Item -ItemType directory -Force -Path "$target\$configuration$quantum\$platform\net8.0")
        Copy-Item "$source\$fileName" "$target\$configuration$quantum\$platform\net8.0\$fileName"
        [void](New-Item -ItemType directory -Force -Path "$target\$configuration$quantum\$platform\net8.0-windows")
        Copy-Item "$source\$fileName" "$target\$configuration$quantum\$platform\net8.0-windows\$fileName"
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
    Copy-Item "$source\**\content\*.md" -Force "$target"
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

    [void](New-Item -ItemType directory -Path "$target\win")
    Copy-Item "$source\**\content\windows\**\**\*.dll" -Force "$target\win"
    copyNotice "$source\**\content\windows\NOTICE" "$target\win\Notice.txt"

    [void](New-Item -ItemType directory -Path "$target\linux")
    Copy-Item "$source\**\content\linux\**\**\*.so" -Force "$target\linux"
    copyNotice "$source\**\content\linux\NOTICE" "$target\linux\Notice.txt"

    [void](New-Item -ItemType directory -Path "$target\linux-musl")
    Copy-Item "$source\**\content\linux-musl\**\**\*.so" -Force "$target\linux-musl"
    copyNotice "$source\**\content\linux-musl\NOTICE" "$target\linux-musl\Notice.txt"

    [void](New-Item -ItemType directory -Path "$target\osx")
    Copy-Item "$source\**\content\macos\**\**\*.dylib" -Force "$target\osx"
    copyNotice "$source\**\content\macos\NOTICE" "$target\osx\Notice.txt"
}

function copyResource($source, $target, $quantum) {
    [void](New-Item -ItemType directory -Path "$target\Release$quantum")
    Copy-Item "$source\**\content\resources\Release$quantum\x64\*.xml" -Force "$target\Release$quantum"
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

    $imageMagickVersion = (Get-Item "$source\Magick.Native-Q8-x64.dll").VersionInfo.FileVersion
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
copyToTestProjects $windowsLibraries "$testsFolder\Magick.NET.SystemDrawing.Tests\bin"
copyToTestProjects $windowsLibraries "$testsFolder\Magick.NET.SystemWindowsMedia.Tests\bin"
copyToSamplesProjects $windowsLibraries $samplesFolder
createTrademarkAttribute $windowsLibraries $PSScriptRoot
Remove-Item $folder -Recurse -ErrorAction Ignore
