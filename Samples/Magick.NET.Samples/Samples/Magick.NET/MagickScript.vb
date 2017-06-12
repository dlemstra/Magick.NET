'==================================================================================================
'  Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
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

Imports System.IO
Imports ImageMagick

Namespace RootNamespace.Samples.MagickNET

    Public NotInheritable Class MagickScriptSamples

        Private Shared Sub OnScriptRead(sender As Object, arguments As ScriptReadEventArgs)
            arguments.Image = New MagickImage(SampleFiles.SnakewareJpg)
        End Sub

        Private Shared Sub OnScriptWrite(sender As Object, arguments As ScriptWriteEventArgs)
            arguments.Image.Write(SampleFiles.SnakewarePng)
        End Sub

        Public Shared Sub Resize()
            ' Load resize script and execute it
            Dim script As New MagickScript(SampleFiles.ResizeMsl)
            script.Execute()
        End Sub

        Public Shared Sub ReuseSameScript()
            ' Load wave script
            Dim script As New MagickScript(SampleFiles.WaveMsl)

            ' Execute script multiple times
            Dim files As String() = New String() {SampleFiles.FujiFilmFinePixS1ProJpg, SampleFiles.SnakewareJpg}
            For Each fileName As String In files
                ' Read image from file
                Using image As New MagickImage(fileName)
                    ' Execute script with the image and write it to a jpg file
                    script.Execute(image)
                    image.Write(SampleFiles.OutputDirectory + fileName & ".wave.jpg")
                End Using
            Next
        End Sub

        Public Shared Sub ReadWriteEvents()
            ' Load crop script
            Dim script As New MagickScript(SampleFiles.CropMsl)
            ' Event that will be raised when the script wants to read a file
            AddHandler script.Read, AddressOf OnScriptRead
            ' Event that will be raised when the script wants to write a file
            AddHandler script.Write, AddressOf OnScriptWrite
            ' Execute the script
            script.Execute()
        End Sub

        Public Shared Sub WriteMultipleOutputFiles()
            ' Load clone script and execute it
            Dim script As New MagickScript(SampleFiles.CloneMsl)
            script.Execute()
        End Sub

    End Class

End Namespace