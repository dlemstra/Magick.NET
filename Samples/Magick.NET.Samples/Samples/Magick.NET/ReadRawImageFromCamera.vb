' Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
'
' Licensed under the ImageMagick License (the "License"); you may Not use this file except in
' compliance with the License. You may obtain a copy of the License at
'
'   https://www.imagemagick.org/script/license.php
'
' Unless required by applicable law Or agreed to in writing, software distributed under the
' License Is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES Or CONDITIONS OF ANY KIND,
' either express Or implied. See the License for the specific language governing permissions
' And limitations under the License.

Imports ImageMagick

Namespace RootNamespace.Samples.MagickNET

    ''' <summary>
    ''' You need to put the executable dcraw.exe into the directory that contains the Magick.NET dll.
    ''' The zip file ImageMagick-6.X.X-X-Q16-x86-windows.zip that you can download from
    ''' http://www.imagemagick.org/script/binary-releases.php#windows contains this file.
    ''' </summary>
    Public NotInheritable Class ReadRawImageFromCameraSamples

        Public Shared Sub ConvertCR2ToJPG()
            Using image As New MagickImage(SampleFiles.StillLifeCR2)
                image.Write(SampleFiles.OutputDirectory + "StillLife.jpg")
            End Using
        End Sub

    End Class

End Namespace