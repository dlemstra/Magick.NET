// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class Jp2ReadDefinesTests
{
    public class TheAssumeAlphaProperty
    {
        [Fact]
        public void ShouldTreatMetaChannelAsAlpha()
        {
            var settings = new MagickReadSettings
            {
                Defines = new Jp2ReadDefines
                {
                    AssumeAlpha = true,
                },
            };

            using var image = new MagickImage();
            image.Read(Files.Coders.TestJP2, settings);

            Assert.Equal("true", image.Settings.GetDefine(MagickFormat.Jp2, "assume-alpha"));
            Assert.True(image.HasAlpha);
            Assert.Equal(4U, image.ChannelCount);
            Assert.Contains(PixelChannel.Alpha, image.Channels);
        }
    }
}
