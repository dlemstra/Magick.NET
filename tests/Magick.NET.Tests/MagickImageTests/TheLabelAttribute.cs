// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheLabelAttribute
        {
            [Fact]
            public void ShouldGetTheLabelAttribute()
            {
                using (var image = new MagickImage())
                {
                    image.SetAttribute("label", "foo");

                    Assert.Equal("foo", image.Label);
                }
            }

            [Fact]
            public void ShouldSetTheLabelAttribute()
            {
                using (var image = new MagickImage())
                {
                    image.Label = "foo";

                    Assert.Equal("foo", image.GetAttribute("label"));
                }
            }

            [Fact]
            public void ShouldRemoveTheLabelAttributeWhenSetToNull()
            {
                using (var image = new MagickImage())
                {
                    image.SetAttribute("label", "foo");

                    image.Label = null;

                    Assert.Null(image.GetAttribute("label"));
                }
            }
        }
    }
}
