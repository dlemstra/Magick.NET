// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheSignatureProperty
        {
            [Fact]
            public void ShouldReturnImageSignature()
            {
                using (var image = new MagickImage())
                {
                    Assert.Equal(0, image.Width);
                    Assert.Equal(0, image.Height);
                    Assert.Equal("e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855", image.Signature);
                }
            }
        }
    }
}
