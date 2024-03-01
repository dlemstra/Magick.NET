// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheCloneMethod
    {
        public class WithoutArguments
        {
            [Fact]
            public void ShouldThrowExceptionWhenNoImageIsRead()
            {
                using var image = new MagickImage();

                Assert.Throws<MagickCorruptImageErrorException>(() => image.Clone());
            }

            [Fact]
            public void ShouldCloneTheImage()
            {
                using var image = new MagickImage(Files.Builtin.Logo);

                var clone = image.Clone();

                Assert.Equal(image, clone);
                Assert.False(ReferenceEquals(image, clone));
            }
        }

        public class WithWidthAndHeight
        {
            [Fact]
            public void ShouldThrowExceptionWhenWidthIsNegative()
            {
                using var image = new MagickImage(Files.MagickNETIconPNG);
                Assert.Throws<ArgumentException>("width", () => image.Clone(-1, 64));
            }

            [Fact]
            public void ShouldThrowExceptionWhenHeightIsNegative()
            {
                using var image = new MagickImage(Files.MagickNETIconPNG);
                Assert.Throws<ArgumentException>("height", () => image.Clone(32, -1));
            }

            [Fact]
            public void ShouldClonePartOfTheImageWhenWidthAndHeightAreSpecified()
            {
                using var icon = new MagickImage(Files.MagickNETIconPNG);
                using var area = icon.Clone();
                area.Crop(32, 64, Gravity.Northwest);

                Assert.Equal(32, area.Width);
                Assert.Equal(64, area.Height);

                using var part = icon.Clone(32, 64);

                Assert.Equal(area.Width, part.Width);
                Assert.Equal(area.Height, part.Height);
                Assert.Equal(0.0, area.Compare(part, ErrorMetric.RootMeanSquared));
                Assert.Equal(8192, part.ToByteArray(MagickFormat.Rgba).Length);
            }
        }

        public class WithWidthAndHeightAndOffset
        {
            [Fact]
            public void ShouldThrowExceptionWhenWidthIsNegative()
            {
                using var image = new MagickImage(Files.MagickNETIconPNG);
                Assert.Throws<ArgumentException>("width", () => image.Clone(0, 0, -1, 64));
            }

            [Fact]
            public void ShouldThrowExceptionWhenHeightIsNegative()
            {
                using var image = new MagickImage(Files.MagickNETIconPNG);
                Assert.Throws<ArgumentException>("height", () => image.Clone(0, 0, 32, -1));
            }

            [Fact]
            public void ShouldCloneUsingTheSpecifiedOffset()
            {
                using var icon = new MagickImage(Files.MagickNETIconPNG);
                using var area = icon.Clone();
                area.Crop(64, 64, Gravity.Southeast);
                area.RePage();
                area.Crop(64, 32, Gravity.North);

                using var part = icon.Clone(64, 64, 64, 32);

                Assert.Equal(area.Width, part.Width);
                Assert.Equal(area.Height, part.Height);

                Assert.Equal(0.0, area.Compare(part, ErrorMetric.RootMeanSquared));
            }
        }

        public class WithGeometry
        {
            [Fact]
            public void ShouldCloneTheSpecifiedGeometry()
            {
                using var icon = new MagickImage(Files.MagickNETIconPNG);
                using var area = icon.Clone();
                area.Crop(64, 64, Gravity.Southeast);
                area.RePage();
                area.Crop(64, 32, Gravity.North);

                using var part = icon.Clone(new MagickGeometry(64, 64, 64, 32));

                Assert.Equal(area.Width, part.Width);
                Assert.Equal(area.Height, part.Height);

                Assert.Equal(0.0, area.Compare(part, ErrorMetric.RootMeanSquared));
            }
        }
    }
}
