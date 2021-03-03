// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Threading.Tasks;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class ThePdfCoder
    {
        [Fact]
        public void ShouldReadFileMultithreadedCorrectly()
        {
            if (!Ghostscript.IsAvailable)
                return;

            var results = new Task[3];

            for (int i = 0; i < results.Length; ++i)
            {
                results[i] = Task.Run(() =>
                {
                    using (var image = new MagickImage())
                    {
                        image.Read(Files.Coders.CartoonNetworkStudiosLogoAI);

                        Assert.Equal(765, image.Width);
                        Assert.Equal(361, image.Height);
                        Assert.Equal(MagickFormat.Ai, image.Format);
                    }
                });
            }

            for (int i = 0; i < results.Length; ++i)
            {
                results[i].Wait();
            }
        }
    }
}
