// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class StatisticsTests
    {
        public class TheEqualsMethod
        {
            [Fact]
            public void ShouldReturnFalseWhenValueIsNull()
            {
                using (var image = new MagickImage("xc:red", 1, 1))
                {
                    var statistics = image.Statistics();

                    Assert.False(statistics.Equals(null));
                }
            }

            [Fact]
            public void ShouldReturnTrueWhenValueIsSame()
            {
                using (var image = new MagickImage("xc:red", 1, 1))
                {
                    var statistics = image.Statistics();

                    Assert.True(statistics.Equals(statistics));
                }
            }

            [Fact]
            public void ShouldReturnTrueWhenValueIsSameObject()
            {
                using (var image = new MagickImage("xc:red", 1, 1))
                {
                    var statistics = image.Statistics();

                    Assert.True(statistics.Equals((object)statistics));
                }
            }

            [Fact]
            public void ShouldReturnTrueWhenValueIsFromSameImage()
            {
                using (var firstImage = new MagickImage("xc:red", 1, 1))
                {
                    var first = firstImage.Statistics();

                    using (var secondImage = new MagickImage("xc:red", 1, 1))
                    {
                        var second = secondImage.Statistics();

                        Assert.True(first.Equals(second));
                    }
                }
            }

            [Fact]
            public void ShouldReturnTrueWhenObjectIsFromSameImage()
            {
                using (var firstImage = new MagickImage("xc:red", 1, 1))
                {
                    var first = firstImage.Statistics();

                    using (var secondImage = new MagickImage("xc:red", 1, 1))
                    {
                        var second = secondImage.Statistics();

                        Assert.True(first.Equals((object)second));
                    }
                }
            }

            [Fact]
            public void ShouldReturnFalseWhenValueIsFromDifferentImage()
            {
                using (var firstImage = new MagickImage("xc:red", 1, 1))
                {
                    var first = firstImage.Statistics();

                    using (var secondImage = new MagickImage("xc:green", 1, 1))
                    {
                        var second = secondImage.Statistics();

                        Assert.False(first.Equals(second));
                    }
                }
            }
        }
    }
}
