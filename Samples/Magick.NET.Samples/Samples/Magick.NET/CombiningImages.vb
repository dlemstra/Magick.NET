' Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

    Public NotInheritable Class CombiningImagesSamples

        Public Shared Sub MergeMultipleImages()
            Using images As New MagickImageCollection()
                ' Add the first image
                Dim first As New MagickImage(SampleFiles.SnakewarePng)
                images.Add(first)

                ' Add the second image
                Dim second As New MagickImage(SampleFiles.SnakewarePng)
                images.Add(second)

                ' Create a mosaic from both images
                Using result As IMagickImage = images.Mosaic()
                    ' Save the result
                    result.Write(SampleFiles.OutputDirectory + "Mosaic.png")
                End Using
            End Using
        End Sub

        Public Shared Sub CreateAnimatedGif()
            Using collection As New MagickImageCollection()
                ' Add first image and set the animation delay to 100ms
                collection.Add("Snakeware.png")
                collection(0).AnimationDelay = 100

                ' Add second image, set the animation delay to 100ms and flip the image
                collection.Add("Snakeware.png")
                collection(1).AnimationDelay = 100
                collection(1).Flip()

                ' Optionally reduce colors
                Dim settings As New QuantizeSettings()
                settings.Colors = 256
                collection.Quantize(settings)

                ' Optionally optimize the images (images should have the same size).
                collection.Optimize()

                ' Save gif
                collection.Write(SampleFiles.OutputDirectory + "Snakeware.Animated.gif")
            End Using
        End Sub

    End Class

End Namespace