// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class IDeskewSettingsExtensionsTests
    {
        public class TheSetImageArtifactsMethod
        {
            [Fact]
            public void ShouldNotSetTheAttributesWhenTheyAreNotSpecified()
            {
                using (var image = new MagickImage())
                {
                    var settings = new DeskewSettings();

                    settings.SetImageArtifacts(image);

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

                    settings.SetImageArtifacts(image);

                    Assert.Single(image.ArtifactNames);
                    Assert.Equal("true", image.GetArtifact("deskew:auto-crop"));
                }
            }
        }
    }
}
