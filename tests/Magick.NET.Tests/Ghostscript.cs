// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;

namespace Magick.NET.Tests;

public static class Ghostscript
{
    public static bool IsAvailable { get; }
#if DEBUG_TEST
        = GetGhostscriptDirectory() is not null;
#else
        = Runtime.IsWindows || File.Exists("/usr/bin/gs");
#endif

    public static void Initialize()
    {
        var directory = GetGhostscriptDirectory();
        if (directory is not null)
            MagickNET.SetGhostscriptDirectory(directory);
    }

    private static string? GetGhostscriptDirectory()
    {
        if (!Runtime.IsWindows)
            return null;

        foreach (var version in new[] { "10.01.1", "10.00.1" })
        {
            var directory = @$"C:\Program Files (x86)\gs\gs{version}\bin";
            if (Directory.Exists(directory))
                return directory;
        }

        return null;
    }
}
