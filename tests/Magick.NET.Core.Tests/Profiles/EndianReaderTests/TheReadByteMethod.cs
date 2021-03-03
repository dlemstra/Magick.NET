// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class EndianReaderTests
    {
        public class TheReadByteMethod : EndianReaderTests
        {
            [Fact]
            public void ShouldReturnNullWhenBufferIsNotLongEnough()
            {
                var reader = new EndianReader(new byte[1] { 0 });

                var result = reader.ReadByte();
                result = reader.ReadByte();

                Assert.Null(result);
            }

            [Fact]
            public void ShouldReadByte()
            {
                var reader = new EndianReader(new byte[1] { 42 });

                var result = reader.ReadByte();

                Assert.Equal((byte)42, result);
            }

            [Fact]
            public void ShouldChangeTheIndex()
            {
                var reader = new EndianReader(new byte[1] { 0 });

                reader.ReadByte();

                Assert.Equal(1U, reader.Index);
            }
        }
    }
}
