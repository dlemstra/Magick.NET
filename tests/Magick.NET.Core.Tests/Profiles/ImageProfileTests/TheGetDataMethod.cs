// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ImageProfileTests
{
    public class TheGetDataMethod
    {
        [Fact]
        public void ShouldReturnNullWhenDataIsNull()
        {
            var profile = new TestProfile();
            var bytes = profile.GetData();

            Assert.Null(bytes);
        }

        private class TestProfile : ImageProfile
        {
            public TestProfile()
                : base("test")
            {
            }
        }
    }
}
