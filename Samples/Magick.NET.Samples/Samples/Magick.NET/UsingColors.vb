'==================================================================================================
'  Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
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

Imports System.Drawing
Imports ImageMagick

Namespace RootNamespace.Samples.MagickNET

  Public NotInheritable Class UsingColorsSamples

    Public Shared Sub UsingColors()
      Using image As New MagickImage(SampleFiles.SnakewarePng)
        image.TransparentChroma(Color.Black, Color.Blue)
        image.BackgroundColor = New ColorMono(True)

        ' Q16 (Blue):
        image.TransparentChroma(New MagickColor(0, 0, 0), New MagickColor(0, 0, 65535))
        image.TransparentChroma(New ColorRGB(0, 0, 0), New ColorRGB(0, 0, 65535))
        image.BackgroundColor = New MagickColor("#00f")
        image.BackgroundColor = New MagickColor("#0000ff")
        image.BackgroundColor = New MagickColor("#00000000ffff")

        ' With transparency (Red):
        image.BackgroundColor = New MagickColor(0, 0, 65535, 32767)
        image.BackgroundColor = New MagickColor("#0000ff80")

        ' Q8 (Green):
        image.TransparentChroma(New MagickColor(0, 0, 0), New MagickColor(0, 255, 0))
        image.TransparentChroma(New ColorRGB(0, 0, 0), New ColorRGB(0, 255, 0))
        image.BackgroundColor = New MagickColor("#0f0")
        image.BackgroundColor = New MagickColor("#00ff00")
      End Using
    End Sub

  End Class

End Namespace