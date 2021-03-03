// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace Magick.NET.Tests
{
    public partial class TiffWriteDefinesTests
    {
        private static IMagickImage<QuantumType> WriteTiff(IMagickImage<QuantumType> image)
        {
            using (var memStream = new MemoryStream())
            {
                image.Format = MagickFormat.Tiff;
                image.Write(memStream);
                memStream.Position = 0;
                return new MagickImage(memStream);
            }
        }
    }
}
