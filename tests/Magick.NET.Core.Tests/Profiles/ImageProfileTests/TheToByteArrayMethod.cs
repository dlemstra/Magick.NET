// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class ImageProfileTests
{
    public class TheToByteArrayMethod
    {
        [Fact]
        public void ShouldReturnEmptyArrayWhenDataIsNull()
        {
            var profile = new TestProfile();
            var bytes = profile.ToByteArray();

            Assert.Empty(bytes);
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
