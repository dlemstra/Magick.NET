// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class ExifProfileTests
    {
        public class TheGetValueMethod
        {
            [Fact]
            public void ShouldReturnStringWhenValueIsString()
            {
                var profile = new ExifProfile();
                profile.SetValue(ExifTag.Software, "Magick.NET");

                var value = profile.GetValue(ExifTag.Software);
                TestValue(value, "Magick.NET");
            }
        }
    }
}
