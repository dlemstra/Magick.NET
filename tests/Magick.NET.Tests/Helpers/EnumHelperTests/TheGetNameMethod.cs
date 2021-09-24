// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class EnumHelperTests
    {
        public class TheGetNameMethod
        {
            [Fact]
            public void ShouldReturnTheNameOfTheValue()
            {
                var blue = PixelChannel.Blue;

                var result = EnumHelper.GetName(blue);

                Assert.Equal("Blue", result);
            }

            [Fact]
            public void ShouldReturnNullWhenValueIsNotInEnum()
            {
                var invalid = (PixelChannel)42;

                var result = EnumHelper.GetName(invalid);

                Assert.Null(result);
            }
        }
    }
}
