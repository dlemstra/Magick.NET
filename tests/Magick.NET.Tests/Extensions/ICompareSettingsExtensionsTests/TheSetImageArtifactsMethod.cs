// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class ICompareSettingsExtensionsTests
    {
        public class TheSetImageArtifactsMethod : ICompareSettingsExtensionsTests
        {
            [Fact]
            public void ShouldNotSetTheAttributesWhenTheyAreNotSpecified()
            {
                using (var image = new MagickImage())
                {
                    var settings = new CompareSettings();

                    settings.SetImageArtifacts(image);

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

                    settings.SetImageArtifacts(image);

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

                    settings.SetImageArtifacts(image);

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

                    settings.SetImageArtifacts(image);

                    Assert.Single(image.ArtifactNames);

#if Q8
                    Assert.Equal("#FF00FFFF", image.GetArtifact("compare:masklight-color"));
#else
                    Assert.Equal("#FFFF0000FFFFFFFF", image.GetArtifact("compare:masklight-color"));
#endif
                }
            }
        }
    }
}
