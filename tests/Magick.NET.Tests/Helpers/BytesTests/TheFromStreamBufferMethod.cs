// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class BytesTests
{
    public class TheFromStreamMethod
    {
        [Fact]
        public void ShouldReturnNullWhenStreamIsFileStream()
        {
            using var fileStream = File.OpenRead(Files.ImageMagickJPG);
            var bytes = Bytes.FromStreamBuffer(fileStream);

            Assert.Null(bytes);
        }

        [Fact]
        public void ShouldReturnObjectWhenStreamIsMemoryStream()
        {
            using var memStream = new MemoryStream();
            var bytes = Bytes.FromStreamBuffer(memStream);

            Assert.NotNull(bytes);
        }
    }
}
