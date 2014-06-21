'==================================================================================================
'  Copyright 2013-2014 Dirk Lemstra <https://magick.codeplex.com/>
' 
'  Licensed under the ImageMagick License (the "License"); you may not use this file except in 
'  compliance with the License. You may obtain a copy of the License at
' 
'    http://www.imagemagick.org/script/license.php
' 
'  Unless required by applicable law or agreed to in writing, software distributed under the
'  License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
'  express or implied. See the License for the specific language governing permissions and
'  limitations under the License.
'==================================================================================================

Namespace $rootnamespace$.Samples.MagickNET

	Public NotInheritable Class SampleFiles

		Private Const _FilesDirectory As String = "$fullpath$Samples\Magick.NET\Files\"
		Private Const _ScriptsDirectory As String = "$fullpath$Samples\Magick.NET\Scripts\"

		Public Shared ReadOnly Property CorruptImageJpg() As String
			Get
				Return _FilesDirectory & "CorruptImage.jpg"
			End Get
		End Property

		Public Shared ReadOnly Property CloneMsl() As String
			Get
				Return _ScriptsDirectory & "Clone.msl"
			End Get
		End Property

		Public Shared ReadOnly Property CropMsl() As String
			Get
				Return _ScriptsDirectory & "Crop.msl"
			End Get
		End Property

		Public Shared ReadOnly Property FileWithWarningJpg() As String
			Get
				Return _FilesDirectory & "FileWithWarning.jpg"
			End Get
		End Property

		Public Shared ReadOnly Property FujiFilmFinePixS1ProJpg() As String
			Get
				Return _FilesDirectory & "FujiFilmFinePixS1Pro.jpg"
			End Get
		End Property

		Public Shared ReadOnly Property InvalidFileJpg() As String
			Get
				Return _FilesDirectory & "InvalidFile.jpg"
			End Get
		End Property

		Public Shared ReadOnly Property OutputDirectory() As String
			Get
				Return "$fullpath$Samples\Magick.NET\Output\"
			End Get
		End Property

		Public Shared ReadOnly Property ResizeMsl() As String
			Get
				Return _ScriptsDirectory & "Resize.msl"
			End Get
		End Property

		Public Shared ReadOnly Property SnakewareEps() As String
			Get
				Return _FilesDirectory & "Snakeware.eps"
			End Get
		End Property

		Public Shared ReadOnly Property SnakewareGif() As String
			Get
				Return _FilesDirectory & "Snakeware.gif"
			End Get
		End Property

		Public Shared ReadOnly Property SnakewareJpg() As String
			Get
				Return _FilesDirectory & "Snakeware.jpg"
			End Get
		End Property

		Public Shared ReadOnly Property SnakewarePdf() As String
			Get
				Return _FilesDirectory & "Snakeware.pdf"
			End Get
		End Property

		Public Shared ReadOnly Property SnakewarePng() As String
			Get
				Return _FilesDirectory & "Snakeware.png"
			End Get
		End Property

		Public Shared ReadOnly Property StillLifeCR2() As String
			Get
				Return _FilesDirectory & "StillLife.cr2"
			End Get
		End Property

		Public Shared ReadOnly Property WaveMsl() As String
			Get
				Return _ScriptsDirectory & "Wave.msl"
			End Get
		End Property

		Public Shared ReadOnly Property YourProfileIcc() As String
			Get
				Return _FilesDirectory & "YourProfile.icc"
			End Get
		End Property

	End Class

End Namespace
