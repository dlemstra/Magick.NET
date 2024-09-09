// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class BytesTests
{
    public class TheCreateMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenStreamIsNull()
            => Assert.Throws<ArgumentNullException>("stream", () => Bytes.Create(null!));

        [Fact]
        public void ShouldThrowExceptionWhenStreamIsEmpty()
        {
            using var memStream = new MemoryStream();

            Assert.Throws<ArgumentException>("stream", () => Bytes.Create(memStream));
        }

        [Fact]
        public void ShouldSetPropertiesWhenStreamIsEmptyAndAllowEmptyStreamIsSet()
        {
            using var memStream = new MemoryStream();
            var bytes = Bytes.Create(memStream, allowEmptyStream: true);

            Assert.Equal(0, bytes.Length);
            Assert.NotNull(bytes.GetData());
            Assert.Empty(bytes.GetData());
        }

        [Fact]
        public void ShouldThrowExceptionWhenStreamCannotRead()
        {
            using var stream = TestStream.ThatCannotRead();

            Assert.Throws<ArgumentException>("stream", () => Bytes.Create(stream));
        }

        [Fact]
        public void ShouldThrowExceptionWhenStreamIsTooLong()
        {
            using var stream = TestStream.ThatCannotWrite();
            stream.SetLength(long.MaxValue);

            Assert.Throws<ArgumentException>("length", () => Bytes.Create(stream));
        }

        [Fact]
        public void ShouldSetPropertiesWhenStreamIsFileStream()
        {
            using var fileStream = File.OpenRead(Files.ImageMagickJPG);
            var bytes = Bytes.Create(fileStream);

            Assert.Equal(18749, bytes.Length);

            var data = bytes.GetData();
            Assert.NotNull(data);
            Assert.Equal(18749, data.Length);
        }

        [Fact]
        public void ShouldSetPropertiesWhenStreamIsNonSeekable()
        {
            using var fileStream = new NonSeekableStream(Files.ImageMagickJPG);
            var bytes = Bytes.Create(fileStream);

            Assert.Equal(18749, bytes.Length);
            Assert.NotNull(bytes.GetData());
        }
    }
}
