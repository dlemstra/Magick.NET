// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class PngWriteDefinesTests
{
    public class TheTimestampProperty
    {
        [Fact]
        public void ShouldSetTheDefineInUtc()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PngWriteDefines
            {
                Timestamp = new DateTime(2025, 1, 2, 3, 4, 5, DateTimeKind.Utc),
            });

            Assert.Equal("2025-01-02T03:04:05Z", image.Settings.GetDefine(MagickFormat.Png, "tIME"));
        }
    }
}
