// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheRemoveReadMaskMethod
    {
        [Fact]
        public void ShouldRemoveReadMask()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            using var readMask = new MagickImage(MagickColors.Black, image.Width, image.Height);
            image.SetReadMask(readMask);

            using var readMaskBeforeRemoval = image.GetReadMask();

            Assert.NotNull(readMaskBeforeRemoval);

            image.RemoveReadMask();

            using var readMaskAfterRemoval = image.GetReadMask();

            Assert.Null(readMaskAfterRemoval);
        }
    }
}
