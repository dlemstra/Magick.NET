// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class EndianReaderTests
    {
        public class TheReadStringMethod : EndianReaderTests
        {
            [Fact]
            public void ShouldReturnNullWhenBufferIsNotLongEnough()
            {
                var reader = new EndianReader(new byte[1] { 0 });

                var result = reader.ReadString(2);

                Assert.Null(result);
            }

            [Fact]
            public void ShouldReadString()
            {
                var reader = new EndianReader(new byte[12] { 77, 97, 103, 105, 99, 107, 46, 78, 69, 84, 0, 77 });

                var result = reader.ReadString(10);

                Assert.Equal("Magick.NET", result);
            }

            [Fact]
            public void ShouldChangeTheIndex()
            {
                var reader = new EndianReader(new byte[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

                reader.ReadString(10);

                Assert.Equal(10U, reader.Index);
            }
        }
    }
}
