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

Imports System.IO
Imports ImageMagick

Namespace RootNamespace.Samples.MagickNET

    Public NotInheritable Class ConvertImageSamples

        Public Shared Sub ConvertImageFromOneFormatToAnother()
            ' Read first frame of gif image
            Using image As New MagickImage(SampleFiles.SnakewareGif)
                ' Save frame as jpg
                image.Write(SampleFiles.OutputDirectory + "Snakeware.jpg")
            End Using

            Dim settings As New MagickReadSettings()
            ' Tells the xc: reader the image to create should be 800x600
            settings.Width = 800
            settings.Height = 600

            Using memStream As New MemoryStream()
                ' Create image that is completely purple and 800x600
                Using image As New MagickImage("xc:purple", settings)
                    ' Sets the output format to png
                    image.Format = MagickFormat.Png
                    ' Write the image to the memorystream
                    image.Write(memStream)
                End Using
            End Using

            ' Read image from file
            Using image As New MagickImage(SampleFiles.SnakewarePng)
                ' Sets the output format to jpeg
                image.Format = MagickFormat.Jpeg
                ' Create byte array that contains a jpeg file
                Dim data As Byte() = image.ToByteArray()
            End Using
        End Sub

        Public Shared Sub ConvertCmykToRgb()
            ' Uses sRGB.icm, eps/pdf produce better result when you set this before loading.
            Dim settings As New MagickReadSettings()
            settings.ColorSpace = ColorSpace.sRGB

            ' Create empty image
            Using image As New MagickImage()
                ' Reads the eps image, the specified settings tell Ghostscript to create an sRGB image
                image.Read(SampleFiles.SnakewareEps, settings)
                ' Save image as tiff
                image.Write(SampleFiles.OutputDirectory + "Snakeware.tiff")
            End Using

            ' Read image from file
            Using image As New MagickImage(SampleFiles.SnakewareJpg)
                ' First add a CMYK profile if your image does not contain a color profile.
                image.AddProfile(ColorProfile.USWebCoatedSWOP)

                ' Adding the second profile will transform the colorspace from CMYK to RGB
                image.AddProfile(ColorProfile.SRGB)
                ' Save image as png
                image.Write(SampleFiles.OutputDirectory + "Snakeware.png")
            End Using

            ' Read image from file
            Using image As New MagickImage(SampleFiles.SnakewareJpg)
                ' First add a CMYK profile if your image does not contain a color profile.
                image.AddProfile(ColorProfile.USWebCoatedSWOP)

                ' Adding the second profile will transform the colorspace from your custom icc profile
                image.AddProfile(New ColorProfile(SampleFiles.YourProfileIcc))
                ' Save image as tiff
                image.Write(SampleFiles.OutputDirectory + "Snakeware.tiff")
            End Using
        End Sub

    End Class

End Namespace