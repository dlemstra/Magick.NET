// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using Xunit;

namespace Magick.NET.Tests
{
    public partial class IcoOptimizerTests
    {
        public class TheOptimalCompressionProperty : IcoOptimizerTests
        {
            [Fact]
            public void ShouldReturnFalse()
            {
                Assert.False(Optimizer.OptimalCompression);
            }
        }
    }
}
