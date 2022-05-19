// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public class TheMontageMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenCollectionIsEmpty()
            {
                using (var images = new MagickImageCollection())
                {
                    Assert.Throws<InvalidOperationException>(() => images.Montage(null));
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenSettingsIsNull()
            {
                using (var images = new MagickImageCollection())
                {
                    images.Add(new MagickImage(MagickColors.Magenta, 1, 1));

                    Assert.Throws<ArgumentNullException>("settings", () =>
                    {
                        images.Montage(null);
                    });
                }
            }

            [Fact]
            public void ShouldMontageTheImages()
            {
                using (var collection = new MagickImageCollection())
                {
                    for (var i = 0; i < 9; i++)
                        collection.Add(Files.Builtin.Logo);

                    var settings = new MontageSettings();
                    settings.Geometry = new MagickGeometry(string.Format("{0}x{1}", 200, 200));
                    settings.TileGeometry = new MagickGeometry(string.Format("{0}x", 2));

                    using (var montageResult = collection.Montage(settings))
                    {
                        Assert.NotNull(montageResult);
                        Assert.Equal(400, montageResult.Width);
                        Assert.Equal(1000, montageResult.Height);
                    }
                }
            }
        }
    }
}
