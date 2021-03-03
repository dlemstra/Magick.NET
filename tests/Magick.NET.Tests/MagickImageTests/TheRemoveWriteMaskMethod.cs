// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheRemoveWriteMaskMethod
        {
            [Fact]
            public void ShouldRemoveWriteMask()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    using (var writeMask = new MagickImage(MagickColors.Black, image.Width, image.Height))
                    {
                        image.SetWriteMask(writeMask);
                    }

                    using (var writeMask = image.GetWriteMask())
                    {
                        Assert.NotNull(writeMask);
                    }

                    image.RemoveWriteMask();

                    using (var writeMask = image.GetWriteMask())
                    {
                        Assert.Null(writeMask);
                    }
                }
            }
        }
    }
}
