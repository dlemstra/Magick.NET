// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class HeicReadDefinesTests
{
    public class TheMaxChildrenPerBoxProperty
    {
        [Fact]
        public void ShouldSetTheDefine()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new HeicReadDefines
            {
                MaxChildrenPerBox = 42,
            });
            Assert.Equal("42", image.Settings.GetDefine(MagickFormat.Heic, "max-children-per-box"));
        }
    }
}
