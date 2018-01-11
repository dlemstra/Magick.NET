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
Imports System.IO
Imports ImageMagick

Namespace RootNamespace.Samples.MagickNET

    Public NotInheritable Class LosslessCompressionSamples

        Public Shared Sub MakeGooglePageSpeedInsightsHappy()
            Dim snakewareLogo As New FileInfo(SampleFiles.OutputDirectory + "OptimizeTest.jpg")
            File.Copy(SampleFiles.SnakewareJpg, snakewareLogo.FullName, True)

            Console.WriteLine("Bytes before: " + snakewareLogo.Length)

            Dim optimizer As New ImageOptimizer()
            optimizer.LosslessCompress(snakewareLogo)

            snakewareLogo.Refresh()
            Console.WriteLine("Bytes after:  " + snakewareLogo.Length)
        End Sub

    End Class

End Namespace