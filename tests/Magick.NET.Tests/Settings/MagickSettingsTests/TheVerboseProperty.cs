// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickSettingsTests
    {
        public class TheVerboseProperty
        {
            [Fact]
            public void ShouldDefaultToFalse()
            {
                using (var image = new MagickImage())
                {
                    Assert.False(image.Settings.Verbose);
                }
            }
        }
    }
}
