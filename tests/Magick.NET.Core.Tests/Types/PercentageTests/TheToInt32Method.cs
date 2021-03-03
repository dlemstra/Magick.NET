// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class PercentageTests
    {
        public class TheToInt32Method
        {
            [Fact]
            public void ShouldReturnZeroForZeroPointFour()
            {
                var percentage = new Percentage(0.4);
                Assert.Equal(0, percentage.ToInt32());
            }

            [Fact]
            public void ShouldReturnOneForZeroPointFive()
            {
                var percentage = new Percentage(0.5);
                Assert.Equal(1, percentage.ToInt32());
            }
        }
    }
}
