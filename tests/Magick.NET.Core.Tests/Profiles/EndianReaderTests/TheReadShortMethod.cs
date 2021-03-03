// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class EndianReaderTests
    {
        public class TheReadShortMethod : EndianReaderTests
        {
            [Fact]
            public void ShouldReturnNullWhenBufferIsNotLongEnough()
            {
                var reader = new EndianReader(new byte[1] { 0 });

                var result = reader.ReadShort();

                Assert.Null(result);
            }

            [Fact]
            public void ShouldReadShortBigEndian()
            {
                var reader = new EndianReader(new byte[2] { 178, 164 });

                var result = reader.ReadShort();

                Assert.Equal((ushort)45732, result);
            }

            [Fact]
            public void ShouldReadShortLittleEndian()
            {
                var reader = new EndianReader(new byte[2] { 164, 178 });
                reader.IsLittleEndian = true;

                var result = reader.ReadShort();

                Assert.Equal((ushort)45732, result);
            }

            [Fact]
            public void ShouldChangeTheIndex()
            {
                EndianReader reader = new EndianReader(new byte[2] { 0, 0 });

                reader.ReadShort();

                Assert.Equal(2U, reader.Index);
            }
        }
    }
}
