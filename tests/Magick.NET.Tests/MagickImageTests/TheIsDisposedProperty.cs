// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheIsDisposedProperty
        {
            [Fact]
            public void ShouldReturnFalseWhenTheImageIsNotDisposed()
            {
                using (var image = new MagickImage())
                {
                    Assert.False(image.IsDisposed);
                }
            }

            [Fact]
            public void ShouldReturnTrueWhenTheImageIsDisposed()
            {
                var image = new MagickImage();
                image.Dispose();

                Assert.True(image.IsDisposed);
            }
        }
    }
}
