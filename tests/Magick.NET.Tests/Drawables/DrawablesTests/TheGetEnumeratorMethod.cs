// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class DrawablesTests
    {
        public class TheGetEnumeratorMethod
        {
            [Fact]
            public void ShouldReturnAnEnumerator()
            {
                var drawables = new Drawables()
                  .FillColor(MagickColors.Red)
                  .Rectangle(10, 10, 90, 90);

                var enumerator = ((IEnumerable)drawables).GetEnumerator();
                Assert.True(enumerator.MoveNext());
                Assert.True(enumerator.MoveNext());
                Assert.False(enumerator.MoveNext());
            }
        }
    }
}
