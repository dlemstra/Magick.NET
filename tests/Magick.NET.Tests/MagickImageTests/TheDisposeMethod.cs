// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheDisposeMethod
        {
            [Fact]
            public void ShouldDetermineTheColorTypeOfTheImage()
            {
                var image = new MagickImage();
                image.Dispose();

                Assert.Throws<ObjectDisposedException>(() =>
                {
                    image.HasAlpha = true;
                });
            }
        }
    }
}
