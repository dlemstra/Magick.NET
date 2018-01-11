﻿' Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
Imports System.IO
Imports ImageMagick

Namespace RootNamespace.Samples.MagickNET

    Public NotInheritable Class ReadImageSamples

        Private Shared Function LoadMemoryStreamImage() As MemoryStream
            Return New MemoryStream(LoadImageBytes())
        End Function

        Private Shared Function LoadImageBytes() As Byte()
            Return File.ReadAllBytes(SampleFiles.SnakewarePng)
        End Function

        Public Shared Sub ReadImage()
            ' Read from file.
            Using image As New MagickImage(SampleFiles.SnakewareJpg)
            End Using

            ' Read from stream.
            Using memStream As MemoryStream = LoadMemoryStreamImage()
                Using image As New MagickImage(memStream)
                End Using
            End Using

            ' Read from byte array.
            Dim data As Byte() = LoadImageBytes()
            Using image As New MagickImage(data)
            End Using

            ' Read image that has no predefined dimensions.
            Dim settings As New MagickReadSettings()
            settings.Width = 800
            settings.Height = 600
            Using image As New MagickImage("xc:yellow", settings)
            End Using

            Using image As New MagickImage()
                image.Read(SampleFiles.SnakewareJpg)
                image.Read(data)
                image.Read("xc:yellow", settings)

                Using memStream As MemoryStream = LoadMemoryStreamImage()
                    image.Read(memStream)
                End Using
            End Using
        End Sub

        Public Shared Sub ReadBasicImageInformation()
            ' Read from file
            Dim info As New MagickImageInfo(SampleFiles.SnakewarePng)

            ' Read from stream
            Using memStream As MemoryStream = LoadMemoryStreamImage()
                info = New MagickImageInfo(memStream)
            End Using

            ' Read from byte array
            Dim data As Byte() = LoadImageBytes()
            info = New MagickImageInfo(data)

            info = New MagickImageInfo()
            info.Read(SampleFiles.SnakewarePng)
            Using memStream As MemoryStream = LoadMemoryStreamImage()
                info.Read(memStream)
            End Using
            info.Read(data)

            Console.WriteLine(info.Width)
            Console.WriteLine(info.Height)
            Console.WriteLine(info.ColorSpace)
            Console.WriteLine(info.Format)
            Console.WriteLine(info.Density.X)
            Console.WriteLine(info.Density.Y)
            Console.WriteLine(info.Density.Units)
        End Sub

        Public Shared Sub ReadImageWithMultipleFrames()
            ' Read from file
            Using collection As New MagickImageCollection(SampleFiles.SnakewareJpg)
            End Using

            ' Read from stream
            Using memStream As MemoryStream = LoadMemoryStreamImage()
                Using collection As New MagickImageCollection(memStream)
                End Using
            End Using

            ' Read from byte array
            Dim data As Byte() = LoadImageBytes()
            Using collection As New MagickImageCollection(data)
            End Using

            ' Read pdf with custom density.
            Dim settings As New MagickReadSettings()
            settings.Density = New Density(144)

            Using collection As New MagickImageCollection(SampleFiles.SnakewarePdf, settings)
            End Using

            Using collection As New MagickImageCollection()
                collection.Read(SampleFiles.SnakewareJpg)
                Using memStream As MemoryStream = LoadMemoryStreamImage()
                    collection.Read(memStream)
                End Using
                collection.Read(data)
                collection.Read(SampleFiles.SnakewarePdf, settings)
            End Using
        End Sub

    End Class

End Namespace