// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class TheGifCoder
    {
        [Fact]
        public void ShouldReturnTheCorrectNumberOfAnimationIterations()
        {
            using (var images = new MagickImageCollection())
            {
                images.Add(new MagickImage(MagickColors.Red, 1, 1));
                images.Add(new MagickImage(MagickColors.Green, 1, 1));

                using (var file = new TemporaryFile("output.gif"))
                {
                    images[0].AnimationIterations = 1;
                    images.Write(file.FullName);

                    images.Read(file.FullName);
                    Assert.Equal(1, images[0].AnimationIterations);
                }
            }
        }
    }
}
