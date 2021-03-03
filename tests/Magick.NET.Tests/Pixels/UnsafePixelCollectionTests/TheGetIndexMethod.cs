// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class UnsafePixelCollectionTests
    {
        public class TheGetIndexMethod
        {
            [Fact]
            public void ShouldReturnMinusOneForInvalidChannel()
            {
                using (var image = new MagickImage(Files.MagickNETIconPNG))
                {
                    using (var pixels = image.GetPixelsUnsafe())
                    {
                        int index = pixels.GetIndex(PixelChannel.Black);
                        Assert.Equal(-1, index);
                    }
                }
            }

            [Fact]
            public void ShouldReturnIndexForValidChannel()
            {
                using (var image = new MagickImage(Files.MagickNETIconPNG))
                {
                    using (var pixels = image.GetPixelsUnsafe())
                    {
                        int index = pixels.GetIndex(PixelChannel.Green);
                        Assert.Equal(1, index);
                    }
                }
            }
        }
    }
}
