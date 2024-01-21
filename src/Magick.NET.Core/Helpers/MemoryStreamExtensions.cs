// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;

namespace ImageMagick
{
    internal static class MemoryStreamExtensions
    {
        public static void WriteBytes(this MemoryStream stream, byte[] bytes)
            => stream.Write(bytes, 0, bytes.Length);
    }
}
