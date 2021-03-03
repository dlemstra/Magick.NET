// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if WINDOWS_BUILD

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
                using (NonSeekableStream stream = new NonSeekableStream(Files.Coders.CartoonNetworkStudiosLogoAI))
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

#endif