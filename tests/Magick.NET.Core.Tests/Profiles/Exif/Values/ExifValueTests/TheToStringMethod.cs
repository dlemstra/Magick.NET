// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class ExifValueTests
    {
        public class TheToStringMethod
        {
            [Fact]
            public void ShouldReturnTheValueAsString()
            {
                var value = new ExifShort(ExifTag.GPSDifferential);
                value.Value = 42;

                Assert.Equal("42", value.ToString());
            }
        }
    }
}
