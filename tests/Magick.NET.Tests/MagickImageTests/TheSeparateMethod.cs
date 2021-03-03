// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheSeparateMethod
        {
            [Fact]
            public void ShouldReturnTheCorrectNumberOfChannels()
            {
                using (var rose = new MagickImage(Files.Builtin.Rose))
                {
                    var i = 0;
                    foreach (MagickImage image in rose.Separate())
                    {
                        i++;
                        image.Dispose();
                    }

                    Assert.Equal(3, i);
                }
            }

            [Fact]
            public void ShouldReturnTheSpecifiedChannels()
            {
                using (var rose = new MagickImage(Files.Builtin.Rose))
                {
                    var i = 0;
                    foreach (MagickImage image in rose.Separate(Channels.Red | Channels.Green))
                    {
                        i++;
                        image.Dispose();
                    }

                    Assert.Equal(2, i);
                }
            }

            [Fact]
            public void ShouldReturnImageWithGrayColorspace()
            {
                using (var logo = new MagickImage(Files.Builtin.Logo))
                {
                    using (var blue = logo.Separate(Channels.Blue).First())
                    {
                        Assert.Equal(ColorSpace.Gray, blue.ColorSpace);
                    }
                }
            }
        }
    }
}
