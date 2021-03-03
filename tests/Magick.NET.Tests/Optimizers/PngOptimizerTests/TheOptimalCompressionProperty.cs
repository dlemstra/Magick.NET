// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using Xunit;

namespace Magick.NET.Tests
{
    public partial class PngOptimizerTests
    {
        public class TheOptimalCompressionProperty : PngOptimizerTests
        {
            [Fact]
            public void ShouldReturnFalse()
            {
                Assert.False(Optimizer.OptimalCompression);
            }
        }
    }
}
