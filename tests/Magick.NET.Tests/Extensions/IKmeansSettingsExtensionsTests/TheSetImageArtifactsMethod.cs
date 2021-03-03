// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class IKmeansSettingsExtensionsTests
    {
        public class TheSetImageArtifactsMethod
        {
            [Fact]
            public void ShouldNotSetTheAttributesWhenTheyAreNotSpecified()
            {
                using (var image = new MagickImage())
                {
                    var settings = new KmeansSettings();

                    settings.SetImageArtifacts(image);

                    Assert.Empty(image.ArtifactNames);
                }
            }

            [Fact]
            public void ShouldSetSeedColors()
            {
                using (var image = new MagickImage())
                {
                    var settings = new KmeansSettings
                    {
                        SeedColors = "red;blue",
                    };

                    settings.SetImageArtifacts(image);

                    Assert.Single(image.ArtifactNames);
                    Assert.Equal("red;blue", image.GetArtifact("kmeans:seed-colors"));
                }
            }
        }
    }
}
