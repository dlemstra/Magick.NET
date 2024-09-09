// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheSetArtifactMethod
    {
        public class WithBoolean
        {
            [Fact]
            public void ShouldThrowExceptionWhenNameIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("name", () => image.SetArtifact(null!, false));
            }

            [Fact]
            public void ShouldThrowExceptionWhenNameIsEmpty()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("name", () => image.SetArtifact(string.Empty, true));
            }

            [Fact]
            public void ShouldSetValue()
            {
                using var image = new MagickImage();
                image.SetArtifact("test", true);

                Assert.Equal("true", image.GetArtifact("test"));
            }
        }

        public class WithString
        {
            [Fact]
            public void ShouldThrowExceptionWhenNameIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("name", () => image.SetArtifact(null!, "foo"));
            }

            [Fact]
            public void ShouldThrowExceptionWhenNameIsEmpty()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("name", () => image.SetArtifact(string.Empty, "foo"));
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("value", () => image.SetArtifact("foo", null!));
            }

            [Fact]
            public void ShouldSetEmptyValue()
            {
                using var image = new MagickImage();
                image.SetArtifact("test", string.Empty);

                var value = image.GetArtifact("test");

                Assert.NotNull(value);
                Assert.Empty(value);
            }

            [Fact]
            public void ShouldSetValue()
            {
                using var image = new MagickImage();
                image.SetArtifact("test", "123");

                Assert.Equal("123", image.GetArtifact("test"));
            }
        }
    }
}
