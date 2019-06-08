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

    ''' <summary>
    ''' You need to install the latest version of GhostScript before you can convert a pdf using
    ''' Magick.NET. Make sure you only install the version of GhostScript with the same platform. If
    ''' you use the 64-bit version of Magick.NET you should also install the 64-bit version of 
    ''' Ghostscript. You can use the 32-bit version together with the 64-version but you will get a
    ''' better performance if you keep the platforms the same.
    ''' </summary>
    Public NotInheritable Class ConvertPDFSamples

        Public Shared Sub ConvertPDFToMultipleImages()
            Dim settings As New MagickReadSettings()
            ' Settings the density to 300 dpi will create an image with a better quality
            settings.Density = New Density(300, 300)

            Using images As New MagickImageCollection()
                ' Add all the pages of the pdf file to the collection
                images.Read(SampleFiles.SnakewarePdf, settings)

                Dim page As Integer = 1
                For Each image As MagickImage In images
                    ' Write page to file that contains the page number
                    image.Write(SampleFiles.OutputDirectory + "Snakeware.Page" & page & ".png")
                    ' Writing to a specific format works the same as for a single image
                    image.Format = MagickFormat.Ptif
                    image.Write(SampleFiles.OutputDirectory + "Snakeware.Page" & page & ".tif")
                    page += 1
                Next
            End Using
        End Sub

        Public Shared Sub ConvertPDFTOneImage()
            Dim settings As New MagickReadSettings()
            ' Settings the density to 300 dpi will create an image with a better quality
            settings.Density = New Density(300)

            Using images As New MagickImageCollection()
                ' Add all the pages of the pdf file to the collection
                images.Read(SampleFiles.SnakewarePdf, settings)

                ' Create new image that appends all the pages horizontally
                Using horizontal As IMagickImage = images.AppendHorizontally()
                    ' Save result as a png
                    horizontal.Write(SampleFiles.OutputDirectory + "Snakeware.horizontal.png")
                End Using

                ' Create new image that appends all the pages vertically
                Using vertical As IMagickImage = images.AppendVertically()
                    ' Save result as a png
                    vertical.Write(SampleFiles.OutputDirectory + "Snakeware.vertical.png")
                End Using
            End Using
        End Sub

        Public Shared Sub CreatePDFFromTwoImages()
            Using collection As New MagickImageCollection()
                ' Add first page
                collection.Add(New MagickImage(SampleFiles.SnakewareJpg))
                ' Add second page
                collection.Add(New MagickImage(SampleFiles.SnakewareJpg))

                ' Create pdf file with two pages
                collection.Write(SampleFiles.OutputDirectory + "Snakeware.pdf")
            End Using
        End Sub

        Public Shared Sub CreatePDFFromSingleImage()
            ' Read image from file
            Using image As New MagickImage(SampleFiles.SnakewareJpg)
                ' Create pdf file with a single page
                image.Write(SampleFiles.OutputDirectory + "Snakeware.pdf")
            End Using
        End Sub

        Public Shared Sub ReadSinglePageFromPDF()
            Using collection As New MagickImageCollection()
                Dim settings As New MagickReadSettings()
                settings.FrameIndex = 0 ' First page
                settings.FrameCount = 1 ' Number of pages
                ' Read only the first page of the pdf file
                collection.Read(SampleFiles.OutputDirectory + "Snakeware.pdf", settings)

                ' Clear the collection
                collection.Clear()

                settings.FrameCount = 2 ' Number of pages

                ' Read the first two pages of the pdf file
                collection.Read(SampleFiles.OutputDirectory + "Snakeware.pdf", settings)
            End Using
        End Sub

    End Class

End Namespace
