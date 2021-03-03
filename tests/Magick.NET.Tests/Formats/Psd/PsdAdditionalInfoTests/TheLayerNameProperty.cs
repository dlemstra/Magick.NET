// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class PsdAdditionalInfoTests
    {
        public class TheLayerNameProperty
        {
            [Fact]
            public void ShouldReturnNullWhenImageHasNoPsdAdditionalInfo()
            {
                using (var images = new MagickImageCollection(Files.Coders.WizardPSD))
                {
                    var info = PsdAdditionalInfo.FromImage(images[1]);

                    Assert.NotNull(info);
                    Assert.Equal("Волшебник-2", info.LayerName);

                    info = PsdAdditionalInfo.FromImage(images[2]);

                    Assert.NotNull(info);
                    Assert.Equal("Wizard-1", info.LayerName);
                }
            }
        }
    }
}
