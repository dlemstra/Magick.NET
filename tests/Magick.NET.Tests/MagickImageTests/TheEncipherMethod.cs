// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheEncipherMethod
        {
            [Fact]
            public void ShouldChangeThePixels()
            {
                using (var original = new MagickImage(Files.SnakewarePNG))
                {
                    using (var enciphered = original.Clone())
                    {
                        enciphered.Encipher("All your base are belong to us");
                        Assert.NotEqual(original, enciphered);
                    }
                }
            }
        }
    }
}
