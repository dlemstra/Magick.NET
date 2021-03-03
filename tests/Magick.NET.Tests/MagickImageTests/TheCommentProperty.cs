// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheCommentProperty
        {
            [Fact]
            public void ShouldGetTheCommentAttribute()
            {
                using (var image = new MagickImage())
                {
                    image.SetAttribute("comment", "foo");

                    Assert.Equal("foo", image.Comment);
                }
            }

            [Fact]
            public void ShouldSetTheCommentAttribute()
            {
                using (var image = new MagickImage())
                {
                    image.Comment = "foo";

                    Assert.Equal("foo", image.GetAttribute("comment"));
                }
            }

            [Fact]
            public void ShouldRemoveTheCommentAttributeWhenSetToNull()
            {
                using (var image = new MagickImage())
                {
                    image.SetAttribute("comment", "foo");

                    image.Comment = null;

                    Assert.Null(image.GetAttribute("comment"));
                }
            }
        }
    }
}
