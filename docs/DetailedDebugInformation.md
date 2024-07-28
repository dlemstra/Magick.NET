# Detailed debug information

## Get detailed debug information from ImageMagick

```C#
public static void MagickNET_Log(object? sender, LogEventArgs arguments)
{
    // Write log message
    WriteLogMessage(arguments.Message);
}

public static void ReadImage()
{
    // Log all events
    MagickNET.SetLogEvents(LogEventTypes.All);

    // Set the log handler (all threads use the same handler)
    MagickNET.Log += MagickNET_Log;

    using var image = new MagickImage();

    // Trace logging checks if this is set to true.
    image.Settings.Debug = true;

    // Reading the image will send all log events to the log handler
    image.Read(SampleFiles.SnakewarePng);
}
```
