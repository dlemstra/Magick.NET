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

    $process = Start-Process -FilePath $nugetPath -ArgumentList "install Magick.Native -Version $version -OutputDirectory $target" -PassThru -NoNewWindow
    $process.WaitForExit()

    Remove-Item $temp -Recurse -ErrorAction Ignore
}

function copyToSamplesProject($source, $target, $quantum, $platform) {
    $fileName = "Magick.Native-$quantum-$platform.dll"
    [void](New-Item -ItemType directory -Force -Path "$target\Test$quantum\$platform\net40")
    Copy-Item "$source\$fileName" "$target\Test$quantum\$platform\net40\$fileName"
}

function copyToSamplesProjects($source, $target) {
    copyToSamplesProject $source $target "Q8" "x86"
    copyToSamplesProject $source $target "Q8" "x64"
    copyToSamplesProject $source $target "Q16" "x86"
    copyToSamplesProject $source $target "Q16" "x64"
    copyToSamplesProject $source $target "Q16-HDRI" "x86"
    copyToSamplesProject $source $target "Q16-HDRI" "x64"
}

function copyToTestProject($source, $target, $quantum, $platform) {
    $fileName = "Magick.Native-$quantum-$platform.dll"
    [void](New-Item -ItemType directory -Force -Path "$target\Test$quantum\$platform\net452")
    Copy-Item "$source\$fileName" "$target\Test$quantum\$platform\net452\$fileName"
    [void](New-Item -ItemType directory -Force -Path "$target\Test$quantum\$platform\netcoreapp3.1")
    Copy-Item "$source\$fileName" "$target\Test$quantum\$platform\netcoreapp3.1\$fileName"
}

function copyToTestProjects($source, $target) {
    copyToTestProject $source $target "Q8" "x86"
    copyToTestProject $source $target "Q8" "x64"
    copyToTestProject $source $target "Q8-OpenMP" "x64"
    copyToTestProject $source $target "Q16" "x86"
    copyToTestProject $source $target "Q16" "x64"
    copyToTestProject $source $target "Q16-OpenMP" "x64"
    copyToTestProject $source $target "Q16-HDRI" "x86"
    copyToTestProject $source $target "Q16-HDRI" "x64"
    copyToTestProject $source $target "Q16-HDRI-OpenMP" "x64"
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
    copyNotice "$source\**\content\windows\Notice.txt" "$target\win\Notice.txt"

    [void](New-Item -ItemType directory -Path "$target\linux")
    Copy-Item "$source\**\content\linux\**\**\*.so" -Force "$target\linux"
    copyNotice "$source\**\content\linux\Notice.txt" "$target\linux\Notice.txt"

    [void](New-Item -ItemType directory -Path "$target\linux-musl")
    Copy-Item "$source\**\content\linux-musl\**\**\*.so" -Force "$target\linux-musl"
    copyNotice "$source\**\content\linux-musl\Notice.txt" "$target\linux-musl\Notice.txt"

    [void](New-Item -ItemType directory -Path "$target\osx")
    Copy-Item "$source\**\content\macos\**\**\*.dylib" -Force "$target\osx"
    copyNotice "$source\**\content\macos\Notice.txt" "$target\osx\Notice.txt"
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

function createCompressedLibrary($folder, $quantum, $platform) {
    $output = New-Object System.IO.FileStream "$folder\Magick.Native-$quantum-$platform.gz", ([IO.FileMode]::Create), ([IO.FileAccess]::Write), ([IO.FileShare]::None)
    $gzipStream = New-Object System.IO.Compression.GzipStream $output, ([IO.Compression.CompressionMode]::Compress)
    $input = New-Object System.IO.FileStream "$folder\Magick.Native-$quantum-$platform.dll", ([IO.FileMode]::Open), ([IO.FileAccess]::Read), ([IO.FileShare]::Read)
    $buffer = New-Object byte[]($input.Length)
    $buffer = New-Object byte[]($input.Length)
    [void]($input.Read($buffer, 0, $input.Length))
    $gzipStream.Write($buffer, 0, $buffer.Length)
    $input.Close()
    $gzipStream.Close()
    $output.Close()
}

function createCompressedLibraries($folder) {
    createCompressedLibrary $folder "Q8" "x86"
    createCompressedLibrary $folder "Q8" "x64"
    createCompressedLibrary $folder "Q16" "x86"
    createCompressedLibrary $folder "Q16" "x64"
    createCompressedLibrary $folder "Q16-HDRI" "x86"
    createCompressedLibrary $folder "Q16-HDRI" "x64"
}

function createTrademarkAttribute($source, $target) {
    $fileName = "$target\TrademarkAttribute.cs"
    Remove-Item $target -Recurse -ErrorAction Ignore

    $imageMagickVersion = (Get-Item "$source\Magick.Native-Q8-x64.dll").VersionInfo.FileVersion
    ([IO.File]::WriteAllText($fileName, "// <auto-generated/>
[assembly: System.Reflection.AssemblyTrademark(""ImageMagick $imageMagickVersion"")]"))
}

$version = [IO.File]::ReadAllText("$PSScriptRoot\Magick.Native.version").Trim()
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
createCompressedLibraries $windowsLibraries
createTrademarkAttribute $windowsLibraries $PSScriptRoot
Remove-Item $folder -Recurse -ErrorAction Ignore
