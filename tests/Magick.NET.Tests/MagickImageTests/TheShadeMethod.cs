// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheShadeMethod
        {
            [Fact]
            public void ShouldUseTheCorrectDefaultValues()
            {
                using (var image = new MagickImage(Files.Builtin.Wizard))
                {
                    using (var other = image.Clone())
                    {
                        image.Shade();
                        other.Shade(30, 30);

                        var distortion = image.Compare(other, ErrorMetric.RootMeanSquared);
                        Assert.Equal(0.0, distortion);
                    }
                }
            }

            [Fact]
            public void ShouldUseTheCorrectDefaultChannel()
            {
                using (var image = new MagickImage(Files.MagickNETIconPNG))
                {
                    using (var other = image.Clone())
                    {
                        image.Shade(20, 20);
                        other.Shade(20, 20, Channels.RGB);

                        var distortion = image.Compare(other, ErrorMetric.RootMeanSquared);
                        Assert.Equal(0.0, distortion);
                    }
                }
            }

            [Fact]
            public void ShouldAddShade()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.FontPointsize = 90;
                    image.Read("label:Magick.NET");

                    image.Shade();

                    ColorAssert.Equal(MagickColors.Black, image, 64, 48);
                    ColorAssert.Equal(MagickColors.Black, image, 118, 48);
                    ColorAssert.Equal(new MagickColor("#7fff7fff7fff"), image, 148, 48);
                }
            }

            [Fact]
            public void ShouldUseTheSpecifiedChannels()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.FontPointsize = 90;
                    image.Read("label:Magick.NET");

                    image.Shade(10, 20, Channels.Composite);

                    ColorAssert.Equal(new MagickColor("#000000000000578e"), image, 64, 48);
                    ColorAssert.Equal(new MagickColor("#0000000000000000"), image, 118, 48);
                    ColorAssert.Equal(new MagickColor("#578e578e578e578e"), image, 148, 48);
                }
            }

            [Fact]
            public void ShouldPreserveImageColorType()
            {
                using (var image = new MagickImage(Files.MagickNETIconPNG))
                {
                    image.Shade();

                    var colorType = image.DetermineColorType();

                    Assert.Equal(ColorType.TrueColorAlpha, colorType);
                }
            }
        }
    }
}
