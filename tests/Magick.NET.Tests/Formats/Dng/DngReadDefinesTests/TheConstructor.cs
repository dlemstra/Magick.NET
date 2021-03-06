// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class DngReadDefinesTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldNotSetAnyDefines()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new DngReadDefines());

                    Assert.Null(image.Settings.GetDefine(MagickFormat.Dng, "use-camera-wb"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Dng, "use-auto-wb"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Dng, "no-auto-bright"));
                    Assert.Null(image.Settings.GetDefine(MagickFormat.Dng, "output-color"));
                }
            }
        }
    }
}
