// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickFormatInfoTests
{
    public class TheUnregisterMethod
    {
        [Fact]
        public void ShouldDisableFormat()
        {
            var formatInfo = MagickFormatInfo.Create(MagickFormat.X3f);
            Assert.NotNull(formatInfo);
            Assert.True(formatInfo.Unregister());

            var settings = new MagickReadSettings
            {
                Format = MagickFormat.X3f,
            };

            Assert.Throws<MagickMissingDelegateErrorException>(() =>
            {
                var image = new MagickImage();
                image.Read(new byte[] { 1, 2, 3, 4 }, settings);
            });
        }
    }
}
