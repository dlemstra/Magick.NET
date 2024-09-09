// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheSetProfileMethod
    {
        public class WithColorProfile
        {
            [Fact]
            public void ShouldThrowExceptionWhenProfileIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("profile", () => image.SetProfile(null!));
            }

            [Fact]
            public void ShouldSetTheIccProperties()
            {
                using var image = new MagickImage(Files.MagickNETIconPNG);
                image.SetProfile(ColorProfile.SRGB);

                Assert.Equal("sRGB IEC61966-2.1", image.GetAttribute("icc:description"));
                Assert.Equal("IEC http://www.iec.ch", image.GetAttribute("icc:manufacturer"));
                Assert.Equal("IEC 61966-2.1 Default RGB colour space - sRGB", image.GetAttribute("icc:model"));
                Assert.Equal("Copyright (c) 1998 Hewlett-Packard Company", image.GetAttribute("icc:copyright"));
            }

            [Fact]
            public void ShouldUseIccAsTheDefaultColorProfileName()
            {
                using var image = new MagickImage(Files.SnakewarePNG);
                var profile = image.GetColorProfile();

                Assert.Null(profile);

                image.SetProfile(ColorProfile.SRGB);

                Assert.True(image.HasProfile("icc"));
                Assert.False(image.HasProfile("icm"));
            }

            [Fact]
            public void ShouldUseTheCorrectProfileName()
            {
                using var image = new MagickImage(Files.SnakewarePNG);
                var profile = image.GetColorProfile();

                Assert.Null(profile);

                image.SetProfile(new ImageProfile("icm", ColorProfile.SRGB.ToByteArray()));

                profile = image.GetColorProfile();

                Assert.NotNull(profile);
                Assert.Equal("icm", profile.Name);
                Assert.Equal(3144, profile.ToByteArray().Length);
            }

            [Fact]
            public void ShouldOverwriteExistingProfile()
            {
                using var image = new MagickImage(Files.SnakewarePNG);
                image.SetProfile(ColorProfile.SRGB);

                image.SetProfile(ColorProfile.AppleRGB);

                var profile = image.GetColorProfile();

                Assert.NotNull(profile);
                Assert.Equal(ColorProfile.AppleRGB.ToByteArray().Length, profile.ToByteArray().Length);
            }

            [Fact]
            public void ShouldUseTheSpecifiedMode()
            {
                using var quantumImage = new MagickImage(Files.PictureJPG);
                quantumImage.SetProfile(ColorProfile.USWebCoatedSWOP);

                using var highResImage = new MagickImage(Files.PictureJPG);
                highResImage.SetProfile(ColorProfile.USWebCoatedSWOP, ColorTransformMode.HighRes);

                var difference = quantumImage.Compare(highResImage, ErrorMetric.RootMeanSquared);

#if Q16HDRI
                Assert.Equal(0.0, difference);
#else
                Assert.NotEqual(0.0, difference);
#endif
            }
        }

        public class WithInterface
        {
            [Fact]
            public void ShouldThrowExceptionWhenProfileIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("profile", () => image.SetProfile((IImageProfile)null!));
            }

            [Fact]
            public void ShouldNotSetTheProfileWhenArrayIsNull()
            {
                using var image = new MagickImage(Files.SnakewarePNG);
                image.SetProfile(new TestImageProfile("foo", null!));

                Assert.False(image.HasProfile("foo"));
            }

            [Fact]
            public void ShouldNotSetTheProfileWhenArrayIsEmpty()
            {
                using var image = new MagickImage(Files.SnakewarePNG);
                image.SetProfile(new TestImageProfile("foo", Array.Empty<byte>()));

                Assert.False(image.HasProfile("foo"));
            }

            [Fact]
            public void ShouldOverwriteExistingProfile()
            {
                var profileA = new TestImageProfile("foo", new byte[1]);
                var profileB = new TestImageProfile("foo", new byte[2]);

                using var image = new MagickImage(Files.SnakewarePNG);
                image.SetProfile(profileA);
                image.SetProfile(profileB);

                var profile = image.GetProfile("foo");

                Assert.NotNull(profile);
                Assert.Equal(2, profile.ToByteArray().Length);
            }

            [Fact]
            public void ShouldSetTheIptcProfile()
            {
                using var input = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
                var profile = input.GetIptcProfile();
                Assert.NotNull(profile);

                profile.SetValue(IptcTag.ReferenceDate, new DateTimeOffset(2020, 1, 2, 3, 4, 5, TimeSpan.Zero));

                // Remove the 8bim profile so we can overwrite the iptc profile.
                input.RemoveProfile("8bim");
                input.SetProfile(profile);

                using var memStream = new MemoryStream();
                input.Write(memStream);
                memStream.Position = 0;

                using var output = new MagickImage(memStream);
                profile = input.GetIptcProfile();

                Assert.NotNull(profile);
                Assert.Equal("20200102", profile.GetValue(IptcTag.ReferenceDate)?.Value);
            }
        }
    }
}
