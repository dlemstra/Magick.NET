// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class Jp2WriteDefinesTests
    {
        public class TheRateProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new Jp2WriteDefines
                    {
                        Rate = new float[] { 4, 2 },
                    });

                    Assert.Equal("4,2", image.Settings.GetDefine(MagickFormat.Jp2, "rate"));
                }
            }

            [Fact]
            public void ShouldNotSetTheDefineWhenTheCollectionIsEmpty()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new Jp2WriteDefines
                    {
                        Rate = new float[] { },
                    });

                    Assert.Null(image.Settings.GetDefine(MagickFormat.Jp2, "rate"));
                }
            }
        }
    }
}
