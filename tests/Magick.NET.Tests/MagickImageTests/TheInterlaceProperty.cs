﻿// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheInterlaceProperty
    {
        [Fact]
        public void ShouldUseNoInterlaceAsTheDefault()
        {
            using var image = new MagickImage(MagickColors.Fuchsia, 100, 60);
            using var memStream = new MemoryStream();
            image.Write(memStream, MagickFormat.Jpeg);

            memStream.Position = 0;
            image.Read(memStream);

            Assert.Equal(Interlace.NoInterlace, image.Interlace);
        }
    }
}
