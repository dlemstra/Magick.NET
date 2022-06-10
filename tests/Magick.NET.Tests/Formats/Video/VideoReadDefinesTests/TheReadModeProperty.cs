// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class VideoReadDefinesTests
    {
        public class TheReadModeProperty
        {
            [Fact]
            public void ShouldSetTheDefineWhenToPamWhenValueIsByFrame()
            {
                using (var image = new MagickImage(MagickColors.Magenta, 1, 1))
                {
                    image.Settings.SetDefines(new VideoReadDefines(MagickFormat.Mp4)
                    {
                        ReadMode = VideoReadMode.ByFrame,
                    });

                    Assert.Equal("pam", image.Settings.GetDefine("video:intermediate-format"));
                }
            }

            [Fact]
            public void ShouldSetTheDefineWhenToWebpWhenValueIsByDuration()
            {
                using (var image = new MagickImage(MagickColors.Magenta, 1, 1))
                {
                    image.Settings.SetDefines(new VideoReadDefines(MagickFormat.Mp4)
                    {
                        ReadMode = VideoReadMode.ByDuration,
                    });

                    Assert.Equal("webp", image.Settings.GetDefine("video:intermediate-format"));
                }
            }

            [Fact]
            public void ShouldNotSetTheDefineWhenValueIsNotSet()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new VideoReadDefines(MagickFormat.Mp4)
                    {
                        ReadMode = null,
                    });

                    Assert.Null(image.Settings.GetDefine("video:intermediate-format"));
                }
            }
        }
    }
}
