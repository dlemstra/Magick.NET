// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class DngReadDefinesTests
{
    public class TheReadThumbnailsProperty
    {
        [Fact]
        public void ShouldSetTheDefineWhenTrue()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new DngReadDefines
            {
                ReadThumbnails = true,
            });

            Assert.Equal("true", image.Settings.GetDefine(MagickFormat.Dng, "read-thumbnails"));
        }

        [Theory]
        [InlineData(false)]
        [InlineData(null)]
        public void ShouldNotSetTheDefineWhenFalseOrNull(bool? value)
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new DngReadDefines
            {
                ReadThumbnails = value,
            });

            Assert.Null(image.Settings.GetDefine(MagickFormat.Dng, "read-thumbnails"));
        }
    }
}
