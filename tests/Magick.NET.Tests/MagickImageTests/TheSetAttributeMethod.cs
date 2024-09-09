// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheSetAttributeMethod
    {
        public class WithBoolean
        {
            [Fact]
            public void ShouldThrowExceptionWhenNameIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("name", () => image.SetAttribute(null!, true));
            }

            [Fact]
            public void ShouldThrowExceptionWhenNameIsEmpty()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("name", () => image.SetAttribute(string.Empty, true));
            }

            [Fact]
            public void ShouldSetValue()
            {
                using var image = new MagickImage();
                image.SetAttribute("test", true);

                Assert.Equal("true", image.GetAttribute("test"));
            }
        }

        public class WithString
        {
            [Fact]
            public void ShouldThrowExceptionWhenNameIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("name", () => image.SetAttribute(null!, "foo"));
            }

            [Fact]
            public void ShouldThrowExceptionWhenNameIsEmpty()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("name", () => image.SetAttribute(string.Empty, "foo"));
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("value", () => image.SetAttribute("foo", null!));
            }

            [Fact]
            public void ShouldSetEmptyValue()
            {
                using var image = new MagickImage();
                image.SetAttribute("test", string.Empty);

                var value = image.GetAttribute("test");

                Assert.NotNull(value);
                Assert.Empty(value);
            }

            [Fact]
            public void ShouldSetValue()
            {
                using var image = new MagickImage();
                image.SetAttribute("test", "123");

                Assert.Equal("123", image.GetAttribute("test"));
            }
        }
    }
}
