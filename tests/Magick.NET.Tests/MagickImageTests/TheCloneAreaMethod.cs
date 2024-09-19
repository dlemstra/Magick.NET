// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheCloneAreaMethod
    {
        public class WithGeometry
        {
            [Fact]
            public void ShouldCloneTheSpecifiedGeometry()
            {
                using var icon = new MagickImage(Files.MagickNETIconPNG);
                using var area = icon.Clone();
                area.Crop(64, 64, Gravity.Southeast);
                area.ResetPage();
                area.Crop(64, 32, Gravity.North);

                using var part = icon.CloneArea(new MagickGeometry(64, 64, 64, 32));

                Assert.Equal(area.Width, part.Width);
                Assert.Equal(area.Height, part.Height);

                Assert.Equal(0.0, area.Compare(part, ErrorMetric.RootMeanSquared));
            }
        }

        public class WithWidthAndHeight
        {
            [Fact]
            public void ShouldClonePartOfTheImageWhenWidthAndHeightAreSpecified()
            {
                using var icon = new MagickImage(Files.MagickNETIconPNG);
                using var area = icon.Clone();
                area.Crop(32, 64, Gravity.Northwest);

                Assert.Equal(32U, area.Width);
                Assert.Equal(64U, area.Height);

                using var part = icon.CloneArea(32, 64);

                Assert.Equal(area.Width, part.Width);
                Assert.Equal(area.Height, part.Height);
                Assert.Equal(0.0, area.Compare(part, ErrorMetric.RootMeanSquared));
                Assert.Equal(8192, part.ToByteArray(MagickFormat.Rgba).Length);
            }
        }

        public class WithWidthAndHeightAndOffset
        {
            [Fact]
            public void ShouldCloneUsingTheSpecifiedOffset()
            {
                using var icon = new MagickImage(Files.MagickNETIconPNG);
                using var area = icon.Clone();
                area.Crop(64, 64, Gravity.Southeast);
                area.ResetPage();
                area.Crop(64, 32, Gravity.North);

                using var part = icon.CloneArea(64, 64, 64, 32);

                Assert.Equal(area.Width, part.Width);
                Assert.Equal(area.Height, part.Height);

                Assert.Equal(0.0, area.Compare(part, ErrorMetric.RootMeanSquared));
            }
        }
    }
}
