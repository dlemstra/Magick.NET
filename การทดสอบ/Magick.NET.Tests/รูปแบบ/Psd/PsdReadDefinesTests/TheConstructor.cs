﻿// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class PsdReadDefinesTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldNotSetAnyDefine()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new PsdReadDefines());

                    Assert.Null(image.Settings.GetDefine(MagickFormat.Psd, "alpha-unblend"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Psd, "preserve-opacity-mask"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Psd, "replicate-profile"));
                }
            }
        }
    }
}
