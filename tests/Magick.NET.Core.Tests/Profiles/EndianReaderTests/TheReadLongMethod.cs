// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class EndianReaderTests
    {
        public class TheReadLongMethod : EndianReaderTests
        {
            [Fact]
            public void ShouldReturnNullWhenBufferIsNotLongEnough()
            {
                var reader = new EndianReader(new byte[1] { 0 });

                var result = reader.ReadLong();

                Assert.Null(result);
            }

            [Fact]
            public void ShouldReadLongBigEndian()
            {
                var reader = new EndianReader(new byte[4] { 4, 197, 149, 223 });

                var result = reader.ReadLong();

                Assert.Equal(80057823U, result);
            }

            [Fact]
            public void ShouldReadLongLittleEndian()
            {
                var reader = new EndianReader(new byte[4] { 223, 149, 197, 4 });
                reader.IsLittleEndian = true;

                var result = reader.ReadLong();

                Assert.Equal(80057823U, result);
            }

            [Fact]
            public void ShouldChangeTheIndex()
            {
                var reader = new EndianReader(new byte[4] { 0, 0, 0, 0 });

                reader.ReadLong();

                Assert.Equal(4U, reader.Index);
            }
        }
    }
}
