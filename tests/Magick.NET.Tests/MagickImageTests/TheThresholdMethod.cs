// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheThresholdMethod
        {
            [Fact]
            public void ShouldThresholdTheImage()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var memStream = new MemoryStream())
                    {
                        image.Threshold(new Percentage(80));
                        image.Settings.Compression = CompressionMethod.Group4;
                        image.Format = MagickFormat.Pdf;
                        image.Write(memStream);
                    }
                }
            }

            [Fact]
            public void ShouldUseThecorrectDefaultValues()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var other = image.Clone())
                    {
                        image.Threshold(new Percentage(80));
                        other.Threshold(new Percentage(80), Channels.Default);

                        var difference = image.Compare(other, ErrorMetric.RootMeanSquared);
                        Assert.Equal(0.0, difference);
                    }
                }
            }
        }
    }
}
