// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;

namespace ImageMagick
{
    internal static class StreamHelper
    {
        public static void Copy(Stream source, Stream destination)
            => source.CopyTo(destination);
    }
}
