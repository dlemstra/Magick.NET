// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class ArtifactsHelperTests
    {
        public class TheSetImageArtifactsMethod
        {
            public class WithICompareSettings
            {
                [Fact]
                public void ShouldNotSetTheAttributesWhenTheyAreNotSpecified()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new CompareSettings();

                        ArtifactsHelper.SetImageArtifacts(image, settings);

                        Assert.Empty(image.ArtifactNames);
                    }
                }

                [Fact]
                public void ShouldSetTheHighlightColor()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new CompareSettings
                        {
                            HighlightColor = MagickColors.Magenta,
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);

                        Assert.Single(image.ArtifactNames);

#if Q8
                        Assert.Equal("#FF00FFFF", image.GetArtifact("compare:highlight-color"));
#else
                        Assert.Equal("#FFFF0000FFFFFFFF", image.GetArtifact("compare:highlight-color"));
#endif
                    }
                }

                [Fact]
                public void ShouldSetTheLowlightColor()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new CompareSettings
                        {
                            LowlightColor = MagickColors.Magenta,
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);

                        Assert.Single(image.ArtifactNames);

#if Q8
                        Assert.Equal("#FF00FFFF", image.GetArtifact("compare:lowlight-color"));
#else
                        Assert.Equal("#FFFF0000FFFFFFFF", image.GetArtifact("compare:lowlight-color"));
#endif
                    }
                }

                [Fact]
                public void ShouldSetTheMasklightColor()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new CompareSettings
                        {
                            MasklightColor = MagickColors.Magenta,
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);

                        Assert.Single(image.ArtifactNames);

#if Q8
                        Assert.Equal("#FF00FFFF", image.GetArtifact("compare:masklight-color"));
#else
                        Assert.Equal("#FFFF0000FFFFFFFF", image.GetArtifact("compare:masklight-color"));
#endif
                    }
                }
            }

            public class WithIComplexSettings
            {
                [Fact]
                public void ShouldNotSetTheAttributesWhenTheyAreNotSpecified()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new ComplexSettings();

                        ArtifactsHelper.SetImageArtifacts(image, settings);

                        Assert.Empty(image.ArtifactNames);
                    }
                }

                [Fact]
                public void ShouldSetTheSignalToNoiseRatio()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new ComplexSettings
                        {
                            SignalToNoiseRatio = 1.2,
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);

                        Assert.Single(image.ArtifactNames);
                        Assert.Equal("1.2", image.GetArtifact("complex:snr"));
                    }
                }
            }
        }
    }
}
