// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class ICompareSettingsExtensionsTests
    {
        public class TheRemoveImageArtifactsMethod : ICompareSettingsExtensionsTests
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

                    settings.SetImageArtifacts(image);
                    settings.RemoveImageArtifacts(image);

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

                    settings.SetImageArtifacts(image);
                    settings.RemoveImageArtifacts(image);

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

                    settings.SetImageArtifacts(image);
                    settings.RemoveImageArtifacts(image);

                    Assert.Empty(image.ArtifactNames);
                }
            }
        }
    }
}
