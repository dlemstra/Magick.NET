// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheVirtualPixelMethodProperty
        {
            [Fact]
            public void ShouldReturnUndefinedAsTheDefaultValue()
            {
                using (var image = new MagickImage())
                {
                    Assert.Equal(VirtualPixelMethod.Undefined, image.VirtualPixelMethod);
                }
            }

            [Fact]
            public void ShouldChangeTheVirtualPixelMethod()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.VirtualPixelMethod = VirtualPixelMethod.Random;
                    Assert.Equal(VirtualPixelMethod.Random, image.VirtualPixelMethod);
                }
            }
        }
    }
}
