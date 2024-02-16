// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class EndianReaderTests
{
    public class TheSeekMethod
    {
        [Fact]
        public void ShouldReturnFalseWhenIndexIsTooHigh()
        {
            var reader = new EndianReader(new byte[] { 0 });

            var result = reader.Seek(1);
            Assert.False(result);
        }

        [Fact]
        public void ShouldChangeTheIndex()
        {
            var reader = new EndianReader(new byte[] { 0, 0, 0 });

            reader.Seek(2);
            Assert.Equal(2U, reader.Index);
        }
    }
}
