// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class IDistortSettingsExtensionsTests
    {
        public class TheSetImageArtifactsMethod
        {
            [Fact]
            public void ShouldNotSetTheAttributesWhenTheyAreNotSpecified()
            {
                using (var image = new MagickImage())
                {
                    var settings = new DistortSettings();

                    settings.SetImageArtifacts(image);

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

                    settings.SetImageArtifacts(image);

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

                    settings.SetImageArtifacts(image);

                    Assert.Single(image.ArtifactNames);
                    Assert.Equal("3x4+1+2", image.GetArtifact("distort:viewport"));
                }
            }
        }
    }
}
