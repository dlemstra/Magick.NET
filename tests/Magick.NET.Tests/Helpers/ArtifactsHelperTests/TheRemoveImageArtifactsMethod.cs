// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class ArtifactsHelperTests
    {
        public class TheRemoveImageArtifactsMethod
        {
            public class WithICompareSettings
            {
                [Fact]
                public void ShouldRemoveTheHighlightColor()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new CompareSettings
                        {
                            HighlightColor = MagickColors.Magenta,
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);
                        ArtifactsHelper.RemoveImageArtifacts(image, settings);

                        Assert.Empty(image.ArtifactNames);
                    }
                }

                [Fact]
                public void ShouldRemoveTheLowlightColor()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new CompareSettings
                        {
                            LowlightColor = MagickColors.Magenta,
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);
                        ArtifactsHelper.RemoveImageArtifacts(image, settings);

                        Assert.Empty(image.ArtifactNames);
                    }
                }

                [Fact]
                public void ShouldRemoveTheMasklightColor()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new CompareSettings
                        {
                            MasklightColor = MagickColors.Magenta,
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);
                        ArtifactsHelper.RemoveImageArtifacts(image, settings);

                        Assert.Empty(image.ArtifactNames);
                    }
                }
            }

            public class WithIComplexSettings
            {
                [Fact]
                public void ShouldRemoveTheSignalToNoiseRatio()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new ComplexSettings
                        {
                            SignalToNoiseRatio = 1.2,
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);
                        ArtifactsHelper.RemoveImageArtifacts(image, settings);

                        Assert.Empty(image.ArtifactNames);
                    }
                }
            }

            public class WithIConnectedComponentsSettings
            {
                [Fact]
                public void ShouldRemoveTheAngleThreshold()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new ConnectedComponentsSettings
                        {
                            AngleThreshold = new Threshold(1.5),
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);
                        ArtifactsHelper.RemoveImageArtifacts(image, settings);

                        Assert.Empty(image.ArtifactNames);
                    }
                }

                [Fact]
                public void ShouldRemoveTheAreaThreshold()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new ConnectedComponentsSettings
                        {
                            AreaThreshold = new Threshold(1.5),
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);
                        ArtifactsHelper.RemoveImageArtifacts(image, settings);

                        Assert.Empty(image.ArtifactNames);
                    }
                }

                [Fact]
                public void ShouldRemoveTheCircularityThreshold()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new ConnectedComponentsSettings
                        {
                            CircularityThreshold = new Threshold(1.5),
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);
                        ArtifactsHelper.RemoveImageArtifacts(image, settings);

                        Assert.Empty(image.ArtifactNames);
                    }
                }

                [Fact]
                public void ShouldRemoveTheDiameterThreshold()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new ConnectedComponentsSettings
                        {
                            DiameterThreshold = new Threshold(1.5),
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);
                        ArtifactsHelper.RemoveImageArtifacts(image, settings);

                        Assert.Empty(image.ArtifactNames);
                    }
                }

                [Fact]
                public void ShouldRemoveTheEccentricityThreshold()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new ConnectedComponentsSettings
                        {
                            EccentricityThreshold = new Threshold(1.5),
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);
                        ArtifactsHelper.RemoveImageArtifacts(image, settings);

                        Assert.Empty(image.ArtifactNames);
                    }
                }

                [Fact]
                public void ShouldRemoveTheMajorAxisThreshold()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new ConnectedComponentsSettings
                        {
                            MajorAxisThreshold = new Threshold(1.5),
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);
                        ArtifactsHelper.RemoveImageArtifacts(image, settings);

                        Assert.Empty(image.ArtifactNames);
                    }
                }

                [Fact]
                public void ShouldRemoveMeanColor()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new ConnectedComponentsSettings
                        {
                            MeanColor = true,
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);
                        ArtifactsHelper.RemoveImageArtifacts(image, settings);

                        Assert.Empty(image.ArtifactNames);
                    }
                }

                [Fact]
                public void ShouldRemoveTheMinorAxisThreshold()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new ConnectedComponentsSettings
                        {
                            MinorAxisThreshold = new Threshold(1.5),
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);
                        ArtifactsHelper.RemoveImageArtifacts(image, settings);

                        Assert.Empty(image.ArtifactNames);
                    }
                }

                [Fact]
                public void ShouldRemoveThePerimeterThreshold()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new ConnectedComponentsSettings
                        {
                            PerimeterThreshold = new Threshold(1.5),
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);
                        ArtifactsHelper.RemoveImageArtifacts(image, settings);

                        Assert.Empty(image.ArtifactNames);
                    }
                }
            }

            public class WithIDeskewSettings
            {
                [Fact]
                public void ShouldRemoveAutoCrop()
                {
                    using (var image = new MagickImage())
                    {
                        var settings = new DeskewSettings
                        {
                            AutoCrop = true,
                        };

                        ArtifactsHelper.SetImageArtifacts(image, settings);
                        ArtifactsHelper.RemoveImageArtifacts(image, settings);

                        Assert.Empty(image.ArtifactNames);
                    }
                }
            }
        }
    }
}
