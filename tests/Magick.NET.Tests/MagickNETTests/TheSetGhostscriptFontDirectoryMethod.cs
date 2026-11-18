// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using ImageMagick.Factories;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickNETTests
{
    public class TheSetGhostscriptFontDirectoryMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenPathIsNull()
        {
            Assert.Throws<ArgumentNullException>("path", () =>
            {
                MagickNET.SetGhostscriptFontDirectory(null!);
            });
        }

        [Fact]
        public void ShouldThrowExceptionWhenPathIsInvalid()
        {
            Assert.Throws<ArgumentException>("path", () =>
            {
                MagickNET.SetGhostscriptFontDirectory("Invalid");
            });
        }

        [Fact]
        public void ShouldSetFontPathWhenCalledThroughInterface()
        {
            var directory = Path.GetTempPath();
            var magickNET = new MagickFactory().MagickNET;

            magickNET.SetGhostscriptFontDirectory(directory);

            Assert.Equal(Path.GetFullPath(directory), MagickNET.GetEnvironmentVariable("MAGICK_GHOSTSCRIPT_FONT_PATH"));
        }
    }
}
