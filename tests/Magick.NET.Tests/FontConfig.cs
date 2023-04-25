// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;

namespace Magick.NET.Tests
{
    public static class FontConfig
    {
        public static void Initialize()
        {
            if (Runtime.IsMacOS)
                MagickNET.SetFontConfigDirectory("/opt/X11/lib/X11/fontconfig");
        }
    }
}
