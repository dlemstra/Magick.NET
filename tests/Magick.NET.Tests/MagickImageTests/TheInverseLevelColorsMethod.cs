// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheInverseLevelColorsMethod
        {
            [Fact]
            public void ShouldDoTheInverseOfLevelTheColors()
            {

                using (var first = new MagickImage(Files.MagickNETIconPNG))
                {
                    first.LevelColors(MagickColors.Fuchsia, MagickColors.Goldenrod, Channels.Blue);
                    first.InverseLevelColors(MagickColors.Fuchsia, MagickColors.Goldenrod, Channels.Blue);
                    first.Alpha(AlphaOption.Background);

                    using (var second = new MagickImage(Files.MagickNETIconPNG))
                    {
                        second.Alpha(AlphaOption.Background);
#if Q8 || Q16
                        Assert.Equal(0.0, first.Compare(second, ErrorMetric.RootMeanSquared));
#else
                        Assert.InRange(first.Compare(second, ErrorMetric.RootMeanSquared), 0.0, 0.00000001);
#endif
                    }
                }
            }
        }
    }
}
