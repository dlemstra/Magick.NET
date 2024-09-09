// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickNETTests
{
    public class TheSetDefaultFontFileMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenFileIsNull()
        {
            Assert.Throws<ArgumentNullException>("file", () =>
            {
                MagickNET.SetDefaultFontFile((FileInfo)null!);
            });
        }

        [Fact]
        public void ShouldThrowExceptionWhenFileNameIsNull()
        {
            Assert.Throws<ArgumentNullException>("fileName", () =>
            {
                MagickNET.SetDefaultFontFile((string)null!);
            });
        }

        [Fact]
        public void ShouldThrowExceptionWhenFileNameIsEmpty()
        {
            Assert.Throws<ArgumentException>("fileName", () =>
            {
                MagickNET.SetDefaultFontFile(string.Empty);
            });
        }
    }
}
