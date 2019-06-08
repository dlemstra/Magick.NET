Param($installPath, $toolsPath, $package, $project)

$invalidExtension = ".vb"
if ($project.Type -eq "VB.NET")
{
	$invalidExtension = ".cs"
}

Function RemoveInvalidFiles($item)
{
	if ($item.Name.EndsWith($invalidExtension))
	{
		Write-Host "Removing: $($item.Name)"
		Remove-Item $item.FileNames(1)
		$item.Remove()
	}
	else
	{
		$item.ProjectItems | ForEach-Object { RemoveInvalidFiles($_) }
	}
}

Write-Host ===================================================
Write-Host Install.ps1
Write-Host ===================================================

RemoveInvalidFiles $project.ProjectItems.Item("Samples").ProjectItems.Item("Magick.NET")