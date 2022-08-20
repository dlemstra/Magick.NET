// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class PixelTests
    {
        public class TheEqualsMethod
        {
            public class WithObject
            {
                [Fact]
                public void ShouldReturnFalseWhenValueIsNull()
                {
                    var pixel = new Pixel(0, 0, 3);

                    Assert.False(pixel.Equals((object)null));
                }

                [Fact]
                public void ShouldReturnTrueWhenValueIsTheSame()
                {
                    var pixel = new Pixel(0, 0, 3);

                    Assert.True(pixel.Equals((object)pixel));
                }

                [Fact]
                public void ShouldReturnTrueWhenValueIsEqual()
                {
                    var first = new Pixel(0, 0, 3);
                    first.SetChannel(0, 100);
                    first.SetChannel(1, 150);
                    first.SetChannel(2, 200);

                    var second = new Pixel(0, 0, 3);
                    second.SetChannel(0, 100);
                    second.SetChannel(1, 150);
                    second.SetChannel(2, 200);

                    Assert.True(first.Equals((object)second));
                }

                [Fact]
                public void ShouldReturnFalseWhenValueIsNotEqual()
                {
                    var first = new Pixel(0, 0, 1);
                    first.SetChannel(0, 100);

                    var second = new Pixel(0, 0, 1);
                    second.SetChannel(0, 50);

                    Assert.False(first.Equals((object)second));
                }
            }

            public class WithPixel
            {
                [Fact]
                public void ShouldReturnFalseWhenValueIsNull()
                {
                    var pixel = new Pixel(0, 0, 3);

                    Assert.False(pixel.Equals((Pixel)null));
                }

                [Fact]
                public void ShouldReturnTrueWhenValueIsTheSame()
                {
                    var pixel = new Pixel(0, 0, 3);

                    Assert.True(pixel.Equals(pixel));
                }

                [Fact]
                public void ShouldReturnTrueWhenValueIsEqual()
                {
                    var first = new Pixel(0, 0, 3);
                    first.SetChannel(0, 100);
                    first.SetChannel(1, 150);
                    first.SetChannel(2, 200);

                    var second = new Pixel(0, 0, 3);
                    second.SetChannel(0, 100);
                    second.SetChannel(1, 150);
                    second.SetChannel(2, 200);

                    Assert.True(first.Equals(second));
                }

                [Fact]
                public void ShouldReturnFalseWhenValueIsNotEqual()
                {
                    var first = new Pixel(0, 0, 1);
                    first.SetChannel(0, 100);

                    var second = new Pixel(0, 0, 1);
                    second.SetChannel(0, 50);

                    Assert.False(first.Equals(second));
                }
            }
        }
    }
}
