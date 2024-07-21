// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class DrawablePathTests
{
    public class TheDrawMethod
    {
        [Fact]
        public void ShouldSupportNullArgument()
        {
            var path = new DrawablePath();

            ((IDrawingWand)path).Draw(null);
        }
    }
}
