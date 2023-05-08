// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public partial class TheReadMethod
        {
            [Fact]
            public void ShouldReadAIFromNonSeekableStream()
            {
                if (!Ghostscript.IsAvailable)
                    return;

                using (var stream = new NonSeekableStream(Files.Coders.CartoonNetworkStudiosLogoAI))
                {
                    using (var image = new MagickImage())
                    {
                        image.Read(stream);
                    }
                }
            }
        }
    }
}
