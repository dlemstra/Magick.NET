// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class IConnectedComponentsSettingsExtensionsTests
    {
        public class TheRemoveImageArtifactsMethod
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

                    settings.SetImageArtifacts(image);
                    settings.RemoveImageArtifacts(image);

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

                    settings.SetImageArtifacts(image);
                    settings.RemoveImageArtifacts(image);

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

                    settings.SetImageArtifacts(image);
                    settings.RemoveImageArtifacts(image);

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

                    settings.SetImageArtifacts(image);
                    settings.RemoveImageArtifacts(image);

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

                    settings.SetImageArtifacts(image);
                    settings.RemoveImageArtifacts(image);

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

                    settings.SetImageArtifacts(image);
                    settings.RemoveImageArtifacts(image);

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

                    settings.SetImageArtifacts(image);
                    settings.RemoveImageArtifacts(image);

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

                    settings.SetImageArtifacts(image);
                    settings.RemoveImageArtifacts(image);

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

                    settings.SetImageArtifacts(image);
                    settings.RemoveImageArtifacts(image);

                    Assert.Empty(image.ArtifactNames);
                }
            }
        }
    }
}
