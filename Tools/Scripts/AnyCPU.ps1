#==================================================================================================
$scriptPath = Split-Path -parent $MyInvocation.MyCommand.Path
. $scriptPath\Shared\Functions.ps1
SetFolder $scriptPath
#==================================================================================================
. Tools\Scripts\Shared\GzipAssembly.ps1
. Tools\Scripts\Shared\FileGenerator.ps1
. Tools\Scripts\Shared\ProjectFiles.ps1
#==================================================================================================
function BuildMagickNET()
{
	BuildSolution "Magick.NET.sln" "Configuration=ReleaseQ8,RunCodeAnalysis=false,Platform=Win32"
	BuildSolution "Magick.NET.sln" "Configuration=ReleaseQ8,RunCodeAnalysis=false,Platform=x64"
	BuildSolution "Magick.NET.sln" "Configuration=ReleaseQ16,RunCodeAnalysis=false,Platform=Win32"
	BuildSolution "Magick.NET.sln" "Configuration=ReleaseQ16,RunCodeAnalysis=false,Platform=x64"
}
#==================================================================================================
BuildMagickNET
GzipAssemblies
GenerateAnyCPUFiles
CreateAnyCPUProjectFiles
#==================================================================================================