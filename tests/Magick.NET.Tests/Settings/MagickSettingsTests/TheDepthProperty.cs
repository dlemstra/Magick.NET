// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickSettingsTests
    {
        public class TheDepthProperty
        {
            [Fact]
            public void ShouldChangeTheDepthOfTheOutputImage()
            {
                using (var input = new MagickImage(Files.Builtin.Logo))
                {
                    input.Settings.Depth = 5;

                    var bytes = input.ToByteArray(MagickFormat.Tga);

                    using (var output = new MagickImage(bytes, MagickFormat.Tga))
                    {
                        Assert.Equal(5, output.Depth);
                    }
                }
            }
        }
    }
}
