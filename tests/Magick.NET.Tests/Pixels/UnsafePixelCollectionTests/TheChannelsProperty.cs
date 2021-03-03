// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class UnsafePixelCollectionTests
    {
        public class TheChannelsProperty
        {
            [Fact]
            public void ShouldReturnChannelCountOfImage()
            {
                using (var image = new MagickImage(Files.CMYKJPG))
                {
                    using (var pixels = image.GetPixelsUnsafe())
                    {
                        Assert.Equal(image.ChannelCount, pixels.Channels);

                        image.HasAlpha = true;

                        Assert.Equal(image.ChannelCount, pixels.Channels);
                    }
                }
            }
        }
    }
}
