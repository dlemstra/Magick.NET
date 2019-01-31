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

Imports System
Imports ImageMagick

Namespace RootNamespace.Samples.MagickNET

    Public NotInheritable Class ExifDataSamples

        Public Shared Sub ReadExifData()
            ' Read image from file
            Using image As New MagickImage(SampleFiles.FujiFilmFinePixS1ProJpg)
                ' Retrieve the exif information
                Dim profile As ExifProfile = image.GetExifProfile()

                ' Check if image contains an exif profile
                If profile Is Nothing Then
                    Console.WriteLine("Image does not contain exif information.")
                Else
                    ' Write all values to the console
                    For Each value As ExifValue In profile.Values
                        Console.WriteLine("{0}({1}): {2}", value.Tag, value.DataType, value.ToString())
                    Next
                End If
            End Using
        End Sub

        Public Shared Sub CreateThumbnailFromExifData()
            ' Read image from file
            Using image As New MagickImage(SampleFiles.FujiFilmFinePixS1ProJpg)
                ' Retrieve the exif information
                Dim profile As ExifProfile = image.GetExifProfile()

                ' Create thumbnail from exif information
                Using thumbnail As MagickImage = profile.CreateThumbnail()
                    ' Check if exif profile contains thumbnail and save it
                    If thumbnail IsNot Nothing Then
                        thumbnail.Write(SampleFiles.OutputDirectory + "FujiFilmFinePixS1Pro.thumb.jpg")
                    End If
                End Using
            End Using
        End Sub

    End Class

End Namespace