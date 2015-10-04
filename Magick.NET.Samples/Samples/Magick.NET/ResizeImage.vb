'==================================================================================================
'  Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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

Imports System
Imports System.IO
Imports ImageMagick

Namespace RootNamespace.Samples.MagickNET

  Public NotInheritable Class ResizeImageSamples

    Public Shared Sub ResizeAnimatedGif()
      ' Read from file
      Using collection As New MagickImageCollection(SampleFiles.SnakewareGif)

        ' This will remove the optimization and change the image to how it looks at that point
        ' during the animation. More info here: http://www.imagemagick.org/Usage/anim_basics/#coalesce
        collection.Coalesce()

        ' Resize each image in the collection to a width of 200. When zero is specified for the height
        ' the height will be calculated with the aspect ratio.
        For Each image As MagickImage In collection
          image.Resize(200, 0)
        Next

        ' Save the result
        collection.Write(SampleFiles.OutputDirectory + "Snakeware.resized.gif")
      End Using
    End Sub

    Public Shared Sub ResizeToFixedSize()
      ' Read from file
      Using image As New MagickImage(SampleFiles.SnakewarePng)

        Dim size = New MagickGeometry(100, 100)
        ' This will resize the image to a fixed size without maintaining the aspect ratio.
        ' Normally an image will be resized to fit inside the specified size.
        size.IgnoreAspectRatio = True

        image.Resize(size)

        ' Save the result
        image.Write(SampleFiles.OutputDirectory + "Snakeware.100x100.png")
      End Using
    End Sub

  End Class

End Namespace