// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class IMorphologySettingsExtensionsTests
    {
        public class TheSetImageArtifactsMethod
        {
            [Fact]
            public void ShouldNotSetTheAttributesWhenTheyAreNotSpecified()
            {
                using (var image = new MagickImage())
                {
                    var settings = new MorphologySettings();

                    settings.SetImageArtifacts(image);

                    Assert.Empty(image.ArtifactNames);
                }
            }

            [Fact]
            public void ShouldSetConvolveBias()
            {
                using (var image = new MagickImage())
                {
                    var settings = new MorphologySettings
                    {
                        ConvolveBias = new Percentage(70),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.Single(image.ArtifactNames);
                    Assert.Equal("70%", image.GetArtifact("convolve:bias"));
                }
            }

            [Fact]
            public void ShouldSetConvolveScale()
            {
                using (var image = new MagickImage())
                {
                    var settings = new MorphologySettings
                    {
                        ConvolveScale = new MagickGeometry(1, 2, 3, 4),
                    };

                    settings.SetImageArtifacts(image);

                    Assert.Single(image.ArtifactNames);
                    Assert.Equal("3x4+1+2", image.GetArtifact("convolve:scale"));
                }
            }
        }
    }
}
