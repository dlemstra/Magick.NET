// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCOREAPP

using System;
using System.Buffers;
using System.IO;
using ImageMagick;
using ImageMagick.Factories;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageFactoryTests
{
    public partial class TheCreateMethod
    {
        public class WithReadOnlySequence
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                var factory = new MagickImageFactory();

                Assert.Throws<ArgumentException>("data", () => factory.Create(ReadOnlySequence<byte>.Empty));
            }

            [Fact]
            public void ShouldCreateMagickImage()
            {
                var data = File.ReadAllBytes(Files.ImageMagickJPG);
                var factory = new MagickImageFactory();
                using var image = factory.Create(new ReadOnlySequence<byte>(data));

                Assert.IsType<MagickImage>(image);
                Assert.Equal(123, image.Width);
            }
        }

        public class WithReadOnlySequenceAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                var factory = new MagickImageFactory();
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentException>("data", () => factory.Create(ReadOnlySequence<byte>.Empty, settings));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var bytes = File.ReadAllBytes(Files.CirclePNG);
                var factory = new MagickImageFactory();

                using var image = factory.Create(new ReadOnlySequence<byte>(bytes), null);
            }

            [Fact]
            public void ShouldCreateMagickImage()
            {
                var data = File.ReadAllBytes(Files.ImageMagickJPG);
                var settings = new MagickReadSettings
                {
                    BackgroundColor = MagickColors.Goldenrod,
                };
                var factory = new MagickImageFactory();
                using var image = factory.Create(new ReadOnlySequence<byte>(data), settings);

                Assert.IsType<MagickImage>(image);
                Assert.Equal(123, image.Width);
                Assert.Equal(MagickColors.Goldenrod, image.Settings.BackgroundColor);
            }
        }

        public class WithReadOnlySpan
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                var factory = new MagickImageFactory();

                Assert.Throws<ArgumentException>("data", () => factory.Create(Span<byte>.Empty));
            }

            [Fact]
            public void ShouldCreateMagickImage()
            {
                var data = File.ReadAllBytes(Files.ImageMagickJPG);
                var factory = new MagickImageFactory();
                using var image = factory.Create(new Span<byte>(data));

                Assert.IsType<MagickImage>(image);
                Assert.Equal(123, image.Width);
            }
        }

        public class WithReadOnlySpanAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                var settings = new MagickReadSettings();
                var factory = new MagickImageFactory();

                Assert.Throws<ArgumentException>("data", () => factory.Create(Span<byte>.Empty, settings));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var bytes = File.ReadAllBytes(Files.CirclePNG);
                var factory = new MagickImageFactory();
                using var image = factory.Create(new Span<byte>(bytes), (MagickReadSettings)null);
            }

            [Fact]
            public void ShouldCreateMagickImage()
            {
                var data = File.ReadAllBytes(Files.ImageMagickJPG);
                var settings = new MagickReadSettings
                {
                    BackgroundColor = MagickColors.Goldenrod,
                };
                var factory = new MagickImageFactory();

                using var image = factory.Create(new Span<byte>(data), settings);

                Assert.IsType<MagickImage>(image);
                Assert.Equal(123, image.Width);
                Assert.Equal(MagickColors.Goldenrod, image.Settings.BackgroundColor);
            }
        }

        public class WithReadOnlySpanAndPixelReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                var settings = new PixelReadSettings();
                var factory = new MagickImageFactory();

                Assert.Throws<ArgumentException>("data", () => factory.Create(Span<byte>.Empty, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenSettingsIsNull()
            {
                var bytes = File.ReadAllBytes(Files.CirclePNG);
                var factory = new MagickImageFactory();

                Assert.Throws<ArgumentNullException>("settings", () => factory.Create(new Span<byte>(bytes), (PixelReadSettings)null));
            }

            [Fact]
            public void ShouldCreateMagickImage()
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
                var factory = new MagickImageFactory();

                using var image = factory.Create(new Span<byte>(data), settings);

                Assert.IsType<MagickImage>(image);
                Assert.Equal(2, image.Width);
            }
        }
    }
}

#endif
