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

    Public NotInheritable Class CommandLineOptionDefineSamples

        Public Shared Sub CommandLineOptionDefine()
            ' Read image from file
            Using image As New MagickImage(SampleFiles.SnakewarePng)
                ' Tells the dds coder to use dxt1 compression when writing the image
                image.Settings.SetDefine(MagickFormat.Dds, "compression", "dxt1")
                ' Save image as dds file
                image.Write(SampleFiles.OutputDirectory + "Snakeware.dds")
            End Using
        End Sub

        Public Shared Sub DefinesThatNeedToBeSetBeforeReadingAnImage()
            Dim settings As New MagickReadSettings()
            ' Set define that tells the jpeg coder that the output image will be 32x32
            settings.SetDefine(MagickFormat.Jpeg, "size", "32x32")

            ' Read image from file
            Using image As New MagickImage(SampleFiles.SnakewareJpg)
                ' Create thumnail that is 32 pixels wide and 32 pixels high
                image.Thumbnail(32, 32)
                ' Save image as tiff
                image.Write(SampleFiles.OutputDirectory + "Snakeware.tiff")
            End Using
        End Sub

    End Class

End Namespace