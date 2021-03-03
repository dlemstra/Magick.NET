// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class IDistortSettingsExtensionsTests
    {
        public class TheRemoveImageArtifactsMethod
        {
            [Fact]
            public void ShouldRemoveScale()
            {
                using (var image = new MagickImage())
                {
                    var settings = new DistortSettings
                    {
                        Scale = 4.2,
                    };

                    settings.SetImageArtifacts(image);
                    settings.RemoveImageArtifacts(image);

                    Assert.Empty(image.ArtifactNames);
                }
            }

            [Fact]
            public void ShouldRemoveViewport()
            {
                using (var image = new MagickImage())
                {
                    var settings = new DistortSettings
                    {
                        Viewport = new MagickGeometry(1, 2, 3, 4),
                    };

                    settings.SetImageArtifacts(image);
                    settings.RemoveImageArtifacts(image);

                    Assert.Empty(image.ArtifactNames);
                }
            }
        }
    }
}
