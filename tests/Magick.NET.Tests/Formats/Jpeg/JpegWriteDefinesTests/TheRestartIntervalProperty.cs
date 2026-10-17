// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class JpegWriteDefinesTests
{
    public class TheRestartIntervalProperty
    {
        [Theory]
        [InlineData(0)]
        [InlineData(int.MaxValue)]
        public void ShouldSetTheDefine(int value)
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new JpegWriteDefines
            {
                RestartInterval = value,
            });

            Assert.Equal(value.ToString(), image.Settings.GetDefine(MagickFormat.Jpeg, "restart-interval"));
        }
    }
}
