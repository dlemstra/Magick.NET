// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class ImageProfileTests
    {
        [Fact]
        public void Test_Constructor()
        {
            Assert.Throws<ArgumentNullException>("name", () =>
            {
                new ImageProfile(null, Files.SnakewarePNG);
            });

            Assert.Throws<ArgumentException>("name", () =>
            {
                new ImageProfile(string.Empty, Files.SnakewarePNG);
            });

            Assert.Throws<ArgumentNullException>("data", () =>
            {
                new ImageProfile("name", (byte[])null);
            });

            Assert.Throws<ArgumentNullException>("stream", () =>
            {
                new ImageProfile("name", (Stream)null);
            });

            Assert.Throws<ArgumentNullException>("fileName", () =>
            {
                new ImageProfile("name", (string)null);
            });

            Assert.Throws<ArgumentException>("fileName", () =>
            {
                new ImageProfile("name", string.Empty);
            });
        }

        [Fact]
        public void Test_IEquatable()
        {
            using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
            {
                var first = image.GetIptcProfile();

                Assert.False(first == null);
                Assert.False(first.Equals(null));
                Assert.True(first.Equals(first));
                Assert.True(first.Equals((object)first));

                var second = image.GetIptcProfile();
                Assert.NotNull(second);

                Assert.True(first.Equals(second));
                Assert.True(first.Equals((object)second));

                second = new IptcProfile(new byte[] { 0 });

                Assert.True(first != second);
                Assert.False(first.Equals(second));
            }
        }

        [Fact]
        public void Test_ToByteArray()
        {
            using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
            {
                var profile = image.GetIptcProfile();
                Assert.NotNull(profile);

                Assert.Equal(281, profile.ToByteArray().Length);
            }
        }
    }
}
