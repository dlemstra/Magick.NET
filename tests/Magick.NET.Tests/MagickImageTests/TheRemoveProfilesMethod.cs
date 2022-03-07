// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheRemoveProfilesMethod
        {
            public class WithImageProfiles
            {
                [Fact]
                public void ShouldThrowExceptionWhenProfilesIsNull()
                {
                    using (var image = new MagickImage(MagickColors.Red, 1, 1))
                    {
                        Assert.Throws<ArgumentNullException>("profile", () => image.RemoveProfiles((IImageProfile)null));
                    }
                }

                [Fact]
                public void ShouldRemoveTheProfiles()
                {
                    using (var image = new MagickImage(Files.FujiFilmFinePixS1ProPNG))
                    {
                        var colorProfile = image.GetColorProfile();
                        var exifProfile = image.GetExifProfile();
                        var xmpProfile = image.GetXmpProfile();

                        Assert.NotNull(colorProfile);
                        Assert.NotNull(exifProfile);
                        Assert.NotNull(xmpProfile);

                        image.RemoveProfiles(colorProfile, exifProfile, xmpProfile);

                        colorProfile = image.GetColorProfile();
                        exifProfile = image.GetExifProfile();
                        xmpProfile = image.GetXmpProfile();
                        Assert.Null(colorProfile);
                        Assert.Null(exifProfile);
                        Assert.Null(xmpProfile);
                    }
                }
            }

            public class WithParamsString
            {
                [Fact]
                public void ShouldThrowExceptionWhenProfileIsNull()
                {
                    using (var image = new MagickImage(MagickColors.Red, 1, 1))
                    {
                        Assert.Throws<ArgumentNullException>("name", () => image.RemoveProfiles((string)null));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenProfileIsEmpty()
                {
                    using (var image = new MagickImage(MagickColors.Red, 1, 1))
                    {
                        Assert.Throws<ArgumentException>("name", () => image.RemoveProfiles(string.Empty));
                    }
                }

                [Fact]
                public void ShouldRemoveTheProfiles()
                {
                    using (var image = new MagickImage(Files.FujiFilmFinePixS1ProPNG))
                    {
                        var colorProfile = image.GetColorProfile();
                        var exifProfile = image.GetExifProfile();
                        var xmpProfile = image.GetXmpProfile();

                        Assert.NotNull(colorProfile);
                        Assert.NotNull(exifProfile);
                        Assert.NotNull(xmpProfile);

                        Assert.Equal("icc", colorProfile.Name);
                        Assert.Equal("exif", exifProfile.Name);
                        Assert.Equal("xmp", xmpProfile.Name);

                        image.RemoveProfiles(colorProfile.Name, exifProfile.Name, xmpProfile.Name);

                        colorProfile = image.GetColorProfile();
                        exifProfile = image.GetExifProfile();
                        xmpProfile = image.GetXmpProfile();
                        Assert.Null(colorProfile);
                        Assert.Null(exifProfile);
                        Assert.Null(xmpProfile);
                    }
                }
            }
        }
    }
}
