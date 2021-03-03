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
                if (OperatingSystem.IsWindows)
                    return true;

                return File.Exists("/usr/bin/gs");
            }
        }

        public static void Initialize()
        {
            if (OperatingSystem.IsWindows)
                MagickNET.SetGhostscriptDirectory(@"C:\Program Files (x86)\gs\gs9.53.1\bin");
        }
    }
}
