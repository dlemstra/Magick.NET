#==================================================================================================
function _BuildFileGenerator()
{
	BuildSolution "Tools\Magick.NET.FileGenerator.sln" "Configuration=Release"
}
#==================================================================================================
function GenerateFiles()
{
	_BuildFileGenerator
	ExecuteFile "Tools\Magick.NET.FileGenerator\bin\Release\Magick.NET.FileGenerator.exe"
}
#==================================================================================================
