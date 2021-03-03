// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Drawing;
using System.Drawing.Imaging;
using ImageMagick;
using Xunit;

namespace Magick.NET.SystemDrawing.Tests
{
    public partial class MagickImageCollectionTests
    {
        public class TheToBitmapMethod
        {
            [Fact]
            public void ShouldReturnBitmap()
            {
                using (var images = new MagickImageCollection(Files.RoseSparkleGIF))
                {
                    Assert.Equal(3, images.Count);

                    using (Bitmap bitmap = images.ToBitmap())
                    {
                        Assert.NotNull(bitmap);
                        Assert.Equal(3, bitmap.GetFrameCount(FrameDimension.Page));
                    }
                }
            }

            [Fact]
            public void ShouldUseOptimizationForSingleImage()
            {
                using (var images = new MagickImageCollection(Files.RoseSparkleGIF))
                {
                    images.RemoveAt(0);
                    images.RemoveAt(0);

                    Assert.Single(images);

                    using (Bitmap bitmap = images.ToBitmap())
                    {
                        Assert.NotNull(bitmap);
                    }
                }
            }
        }
    }
}