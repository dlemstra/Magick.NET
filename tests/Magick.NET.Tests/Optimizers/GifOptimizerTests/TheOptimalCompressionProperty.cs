// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using Xunit;

namespace Magick.NET.Tests
{
    public partial class GifOptimizerTests
    {
        public class TheOptimalCompressionProperty : GifOptimizerTests
        {
            [Fact]
            public void ShouldReturnFalse()
            {
                Assert.False(Optimizer.OptimalCompression);
            }
        }
    }
}
