// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheLocalContrastMethod
        {
            [Fact]
            public void ShouldOnlyChangeSpecifiedChannels()
            {
                using (var image = new MagickImage("plasma:purple", 100, 100))
                {
                    using (var allChannels = image.Clone())
                    {
                        allChannels.LocalContrast(2, new Percentage(50));
                        image.LocalContrast(2, new Percentage(50), Channels.Red);

                        var difference = image.Compare(allChannels, ErrorMetric.RootMeanSquared);
                        Assert.NotEqual(0, difference);
                    }
                }
            }
        }
    }
}
