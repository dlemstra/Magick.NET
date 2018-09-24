function BuildNative() {
  if ($env:native -eq "true") {
    return $true
  }

  $files = git diff --name-only HEAD~1 HEAD
  return $files -contains "ImageMagick/Source/ImageMagick.commit"
}

if (BuildNative) {
    Write-Host "##vso[task.setvariable variable=BuildNative;isOutput=true]true"
} else {
    Write-Host "Not building the native library."
}