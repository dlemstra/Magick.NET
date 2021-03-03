// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class EndianReaderTests
    {
        public class TheReadDoubleMethod : EndianReaderTests
        {
            [Fact]
            public void ShouldReturnNullWhenBufferIsNotLongEnough()
            {
                var reader = new EndianReader(new byte[1] { 0 });
                reader.IsLittleEndian = true;

                var result = reader.ReadDouble();

                Assert.Null(result);
            }

            [Fact]
            public void ShouldReadDoubleBigEndian()
            {
                var reader = new EndianReader(new byte[8] { 64, 181, 58, 229, 96, 65, 137, 55 });

                var result = reader.ReadDouble();

                Assert.Equal(5434.896, result);
            }

            [Fact]
            public void ShouldReadDoubleLittleEndian()
            {
                var reader = new EndianReader(new byte[8] { 55, 137, 65, 96, 229, 58, 181, 64 });
                reader.IsLittleEndian = true;

                var result = reader.ReadDouble();

                Assert.Equal(5434.896, result);
            }

            [Fact]
            public void ShouldChangeTheIndex()
            {
                var reader = new EndianReader(new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 });
                reader.IsLittleEndian = true;

                reader.ReadDouble();

                Assert.Equal(8U, reader.Index);
            }
        }
    }
}
