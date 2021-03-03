# Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
# Licensed under the Apache License, Version 2.0.

& cmd /c 'git describe --exact-match --tags HEAD > version.txt 2> nul'
$version = [IO.File]::ReadAllText("version.txt").Trim()

if ($version.Length -eq 0) {
    & cmd /c 'git log -1 --date=format:"0.%Y.%m%d.%H%M" --format="%ad" > version.txt 2> nul'
    $version = [IO.File]::ReadAllText("version.txt").Trim()
}

Write-Host "Setting NuGetVersion to $version"
echo "NuGetVersion=$version" | Out-File -FilePath $env:GITHUB_ENV -Encoding utf8 -Append

& cmd /c 'git rev-parse HEAD > commit.txt 2> nul'

$commit = [IO.File]::ReadAllText("commit.txt").Trim()

Write-Host "Setting GitCommitId to $commit"
echo "GitCommitId=$commit" | Out-File -FilePath $env:GITHUB_ENV -Encoding utf8 -Append

Exit 0
