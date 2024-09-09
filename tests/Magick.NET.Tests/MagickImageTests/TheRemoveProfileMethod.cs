// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheRemoveProfileMethod
    {
        public class WithImageProfile
        {
            [Fact]
            public void ShouldThrowExceptionWhenProfileIsNull()
            {
                using var image = new MagickImage(MagickColors.Red, 1, 1);

                Assert.Throws<ArgumentNullException>("profile", () => image.RemoveProfile((IImageProfile)null!));
            }

            [Fact]
            public void ShouldRemoveTheSpecifiedColorProfile()
            {
                using var image = new MagickImage(Files.PictureJPG);
                var profile = image.GetColorProfile();

                Assert.NotNull(profile);

                image.RemoveProfile(profile);

                profile = image.GetColorProfile();

                Assert.Null(profile);
            }

            [Fact]
            public void ShouldRemoveTheSpecifiedIptcProfile()
            {
                using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
                var profile = image.GetIptcProfile();

                Assert.NotNull(profile);

                image.RemoveProfile(profile);

                profile = image.GetIptcProfile();

                Assert.Null(profile);
            }
        }

        public class WithString
        {
            [Fact]
            public void ShouldThrowExceptionWhenProfileIsNull()
            {
                using var image = new MagickImage(MagickColors.Red, 1, 1);

                Assert.Throws<ArgumentNullException>("name", () => image.RemoveProfile((string)null!));
            }

            [Fact]
            public void ShouldThrowExceptionWhenProfileIsEmpty()
            {
                using var image = new MagickImage(MagickColors.Red, 1, 1);

                Assert.Throws<ArgumentException>("name", () => image.RemoveProfile(string.Empty));
            }

            [Fact]
            public void ShouldRemoveTheProfile()
            {
                using var image = new MagickImage(Files.PictureJPG);
                var profile = image.GetColorProfile();

                Assert.NotNull(profile);
                Assert.Equal("icc", profile.Name);

                image.RemoveProfile(profile.Name);

                profile = image.GetColorProfile();

                Assert.Null(profile);
            }
        }
    }
}
