// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickSettingsTests
    {
        public class TheTextInterwordSpacingProperty
        {
            [Fact]
            public void ShouldDefaultToZero()
            {
                using (var image = new MagickImage())
                {
                    Assert.Equal(0, image.Settings.TextInterwordSpacing);
                }
            }

            [Fact]
            public void ShouldBeUsedWhenRenderingText()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.TextInterwordSpacing = 10;
                    image.Read("label:First second");

                    Assert.Equal(74, image.Width);
                    Assert.Equal(15, image.Height);

                    image.Settings.TextInterwordSpacing = 20;
                    image.Read("label:First second");

                    Assert.Equal(84, image.Width);
                    Assert.Equal(15, image.Height);
                }
            }
        }
    }
}
