// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class DngReadDefinesTests
{
    public class TheMaxRawMemoryProperty
    {
        [Theory]
        [InlineData(0U)]
        [InlineData(uint.MaxValue)]
        public void ShouldSetTheDefine(uint value)
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new DngReadDefines
            {
                MaxRawMemory = value,
            });

            Assert.Equal(value.ToString(), image.Settings.GetDefine(MagickFormat.Dng, "max-raw-memory"));
        }
    }
}
