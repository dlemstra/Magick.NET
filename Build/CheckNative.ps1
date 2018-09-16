$files = git diff --name-only HEAD~1 HEAD

if ($files -contains "ImageMagick/Source/Checkout.sh") {
    Write-Host "##vso[task.setvariable variable=BuildNative;isOutput=true]true"
}