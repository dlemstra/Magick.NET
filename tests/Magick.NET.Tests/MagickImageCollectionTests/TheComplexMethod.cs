// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageCollectionTests
{
    public class TheComplexMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenCollectionIsEmpty()
        {
            using var images = new MagickImageCollection();
            var settings = new ComplexSettings(ComplexOperator.RealImaginary);

            Assert.Throws<InvalidOperationException>(() => images.Complex(settings));
        }

        [Fact]
        public void ShouldThrowExceptionWhenSettingsIsNull()
        {
            using var images = new MagickImageCollection();

            Assert.Throws<ArgumentNullException>("complexSettings", () => images.Complex(null));
        }

        [Fact]
        public void ShouldApplyTheOperatorToTheImages()
        {
            if (TestRuntime.HasFlakyLinuxArm64Result)
                return;

            using var images = new MagickImageCollection();
            images.Read(Files.RoseSparkleGIF);

            var settings = new ComplexSettings(ComplexOperator.Conjugate);
            images.Complex(settings);

            Assert.Equal(2, images.Count);

#if Q8
            ColorAssert.Equal(new MagickColor("#abb4ba01"), images[1], 10, 10);

#elif Q16
            ColorAssert.Equal(new MagickColor("#aaabb3b4b9ba0001"), images[1], 10, 10);
#else
            images[1].Clamp();
            ColorAssert.Equal(new MagickColor("#0000000000000000"), images[1], 10, 10);
#endif
        }
    }
}
