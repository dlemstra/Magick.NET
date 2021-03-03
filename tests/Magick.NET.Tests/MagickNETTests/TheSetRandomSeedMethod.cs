// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickNETTests
    {
        public class TheSetRandomSeedMethod
        {
            [Fact]
            public void ShouldPassOrderedTests()
            {
                TestHelper.ExecuteInsideLock(() =>
                {
                    ShouldMakeDifferentPlasmaImageWhenNotSet();

                    ShouldMakeDuplicatePlasmaImagesWhenSet();

                    ShouldMakeDifferentPlasmaImageWhenNotSet();
                });
            }

            private void ShouldMakeDuplicatePlasmaImagesWhenSet()
            {
                using (var first = new MagickImage("plasma:red", 10, 10))
                {
                    using (var second = new MagickImage("plasma:red", 10, 10))
                    {
                        Assert.NotEqual(0.0, first.Compare(second, ErrorMetric.RootMeanSquared));
                    }
                }
            }

            private void ShouldMakeDifferentPlasmaImageWhenNotSet()
            {
                MagickNET.SetRandomSeed(42);

                using (var first = new MagickImage("plasma:red", 10, 10))
                {
                    using (var second = new MagickImage("plasma:red", 10, 10))
                    {
                        Assert.Equal(0.0, first.Compare(second, ErrorMetric.RootMeanSquared));
                    }
                }

                MagickNET.ResetRandomSeed();
            }
        }
    }
}
