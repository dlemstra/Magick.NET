// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class EndianReaderTests
{
    public class TheConstructor : EndianReaderTests
    {
        [Fact]
        public void ShouldThrowExceptionWhenArrayIsNull()
        {
            Assert.Throws<ArgumentNullException>("data", () =>
            {
                new EndianReader(null!);
            });
        }

        [Fact]
        public void ShouldThrowExceptionWhenArrayIsEmpty()
        {
            Assert.Throws<ArgumentException>("data", () =>
            {
                new EndianReader([]);
            });
        }
    }
}
