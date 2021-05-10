// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class VideoWriteDefinesTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldNotSetAnyDefine()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new VideoWriteDefines(MagickFormat.Mp4));

                    Assert.Null(image.Settings.GetDefine("video:pixel-format"));
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenFormatIsInvalid()
            {
                Assert.Throws<ArgumentException>("format", () => new VideoWriteDefines(MagickFormat.Png));
            }
        }
    }
}
