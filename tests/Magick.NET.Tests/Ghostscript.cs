// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;

namespace Magick.NET.Tests
{
    public static class Ghostscript
    {
        public static bool IsAvailable
        {
            get
            {
                if (Runtime.IsWindows)
                    return true;

                return File.Exists("/usr/bin/gs");
            }
        }

        public static void Initialize()
        {
            if (!Runtime.IsWindows)
                return;

            foreach (var version in new[] { "10.01.1", "10.00.1" })
            {
                var directory = @$"C:\Program Files (x86)\gs\gs${version}\bin";
                if (Directory.Exists(directory))
                    MagickNET.SetGhostscriptDirectory(directory);
            }
        }
    }
}
