// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class IptcValueTests
    {
        [Fact]
        public void Test_IEquatable()
        {
            var first = GetIptcValue();
            var second = GetIptcValue();

            Assert.True(first.Equals(second));
            Assert.True(first.Equals((object)second));
        }

        [Fact]
        public void Test_Properties()
        {
            var value = GetIptcValue();

            Assert.Equal(IptcTag.Caption, value.Tag);
            Assert.Equal("Communications", value.ToString());
            Assert.Equal("Communications", value.Value);
            Assert.Equal(14, value.ToByteArray().Length);
        }

        [Fact]
        public void Test_ToString()
        {
            var value = GetIptcValue();

            Assert.Equal("Communications", value.ToString());

            value.Value = "Test";
            Assert.Equal("Test", value.ToString());

            value.Value = string.Empty;
            Assert.Equal(string.Empty, value.ToString());
            value.Value = "Test";
            Assert.Equal("Test", value.ToString());
            value.Value = null;
            Assert.Equal(string.Empty, value.ToString());
        }

        private static IIptcValue GetIptcValue()
        {
            using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
            {
                var profile = image.GetIptcProfile();
                return profile.Values.ElementAt(1);
            }
        }
    }
}
