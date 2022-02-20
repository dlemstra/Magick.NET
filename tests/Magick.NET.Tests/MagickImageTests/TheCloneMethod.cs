// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheCloneMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenNoImageIsRead()
            {
                using (var image = new MagickImage())
                {
                    // Assert.Throws<MagickCorruptImageErrorException>(() => image.Clone());
                }
            }

            [Fact]
            public void ShouldCloneTheImage()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    var clone = image.Clone();

                    Assert.False(ReferenceEquals(image, clone));
                }
            }
        }
    }
}
