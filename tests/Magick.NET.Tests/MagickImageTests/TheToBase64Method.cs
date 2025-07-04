// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheToBase64Method
    {
        [Fact]
        public void ShouldReturnBase64EncodedString()
        {
            using var image = new MagickImage(Files.SnakewarePNG);
            var base64 = image.ToBase64();

            Assert.NotNull(base64);
            Assert.Equal(11752, base64.Length);

            var bytes = Convert.FromBase64String(base64);

            Assert.NotNull(bytes);
            Assert.Equal(8814, bytes.Length);
        }

        [Fact]
        public void ShouldReturnBase64EncodedStringUsingTheSpecifiedFormat()
        {
            using var image = new MagickImage(Files.SnakewarePNG);
            var base64 = image.ToBase64(MagickFormat.Jpeg);

            Assert.NotNull(base64);

            if (TestRuntime.IsLinuxArm64)
                Assert.InRange(base64.Length, 1140, 1144);
            else
                Assert.Equal(1140, base64.Length);

            var bytes = Convert.FromBase64String(base64);

            Assert.NotNull(bytes);

            if (TestRuntime.IsLinuxArm64)
                Assert.InRange(bytes.Length, 853, 858);
            else
                Assert.Equal(853, bytes.Length);
        }

        [Fact]
        public void ShouldReturnBase64EncodedStringUsingTheSpecifiedDefines()
        {
            using var image = new MagickImage(Files.SnakewarePNG);
            var defines = new TiffWriteDefines
            {
                PreserveCompression = true,
            };
            var base64 = image.ToBase64(defines);

            Assert.NotNull(base64);
            Assert.Equal(10856, base64.Length);

            var bytes = Convert.FromBase64String(base64);

            Assert.NotNull(bytes);
        }
    }
}
