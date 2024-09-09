// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ExifProfileTests
{
    public class TheConstructor
    {
        [Fact]
        public void ShouldThrowExceptionWhenStreamNull()
        {
            Assert.Throws<ArgumentNullException>("stream", () => new ExifProfile((Stream)null!));
        }

        [Fact]
        public void ShouldThrowExceptionWhenFileNameNull()
        {
            Assert.Throws<ArgumentNullException>("fileName", () => new ExifProfile((string)null!));
        }

        [Fact]
        public void ShouldThrowExceptionWhenDataNull()
        {
            Assert.Throws<ArgumentNullException>("data", () => new ExifProfile((byte[])null!));
        }

        [Fact]
        public void ShouldAllowEmptyStream()
        {
            using var image = new MagickImage();
            using var memStream = new MemoryStream();
            var profile = new ExifProfile(memStream);
            image.SetProfile(profile);
        }

        [Fact]
        public void ShouldAllowEmptyData()
        {
            using var image = new MagickImage();
            var data = Array.Empty<byte>();
            var profile = new ExifProfile(data);
            image.SetProfile(profile);
        }
    }
}
