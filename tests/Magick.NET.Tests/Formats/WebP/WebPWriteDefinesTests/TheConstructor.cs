// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class WebPWriteDefinesTests
{
    public class TheConstructor : WebPWriteDefinesTests
    {
        [Fact]
        public void ShouldNotSetAnyDefine()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new WebPWriteDefines());

            Assert.Null(image.Settings.GetDefine(MagickFormat.WebP, "alpha-compression"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.WebP, "alpha-filtering"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.WebP, "alpha-quality"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.WebP, "auto-filter"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.WebP, "emulate-jpeg-size"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.WebP, "exact"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.WebP, "filter-strength"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.WebP, "filter-sharpness"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.WebP, "filter-type"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.WebP, "image-hint"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.WebP, "low-memory"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.WebP, "lossless"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.WebP, "method"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.WebP, "partition-limit"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.WebP, "partitions"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.WebP, "pass"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.WebP, "preprocessing"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.WebP, "segment"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.WebP, "show-compressed"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.WebP, "sns-strength"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.WebP, "target-psnr"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.WebP, "target-size"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.WebP, "thread-level"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.WebP, "use-sharp-yuv"));
        }
    }
}
