#==================================================================================================
$scriptPath = Split-Path -parent $MyInvocation.MyCommand.Path
. $scriptPath\Shared\Functions.ps1
SetFolder $scriptPath
#==================================================================================================
. Tools\Scripts\Shared\FileGenerator.ps1
#==================================================================================================
function BuildMagickNET()
{
	BuildSolution "Magick.NET.sln" "Configuration=ReleaseQ8,RunCodeAnalysis=false,Platform=x86"
	BuildSolution "Magick.NET.sln" "Configuration=ReleaseQ16,RunCodeAnalysis=false,Platform=x86"
	BuildSolution "Magick.NET.sln" "Configuration=ReleaseQ16-HDRI,RunCodeAnalysis=false,Platform=x86"
}
#==================================================================================================
BuildMagickNET
GenerateFiles
BuildMagickNET
#==================================================================================================
