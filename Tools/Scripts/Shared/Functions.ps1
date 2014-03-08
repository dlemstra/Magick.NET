#==================================================================================================
function BuildSolution($solution, $properties)
{
	$path = FullPath $solution
	$directory = Split-Path -parent $path
	$filename = Split-Path -leaf $path

	$location = $(Get-Location)
	Set-Location $directory

	msbuild /m $filename /t:Rebuild ("/p:$($properties)")
	CheckExitCode "Failed to build: $($path)"

	Set-Location $location
}
#==================================================================================================
function CheckExitCode($msg)
{
	if ($LastExitCode -ne 0)
	{
		Write-Error $msg
		Exit 1
	}
}
#==================================================================================================
function CheckFolder($folder)
{
	if (Test-Path $folder)
	{
		return;
	}

	Write-Error "Unable to find folder: $($folder)"
	Exit 1
}
#==================================================================================================
function ExecuteFile($path)
{
	Invoke-Expression $path
	CheckExitCode "Failed to execute: $($path)"
}
#==================================================================================================
function FullPath($path)
{
	$location = $(Get-Location)
	return "$($location)\$($path)"
}
#==================================================================================================
function SelectNodes($xml, $xpath)
{
	[System.Xml.XmlNamespaceManager] $nsmgr = $xml.NameTable;
	$nsmgr.AddNamespace("msb", "http://schemas.microsoft.com/developer/msbuild/2003");
	
	return $xml.SelectNodes($xpath, $nsmgr)
}
#==================================================================================================
function SetFolder($scriptPath)
{
	Set-Location "${scriptPath}\..\.."
}
#==================================================================================================