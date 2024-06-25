// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;

namespace Magick.NET.Samples;

public static class DetailedDebugInformationSamples
{
    public static void MagickNET_Log(object sender, LogEventArgs arguments)
    {
        // Write log message
        if (arguments.Message is not null)
        {
            Console.WriteLine(arguments.Message);
        }
    }

    public static void ReadImage()
    {
        // Log all events
        MagickNET.SetLogEvents(LogEvents.All);

        // Set the log handler (all threads use the same handler)
        MagickNET.Log += MagickNET_Log;

        using var image = new MagickImage();

        // Trace logging checks if this is set to true.
        image.Settings.Debug = true;

        // Reading the image will send all log events to the log handler
        image.Read(SampleFiles.SnakewarePng);
    }
}
