# Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
# Licensed under the Apache License, Version 2.0.

Write-Host "Downloading Ghostscript 10.00.0"
$sourceGhostscriptExe = "https://github.com/dlemstra/Magick.NET.BuildDependencies/releases/download/build-binaries-2025-08-30/gs1000w32.exe"
$targetGhostscriptExe = "$PSScriptRoot\..\..\tools\windows\gs1000w32.exe"
Invoke-WebRequest $sourceGhostscriptExe -OutFile $targetGhostscriptExe

Write-Host "Downloading FFmpeg 4.2.3"
$sourceFFmpegExe = "https://github.com/dlemstra/Magick.NET.BuildDependencies/releases/download/build-binaries-2025-08-30/ffmpeg-4.2.3-win64.exe"
$targetFFmpegExe = "c:\vcpkg\ffmpeg.exe"
Invoke-WebRequest $sourceFFmpegExe -OutFile $targetFFmpegExe
