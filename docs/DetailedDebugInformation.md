# Detailed debug information

## Get detailed debug information from ImageMagick

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