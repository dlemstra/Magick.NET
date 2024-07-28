// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;

namespace Magick.NET.Samples;

public static class ExceptionHandlingSamples
{
    private static void MagickImage_Warning(object? sender, WarningEventArgs arguments)
        => Console.WriteLine(arguments.Message);

    public static void ExceptionHandling()
    {
        try
        {
            // Read invalid jpg file
            using var image = new MagickImage(SampleFiles.InvalidFileJpg);
        }
        // Catch any MagickException
        catch (MagickException exception)
        {
            // Write excepion raised when reading the invalid jpg to the console
            Console.WriteLine(exception.Message);
        }

        try
        {
            // Read corrupt jpg file
            using var image = new MagickImage(SampleFiles.CorruptImageJpg);
        }
        // Catch only MagickCorruptImageErrorException
        catch (MagickCorruptImageErrorException exception)
        {
            // Write excepion raised when reading the corrupt jpg to the console
            Console.WriteLine(exception.Message);
        }
    }

    public static void ObtainWarningThatOccurredDuringRead()
    {
        using var image = new MagickImage();

        // Attach event handler to warning event
        image.Warning += MagickImage_Warning;

        // Read file that will raise a warning.
        image.Read(SampleFiles.FileWithWarningJpg);
    }
}
