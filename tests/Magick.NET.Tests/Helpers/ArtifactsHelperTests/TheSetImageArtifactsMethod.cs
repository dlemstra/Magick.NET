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

            public class WithIConnectedComponentsSettings
            {
                [Fact]
                public void ShouldNotSetTheAttributesWhenTheyAreNotSpecified()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new ConnectedComponentsSettings();

                        ArtifactsHelper.SetImageArtifacts(image, settings);

                        Assert.Empty(image.ArtifactNames);
                    }
                }

                [Fact]
                public void ShouldSetTheAngleThreshold()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new ConnectedComponentsSettings
                        {
                            AngleThreshold = new Threshold(1.5),
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);

                        Assert.Single(image.ArtifactNames);
                        Assert.Equal("1.5", image.GetArtifact("connected-components:angle-threshold"));
                    }
                }

                [Fact]
                public void ShouldSetTheMinumunAndMaximumAngleThreshold()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new ConnectedComponentsSettings
                        {
                            AngleThreshold = new Threshold(1.2, 3.4),
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);

                        Assert.Single(image.ArtifactNames);
                        Assert.Equal("1.2-3.4", image.GetArtifact("connected-components:angle-threshold"));
                    }
                }

                [Fact]
                public void ShouldSetTheAreaThreshold()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new ConnectedComponentsSettings
                        {
                            AreaThreshold = new Threshold(1.5),
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);

                        Assert.Single(image.ArtifactNames);
                        Assert.Equal("1.5", image.GetArtifact("connected-components:area-threshold"));
                    }
                }

                [Fact]
                public void ShouldSetTheMinumunAndMaximumAreaThreshold()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new ConnectedComponentsSettings
                        {
                            AreaThreshold = new Threshold(1.2, 3.4),
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);

                        Assert.Single(image.ArtifactNames);
                        Assert.Equal("1.2-3.4", image.GetArtifact("connected-components:area-threshold"));
                    }
                }

                [Fact]
                public void ShouldSetTheCircularityThreshold()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new ConnectedComponentsSettings
                        {
                            CircularityThreshold = new Threshold(1.5),
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);

                        Assert.Single(image.ArtifactNames);
                        Assert.Equal("1.5", image.GetArtifact("connected-components:circularity-threshold"));
                    }
                }

                [Fact]
                public void ShouldSetTheMinumunAndMaximumCircularityThreshold()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new ConnectedComponentsSettings
                        {
                            CircularityThreshold = new Threshold(1.2, 3.4),
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);

                        Assert.Single(image.ArtifactNames);
                        Assert.Equal("1.2-3.4", image.GetArtifact("connected-components:circularity-threshold"));
                    }
                }

                [Fact]
                public void ShouldSetTheDiameterThreshold()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new ConnectedComponentsSettings
                        {
                            DiameterThreshold = new Threshold(1.5),
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);

                        Assert.Single(image.ArtifactNames);
                        Assert.Equal("1.5", image.GetArtifact("connected-components:diameter-threshold"));
                    }
                }

                [Fact]
                public void ShouldSetTheMinumunAndMaximumDiameterThreshold()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new ConnectedComponentsSettings
                        {
                            DiameterThreshold = new Threshold(1.2, 3.4),
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);

                        Assert.Single(image.ArtifactNames);
                        Assert.Equal("1.2-3.4", image.GetArtifact("connected-components:diameter-threshold"));
                    }
                }

                [Fact]
                public void ShouldSetTheEccentricityThreshold()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new ConnectedComponentsSettings
                        {
                            EccentricityThreshold = new Threshold(1.5),
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);

                        Assert.Single(image.ArtifactNames);
                        Assert.Equal("1.5", image.GetArtifact("connected-components:eccentricity-threshold"));
                    }
                }

                [Fact]
                public void ShouldSetTheMinumunAndMaximumEccentricityThreshold()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new ConnectedComponentsSettings
                        {
                            EccentricityThreshold = new Threshold(1.2, 3.4),
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);

                        Assert.Single(image.ArtifactNames);
                        Assert.Equal("1.2-3.4", image.GetArtifact("connected-components:eccentricity-threshold"));
                    }
                }

                [Fact]
                public void ShouldSetTheMajorAxisThreshold()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new ConnectedComponentsSettings
                        {
                            MajorAxisThreshold = new Threshold(1.5),
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);

                        Assert.Single(image.ArtifactNames);
                        Assert.Equal("1.5", image.GetArtifact("connected-components:major-axis-threshold"));
                    }
                }

                [Fact]
                public void ShouldSetTheMinumunAndMaximumMajorAxisThreshold()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new ConnectedComponentsSettings
                        {
                            MajorAxisThreshold = new Threshold(1.2, 3.4),
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);

                        Assert.Single(image.ArtifactNames);
                        Assert.Equal("1.2-3.4", image.GetArtifact("connected-components:major-axis-threshold"));
                    }
                }

                [Fact]
                public void ShouldSetMeanColor()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new ConnectedComponentsSettings
                        {
                            MeanColor = true,
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);

                        Assert.Single(image.ArtifactNames);
                        Assert.Equal("true", image.GetArtifact("connected-components:mean-color"));
                    }
                }

                [Fact]
                public void ShouldSetTheMinorAxisThreshold()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new ConnectedComponentsSettings
                        {
                            MinorAxisThreshold = new Threshold(1.5),
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);

                        Assert.Single(image.ArtifactNames);
                        Assert.Equal("1.5", image.GetArtifact("connected-components:minor-axis-threshold"));
                    }
                }

                [Fact]
                public void ShouldSetTheMinumunAndMaximumMinorAxisThreshold()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new ConnectedComponentsSettings
                        {
                            MinorAxisThreshold = new Threshold(1.2, 3.4),
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);

                        Assert.Single(image.ArtifactNames);
                        Assert.Equal("1.2-3.4", image.GetArtifact("connected-components:minor-axis-threshold"));
                    }
                }

                [Fact]
                public void ShouldSetThePerimeterThreshold()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new ConnectedComponentsSettings
                        {
                            PerimeterThreshold = new Threshold(1.5),
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);

                        Assert.Single(image.ArtifactNames);
                        Assert.Equal("1.5", image.GetArtifact("connected-components:perimeter-threshold"));
                    }
                }

                [Fact]
                public void ShouldSetTheMinumunAndMaximumPerimeterThreshold()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new ConnectedComponentsSettings
                        {
                            PerimeterThreshold = new Threshold(1.2, 3.4),
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);

                        Assert.Single(image.ArtifactNames);
                        Assert.Equal("1.2-3.4", image.GetArtifact("connected-components:perimeter-threshold"));
                    }
                }
            }

            public class WithIDeskewSettings
            {
                [Fact]
                public void ShouldNotSetTheAttributesWhenTheyAreNotSpecified()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new DeskewSettings();

                        ArtifactsHelper.SetImageArtifacts(image, settings);

                        Assert.Empty(image.ArtifactNames);
                    }
                }

                [Fact]
                public void ShouldSetAutoCrop()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new DeskewSettings
                        {
                            AutoCrop = true,
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);

                        Assert.Single(image.ArtifactNames);
                        Assert.Equal("true", image.GetArtifact("deskew:auto-crop"));
                    }
                }
            }

            public class WithIDistortSettings
            {
                [Fact]
                public void ShouldNotSetTheAttributesWhenTheyAreNotSpecified()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new DistortSettings();

                        ArtifactsHelper.SetImageArtifacts(image, settings);

                        Assert.Empty(image.ArtifactNames);
                    }
                }

                [Fact]
                public void ShouldSetScale()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new DistortSettings
                        {
                            Scale = 4.2,
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);

                        Assert.Single(image.ArtifactNames);
                        Assert.Equal("4.2", image.GetArtifact("distort:scale"));
                    }
                }

                [Fact]
                public void ShouldSetViewport()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new DistortSettings
                        {
                            Viewport = new MagickGeometry(1, 2, 3, 4),
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);

                        Assert.Single(image.ArtifactNames);
                        Assert.Equal("3x4+1+2", image.GetArtifact("distort:viewport"));
                    }
                }
            }
        }
    }
}
