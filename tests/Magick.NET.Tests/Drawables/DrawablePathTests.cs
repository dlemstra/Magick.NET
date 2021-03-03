// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class DrawablePathTests
    {
        [Fact]
        public void Test_DrawablePath()
        {
            DrawablePath path = new DrawablePath();
            Assert.Empty(path.Paths);

            ((IDrawingWand)path).Draw(null);
        }
    }
}
