// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickNETTests
{
    public class TheSetFontConfigDirectoryMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenPathIsNull()
        {
            Assert.Throws<ArgumentNullException>("path", () =>
            {
                MagickNET.SetFontConfigDirectory(null!);
            });
        }

        [Fact]
        public void ShouldThrowExceptionWhenPathIsInvalid()
        {
            Assert.Throws<ArgumentException>("path", () =>
            {
                MagickNET.SetFontConfigDirectory("Invalid");
            });
        }
    }
}
