$env:ChocolateyIgnoreChecksums = "true"
Import-Module "C:\ProgramData\chocolatey\helpers\chocolateyInstaller.psm1"
Install-ChocolateyPackage "Ghostscript.app" "exe" "/S /NCRC" "https://github.com/ArtifexSoftware/ghostpdl-downloads/releases/download/gs920/gs920w32.exe"
