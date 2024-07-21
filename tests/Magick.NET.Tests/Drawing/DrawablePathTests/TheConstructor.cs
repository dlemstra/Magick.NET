// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class DrawablePathTests
{
    public class TheConstructor
    {
        [Fact]
        public void ShouldSetPathsToEmptyCollection()
        {
            var path = new DrawablePath();
            Assert.Empty(path.Paths);
        }
    }
}
