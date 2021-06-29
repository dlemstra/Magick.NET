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
        }
    }
}
