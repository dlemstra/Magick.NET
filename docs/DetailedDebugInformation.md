# Detailed debug information

## Get detailed debug information from ImageMagick

#### C#
```C#
public void MagickNET_Log(object sender, LogEventArgs arguments)
{
    // Write log message
    WriteLogMessage(arguments.Message);
}

public void ReadImage()
{
    // Log all events
    MagickNET.SetLogEvents(LogEvents.All);
    // Set the log handler (all threads use the same handler)
    MagickNET.Log += MagickNET_Log;

    using (MagickImage image = new MagickImage())
    {
        // Reading the image will send all log events to the log handler
        image.Read("Snakeware.png");
    }
}
```

#### VB.NET
```VB.NET
Public Sub MagickNET_Log(sender As Object, arguments As LogEventArgs)
    ' Write log message
    WriteLogMessage(arguments.Message)
End Sub

Public Sub ReadImage()
    ' Log all events
    MagickNET.SetLogEvents(LogEvents.All)
    ' Set the log handler (all threads use the same handler)
    AddHandler MagickNET.Log, AddressOf MagickNET_Log

    Using image As New MagickImage()
      ' Reading the image will send all log events to the log handler
      image.Read("Snakeware.png")
    End Using
End Sub
```