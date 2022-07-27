// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheShaveMethod
        {
            [Fact]
            public void ShouldShavePixelsFromEdges()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Shave(20, 40);

                    Assert.Equal(600, image.Width);
                    Assert.Equal(400, image.Height);
                }
            }
        }
    }
}
