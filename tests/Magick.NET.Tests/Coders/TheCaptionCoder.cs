// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class TheCaptionCoder
    {
        [Fact]
        public void ShouldNotReplaceNonBreakingSpaceWithNewline()
        {
            var settings = new MagickReadSettings
            {
                Font = Files.Fonts.Arial,
                FontPointsize = 19,
                Width = 160,
            };

            using (var image = new MagickImage("caption:Нуорунен в Карелии", settings))
            {
                image.Trim();

                Assert.Equal(89, image.Width);
                Assert.Equal(41, image.Height);
            }
        }

        [Fact]
        public void ShouldReplaceOghamSpaceMarkWithNewline()
        {
            var settings = new MagickReadSettings
            {
                Font = Files.Fonts.Arial,
                FontPointsize = 19,
                Width = 160,
            };

            using (var image = new MagickImage("caption:Нуорунен в Карелии", settings))
            {
                image.Trim();

                Assert.Equal(100, image.Width);
                Assert.Equal(41, image.Height);
            }
        }

        [Fact]
        public void ShouldAddNewlineWhenTextDoesNotContainNewline()
        {
            var settings = new MagickReadSettings
            {
                Font = Files.Fonts.Arial,
                FontPointsize = 19,
                Width = 160,
            };

            using (var image = new MagickImage("caption:НуоруненвКарелии", settings))
            {
                image.Trim();

                Assert.Equal(148, image.Width);
                Assert.Equal(37, image.Height);
            }
        }
    }
}
