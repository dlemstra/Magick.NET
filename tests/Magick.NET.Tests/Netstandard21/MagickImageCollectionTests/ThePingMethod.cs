// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCORE

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public partial class ThePingMethod
        {
            public class WithReadonlySpan
            {
                [Fact]
                public void ShouldThrowExceptionWhenSpanIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("data", () => images.Ping(Span<byte>.Empty));
                    }
                }

                [Fact]
                public void ShouldResetTheFormatAfterReading()
                {
                    var readSettings = new MagickReadSettings
                    {
                        Format = MagickFormat.Png,
                    };

                    var bytes = File.ReadAllBytes(Files.CirclePNG);

                    using (var images = new MagickImageCollection())
                    {
                        images.Ping(new Span<byte>(bytes), readSettings);

                        Assert.Equal(MagickFormat.Unknown, images[0].Settings.Format);
                    }
                }
            }

            public class WitSpanAndMagickReadSettings
            {
                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var bytes = File.ReadAllBytes(Files.SnakewarePNG);

                    using (var images = new MagickImageCollection())
                    {
                        images.Ping(new Span<byte>(bytes), null);

                        Assert.Single(images);
                    }
                }
            }
        }
    }
}

#endif

