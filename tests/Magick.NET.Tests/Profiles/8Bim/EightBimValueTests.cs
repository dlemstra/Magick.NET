// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class EightBimValueTests
    {
        [Fact]
        public void Test_IEquatable()
        {
            var first = Get8BimValue();
            var second = Get8BimValue();

            Assert.True(first.Equals(second));
            Assert.True(first.Equals((object)second));
        }

        [Fact]
        public void Test_ToByteArray()
        {
            var value = Get8BimValue();
            byte[] bytes = value.ToByteArray();
            Assert.Equal(273, bytes.Length);
        }

        private static IEightBimValue Get8BimValue()
        {
            using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
            {
                var profile = image.Get8BimProfile();
                return profile.Values.First();
            }
        }
    }
}
