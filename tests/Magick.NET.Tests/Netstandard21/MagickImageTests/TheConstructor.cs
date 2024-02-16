// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCOREAPP

using System;
using System.Buffers;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public partial class TheConstructor
    {
        public class WithReadOnlySequence
        {
            [Fact]
            public void ShouldThrowExceptionWhenSequenceIsEmpty()
            {
                Assert.Throws<ArgumentException>("data", () => new MagickImage(ReadOnlySequence<byte>.Empty));
            }
        }

        public class WithReadOnlySequenceAndMagickFormat
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                Assert.Throws<ArgumentException>("data", () => new MagickImage(ReadOnlySequence<byte>.Empty, MagickFormat.Png));
            }
        }

        public class WithReadOnlySequenceAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenSequenceIsEmpty()
            {
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentException>("data", () => new MagickImage(ReadOnlySequence<byte>.Empty, settings));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var bytes = File.ReadAllBytes(Files.CirclePNG);
                using var image = new MagickImage(new ReadOnlySequence<byte>(bytes), (MagickReadSettings)null);
            }
        }

        public class WithReadOnlySpan
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                Assert.Throws<ArgumentException>("data", () => new MagickImage(Span<byte>.Empty));
            }
        }

        public class WithReadOnlySpanAndMagickFormat
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                Assert.Throws<ArgumentException>("data", () => new MagickImage(Span<byte>.Empty, MagickFormat.Png));
            }
        }

        public class WithReadOnlySpanAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentException>("data", () => new MagickImage(Span<byte>.Empty, settings));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var bytes = File.ReadAllBytes(Files.CirclePNG);
                using var image = new MagickImage(new Span<byte>(bytes), (MagickReadSettings)null);
            }
        }

        public class WithReadOnlySpanAndPixelReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                var settings = new PixelReadSettings();

                Assert.Throws<ArgumentException>("data", () => new MagickImage(Span<byte>.Empty, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenSettingsIsNull()
            {
                var bytes = new byte[] { 215 };

                Assert.Throws<ArgumentNullException>("settings", () => new MagickImage(new Span<byte>(bytes), (PixelReadSettings)null));
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
                using var image = new MagickImage(new Span<byte>(data), settings);

                Assert.Equal(2, image.Width);
                Assert.Equal(1, image.Height);

                using var pixels = image.GetPixels();
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

#endif
