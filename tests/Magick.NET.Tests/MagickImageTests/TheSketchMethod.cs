// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    [Collection(nameof(IsolatedUnitTest))]
    public class TheSketchMethod
    {
        [Fact]
        public void ShouldSimulatePencilSketch()
        {
            IsolatedUnitTest.Execute(() =>
            {
                using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
                image.Resize(400, 400);

                image.Sketch();
                image.ColorType = ColorType.Bilevel;

                ColorAssert.Equal(MagickColors.White, image, 63, 100);
                ColorAssert.Equal(MagickColors.White, image, 150, 175);
            });
        }

        [Fact]
        public void ShouldUseTheCorrectDefaultValue()
        {
            IsolatedUnitTest.Execute(() =>
            {
                MagickNET.SetRandomSeed(42);

                using var image = new MagickImage(Files.Builtin.Wizard);
                using var other = image.Clone();
                image.Sketch();
                other.Sketch(0.0, 1.0, 0.0);

                var distortion = other.Compare(image, ErrorMetric.RootMeanSquared);

                Assert.Equal(0.0, distortion);

                MagickNET.ResetRandomSeed();
            });
        }
    }
}
