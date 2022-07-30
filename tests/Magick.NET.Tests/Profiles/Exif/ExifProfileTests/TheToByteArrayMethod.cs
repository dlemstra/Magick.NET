// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class ExifProfileTests
    {
        public class TheToByteArrayMethod
        {
            [Fact]
            public void ShouldReturnOriginalDataWhenNotParsed()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    var profile = image.GetExifProfile();

                    var bytes = profile.ToByteArray();
                    Assert.Equal(4706, bytes.Length);
                }
            }

            [Fact]
            public void ShouldPreserveTheThumbnail()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    var profile = image.GetExifProfile();
                    Assert.NotNull(profile);

                    var bytes = profile.ToByteArray();

                    profile = new ExifProfile(bytes);

                    using (var thumbnail = profile.CreateThumbnail())
                    {
                        Assert.NotNull(thumbnail);
                        Assert.Equal(128, thumbnail.Width);
                        Assert.Equal(85, thumbnail.Height);
                        Assert.Equal(MagickFormat.Jpeg, thumbnail.Format);
                    }
                }
            }
        }
    }
}
