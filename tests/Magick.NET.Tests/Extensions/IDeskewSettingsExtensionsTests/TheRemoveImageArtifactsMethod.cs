// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class IDeskewSettingsExtensionsTests
    {
        public class TheRemoveImageArtifactsMethod
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

                    settings.SetImageArtifacts(image);
                    settings.RemoveImageArtifacts(image);

                    Assert.Empty(image.ArtifactNames);
                }
            }
        }
    }
}
