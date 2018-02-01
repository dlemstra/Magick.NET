' Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

    Public NotInheritable Class DetailedDebugInformationSamples

        Private Shared Sub WriteLogMessage(message As String)
            Console.WriteLine(message)
        End Sub

        Public Shared Sub MagickNET_Log(sender As Object, arguments As LogEventArgs)
            ' Write log message
            WriteLogMessage(arguments.Message)
        End Sub

        Public Shared Sub ReadImage()
            ' Log all events
            ImageMagick.MagickNET.SetLogEvents(LogEvents.All)
            ' Set the log handler (all threads use the same handler)
            AddHandler ImageMagick.MagickNET.Log, AddressOf MagickNET_Log

            Using image As New MagickImage()
                ' Reading the image will send all log events to the log handler
                image.Read(SampleFiles.SnakewarePng)
            End Using
        End Sub

    End Class

End Namespace