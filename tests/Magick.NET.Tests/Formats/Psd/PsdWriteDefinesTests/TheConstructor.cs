// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class PsdWriteDefinesTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldSetAdditionalInfoToNullWhenNotSpecified()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new PsdWriteDefines());

                    Assert.Equal("None", image.Settings.GetDefine(MagickFormat.Psd, "additional-info"));
                }
            }
        }
    }
}
