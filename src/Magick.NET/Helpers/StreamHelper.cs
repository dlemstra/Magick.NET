// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;

namespace ImageMagick
{
    internal static class StreamHelper
    {
        public static void Copy(Stream source, Stream destination)
        {
#if NET20
            var buffer = new byte[81920];
            int len;

            while ((len = source.Read(buffer, 0, buffer.Length)) > 0)
                destination.Write(buffer, 0, len);
#else
            source.CopyTo(destination);
#endif
        }
    }
}
