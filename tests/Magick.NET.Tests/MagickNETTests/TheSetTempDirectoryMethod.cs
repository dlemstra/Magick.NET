// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickNETTests
{
    public class TheSetTempDirectoryMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenPathIsNull()
        {
            Assert.Throws<ArgumentNullException>("path", () =>
            {
                MagickNET.SetTempDirectory(null!);
            });
        }

        [Fact]
        public void ShouldThrowExceptionWhenPathIsInvalid()
        {
            Assert.Throws<ArgumentException>("path", () =>
            {
                MagickNET.SetTempDirectory("Invalid");
            });
        }

        [Fact]
        public void ShouldNotThrowExceptionWhenPathIsCorrect()
        {
            MagickNET.SetTempDirectory(Path.GetTempPath());
        }
    }
}
