function GenerateFiles()
{
	.\Magick.NET.FileGenerator\bin\Release\Magick.NET.FileGenerator.exe
}

function Build()
{
	$location = $(get-location)
	set-location "$location\.."

	msbuild /m Magick.NET.sln /t:Rebuild ("/p:Configuration=ReleaseQ8,RunCodeAnalysis=false,Platform=Win32")
	msbuild /m Magick.NET.sln /t:Rebuild ("/p:Configuration=ReleaseQ16,RunCodeAnalysis=false,Platform=Win32")

	set-location $location
}

Build
GenerateFiles
Build