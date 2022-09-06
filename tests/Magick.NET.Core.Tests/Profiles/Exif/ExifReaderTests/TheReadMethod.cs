// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class ExifReaderTests
    {
        public class TheReadMethod
        {
            [Fact]
            public void ShouldReturnEmptyCollectionWhenDataIsEmpty()
            {
                var data = System.Array.Empty<byte>();

                var result = ExifReader.Read(data);

                Assert.Empty(result.Values);
            }

            [Fact]
            public void ShouldReturnEmptyCollectionWhenDataHasNoValues()
            {
                var data = new byte[] { 69, 120, 105, 102, 0, 0 };

                var result = ExifReader.Read(data);

                Assert.Empty(result.Values);
            }

            [Fact]
            public void ShouldCheckArraySize()
            {
                var data = new byte[] { 69, 120, 105, 102, 0, 0, 73, 73, 42, 0, 8, 0, 0, 0, 1, 0, 148, 1, 1, 0, 255, 255, 255, 255, 26, 0, 0, 0, 31, 0, 0, 0, 42 };

                var result = ExifReader.Read(data);

                Assert.Empty(result.Values);
                Assert.Single(result.InvalidTags);
            }

            [Fact]
            public void ShouldCheckTypeOfJPEGInterchangeFormat()
            {
                var data = new byte[] { 69, 120, 105, 102, 0, 0, 73, 73, 42, 0, 8, 0, 0, 0, 1, 0, 1, 2, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 0, 0, 0, 0, 0 };

                var result = ExifReader.Read(data);

                Assert.Empty(result.Values);
                Assert.Equal(2, result.InvalidTags.Count);
            }

            [Fact]
            public void ShouldCheckTypeOfJPEGInterchangeFormatLength()
            {
                var data = new byte[] { 69, 120, 105, 102, 0, 0, 73, 73, 42, 0, 8, 0, 0, 0, 1, 0, 2, 2, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 0, 0, 0, 0, 0 };

                var result = ExifReader.Read(data);

                Assert.Empty(result.Values);
                Assert.Equal(2, result.InvalidTags.Count);
            }

            [Fact]
            public void ShouldBeAbleToReadEmptyStrings()
            {
                var data = new byte[] { 69, 120, 105, 102, 0, 0, 73, 73, 42, 0, 8, 0, 0, 0, 1, 0, 14, 1, 2, 0, 0, 0, 0, 0, 32, 0, 0, 0, 26, 0, 0, 0, 0, 0 };

                var result = ExifReader.Read(data);

                Assert.Single(result.Values);
                Assert.Equal(string.Empty, result.Values.First().GetValue());
            }

            [Fact]
            public void ShouldNotReadThumbnailWhenOffSetIsZero()
            {
                var data = new byte[] { 0x45, 0x78, 0x69, 0x66, 0x00, 0x00, 0x49, 0x49, 0x2a, 0x00, 0x08, 0x00, 0x00, 0x00, 0x01, 0x00, 0x0e, 0x01, 0x02, 0x00, 0x04, 0x00, 0x00, 0x00, 0x41, 0x42, 0x43, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

                var result = ExifReader.Read(data);

                Assert.Empty(result.InvalidTags);
            }
        }
    }
}
