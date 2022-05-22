// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using System.Linq;
using ImageMagick;
using Xunit;

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
    public partial class MagickImageCollectionTests
    {
        [Fact]
        public void Test_Write()
        {
            var fileSize = new FileInfo(Files.RoseSparkleGIF).Length;
            Assert.Equal(9891, fileSize);

            using (var images = new MagickImageCollection(Files.RoseSparkleGIF))
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    images.Write(memStream);

                    Assert.Equal(fileSize, memStream.Length);
                }
            }

            var tempFile = new FileInfo(Path.GetTempFileName() + ".gif");
            try
            {
                using (var images = new MagickImageCollection(Files.RoseSparkleGIF))
                {
                    images.Write(tempFile);

                    Assert.Equal(fileSize, tempFile.Length);
                }
            }
            finally
            {
                Cleanup.DeleteFile(tempFile);
            }
        }
    }
}
