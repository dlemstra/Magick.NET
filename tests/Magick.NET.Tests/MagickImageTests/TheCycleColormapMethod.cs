// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheCycleColormapMethod
        {
            [Fact]
            public void ShouldDoNothingWhenAmountIsZero()
            {
                using (var first = new MagickImage(Files.Builtin.Logo))
                {
                    using (var second = first.Clone())
                    {
                        second.CycleColormap(0);
                        Assert.Equal(first, second);
                    }
                }
            }

            [Fact]
            public void ShouldAllowNegativeValue()
            {
                using (var first = new MagickImage(Files.Builtin.Logo))
                {
                    using (var second = first.Clone())
                    {
                        second.CycleColormap(-128);
                        Assert.NotEqual(0.0, first.Compare(second, ErrorMetric.RootMeanSquared));

                        second.CycleColormap(-128);
                        Assert.Equal(0.0, first.Compare(second, ErrorMetric.RootMeanSquared));
                    }
                }
            }

            [Fact]
            public void ShouldDisplaceTheColormap()
            {
                using (var first = new MagickImage(Files.Builtin.Logo))
                {
                    Assert.Equal(256, first.ColormapSize);

                    using (var second = first.Clone())
                    {
                        second.CycleColormap(128);
                        Assert.NotEqual(0.0, first.Compare(second, ErrorMetric.RootMeanSquared));

                        second.CycleColormap(128);
                        Assert.Equal(0.0, first.Compare(second, ErrorMetric.RootMeanSquared));

                        second.CycleColormap(256);
                        Assert.Equal(0.0, first.Compare(second, ErrorMetric.RootMeanSquared));

                        second.CycleColormap(512);
                        Assert.Equal(0.0, first.Compare(second, ErrorMetric.RootMeanSquared));
                    }
                }
            }
        }
    }
}
