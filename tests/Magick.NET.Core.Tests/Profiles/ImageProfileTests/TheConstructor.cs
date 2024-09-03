// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class ImageProfileTests
{
    public class TheConstructor
    {
        [Fact]
        public void ShouldThrowExceptionWhenNameIsNull()
        {
            Assert.Throws<ArgumentNullException>("name", () => new ImageProfile(null!, "test"));
        }

        [Fact]
        public void ShouldThrowExceptionWhenNameIsEmpty()
        {
            Assert.Throws<ArgumentException>("name", () => new ImageProfile(string.Empty, "test"));
        }

        [Fact]
        public void ShouldThrowExceptionWhenByteArrayIsNull()
        {
            Assert.Throws<ArgumentNullException>("data", () => new ImageProfile("name", (byte[]?)null!));
        }

        [Fact]
        public void ShouldNotThrowExceptionWhenByteArrayIsEmpty()
        {
            var profile = new ImageProfile("name", Array.Empty<byte>());
        }

        [Fact]
        public void ShouldThrowExceptionWhenStreamIsNull()
        {
            Assert.Throws<ArgumentNullException>("stream", () => new ImageProfile("name", (Stream?)null!));
        }

        [Fact]
        public void ShouldThrowExceptionWhenFileNameIsNull()
        {
            Assert.Throws<ArgumentNullException>("fileName", () => new ImageProfile("name", (string?)null!));
        }

        [Fact]
        public void ShouldThrowExceptionWhenFileNameIsEmpty()
        {
            Assert.Throws<ArgumentException>("fileName", () => new ImageProfile("name", string.Empty));
        }
    }
}
