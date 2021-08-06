// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCORE

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public partial class TheReadPixelsMethod
        {
            public class WithReadOnlySpan
            {
                [Fact]
                public void ShouldThrowExceptionWhenDataIsEmpty()
                {
                    var settings = new PixelReadSettings();

                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("data", () =>
                        {
                            image.ReadPixels(Span<byte>.Empty, settings);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenSettingsIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("settings", () =>
                        {
                            image.ReadPixels(new Span<byte>(new byte[] { 215 }), null);
                        });
                    }
                }

                [Fact]
                public void ShouldReadSpan()
                {
                    var data = new byte[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0,
                        0, 0, 0, 0, 0, 0, 0, 0,
                        0, 0, 0, 0, 0, 0, 0, 0,
                        0, 0, 0, 0, 0, 0, 0xf0, 0x3f,
                        0, 0, 0, 0, 0, 0, 0, 0,
                        0, 0, 0, 0, 0, 0, 0xf0, 0x3f,
                        0, 0, 0, 0, 0, 0, 0, 0,
                        0, 0, 0, 0, 0, 0, 0, 0,
                    };

                    var settings = new PixelReadSettings(2, 1, StorageType.Double, PixelMapping.RGBA);

                    using (var image = new MagickImage())
                    {
                        image.ReadPixels(new Span<byte>(data), settings);

                        Assert.Equal(2, image.Width);
                        Assert.Equal(1, image.Height);

                        using (var pixels = image.GetPixels())
                        {
                            var pixel = pixels.GetPixel(0, 0);
                            Assert.Equal(4, pixel.Channels);
                            Assert.Equal(0, pixel.GetChannel(0));
                            Assert.Equal(0, pixel.GetChannel(1));
                            Assert.Equal(0, pixel.GetChannel(2));
                            Assert.Equal(Quantum.Max, pixel.GetChannel(3));

                            pixel = pixels.GetPixel(1, 0);
                            Assert.Equal(4, pixel.Channels);
                            Assert.Equal(0, pixel.GetChannel(0));
                            Assert.Equal(Quantum.Max, pixel.GetChannel(1));
                            Assert.Equal(0, pixel.GetChannel(2));
                            Assert.Equal(0, pixel.GetChannel(3));
                        }
                    }
                }
            }
        }
    }
}

#endif
