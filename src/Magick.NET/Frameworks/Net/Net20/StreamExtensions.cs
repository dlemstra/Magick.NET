// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NET20
using System.IO;

namespace ImageMagick
{
    internal static class StreamExtensions
    {
        public static void CopyTo(this Stream self, Stream output)
        {
            var buffer = new byte[81920];
            int len;

            while ((len = self.Read(buffer, 0, buffer.Length)) > 0)
                output.Write(buffer, 0, len);
        }
    }
}
#endif
