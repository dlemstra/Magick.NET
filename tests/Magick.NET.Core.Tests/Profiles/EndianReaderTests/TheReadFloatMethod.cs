// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class EndianReaderTests
    {
        public class TheReadFloatMethod : EndianReaderTests
        {
            [Fact]
            public void ShouldReturnNullWhenBufferIsNotLongEnough()
            {
                var reader = new EndianReader(new byte[1] { 0 });

                var result = reader.ReadFloat();

                Assert.Null(result);
            }

            [Fact]
            public void ShouldReadSingleBigEndian()
            {
                var reader = new EndianReader(new byte[4] { 69, 169, 215, 43 });

                var result = reader.ReadFloat();

                Assert.Equal(5434.896f, result);
            }

            [Fact]
            public void ShouldReadSingleLittleEndian()
            {
                var reader = new EndianReader(new byte[4] { 43, 215, 169, 69 });
                reader.IsLittleEndian = true;

                var result = reader.ReadFloat();

                Assert.Equal(5434.896f, result);
            }

            [Fact]
            public void ShouldChangeTheIndex()
            {
                var reader = new EndianReader(new byte[4] { 0, 0, 0, 0 });

                reader.ReadFloat();

                Assert.Equal(4U, reader.Index);
            }
        }
    }
}
